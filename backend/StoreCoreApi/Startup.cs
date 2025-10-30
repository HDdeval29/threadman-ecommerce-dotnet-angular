using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StoreCoreApi.BAL.Interface;
using StoreCoreApi.BAL.Repository;
using StoreCoreApi.DAL.AdminPortal;
using StoreCoreApi.DAL.Interface;
using StoreCoreApi.DAL.Repository;
using StoreCoreApi.DAL.StoreDTO;
using System.Text;

namespace StoreCoreApi
{
    public class Startup
    {
        private readonly string _secretKey;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _secretKey = Configuration["Authentication:SecretKey"];
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //var key = "This is my first Test Key for authenticate Api's";

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_secretKey))
                };
            });

            services.AddSwaggerGen(gen =>
            {
                gen.SwaggerDoc
                ("v2",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Store Core API",
                        Version = "v2",
                        Description = "Store Core API"
                    }
                );

                gen.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Description = "Insert token here"
                });

                gen.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            });

            services.AddScoped<IJwtAuthentication, JwtAuthentication>();
            //services.AddSingleton<IJwtAuthentication>(new JwtAuthentication(key, Configuration));
            services.AddTransient<IStoreServices, StoreServices>();
            services.AddTransient<IStoreDTO, StoreDTO>();
            services.AddTransient<IAdmin, Admin>();
            services.AddTransient<IUser, User>();
            services.AddTransient<IMasterServices, MasterServices>();
            services.AddTransient<IProduct, Product>();
            services.AddTransient<IOrder, Order>();
            services.AddTransient<IDbServices, DbServices>();

            //string conStr = this.Configuration.GetConnectionString("DefaultConnection");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StoreCoreApi", Version = "v1" });
            });

            services.AddCors(o => o.AddPolicy("MyStoreCoreApi", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StoreCoreApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("MyStoreCoreApi");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
