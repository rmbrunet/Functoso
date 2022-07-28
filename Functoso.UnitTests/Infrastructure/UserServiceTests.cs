// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
using System.Net;
using RichardSzalay.MockHttp;

namespace Functoso.UnitTests.Infrastructure;

public class UserServiceTests
{
    [Fact]
    public async Task Service_ReturnErro404_WhenUserNotFound()
    {
        // Arrange
        var handler = new MockHttpMessageHandler();

        handler.Expect("/users/1").Respond(HttpStatusCode.NotFound);
        var client = handler.ToHttpClient();
        client.BaseAddress = new Uri("https://localhost");

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
