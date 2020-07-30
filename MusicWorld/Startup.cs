using CloudinaryDotNet;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicWorld.Controllers;
using MusicWorld.Data;
using MusicWorld.Services;
using MusicWorld.Services.Contracts;

namespace MusicWorld
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddSession();

            services.AddDbContext<MusicWorldContext>();

            services.AddScoped<IUserServices, UserServices>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddScoped<IArtistService, ArtistService>();
            services.AddTransient<IAlbumService, AlbumService>();
            
            services.AddScoped<UsersController>();
            services.AddScoped<ArtistsController>();

            Account cloudinaryCredentials = new Account(
               this.Configuration["Cloudinary:CloudName"],
               this.Configuration["Cloudinary:ApiKey"],
               this.Configuration["Cloudinary:ApiSecret"]);
            Cloudinary cloudinaryUtility = new Cloudinary(cloudinaryCredentials);

            services.AddSingleton(cloudinaryUtility);
            services.AddScoped<AlbumsController>();
            services.AddScoped<SongsController>();
            services.AddScoped<CatalogsController>();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
