using Functoso.UnitTests.Common;
using System.Net;

namespace Functoso.UnitTests.Infrastructure;

public class UserServiceTests
{
    [Fact]
    public async Task Service_ReturnErro404_WhenUserNotFound()
    {
        // Arrange
        var handler = new MockingHandler(() => Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound)));

        HttpClient client = new(handler)
        {
            BaseAddress = new Uri("https://fakeurl")
        };

        IUserService sut = new UserService(client);

        // Act
        Fin<Domain.User> fin = await sut.GetUser(1).Run();

        //Assert
        Assert.True(fin.Match<bool>(
            u => false,
            e =>
            {
                return e.Code == 404;
            }));
    }
}
