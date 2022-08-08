using System.Net;
using Functoso.UnitTests.Common;

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
        using var handler = new MockingHandler(() => Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound)));

        HttpClient client = new(handler)
        {
            BaseAddress = new Uri("https://fakeurl")
        };

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
