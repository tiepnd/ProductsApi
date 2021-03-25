using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySql.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ProductsApi.Models.v2
{
    public class ProductsContext : DbContext
    {

        public ProductsContext() { }
        
        public ProductsContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public DbSet<Product1> products1 { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL(Configuration.GetConnectionString("MySqlConnection"));
        }

        public static async Task CreateDatabase()
        {
            using (var dbcontext = new ProductsContext())
            {
                String databasename = dbcontext.Database.GetDbConnection().Database;

                Console.WriteLine("Tạo " + databasename);

                bool result = await dbcontext.Database.EnsureCreatedAsync();
                string resultstring = result ? "tạo  thành  công" : "đã có trước đó";
                Console.WriteLine($"CSDL {databasename} : {resultstring}");
            }
        }

        public static async Task DeleteDatabaseAsync()
        {
            using (var dbContext = new ProductsContext())
            {
                String databasename = dbContext.Database.GetDbConnection().Database;
                Console.Write($"Có chắc chắn xóa {databasename} (y) ? ");
                string input = Console.ReadLine();

                // Hỏi lại cho chắc
                if (input.ToLower() == "y")
                {
                    bool deleted = await dbContext.Database.EnsureDeletedAsync();
                    string deletionInfo = deleted ? "đã xóa" : "không xóa được";
                    Console.WriteLine($"{databasename} {deletionInfo}");
                }
            }
        }
        // Thực hiện chèn hai dòng dữ liệu vào bảng Product
        // Dùng AddAsync trong DbSet và trong DbContext
        public static async Task InsertProduct()
        {
            using (var context = new ProductsContext())
            {
                //// Thêm sản phẩm 1
                //await context.products1.AddAsync(new Product1
                //{
                //    Name = "Sản phẩm 1"
                //});
                //// Thêm sản phẩm 2
                //await context.AddAsync(new Product1()
                //{
                //    Name = "Sản phẩm 2"
                //});

                var p1 = new Product1() { Name = "Sản phẩm 3", Price = 100 };
                var p2 = new Product1() { Name = "Sản phẩm 4", Price = 100 };
                var p3 = new Product1() { Name = "Sản phẩm 4", Price = 100 };

                context.AddRange(new Product1[] { p1, p2, p3 });

                // Thực hiện cập nhật thay đổi trong DbContext lên Server
                int rows = await context.SaveChangesAsync();
                Console.WriteLine($"Đã lưu {rows} sản phẩm");

            }
        }

        public static async Task ReadProducts()
        {
            using (var context = new ProductsContext())
            {
                var products = await context.products1.ToListAsync();
                Console.WriteLine("Tất cả sản phẩm");
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.Id,2} {product.Name,10} - {product.CreatedDate}");
                }
            }
        }



        //static async Task Main(string[] args)
        //{
        //    //await CreateDatabase();
        //    await InsertProduct();
        //    //await ReadProducts();
        //}
    }
}
