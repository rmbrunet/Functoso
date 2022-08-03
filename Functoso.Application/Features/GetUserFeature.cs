using FluentValidation;
using FluentValidation.Results;
using Functoso.Application.Errors;
using Microsoft.Extensions.Caching.Memory;

namespace Functoso.Application.Features;

public class GetUserFeature
{
    public record Query(int Id) : IRequest<Either<Error, UserDto>>;

    public class QueryValidator : AbstractValidator<Query>
    {
        public QueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }

    public class Handler : IRequestHandler<Query, Either<Error, UserDto>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IValidator<Query> _queryValidator;
        private readonly IMemoryCache _memoryCache;

        public Handler(IUserService userService, IMapper mapper, IValidator<Query> queryValidator, IMemoryCache memoryCache)
        {
            _userService = userService;
            _mapper = mapper;
            _queryValidator = queryValidator;
            _memoryCache = memoryCache;
        }

        public async Task<Either<Error, UserDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            Fin<UserDto> t = await (from _ in ValidateQuery(request, _queryValidator)
                                    from u in (GetUserFromCache(request, _memoryCache) |
                                        (from a in _userService.GetUser(request.Id)
                                         from b in AddUserToCache(request, _memoryCache, a)
                                         select b))
                                    from d in MappUser(u, _mapper)
                                    select d).Run();

            return t.ToEither();
        }

        private static Eff<LanguageExt.Unit> ValidateQuery(Query request, IValidator<Query> validator)
        {
            ValidationResult result = validator.Validate(request);
            return result.IsValid ? unitEff : FailEff<LanguageExt.Unit>(new ValidationError(result));
        }

#pragma warning disable S3242

        private static Eff<UserDto> MappUser(User user, IMapper mapper)
            => Eff(() => mapper.Map<UserDto>(user));

#pragma warning restore S3242

        private static Aff<User> GetUserFromCache(Query request, IMemoryCache cache)
        {
            User u = cache.Get<User>($"users/{request.Id}");
            return Optional(u).ToAff();
        }

        private static Eff<User> AddUserToCache(Query request, IMemoryCache cache, User user)
        {
            cache.Set($"users/{request.Id}", user, TimeSpan.FromSeconds(60));
            return Eff<User>(() => user);
        }
    }
}
