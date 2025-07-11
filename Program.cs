using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Tournament.Management.API.Data;
using Tournament.Management.API.Helpers.Implementations;
using Tournament.Management.API.Helpers.Interfaces;
using Tournament.Management.API.Repository.Implementations;
using Tournament.Management.API.Repository.Interfaces;
using Tournament.Management.API.Services.Implementations;
using Tournament.Management.API.Services.Interfaces;

namespace Tournament.Management.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        AddSwagger(builder.Services);
        AddCors(builder.Services);
        AddDatabase(builder.Services, builder.Configuration);
        AddRepositories(builder.Services);
        AddServices(builder.Services);
        AddJwtAuthentication(builder.Services, builder.Configuration);

        var app = builder.Build();

        app.UseCors("CorsPolicy");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tournament Management API v1");
                c.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }

    private static void AddSwagger(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Tournament Management API",
                Version = "v1",
                Description = "API for managing tournaments, teams, and players"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: 'Authorization: Bearer {token}'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });

            var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }

    private static void AddCors(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
            });
        });
    }

    private static void AddDatabase(IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<TournamentManagerContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        });
    }

    private static void AddJwtAuthentication(IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = config["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = config["Jwt:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!))
                };
            });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IPlayerStatRepository, PlayerStatRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ITeamMatchRepository, TeamMatchRepository>();
        services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<ITournamentFormatRepository, TournamentFormatRepository>();
        services.AddScoped<ITournamentTeamRepository, TournamentTeamRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITournamentRepository, UserTournamentRepository>();
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IPlayerStatService, PlayerStatService>();
        services.AddScoped<ITeamMatchService, TeamMatchService>();
        services.AddScoped<ITeamMemberService, TeamMemberService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ITournamentFormatService, TournamentFormatService>();
        services.AddScoped<ITournamentService, TournamentService>();
        services.AddScoped<ITournamentTeamService, TournamentTeamService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPasswordHelper, PasswordHelper>();
    }
}
