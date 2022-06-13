using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.Repository;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddControllers().AddOData(options => {
    options.Select().Filter().OrderBy();
    options.AddRouteComponents("odata", BuildModels());
    static IEdmModel BuildModels()
    {
        var builder = new ODataConventionModelBuilder();
        builder.EntitySet<UserModel>("Users");
        return builder.GetEdmModel();
    }
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserDBContext>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<AuthService>(new AuthService(new UserRepository(new UserDBContext()), configuration.GetValue<string>("JWTSecretKey")));

builder.Services.AddCors(options => 
{
    options.AddPolicy("default", builder =>
    {
        builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("default");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

