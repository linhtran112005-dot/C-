using System.Text;
using Day3.Model;

namespace Day3.View
{
    public class ProductView
    {
        public string RenderHtml(List<ProductModel> products)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("    <meta charset=\"UTF-8\">");
            sb.AppendLine("    <title>Quản lý Sản phẩm - Day 3 MVC Web</title>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("    <h1>HỆ THỐNG QUẢN LÝ SẢN PHẨM (DAY 3 - MVC WEB)</h1>");
            sb.AppendLine("    <hr>");
            sb.AppendLine("    <h2>1. Danh sách sản phẩm (Data ảo)</h2>");
            sb.AppendLine("    <table border=\"1\" cellpadding=\"6\" cellspacing=\"0\">");
            sb.AppendLine("        <thead>");
            sb.AppendLine("            <tr>");
            sb.AppendLine("                <th>ID</th>");
            sb.AppendLine("                <th>Tên sản phẩm</th>");
            sb.AppendLine("                <th>Đơn giá (VNĐ)</th>");
            sb.AppendLine("                <th>Danh mục</th>");
            sb.AppendLine("                <th>Thao tác</th>");
            sb.AppendLine("            </tr>");
            sb.AppendLine("        </thead>");
            sb.AppendLine("        <tbody>");

            if (products.Count == 0)
            {
                sb.AppendLine("            <tr><td colspan=\"5\" align=\"center\">Kho dữ liệu ảo đang trống!</td></tr>");
            }
            else
            {
                foreach (var p in products)
                {
                    sb.AppendLine("            <tr>");
                    sb.AppendLine($"                <td align=\"center\">{p.Id}</td>");
                    sb.AppendLine($"                <td>{p.Name}</td>");
                    sb.AppendLine($"                <td align=\"right\">{p.Price:N0} đ</td>");
                    sb.AppendLine($"                <td>{p.Category}</td>");
                    sb.AppendLine($"                <td align=\"center\"><a href=\"/delete?id={p.Id}\">Xóa</a></td>");
                    sb.AppendLine("            </tr>");
                }
            }

            sb.AppendLine("        </tbody>");
            sb.AppendLine("    </table>");
            sb.AppendLine($"    <p>Tổng số lượng: <b>{products.Count}</b> sản phẩm.</p>");
            sb.AppendLine("    <hr>");
            sb.AppendLine("    <h2>2. Thêm sản phẩm mới</h2>");
            sb.AppendLine("    <form method=\"POST\" action=\"/add\">");
            sb.AppendLine("        <p>");
            sb.AppendLine("            <label>Tên sản phẩm:</label><br>");
            sb.AppendLine("            <input type=\"text\" name=\"name\" required size=\"35\">");
            sb.AppendLine("        </p>");
            sb.AppendLine("        <p>");
            sb.AppendLine("            <label>Đơn giá (VNĐ):</label><br>");
            sb.AppendLine("            <input type=\"number\" name=\"price\" required min=\"0\">");
            sb.AppendLine("        </p>");
            sb.AppendLine("        <p>");
            sb.AppendLine("            <label>Danh mục:</label><br>");
            sb.AppendLine("            <input type=\"text\" name=\"category\" required size=\"25\">");
            sb.AppendLine("        </p>");
            sb.AppendLine("        <p>");
            sb.AppendLine("            <button type=\"submit\">Thêm sản phẩm</button>");
            sb.AppendLine("            <button type=\"reset\">Làm lại</button>");
            sb.AppendLine("        </p>");
            sb.AppendLine("    </form>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }
    }
}
