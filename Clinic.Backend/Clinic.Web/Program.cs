using Clinic.DataAccess;
using Clinic.Application;
using Clinic.Infrastructure;
using Clinic.Web.Extensions;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.Options;
using Serilog;
using Clinic.Infrastructure.Authentication;
using Clinic.Web.Infrastructure;
using Clinic.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// ��������� Serilog
builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

var services = builder.Services;
var configuration = builder.Configuration;

// ����������� ��������������
builder.Services.AddApiAuthentication(builder.Configuration);

// ���������� ������������
builder.Services.AddControllers();

// ��������� Swagger ��� ������������ API
builder.Services.AddSwaggerGen();

// ��������� JWT � �����
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.Configure<RolePermissionsOptions>(builder.Configuration.GetSection("RolePermissionsOptions"));

// ����������� ��������
services
    .AddPersistence(configuration)
    .AddApplication()
    .AddInfrastructure();

builder.Services.AddProblemDetails();

// ����������� ����������� ����������
services.AddExceptionHandler<GlobalExceptionHandler>();

// �������� � ��������� ����������
var app = builder.Build();

// ��������� ����� ����������
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ������������� ����������� ������
app.UseStaticFiles();

// ������������ �������� cookie
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

// ������������� �������������� � �����������
app.UseAuthentication();
app.UseAuthorization();

// ��������� ��������� ����������
app.UseExceptionHandler("/error");

// ����������� ��������
app.UseSerilogRequestLogging();

// ��������� �������������
app.UseRouting();

// ����������� ����������������� middleware ��� ����������� ��������� �������
app.UseMiddleware<RequestLogContextMiddleware>();

// ��������� ���������
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();













//app.UseCors(x =>
//{
//    x.WithHeaders().AllowAnyHeader();
//    x.WithOrigins("http://localhost:3000");
//    x.WithMethods().AllowAnyMethod();
//});

//// ����������� ������� ����� ��������� �������� � ����� �������� ����������
//using (var scope = app.Services.CreateScope())
//{
//    var policyProvider = scope.ServiceProvider.GetRequiredService<PolicyProvider>();
//    var authorizationOptions = scope.ServiceProvider.GetRequiredService<IOptions<Microsoft.AspNetCore.Authorization.AuthorizationOptions>>().Value;
//    policyProvider.RegisterPolicies(authorizationOptions);
//}


