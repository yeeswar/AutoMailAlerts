using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace AutoMailAlerts
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter To Address:");
            string to = Console.ReadLine().Trim();

            Console.WriteLine("Enter Subject:");
            string subject = Console.ReadLine().Trim();

            Console.WriteLine("Enter Body:");
            string body = Console.ReadLine().Trim();

            Console.WriteLine("Enter From Address:");
            string from = Console.ReadLine().Trim();

            Console.WriteLine("Password");
            string password = Console.ReadLine();

            using (MailMessage mm = new MailMessage(from, to))
            {
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = ConfigurationManager.AppSettings["Host"];
                smtp.EnableSsl = true;
                //NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["Username"], ConfigurationManager.AppSettings["Password"]);
                NetworkCredential NetworkCred = new NetworkCredential(from, password);
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                Console.WriteLine("Sending Email......");
                smtp.Send(mm);
                Console.WriteLine("Email Sent.");
                System.Threading.Thread.Sleep(3000);
                Environment.Exit(0);
            }
        }
    }
}
