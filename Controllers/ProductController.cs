using SimpleMVC.Models;
using SimpleMVC.Views;

namespace SimpleMVC.Controllers
{
    /// <summary>
    /// Controller đóng vai trò là "Bộ điều phối":
    /// 1. Nhận yêu cầu từ người dùng (thông qua tương tác hoặc Program).
    /// 2. Thao tác với Model (thêm, sửa, xóa, tìm kiếm dữ liệu).
    /// 3. Gọi View để hiển thị kết quả tương ứng.
    /// </summary>
    public class ProductController
    {
        private readonly List<Product> _products;
        private readonly ProductView _view;
        private int _autoIncrementId = 1;

        public ProductController(ProductView view)
        {
            _view = view;
            _products = new List<Product>();

            // Khởi tạo một số dữ liệu mẫu ban đầu để dễ test
            SeedInitialData();
        }

        private void SeedInitialData()
        {
            _products.Add(new Product(_autoIncrementId++, "Bàn phím cơ", 850000, 15));
            _products.Add(new Product(_autoIncrementId++, "Chuột không dây", 350000, 20));
            _products.Add(new Product(_autoIncrementId++, "Màn hình 24 inch", 2800000, 8));
        }

        /// <summary>
        /// Hiển thị toàn bộ danh sách sản phẩm
        /// </summary>
        public void DisplayAllProducts()
        {
            _view.DisplayProductList(_products);
        }

        /// <summary>
        /// Tìm kiếm sản phẩm theo ID
        /// </summary>
        public void FindProductById()
        {
            int id = _view.GetProductIdInput("tìm kiếm");
            if (id <= 0)
            {
                _view.ShowError("ID không hợp lệ!");
                return;
            }

            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _view.DisplayProductDetails(product);
            }
            else
            {
                _view.ShowError($"Không tìm thấy sản phẩm nào có ID = {id}");
            }
        }

        /// <summary>
        /// Thêm sản phẩm mới
        /// </summary>
        public void CreateProduct()
        {
            Product newProduct = _view.GetNewProductInput();
            newProduct.Id = _autoIncrementId++;

            _products.Add(newProduct);
            _view.ShowSuccess($"Thêm sản phẩm '{newProduct.Name}' thành công với ID: {newProduct.Id}");
        }

        /// <summary>
        /// Cập nhật sản phẩm theo ID
        /// </summary>
        public void UpdateProduct()
        {
            int id = _view.GetProductIdInput("cập nhật");
            if (id <= 0)
            {
                _view.ShowError("ID không hợp lệ!");
                return;
            }

            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                _view.ShowError($"Không tìm thấy sản phẩm có ID = {id} để cập nhật.");
                return;
            }

            _view.ShowInfo($"Đang cập nhật sản phẩm hiện tại: {product.Name}");
            Product updatedInfo = _view.GetNewProductInput();

            product.Name = updatedInfo.Name;
            product.Price = updatedInfo.Price;
            product.Quantity = updatedInfo.Quantity;

            _view.ShowSuccess($"Cập nhật sản phẩm ID {id} thành công!");
        }

        /// <summary>
        /// Xóa sản phẩm theo ID
        /// </summary>
        public void DeleteProduct()
        {
            int id = _view.GetProductIdInput("xóa");
            if (id <= 0)
            {
                _view.ShowError("ID không hợp lệ!");
                return;
            }

            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                _view.ShowError($"Không tìm thấy sản phẩm có ID = {id} để xóa.");
                return;
            }

            _products.Remove(product);
            _view.ShowSuccess($"Đã xóa thành công sản phẩm: {product.Name} (ID: {id})");
        }
    }
}
