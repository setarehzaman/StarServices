using Serilog;
using STS.Domain.Service.User;
using STS.Domain.AppService.User;
using STS.Domain.Service.Feature;
using STS.Domain.Service.BaseData;
using STS.Domain.Service.Category;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using STS.Domain.AppService.Feature;
using STS.Domain.Core.Entities.User;
using STS.Domain.AppService.BaseData;
using STS.Domain.AppService.Category;
using STS.Domain.Core.Entities.Configs;
using STS.Domain.Core.Contracts.Service;
using STS.Infrastructure.SqlServer.Common;
using STS.Domain.Core.Contracts.AppService;
using STS.Domain.Core.Contracts.Repository;
using STS.Infrastructure.Cache.Redis;
using STS.Infrastructure.Ef.Repositories.Repositories.User;
using STS.Infrastructure.Ef.Repositories.Repositories.Feature;
using STS.Infrastructure.Ef.Repositories.Repositories.BaseData;
using STS.Infrastructure.Ef.Repositories.Repositories.Category;
using Hangfire;
using STS.Endpoints.RazorPages.Infrastructure;
using STS.Domain.Core.Contracts.Infrastructure;
using STS.Infrastructure.Sms;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

#region Configuration

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var siteSettings = configuration.GetSection("SiteSettings").Get<SiteSettings>();
builder.Services.AddSingleton(siteSettings);

#endregion

#region DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(siteSettings.connectionStrings.SqlConnection));
#endregion

#region Register Services

builder.Services.AddScoped<IOrderAppService, OrderAppService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<ISuggestionRepository, SuggestionRepository>();
builder.Services.AddScoped<ISuggestionAppService, SuggestionAppService>();
builder.Services.AddScoped<ISuggestionService, SuggestionService>();

builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
builder.Services.AddScoped<ISubCategoryAppService, SubCategoryAppService>();
builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();

builder.Services.AddScoped<IMainCategoryAppService, MainCategoryAppService>();
builder.Services.AddScoped<IMainCategoryService, MainCategoryService>();
builder.Services.AddScoped<IMainCategoryRepository, MainCategoryRepository>();

builder.Services.AddScoped<ITaskItemService, TaskItemService>();
builder.Services.AddScoped<ITaskItemAppService, TaskItemAppService>();
builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();

builder.Services.AddScoped<IExpertAppService, ExpertAppService>();
builder.Services.AddScoped<IExpertService, ExpertService>();
builder.Services.AddScoped<IExpertRepository, ExpertRepository>();

builder.Services.AddScoped<IClientAppService, ClientAppService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

builder.Services.AddScoped<IApplicationUserAppService, ApplicationUserAppService>();
builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();

builder.Services.AddScoped<IBaseDataAppService, BaseDataAppService>();
builder.Services.AddScoped<IBaseDataService, BaseDataService>();
builder.Services.AddScoped<IBaseDataRepository, BaseDataRepository>();

builder.Services.AddScoped<IFeedBackAppService, FeedBackAppService>();
builder.Services.AddScoped<IFeedBackService, FeedBackService>();
builder.Services.AddScoped<IFeedBackRepository, FeedBackRepository>();

builder.Services.AddScoped<ISmsService, SmsService>();



#endregion

#region Identity Registration
builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
    .AddRoles<IdentityRole<int>>()
    .AddEntityFrameworkStores<AppDbContext>();
#endregion

#region LogConfiguration

builder.Host.ConfigureLogging(o =>
{
    o.ClearProviders();
    o.AddSerilog();
    o.SetMinimumLevel(LogLevel.Information);
}).UseSerilog((context, config) =>
{
    config
        .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
        .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
        .WriteTo.Seq("http://localhost:5341", apiKey: "FvT1IrWSOoP1DIRjIulv");
});

#endregion

#region CacheConfiguration

builder.Services.AddScoped<IRedisCacheServices, RedisCacheServices>();

//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.Configuration = siteSettings!.RedisConfiguration.ConnectionString;
//    options.ConfigurationOptions = new ConfigurationOptions
//    {
//        Password = siteSettings.RedisConfiguration.Password,
//        DefaultDatabase = siteSettings.RedisConfiguration.DefaultDatabase,
//        ConnectTimeout = siteSettings.RedisConfiguration.ConnectTimeout,
//    };
//    options.ConfigurationOptions.EndPoints.Add(siteSettings.RedisConfiguration.ConnectionString);
//});

#endregion

#region Hangfire

builder.Services.AddHangfire(configuration => configuration
       .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
       .UseSimpleAssemblyNameTypeSerializer()
       .UseRecommendedSerializerSettings()
   .UseSqlServerStorage(siteSettings!.connectionStrings.HangfireConnection));

builder.Services.AddHangfireServer();
builder.Services.AddSingleton<AutoRejectJob>();

#endregion

var app = builder.Build();

// Redirect to the error page for exceptions
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHangfireDashboard();

var autoRejectJob = app.Services.GetRequiredService<AutoRejectJob>();

RecurringJob.AddOrUpdate(
    "auto-reject-orders",
    () => autoRejectJob.AutoReject(default),
    "0 17 * * *",
   new RecurringJobOptions
   {
       TimeZone = TimeZoneInfo.FindSystemTimeZoneById("Iran Standard Time")
   });



app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSerilogRequestLogging();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
