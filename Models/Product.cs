namespace SimpleMVC.Models
{
    /// <summary>
    /// Model đại diện cho thực thể Sản phẩm trong bài Lab MVC.
    /// Chịu trách nhiệm lưu trữ cấu trúc dữ liệu và trạng thái của đối tượng.
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product() { }

        public Product(int id, string name, decimal price, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"[ID: {Id}] {Name,-20} | Giá: {Price,10:N0} VNĐ | Số lượng: {Quantity,4}";
        }
    }
}
