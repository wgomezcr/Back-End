using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PeliculasApi.Controllers;
using PeliculasApi.Filtros;
using PeliculasApi.Utilidades;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Configuracion de libreria automaper para facilidad de compartir entidades y DTOs
            services.AddAutoMapper(typeof(Startup));

            services.AddSingleton(provider =>
                          new MapperConfiguration(config =>
                {
                    var geometryFactory = provider.GetRequiredService<GeometryFactory>();
                    config.AddProfile(new AutoMapperProfiles(geometryFactory));
                }).CreateMapper());

            services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));//srid valor utilizado para mediciones en el planeta tierra

            //Se configuera esta linea para servicio de STORAGE en azure
            services.AddTransient<IAlmacenadorArchivos, AlmacenadorAzureStorage>();

            //Se configura para manejo de archivos en directorios locales
            //services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
            //services.AddHttpContextAccessor();
            //////////////////////////////////////////////////////////////////////

            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection"),
            sqlServer => sqlServer.UseNetTopologySuite())); // Usa paquetes de netTopologySuit para activar querys espaciales



            //El Coors sirve para navegar en dominios diferentes, se activa de la siguiente manera
            services.AddCors(options =>
            {
                var frontendURL = Configuration.GetValue<string>("frontend_url");
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                    .WithExposedHeaders(new string[] { "cantidadTotalRegistros" });//Configuracion para permitir cabecera de metodo en httpContextExtensions
                });
                //{
                //    builder.WithOrigins(frontendURL).AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
                //    .WithExposedHeaders(new string[] { "cantidadTotalRegistros" });//Configuracion para permitir cabecera de metodo en httpContextExtensions
                //});
            });

            //IdentityUser objeto que representa un usuario en la aplicacion
            //IdentityRole objeto que identifica un rol
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
             
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opciones =>
                opciones.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer=false,
                    ValidateAudience=false,
                    //Tiempo de expiracion de los tokens
                    ValidateLifetime=true,
                    //Es la firma con la llave privada
                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["llavejwt"])),
                    //Sirve para evitar problemas de diferencia de tiempos al momento de calcular el vencimiento del TOKEN
                    ClockSkew = TimeSpan.Zero
                });


            //se configura para proteger acciones para un rol en particular
            services.AddAuthorization(opciones =>
            {
                //EsAdmin es el nombre de la regla, se requeire que se tenga un clain con el tipo role y que tenga el valor admin
                opciones.AddPolicy("EsAdmin", policy => policy.RequireClaim("role", "admin"));
            });
            
            
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(FIltroDeExcepcion));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PeliculasApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PeliculasApi v1"));
            }

            app.UseHttpsRedirection();

            //Middleware para manejo de archivos estaticos
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors();


            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
