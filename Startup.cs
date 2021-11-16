using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StudentService.DataService;
using Microsoft.EntityFrameworkCore;

namespace StudentService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string CorsApi = "CorsApi";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            // TODO: create connection  with user & pw
            var builder = new SqlConnectionStringBuilder(Configuration["ConnectionStrings:AWSConnection"]);
            builder.UserID = Configuration["DbUserName"];
            builder.Password = Configuration["DbPassWord"];

            var connection = builder.ConnectionString;
            services.AddDbContext<StudentServiceContext>(opt => opt.UseSqlServer(connection));

            //services.AddDbContext<StudentServiceContext>(opt => opt.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            services.AddCors(options =>
            {
                options.AddPolicy(name:CorsApi,
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StudentService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudentService v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors(CorsApi);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
