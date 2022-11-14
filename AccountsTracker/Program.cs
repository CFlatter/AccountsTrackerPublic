using AccountsTracker.Data.EFCore.Contexts;
using AccountsTracker.Data.EFCore.Repositories;
using AccountsTracker.Data.Repositories;
using AccountsTracker.Shared.Interfaces.Repositories;
using AccountsTracker.Shared.Interfaces.Services;
using AccountsTracker.Shared.Services;
using AccountsTracker.Shared.Settings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SavingPercentages>(builder.Configuration.GetSection(nameof(SavingPercentages)));
builder.Services.Configure<PersonalTransfers>(builder.Configuration.GetSection(nameof(PersonalTransfers)));
builder.Services.Configure<SharedTransfers>(builder.Configuration.GetSection(nameof(SharedTransfers)));
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection(nameof(ConnectionStrings)));
builder.Services.Configure<PensionContributionPercentage>(builder.Configuration.GetSection(nameof(PensionContributionPercentage)));

builder.Services.AddTransient<IPersonRepository, PersonEFRepository>();
builder.Services.AddTransient<IAccountRepository, AccountEFRepository>();
builder.Services.AddTransient<IPersonalOutgoingRepository, PersonalOutgoingEFRepository>();
builder.Services.AddTransient<IAccountLogRepository, AccountLogEFRepository>();

builder.Services.AddTransient<IPersonService, PersonService>();
builder.Services.AddTransient<IAccountService, AccountService>();

//builder.Services.AddDbContext<AccountTrackerContext>(opt =>
//     opt.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"))
//    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
//);

builder.Services.AddDbContext<AccountTrackerContext>(opt =>
{
    var conn = builder.Configuration.GetConnectionString("localdb");
    opt.UseMySQL(NormalizeAzureInAppConnString(conn))
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

string NormalizeAzureInAppConnString(string raw)
{
    string conn = string.Empty;
    try
    {
        var dict =
             raw.Split(';')
                 .Where(kvp => kvp.Contains('='))
                 .Select(kvp => kvp.Split(new char[] { '=' }, 2))
                 .ToDictionary(kvp => kvp[0].Trim(), kvp => kvp[1].Trim(), StringComparer.InvariantCultureIgnoreCase);
        var ds = dict["Data Source"];
        var dsa = ds.Split(":");
        conn = $"Server={dsa[0]};Port={dsa[1]};Database={dict["Database"]};Uid={dict["User Id"]};Pwd={dict["Password"]};";
    }
    catch
    {
        throw new Exception("unexpected connection string: datasource is empty or null");
    }
    return conn;
}

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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

app.Run();
