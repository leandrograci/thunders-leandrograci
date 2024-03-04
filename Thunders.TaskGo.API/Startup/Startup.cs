using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Thunders.TaskGo.Infra;
using Thunders.TaskGo.Web.Startup.Extensions;
using Thunders.TaskGo.Web.Startup.Middlewares;
using System.Reflection;

namespace Thunders.TaskGo.Web.Startup
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<BusinessExceptionFilter>();
            });

            services.AddDbContext<ThundersTaskGoDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("TaskGoConnection")));

       
            services.AddEndpointsApiExplorer();

            var swaggerXMLPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ThundersTaskGo API",
                    Description = "Projeto de teste para avaliação da Thunders"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization Header.<br />
                                Preencha: 'Bearer' [espaço] seu token.<br />
                                Exemplo: 'Bearer 12345abcdef...'",
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
                        Type = ReferenceType.SecurityScheme, Id = "Bearer"
                      }
                    },
                    Array.Empty<string>()
                  }
                });

                c.IncludeXmlComments(swaggerXMLPath);
                c.EnableAnnotations();
            });

            services.AddServices();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddJwtAuthorization();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          

            // Verifica se o banco de dados existe e executa as migrações
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ThundersTaskGoDbContext>();
                dbContext.Database.Migrate();
            }

            app.UseHsts();
           
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
          
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ThundersTaskGoAPIv1");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors("AllowAll");
        }
    }
  }
