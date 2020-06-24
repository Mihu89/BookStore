using BookStore.Domain.Entities;
using BookStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Implementation
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;
        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetail shippingDetail)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(
                     emailSettings.UserName, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }
                StringBuilder body = new StringBuilder()
                    .Append("You have a new order:")
                    .AppendLine("OrderId: " + shippingDetail.OrderId)
                    .AppendLine("##################")
                    .AppendLine("Items:");
                foreach (var line in cart.Lines)
                {
                    var lineTotal = line.Product.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (subTotal: {2:c}", line.Product.Name, line.Quantity, lineTotal);
                }

                body.AppendFormat("Total order value: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("##########")
                    .AppendLine("Ship to:")
                    .AppendLine(shippingDetail.Name)
                    .AppendLine(shippingDetail.Country)
                    .AppendLine(shippingDetail.City)
                    .AppendLine(shippingDetail.State)
                    .AppendLine(shippingDetail.Street)
                    .AppendLine(shippingDetail.StreetNumber)
                    .AppendLine(shippingDetail.Zip);

                MailMessage mailMessage = new MailMessage(
                     emailSettings.MailFromAddress,
                     cart.Client?.ApplicationUser.Email,
                     "You made a new order on BookStore.com",
                     body.ToString()
                    );

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }

    public class EmailSettings
    {
        public string MailToAddress = "";
        public string MailFromAddress = "BookStore@yopmail.com";
        public bool UseSsl = true;
        public string UserName = "MyUsername";
        public string Password = "StrongPassword";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"c:\bookStore_Emails";
    }
}
