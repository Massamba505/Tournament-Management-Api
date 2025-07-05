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
        
        // Add Swagger services
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { 
                Title = "Tournament Management API", 
                Version = "v1",
                Description = "API for managing tournaments, teams, and players"
            });
            
            // Add JWT Authentication to Swagger
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                }
            });
        });

        builder.Services.AddCors(option =>
        {
            option.AddPolicy("CorsPolicy", policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            });
        });

        builder.Services.AddDbContext<TournamentManagerContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        // Repositories
        builder.Services.AddScoped<IPlayerStatRepository, PlayerStatRepository>();
        builder.Services.AddScoped<IRoleRepository, RoleRepository>();
        builder.Services.AddScoped<ITeamMatchRepository, TeamMatchRepository>();
        builder.Services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
        builder.Services.AddScoped<ITeamRepository, TeamRepository>();
        builder.Services.AddScoped<ITournamentFormatRepository, TournamentFormatRepository>();
        builder.Services.AddScoped<ITournamentTeamRepository, TournamentTeamRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ITournamentRepository, UserTournamentRepository>();

        // Services
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IPlayerStatService, PlayerStatService>();
        builder.Services.AddScoped<ITeamMatchService, TeamMatchService>();
        builder.Services.AddScoped<ITeamMemberService, TeamMemberService>();
        builder.Services.AddScoped<ITeamService, TeamService>();
        builder.Services.AddScoped<ITokenService, TokenService>();
        builder.Services.AddScoped<ITournamentFormatService, TournamentFormatService>();
        builder.Services.AddScoped<ITournamentService, TournamentService>();
        builder.Services.AddScoped<ITournamentTeamService, TournamentTeamService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IPasswordHelper, PasswordHelper>();


        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                    ValidateIssuerSigningKey = true,
                };
            });

        var app = builder.Build();


        app.UseCors("CorsPolicy");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => {
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
}
