using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

//Add services to containter

// Add services to the container.
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

//builder.Services.AddRateLimiter(rateLimiterOptions =>
//{
//    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
//    {
//        options.Window = TimeSpan.FromSeconds(10);
//        options.PermitLimit = 5;
//    });
//});

//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer("Bearer", options =>
//    {
//        options.Authority = "https://your-auth-server.com"; // your UserApi or IdentityServer
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateAudience = false,
//            ValidIssuer = "your-issuer",
//            ValidAudience = "your-audience",
//            RoleClaimType = ClaimTypes.Role
//        };
//    });

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminOnly", policy =>
//        policy.RequireRole("Admin"));
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseRateLimiter();

app.MapReverseProxy();

app.Run();
