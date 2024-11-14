using UserIdentifierService.Repositories;
using UserIdentifierService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new ArgumentNullException("Connection string cannot be null");
builder.Services.AddSingleton(new AvatarRepository(connectionString));
builder.Services.AddTransient<AvatarService>();
builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => { policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
});
builder.Services.AddHttpClient<AvatarService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseMiddleware<UserIdentifierService.Middlewares.ExceptionHandlingMiddleware>();

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();