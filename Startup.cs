using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using ProductsApi.Models;
using ProductsApi.Models.v2;
using Microsoft.Extensions.Options;
using ProductsApi.Services.v1;
using ProductsApi.Services.v2;

namespace ProductsApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // như bình thường thì sẽ phải Get the current configuration file.
            Configuration = configuration;
        }

        // requires using Microsoft.Extensions.Configuration;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // requires using Microsoft.Extensions.Options
            /*
             *
             *nameof(ProductstoreDatabaseSettings): return ProductstoreDatabaseSettings;
             *Configuration.GetSection: Trả về đối tượng ConfigurationSection được chỉ định .
             *Configuration.GetSection(nameof(ProductstoreDatabaseSettings))): return: 
             *  dữ liệu từ file apppsetting.json  sử dụng tính năng  JSON configuration provider.
             *  "ProductstoreDatabaseSettings": {
             *      "ProductsCollectionName": "Products",
             *      "ConnectionString": "mongodb://localhost:27017",
             *      "DatabaseName": "ProductstoreDb"
             *     }
             * 
             *=>Cấu hình lớp ProductstoreDatabaseSettings đọc dữ liệu trả về này
             *https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-5.0
             *
             */
            services.Configure<ProductstoreDatabaseSettings>(
                Configuration.GetSection(nameof(ProductstoreDatabaseSettings)));


            //Dependency injection in ASP.NET Core
            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0
            //https://viblo.asia/p/dependency-injection-la-gi-va-khi-nao-thi-nen-su-dung-no-LzD5d0d05jY

            services.AddSingleton<IProductstoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ProductstoreDatabaseSettings>>().Value);

            //Dependency injection in ASP.NET Core
            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0
            //https://viblo.asia/p/dependency-injection-la-gi-va-khi-nao-thi-nen-su-dung-no-LzD5d0d05jY
            services.AddSingleton<Services.v1.ProductService>();
            services.AddSingleton<Services.v2.IProductService,Services.v2.ProductService>(); 
            services.AddSingleton<ProductsContext>();
            //Phương pháp này định cấu hình các dịch vụ MVC cho các tính năng thường được sử dụng với bộ điều khiển cho một API. 
            //https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.mvcservicecollectionextensions.addcontrollers?view=aspnetcore-5.0
            services.AddControllers();
            //Add documenting
            //https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-5.0&tabs=visual-studio
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductsApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductsApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
