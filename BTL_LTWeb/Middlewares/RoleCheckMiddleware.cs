namespace BTL_LTWeb.Middlewares
{
    public class RoleCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public RoleCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Kiểm tra nếu người dùng đã đăng nhập
            if (context.User.Identity.IsAuthenticated)
            {
                // Kiểm tra session để xem người dùng đã được điều hướng hay chưa
                if (context.Session.GetString("HasRedirected") != "true")
                {
                    // Đánh dấu rằng đã điều hướng
                    context.Session.SetString("HasRedirected", "true");

                    // Kiểm tra vai trò của người dùng
                    if (context.User.IsInRole("KhachHang"))
                    {
                        // Nếu không ở trang Home thì điều hướng đến trang Home
                        if (!context.Request.Path.StartsWithSegments("/Home/Home"))
                        {
                            context.Response.Redirect("/Home/Home");
                            return;
                        }
                    }
                    else
                    {
                        // Nếu không ở trang Admin thì điều hướng đến trang Admin
                        if (!context.Request.Path.StartsWithSegments("/Admin/Index"))
                        {
                            context.Response.Redirect("/Admin/Index");
                            return;
                        }
                    }
                }
            }

            // Tiếp tục xử lý request
            await _next(context);
        }
    }
}
