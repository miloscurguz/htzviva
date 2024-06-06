using AutoMapper;
using Data;
using Data.Service;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using viva.webstore.Mapping;

namespace viva.webstore
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
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddControllers();
            services.AddRazorPages();
            services.AddSingleton(mapper);
            services.AddMvc();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IArtikliService, ArtikliService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IWSHelper, WSHelper>();
            services.AddScoped<IWSCalls, WSCalls>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddDNTCaptcha(
                option=>option.UseCookieStorageProvider()
                .ShowThousandsSeparators(false)
                .WithEncryptionKey("This is my secure key!")
                );
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;

            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.IsEssential = true;
    });


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
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
            
                endpoints.MapRazorPages();

                // this is necessary for the captcha's image provider
                endpoints.MapControllerRoute(
                                             "default",
                                             "{controller=Home}/{action=Index}/{id?}");
            
            //endpoints.MapRazorPages();
        });
        }
    }
}
