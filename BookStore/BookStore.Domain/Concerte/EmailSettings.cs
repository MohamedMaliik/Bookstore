using BookStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Entites;
using System.Net.Mail;
using System.Net;
using System.Diagnostics;

namespace BookStore.Domain.Concerte
{       
   public class EmailSettings
    {   

        public string MaliToAddress =  " momalik665@gmail.com ";
        public string MaliFromAddress ="mommalik16@gmail.com";
        public bool UseSsl = true;
        public string Username = "mommlik16@gmail.com";
        public string password = "pesmost972";
        public string Servername = "smtp.gmail.com";
        public int Serverport = 587;
        public bool WriteAsFile = false;
        public string FileLoction = @"C:\orders_bookstore_emails";
           
    }   
        
    public class EmailOrderProcessor : IOrderProcessor
    {
        private
            EmailSettings emailSettings;
        public EmailOrderProcessor (EmailSettings setting)
        {
            emailSettings = setting;
        }
        public void ProcessorOrder(Cart cart, ShppingDetails shppingDetails)
        {

            using (var smptClient = new SmtpClient())
            {
                smptClient.EnableSsl = emailSettings.UseSsl;
                smptClient.Host = emailSettings.Servername;
                smptClient.Port = emailSettings.Serverport;
                smptClient.UseDefaultCredentials = false;
                smptClient.Credentials = new 
                    NetworkCredential( emailSettings.Username, emailSettings.password);
                // if طبيعي  ح تشيك ترو 
                if (emailSettings.WriteAsFile)
                {
                    smptClient.DeliveryFormat = (SmtpDeliveryFormat)SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smptClient.PickupDirectoryLocation = emailSettings.FileLoction;
                    smptClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("A new order has been submitted")
                    .AppendLine("-------")
                    .AppendLine("Books: ");
                foreach (var line in cart.lines)
                {
                    var Subtotal = line.Book.Price * line.Quantity;
                    body.AppendFormat("{0} x {1}  (Subtotal: {2:c})", line.Quantity, line.Book.Title, Subtotal);

                    body.AppendFormat("Total order value : {0:c}", cart.computeTotal())
                        .AppendLine("------")
                        .AppendLine("Ship to")
                        .AppendLine(shppingDetails.Name)
                        .AppendLine(shppingDetails.Line1)
                        .AppendLine(shppingDetails.Line2)
                        .AppendLine(shppingDetails.State)
                        .AppendLine(shppingDetails.City)
                        .AppendLine(shppingDetails.Country)
                        .AppendLine("-----")
                        .AppendFormat("Gift war:{0}", shppingDetails.GiftWrap ? "Yes" : "No");

                    // mail class بياخد الرسالة بتفاصيلها وبكون الرسالة 
                    MailMessage mailmessage = new MailMessage(
                        emailSettings.MaliFromAddress,
                    emailSettings.MaliToAddress,
                    "New order  submitted ",
                    body.ToString());

                    if (emailSettings.WriteAsFile)
                    {
                        mailmessage.BodyEncoding = Encoding.ASCII;
                    }

                    try
                    {
                        smptClient.Send(mailmessage);
                    }
                    catch 
                    (Exception ex)
                    {
                        Debug.Print(ex.Message);
                    }
                }

                
                
            }

        }
    }

}
