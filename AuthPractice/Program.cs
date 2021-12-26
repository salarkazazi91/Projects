using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(config =>
{
    config.UseInMemoryDatabase("MemoryDb");
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(config =>
{
    config.Password.RequireDigit = false;
    config.Password.RequiredLength = 1;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireLowercase = false;
    config.Password.RequireUppercase = false;

})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


builder.Services.ConfigureApplicationCookie(configure=>{
    configure.Cookie.Name="SALAR.IDENTITY"    ;
    configure.ExpireTimeSpan=TimeSpan.FromSeconds(30);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
