using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace ProductsApi.Models.v2
{
    [Table("Products")]
    public class Product1
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Cần được thiết lập")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Nhập trong khoảng 3-50")]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public decimal? Price { get; set; }

        public static implicit operator Product1(List<Product1> v)
        {
            throw new NotImplementedException();
        }
    }

    //public class Test
    //{
    //    public static void Main(string[] args)
    //    {
    //        //CreateHostBuilder(args).Build().Run();

    //        Product1 product1 = new Product1();
    //        product1.Name = null;
    //        ValidationContext context = new ValidationContext(product1);
    //        // results - lưu danh sách ValidationResult, kết quả kiểm tra
    //        List<ValidationResult> results = new List<ValidationResult>();
    //        // thực hiện kiểm tra dữ liệu
    //        bool valid = Validator.TryValidateObject(product1, context, results, true);

    //        if (!valid)
    //        {
    //            // Duyệt qua các lỗi và in ra
    //            foreach (ValidationResult vr in results)
    //            {
    //                Console.ForegroundColor = ConsoleColor.Blue;
    //                Console.Write($"{vr.MemberNames,13}");
    //                Console.ForegroundColor = ConsoleColor.Red;
    //                Console.WriteLine($"    {vr.ErrorMessage}");
    //            }
    //        }

    //    }
    //}
}
