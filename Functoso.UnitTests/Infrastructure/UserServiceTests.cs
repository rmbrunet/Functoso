using System.Net;
using static Functoso.UnitTests.Common.HttpClientMock;
namespace Functoso.UnitTests.Infrastructure;

public class UserServiceTests
{
    /// <summary>
    /// When the user is not found UserService should return ExpectedError with Code 404.
    /// </summary>
    [Fact]
    public async Task Service_ReturnExpectedError404_WhenUserNotFound()
    {
        // Arrange

        HttpClient client = CreateHttpClient(HttpStatusCode.NotFound);

        IUserService sut = new UserService(client);

        // Act
        Fin<Domain.User> fin = await sut.GetUser(1).Run();

        //Assert
        Assert.True(fin.Match<bool>(
            Succ: u => false,
            Fail: e =>
            {
                return e.Code == 404;
            }));
    }
}
