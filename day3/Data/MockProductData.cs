using Day3.Model;

namespace Day3.Data
{
    public class MockProductData
    {
        private readonly List<ProductModel> _mockDatabase;
        private int _autoId = 1;

        public MockProductData()
        {
            _mockDatabase = new List<ProductModel>();
            SeedData();
        }

        private void SeedData()
        {
            _mockDatabase.Add(new ProductModel(_autoId++, "Laptop Asus ROG Strix", 28500000, "Laptop"));
            _mockDatabase.Add(new ProductModel(_autoId++, "Bàn phím cơ AKKO 3087", 1250000, "Phụ kiện"));
            _mockDatabase.Add(new ProductModel(_autoId++, "Chuột gaming Logitech G102", 420000, "Phụ kiện"));
            _mockDatabase.Add(new ProductModel(_autoId++, "Màn hình Dell UltraSharp U2422H", 6390000, "Màn hình"));
            _mockDatabase.Add(new ProductModel(_autoId++, "Tai nghe Sony WH-1000XM4", 5890000, "Âm thanh"));
        }

        public List<ProductModel> GetAll()
        {
            return new List<ProductModel>(_mockDatabase);
        }

        public ProductModel? GetById(int id)
        {
            return _mockDatabase.FirstOrDefault(p => p.Id == id);
        }

        public ProductModel Add(string name, decimal price, string category)
        {
            var product = new ProductModel(_autoId++, name, price, category);
            _mockDatabase.Add(product);
            return product;
        }

        public bool Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _mockDatabase.Remove(product);
                return true;
            }
            return false;
        }
    }
}
