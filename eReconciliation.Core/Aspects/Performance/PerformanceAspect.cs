using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using eReconciliation.Core.Entities.Concrete;
using eReconciliation.Core.Utilities.Interceptors;
using eReconciliation.Core.Utilities.IoC;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.DependencyInjection;

namespace eReconciliation.Core.Aspects.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        //arada geçen süreyi hesaplayacak
        private Stopwatch _stopwatch;
        public PerformanceAspect(int interval)
        {
            _interval = interval;
            _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();
        }
        protected override void OnBefore(IInvocation invocation)
        {
            _stopwatch.Start();
        }
        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                string body = $"Performance:{invocation.Method.DeclaringType.FullName}.{invocation.Method.Name} --> {_stopwatch.Elapsed.TotalSeconds}";
                SendConfirmEmail(body);
            }
            _stopwatch.Reset();
        }
        void SendConfirmEmail(string body)
        {
            string subject = "Performans Maili";

            var sendMail = new SendMailDto()
            {
                Email = "projemaili@zohomail.eu",
                Password = "Ahmet.9122",
                Port = 587,
                SMTP = "smtp.zoho.eu",
                SSL = true,
                email = "ahmet.karabudakk.9122@gmail.com",
                Subject = subject,
                Body = body
            };

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(sendMail.Email);
                mail.To.Add(sendMail.email);
                mail.Subject = sendMail.Subject;
                mail.Body = sendMail.Body;
                //Göndereceğin kodda html kodları kullanacak mısın;
                mail.IsBodyHtml = true;

                // mail.Attachments.Add();
                using (SmtpClient smtp = new SmtpClient(sendMail.SMTP))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(sendMail.Email, sendMail.Password);
                    smtp.EnableSsl = sendMail.SSL;
                    smtp.Port = sendMail.Port;
                    smtp.Send(mail);
                }
            }
        }
    }

}