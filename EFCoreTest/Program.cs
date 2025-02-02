using DotNetTry.serviceInject;
using EFCoreTest.Helpers;
using EFCoreTest.Models.second_dbfirstTry;
using EFCoreTest.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Add allowed origins
              .AllowAnyHeader()  // Allow all headers
              .AllowAnyMethod()  // Allow all HTTP methods
              .AllowCredentials(); // If you need to allow cookies or authorization headers
    });
});
//builder.Services.AddDbContext<Employee_BookDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDBConnection"))
////.LogTo(Console.WriteLine) // console respective sql commands in the o / p window
//);
builder.Configuration.AddUserSecrets<Program>();
//string connectionString = builder.Configuration["AWSPostgreSQLEmployeeDBConnection"];
builder.Services.AddDbContext<Employee_BookDBContext>(options =>
//options.UseNpgsql(connectionString));
options.UseNpgsql(builder.Configuration.GetConnectionString("AWSPostgreSQLEmployeeDBConnection")));
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Configuration to validate JWT
        options.Authority = "https://cognito-idp.us-east-1.amazonaws.com/us-east-1_JK5bCOLUi";
        options.Audience = "2pulk8dsciup51sa1va6l13075";

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://cognito-idp.us-east-1.amazonaws.com/us-east-1_JK5bCOLUi",
            ValidAudience = "2pulk8dsciup51sa1va6l13075",
        };
    });

//callback url https://localhost:5001/signin-oidc
//https://localhost:5001
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddHttpClient();
builder.Services.AddTransient<IDemoService, DemoService>();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowSpecificOrigins");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();




//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
//    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
//.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
//{
//    options.LoginPath = "/Account/Login"; // Redirect path for unauthenticated users.
//    options.LogoutPath = "/Account/Logout"; // Logout path.
//    options.SlidingExpiration = true; // Optional, to auto extend session.
//})
//.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
//{
//    options.ClientId = "54lj2rvipu25ndtav6bs8her47";
//    options.ClientSecret = "10qg0352i6t0rirrdlbc6p9cihvknnf8e902pqsdnjvbugsnnnpt";
//    options.Authority = "https://cognito-idp.us-east-1.amazonaws.com/us-east-1_JK5bCOLUi";
//    options.ResponseType = "code";
//    options.Scope.Add("openid");
//    options.Scope.Add("profile");
//    options.Scope.Add("email");
//    options.SaveTokens = true; // Store tokens to use later.
//    options.CallbackPath = "/signin-oidc"; // The URL that the user will be redirected back to.
//    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//    {
//        NameClaimType = "cognito:username",
//        ValidateIssuer = false, // Disabling issuer validation in case it is not provided by Cognito.
//    };
//    //options.Events = new OpenIdConnectEvents
//    //{
//    //    OnRedirectToIdentityProviderForSignOut = context =>
//    //    {
//    //        var logoutUri = $"https://us-east-1jk5bcolui.auth.us-east-1.amazoncognito.com/logout?client_id=54lj2rvipu25ndtav6bs8her47";
//    //        logoutUri += $"&logout_uri={context.Request.Scheme}://{context.Request.Host}/"; // Redirect after logout.
//    //        context.Response.Redirect(logoutUri);
//    //        context.HandleResponse();
//    //        return Task.CompletedTask;
//    //    }
//    //};
//});