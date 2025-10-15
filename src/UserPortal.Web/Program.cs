using UserPortal.UseCases.Validations.Configurations;
using UserPortal.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Configure FluentValidation
builder.Services.ConfigureFluentValidation()
                .ConfigureAspNetValidation();

// Configure Database and Identity
builder.Services.ConfigureDatabase(builder.Configuration)
                .ConfigureRepositories()
                .ConfigureIdentity();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);  // HTTP
    options.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
