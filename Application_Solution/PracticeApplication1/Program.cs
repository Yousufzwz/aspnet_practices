using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PracticeApplication1;
using PracticeApplication1.Data;
using PracticeApplication1.Models;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

//Serilog configuration
//builder.Host.UseSerilog((ctx, lc) => lc
//    .MinimumLevel.Debug()
//    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
//    .Enrich.FromLogContext()
//    .ReadFrom.Configuration(builder.Configuration)
//);

// Serilog configuration
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    //.WriteTo.Console() // You can also log to the console if needed
    .WriteTo.File("log.txt", restrictedToMinimumLevel: LogEventLevel.Information) // All levels to file
    .WriteTo.MSSqlServer(
        connectionString: "Server=(localdb)\\mssqllocaldb;Database=aspnet-PracticeApplication1-0e510aeb-1828-4e7f-a82f-84e9ece3b9af;Trusted_Connection=True;MultipleActiveResultSets=true",
        tableName: "Logs",
        restrictedToMinimumLevel: LogEventLevel.Error) // Error and above to database
    .WriteTo.Email(
        fromEmail: "app@example.com",
        toEmail: "support@example.com",
        mailServer: "smtp.example.com",
        mailSubject:"Fatal Error",
        restrictedToMinimumLevel: LogEventLevel.Fatal) // Fatal to email
    .CreateLogger();

//try-catch added
try
{

    //Autofac configuration
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.RegisterModule(new WebModule());
    });

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
    builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<ApplicationDbContext>();
    builder.Services.AddControllersWithViews();

    builder.Services.AddSingleton<IEmailSender, HtmlEmailSender>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapRazorPages();

    app.Run();

    Log.Information("Application Starting...");
}
catch(Exception ex)
{
    Log.Fatal(ex, "Failed to Start application");
}
finally
{
    Log.CloseAndFlush();
}
