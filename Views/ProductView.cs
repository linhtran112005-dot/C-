using SimpleMVC.Models;

namespace SimpleMVC.Views
{
    /// <summary>
    /// View đại diện cho giao diện người dùng (Console UI).
    /// Nguyên tắc MVC: View CHỈ lo hiển thị và nhập/xuất dữ liệu, KHÔNG chứa logic nghiệp vụ.
    /// </summary>
    public class ProductView
    {
        public void DisplayMenu()
        {
            Console.WriteLine("\n==========================================");
            Console.WriteLine("    HỆ THỐNG QUẢN LÝ SẢN PHẨM (MÔ HÌNH MVC)");
            Console.WriteLine("==========================================");
            Console.WriteLine("1. Xem danh sách sản phẩm");
            Console.WriteLine("2. Tìm kiếm sản phẩm theo ID");
            Console.WriteLine("3. Thêm mới sản phẩm");
            Console.WriteLine("4. Cập nhật thông tin sản phẩm");
            Console.WriteLine("5. Xóa sản phẩm");
            Console.WriteLine("0. Thoát chương trình");
            Console.WriteLine("------------------------------------------");
            Console.Write("👉 Vui lòng chọn chức năng (0-5): ");
        }

        public void DisplayProductList(List<Product> products)
        {
            Console.WriteLine("\n--- DANH SÁCH SẢN PHẨM HIỆN CÓ ---");
            if (products == null || products.Count == 0)
            {
                Console.WriteLine("⚠️ Hiện chưa có sản phẩm nào trong kho.");
                return;
            }

            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"{"ID",-5} | {"Tên sản phẩm",-20} | {"Giá (VNĐ)",12} | {"SL",6}");
            Console.WriteLine(new string('-', 60));

            foreach (var p in products)
            {
                Console.WriteLine($"{p.Id,-5} | {p.Name,-20} | {p.Price,12:N0} | {p.Quantity,6}");
            }
            Console.WriteLine(new string('-', 60));
            Console.WriteLine($"Tổng số lượng sản phẩm: {products.Count}");
        }

        public void DisplayProductDetails(Product product)
        {
            Console.WriteLine("\n--- CHI TIẾT SẢN PHẨM ---");
            Console.WriteLine($"Mã ID   : {product.Id}");
            Console.WriteLine($"Tên SP  : {product.Name}");
            Console.WriteLine($"Đơn giá : {product.Price:N0} VNĐ");
            Console.WriteLine($"Tồn kho : {product.Quantity} cái");
        }

        public Product GetNewProductInput()
        {
            Console.WriteLine("\n--- NHẬP THÔNG TIN SẢN PHẨM MỚI ---");
            
            string name;
            while (true)
            {
                Console.Write("Nhập tên sản phẩm: ");
                name = Console.ReadLine()?.Trim() ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(name)) break;
                Console.WriteLine("❌ Tên sản phẩm không được để trống!");
            }

            decimal price;
            while (true)
            {
                Console.Write("Nhập đơn giá (VNĐ): ");
                if (decimal.TryParse(Console.ReadLine(), out price) && price >= 0) break;
                Console.WriteLine("❌ Giá sản phẩm không hợp lệ! Vui lòng nhập số >= 0.");
            }

            int quantity;
            while (true)
            {
                Console.Write("Nhập số lượng tồn kho: ");
                if (int.TryParse(Console.ReadLine(), out quantity) && quantity >= 0) break;
                Console.WriteLine("❌ Số lượng không hợp lệ! Vui lòng nhập số nguyên >= 0.");
            }

            return new Product { Name = name, Price = price, Quantity = quantity };
        }

        public int GetProductIdInput(string actionName)
        {
            Console.Write($"\n👉 Nhập ID sản phẩm cần {actionName}: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                return id;
            }
            return -1;
        }

        public void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✔ {message}");
            Console.ResetColor();
        }

        public void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"❌ {message}");
            Console.ResetColor();
        }

        public void ShowInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"ℹ {message}");
            Console.ResetColor();
        }
    }
}
