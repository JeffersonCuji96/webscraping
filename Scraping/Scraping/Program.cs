string policy = "MyPolicy";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null); ;
builder.Services.AddCors(op =>
{
    op.AddPolicy(name: policy, build =>
    {
        build.WithOrigins("https://graficascraping.azurewebsites.net");
    });
});

var app = builder.Build();
app.UseCors(policy);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
