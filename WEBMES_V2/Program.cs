using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WEBMES_V2.Models.Context;
using WEBMES_V2.Models.DomainModels.Login;
using WEBMES_V2.Models.ISQLRepository;
using WEBMES_V2.Models.SQLRepositoryImplementation;
using WEBMES_V2.Services;
using static WEBMES_V2.Models.ISQLRepository.ILoginRepository;

var builder = WebApplication.CreateBuilder(args);


//------------------Service Registration----------------
builder.Services.AddScoped<ILoginRepoConnection, SQLLoginRepository>();
builder.Services.AddSingleton<IDapperConnection, DapperConnectionRepository>();
builder.Services.AddScoped<IPlasmaMagazineRepository, PlasmaMagazineRepository>();
builder.Services.AddScoped<IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IXMLConverter, XMLConverter>();
builder.Services.AddScoped<IDownloadFile, DownloadFiles>();
builder.Services.AddScoped<CacheManagerService>();
builder.Services.AddScoped<CacheProcess>();
//------------------------------------------------------

// Add services to the container.
builder.Services.AddControllersWithViews();


//----------------------Policy---------------------------------
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("UserCred", policy => policy.RequireRole("User"));
});
//--------------------------------------------------------------


//----------------------Connection Context----------------------
builder.Services.AddDbContext<CentralAccessContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("CentralAccessConnection"));
});

builder.Services.AddDbContext<MesAtecContext>(option =>

{
    option.UseSqlServer(builder.Configuration.GetConnectionString("MES_ATEC_Connection"));
});
//---------------------------------------------------------------

//---------------------------Auth--------------------------------
builder.Services.AddAuthentication(
                 CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(option =>
                 {
                     option.LoginPath = "/LogIn/LoginView";
                     option.AccessDeniedPath = "/LogIn/LoginView";
                 });
//---------------------------------------------------------------

//---------------------------Add automapper configuration--------
builder.Services.AddAutoMapper(typeof(Program).Assembly);
//---------------------------------------------------------------

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=LoginView}/{id?}");

app.Run();
