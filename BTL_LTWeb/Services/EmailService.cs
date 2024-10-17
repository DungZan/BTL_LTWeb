using System.Net.Mail;
using System.Net;

namespace BTL_LTWeb.Services
{
    public class EmailService
    {
        private readonly string fromMail = "caminh2k4@gmail.com";
        private readonly string fromPassword = "ceed izuv cnhn nqxr";
        public EmailService()
        {
        }
        public void SendEmail(string to, string name, string code)
        {


            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Test Subject";
            message.To.Add(new MailAddress(to));
            message.Body = GetEmailTemplate(to, name, code);
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }

        private string GetEmailTemplate(string receiver, string name, string code)
        {
            var body =
                @"<!DOCTYPE html>
                <html lang='vi'>
                <head>
                    <meta charset='UTF-8'>
                    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                    <style>
                        body {
                            font-family: Arial, sans-serif;
                            background-color: #f7f7f7;
                            margin: 0;
                            padding: 0;
                        }
                        .container {
                            width: 100%;
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            border-radius: 8px;
                            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
                            overflow: hidden;
                        }
                        .header {
                            background-color: #4CAF50;
                            color: white;
                            padding: 20px;
                            text-align: center;
                        }
                        .content {
                            padding: 20px;
                        }
                        .footer {
                            text-align: center;
                            padding: 10px;
                            font-size: 12px;
                            color: #777;
                        }
                        .button {
                            display: inline-block;
                            padding: 10px 20px;
                            margin: 20px 0;
                            color: white;
                            background-color: #4CAF50;
                            text-decoration: none;
                            border-radius: 5px;
                        }
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h1>Xác Nhận Tài Khoản</h1>
                        </div>
                        <div class='content'>
                            <h2>Chào {register.Name},</h2>
                            <p>Cảm ơn bạn đã đăng ký tài khoản với chúng tôi!</p>
                            <p>Để hoàn tất quá trình đăng ký, xin vui lòng nhấn vào nút bên dưới để xác nhận tài khoản của bạn:</p>
                            <a href='{{ConfirmationLink}}' class='button'>{code}</a>
                            <p>Nếu bạn không thực hiện yêu cầu này, bạn có thể bỏ qua email này.</p>
                            <p>Trân trọng,<br>Đội ngũ hỗ trợ</p>
                        </div>
                        <div class='footer'>
                            <p>&copy; {DateTime.Now.Year} BeA Fashion. Bảo mật thông tin của bạn là ưu tiên hàng đầu của chúng tôi.</p>
                        </div>
                    </div>
                </body>
                </html>"
            ;

            body = body.Replace("{register.Name}", name)
            .Replace("{code}", code)    
           .Replace("{CurrentYear}", DateTime.Now.ToString("hh:mm:ss dd:MM:yyyy"));

            return body;
        }
    }
}
