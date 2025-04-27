using Microsoft.EntityFrameworkCore;
using CheineseSale.Models;
using CheineseSale.Dal;
using CheineseSale.Service;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // זה יקרוא את ההגדרות מ-`appsettings.json`
    .CreateLogger();

builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();  // מבטל את ספקי הלוגים הקיימים
    logging.AddSerilog();  // מוסיף את Serilog לשימוש בלוגים
    logging.SetMinimumLevel(LogLevel.Information);  // הצגת לוגים ברמת Information ומעלה
});



builder.Services.AddDbContext<GiftsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))
);

builder.Services.AddHttpContextAccessor();

// הוספת שאר השירותים
builder.Services.AddScoped<IgiftDal, giftDal>();
builder.Services.AddScoped<IgiftSrvice, giftService>();
builder.Services.AddScoped<IDonterDal, DonterDal>();
builder.Services.AddScoped<IdonterSrv, DonterSrv>();
builder.Services.AddScoped<ILoginDal, LoginDal>();
builder.Services.AddScoped<IloginSrv, loginSrv>();
builder.Services.AddScoped<IregistrDal, RegistrDal>();
builder.Services.AddScoped<IregistrSrv, registrSrv>();
builder.Services.AddScoped<IUserGiftDal, UserGiftDal>();
builder.Services.AddScoped<IUserGiftSrv, UserGiftSrv>();
builder.Services.AddScoped<JwtTokenService>();
builder.Services.AddScoped<IRaffleSrv, RaffleSrv>();
builder.Services.AddScoped<IRaffleDal, RaffleDal>();
builder.Services.AddScoped<ICategoryDal, CategoryDal>();
builder.Services.AddScoped<ICategorySrv, CategorySrv>();
builder.Services.AddScoped<IBaskat, BaskatDal>();
builder.Services.AddScoped<IBaskatSrv, BaskatSrv>();
builder.Services.AddScoped<IUserGiftDal, UserGiftDal>();
builder.Services.AddScoped<IUserGiftSrv, UserGiftSrv>();
builder.Services.AddScoped<IgiftsWithWinnersDal, giftsWithWinnersDal>();
builder.Services.AddScoped<IgiftsWithWinnersSrv, giftsWithWinnersSrv>();

// קישור לאנגולר
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:4200", "development web site")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ChineseAuction",
        Description = "A simple example ASP.NET Core API to manage books",
        Contact = new OpenApiContact
        {
            Name = "sari zalaznik",
            Email = "3267sari@gmail.com",
            Url = new Uri("https://yourwebsite.com"),
        }
    });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer {your_token}'"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ChineseAuction API V1");
        c.RoutePrefix = string.Empty; // Serve Swagger UI at the app's root
    });
}

//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("CorsPolicy");
app.UseAuthentication(); // חייב להיות לפני Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
