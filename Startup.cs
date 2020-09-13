using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using APITest.Domain.Repositories;
using APITest.Domain.Services;
using APITest.Presistence.Contexts;
using APITest.Presistence.Repositories;
using APITest.Services;

namespace APITest
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

            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddDbContext<AppDbContext>(options =>{
                options.UseInMemoryDatabase("apitest-in-memory");
            });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()){
                app.UseDeveloperExceptionPage();
            } else {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseMvc();
        }
    }
}
