using System.Diagnostics;
using System.Net;
using Day3.Controller;
using Day3.Data;
using Day3.View;

namespace Day3
{
    public static class Day3App
    {
        public static void Run()
        {
            var dataContext = new MockProductData();
            var view = new ProductView();
            var controller = new ProductController(dataContext, view);

            string url = "http://localhost:5050/";
            var listener = new HttpListener();
            listener.Prefixes.Add(url);

            try
            {
                listener.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi không thể khởi động Web Server: {ex.Message}");
                return;
            }

            Console.WriteLine("\n========================================================");
            Console.WriteLine("    🌐 WEB SERVER DAY 3 (MVC HTML THUẦN) ĐANG CHẠY!    ");
            Console.WriteLine("========================================================");
            Console.WriteLine($"👉 Đường dẫn trang web: {url}");
            Console.WriteLine("👉 Đang tự động mở trình duyệt web...");
            Console.WriteLine("👉 Nhấn phím 'Q' trên màn hình Console để tắt Web Server");
            Console.WriteLine("--------------------------------------------------------");

            try
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch { }

            var serverThread = new Thread(() =>
            {
                while (listener.IsListening)
                {
                    try
                    {
                        var context = listener.GetContext();
                        ThreadPool.QueueUserWorkItem(_ => controller.HandleRequest(context));
                    }
                    catch
                    {
                        break;
                    }
                }
            });
            serverThread.IsBackground = true;
            serverThread.Start();

            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Q)
                {
                    break;
                }
            }

            listener.Stop();
            Console.WriteLine("\n👋 Đã tắt Web Server và quay lại menu chọn bài.");
        }
    }
}
