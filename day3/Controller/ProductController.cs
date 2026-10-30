using System.Net;
using System.Text;
using Day3.Data;
using Day3.Model;
using Day3.View;

namespace Day3.Controller
{
    public class ProductController
    {
        private readonly MockProductData _dataContext;
        private readonly ProductView _view;

        public ProductController(MockProductData dataContext, ProductView view)
        {
            _dataContext = dataContext;
            _view = view;
        }

        public void HandleRequest(HttpListenerContext context)
        {
            var request = context.Request;
            var response = context.Response;

            string path = request.Url?.AbsolutePath ?? "/";
            string method = request.HttpMethod;

            if (method == "GET" && path == "/")
            {
                var products = _dataContext.GetAll();
                string html = _view.RenderHtml(products);
                SendHtmlResponse(response, html);
            }
            else if (method == "POST" && path == "/add")
            {
                using var reader = new StreamReader(request.InputStream, request.ContentEncoding);
                string formBody = reader.ReadToEnd();
                var formData = ParseFormData(formBody);

                string name = formData.GetValueOrDefault("name", "").Trim();
                decimal.TryParse(formData.GetValueOrDefault("price", "0"), out decimal price);
                string category = formData.GetValueOrDefault("category", "Chung").Trim();

                if (!string.IsNullOrWhiteSpace(name))
                {
                    _dataContext.Add(name, price, string.IsNullOrWhiteSpace(category) ? "Chung" : category);
                }

                Redirect(response, "/");
            }
            else if (method == "GET" && path == "/delete")
            {
                string? idStr = request.QueryString["id"];
                if (int.TryParse(idStr, out int id))
                {
                    _dataContext.Delete(id);
                }

                Redirect(response, "/");
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                SendHtmlResponse(response, "<h1>404 Not Found</h1><p><a href=\"/\">Quay lại trang chủ</a></p>");
            }
        }

        private static void SendHtmlResponse(HttpListenerResponse response, string htmlContent)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(htmlContent);
            response.ContentType = "text/html; charset=utf-8";
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        }

        private static void Redirect(HttpListenerResponse response, string targetUrl)
        {
            response.StatusCode = (int)HttpStatusCode.Redirect;
            response.RedirectLocation = targetUrl;
            response.OutputStream.Close();
        }

        private static Dictionary<string, string> ParseFormData(string body)
        {
            var result = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(body)) return result;

            var pairs = body.Split('&');
            foreach (var pair in pairs)
            {
                var parts = pair.Split('=');
                if (parts.Length == 2)
                {
                    string key = Uri.UnescapeDataString(parts[0].Replace('+', ' '));
                    string value = Uri.UnescapeDataString(parts[1].Replace('+', ' '));
                    result[key] = value;
                }
            }
            return result;
        }
    }
}
