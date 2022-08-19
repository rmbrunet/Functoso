using FluentValidation;
using Functoso.Contracts;
using Functoso.Infrastructure.Services;
using MediatR;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddHttpClient<IUserService, UserService>(client =>
    {
        client.BaseAddress = new Uri(configuration.GetValue<string>("UsersServiceUrl"));
    });

builder.Services.AddMemoryCache();
builder.Services.AddMediatR(typeof(Functoso.Application.Features.GetUsersFeature));
builder.Services.AddValidatorsFromAssemblyContaining<Functoso.Application.Features.GetUserFeature.QueryValidator>();
builder.Services.AddAutoMapper(typeof(Functoso.Application.Mappers.UserMappingProfile));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
