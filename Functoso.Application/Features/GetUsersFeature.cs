namespace Functoso.Application.Features;

public class GetUsersFeature
{
    public record Query() : IRequest<Either<Error, IEnumerable<UserDto>>>;

    public class Handler : IRequestHandler<Query, Either<Error, IEnumerable<UserDto>>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public Handler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<Either<Error, IEnumerable<UserDto>>> Handle(Query _, CancellationToken cancellationToken)
        {
            Fin<IEnumerable<UserDto>> t = await (from u in _userService.Users
                                                 from d in MappUsers(u, _mapper)
                                                 select d).Run();

            return t.ToEither();
        }

#pragma warning disable S3242

        private static Eff<IEnumerable<UserDto>> MappUsers(IEnumerable<User> users, IMapper mapper)
                => Eff(() => mapper.Map<IEnumerable<UserDto>>(users));

#pragma warning restore S3242
    }
}
