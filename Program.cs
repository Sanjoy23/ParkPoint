using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Serilog;

// Log.Logger = new LoggerConfiguration()
//                 .MinimumLevel.Information()
//                 .WriteTo.File("Logs/Log.txt", rollingInterval: RollingInterval.Day)
//                 .CreateLogger();
var builder = WebApplication.CreateBuilder(args);
Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration).CreateLogger();
    
    // Add services to the container.

    // var logger = new LoggerConfiguration()
    //         .ReadFrom.Configuration(builder.Configuration)
    //         .Enrich.FromLogContext()
    //         .CreateLogger();
    // builder.Logging.ClearProviders();
    // builder.Logging.AddSerilog(logger);

    builder.Services.AddControllers();
    builder.Services.AddDbContext<ParkPointContext>(options =>
    options.UseSqlite("Data Source=dbParkPoint"));
    builder.Services.AddScoped<IParkingService, ParkingService>();
    builder.Services.AddScoped<IParkingSlotRepository, ParkingSlotRepository>();
    builder.Services.AddSingleton<JwtTokenService>();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? string.Empty))
        };
    });

    //adding identity to service
    builder.Services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ParkPointContext>()
            .AddDefaultTokenProviders();
    //builder.Services.AddMemoryCache();
    builder.Services.AddDistributedRedisCache(options =>
    {
        options.Configuration = "localhost:6379";
        options.InstanceName = "";
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();




