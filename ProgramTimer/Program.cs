using ALR.Data.Database;
using ALR.Data.Database.Abstract;
using ALR.Data.Database.Repositories;
using ALR.Data.Dto;
using ALR.Domain.Entities.Entities;
using DocumentFormat.OpenXml.Spreadsheet;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MimeKit;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Timer = System.Timers.Timer;
namespace ProgramTimer
{
    class Program
    {
        private static Timer _timer;
        private static readonly EmailConfiguration _emailConfig;

        static void SetTimer()
        {
            _timer = new Timer(1000);
            _timer.Elapsed += TimerOnElapsed;
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private static void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"check {e.SignalTime}");
        }
        static async  Task Main(string[] args)
        {
            var list = await GetListSchedule(args);
            while (true) { 
                foreach (var item in list)
                {
                    if ( item.bookingDate.Ticks <= DateTime.UtcNow.AddDays(1).Ticks && item.bookingDate.Ticks >= DateTime.UtcNow.Ticks)
                    {
                        var message = new EmailMessage(new string[] { item.landlord.Email }, "Advanced Lodging Room notification email", $"Bạn có lịch hẹn xem trọ vào ngày {item.bookingDate} của  {item.tenant.Account}.");
                        var message1 = new EmailMessage(new string[] { item.tenant.Email }, "Advanced Lodging Room notification email", $"Bạn có lịch hẹn xem trọ vào ngày {item.bookingDate} với  {item.landlord.Account}.");
                        SendMail(message);
                        SendMail(message1);
                        
                    }
                }
                
            SetTimer();
            Thread.Sleep(30000);
            }
            
        }
        public static async Task<List<BookingScheduleEntity>> GetListSchedule(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;
            var serviceProvider = new ServiceCollection()
            .AddTransient<IRepository<BookingScheduleEntity>, Repository<BookingScheduleEntity>>()
            .AddDbContext<ALRDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DATN_Spring24_ALR")))
            .BuildServiceProvider();

            // Now you can resolve IRepository<User> wherever needed
            var userRepository = serviceProvider.GetService<IRepository<BookingScheduleEntity>>();
            var list = await userRepository.GetDataDoubleIncludeAsync(x => x.tenant, x=> x.landlord);
            return list.ToList();
        }
        public static void SendMail(EmailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        private static MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("email", "thanhqazsx@gmail.com"));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return emailMessage;
        }
        private static void Send(MimeMessage message)
        {
            using var client = new SmtpClient();

            try
            {

                client.Connect("smtp.gmail.com", 465, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("thanhqazsx@gmail.com", "iktr bkoh ugfm elwy");
                client.Send(message);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }



    }
}