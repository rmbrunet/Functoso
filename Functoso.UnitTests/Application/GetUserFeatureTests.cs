using AutoMapper;
using Functoso.Application.Dtos;
using Functoso.Application.Features;
using Microsoft.Extensions.Caching.Memory;

namespace Functoso.UnitTests.Application;

public class GetUserFeatureTests
{
    /// <summary>
    /// When supplied with an invalid User Id Handler should retorn an
    /// expected Error with code 400.
    /// </summary>
    [Fact]
    public async Task Handler_ReturnExpectedError400_ForInvalidUserId()
    {
        // Arrange
        var query = new GetUserFeature.Query(0);
        var queryValidator = new Functoso.Application.Features.GetUserFeature.QueryValidator();
        var userServiceMock = new Mock<IUserService>();

        IMapper mapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<Functoso.Application.Mappers.UserMappingProfile>();
        }).CreateMapper();

        IMemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        var sut = new GetUserFeature.Handler(userServiceMock.Object, mapper, queryValidator, cache);

        // Act
        Either<LanguageExt.Common.Error, UserDto> either = await sut.Handle(query, CancellationToken.None);

        //Assert
        Assert.True(either.Right(r => false).Left(l => l.Code == 400));


    }
}
