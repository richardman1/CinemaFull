using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IVH7_Cinema.Domain.Entities;
using System.Net;
using System.Net.Mail;
using IVH7_Cinema.Domain.Abstract;
using System.Diagnostics.CodeAnalysis;

namespace IVH7_Cinema.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class MailerEN : IMailer
    {
        static string Recipient = "richarddejongh1995@gmail.com";
        static MailAddress Sender = new MailAddress("noreply@CinemAbioscopen.nl", "CinemA bioscopen");
        static string Subject = "CinemA Newsletter";

        public void SendMessage(Subscriber subscriber)
        {
            // Maak het e-mailbericht
            MailMessage emailBoodschap = new MailMessage();
            emailBoodschap.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            // Stel afzender in
            emailBoodschap.From = Sender;

            // Voeg geadresseerden toe
            emailBoodschap.To.Add(Recipient);

            // Onderwerp
            emailBoodschap.Subject = Subject;

            // Bericht
            emailBoodschap.Body = "Hello," + subscriber.Name + ", " + Environment.NewLine + "This message includes the newsletter from CinemA."
                + Environment.NewLine + "With kind Regards, CinemA";

            // Stel prioriteit in
            emailBoodschap.Priority = MailPriority.Normal;

            // Voeg eventueel bijlagen toe
            //emailBoodschap.Attachments.Add(new Attachment("C:\\EenBijlage.txt"));

            SmtpClient emailVerzender = new SmtpClient("smtp.gmail.com", 587);
            emailVerzender.EnableSsl = true;
            emailVerzender.Timeout = 10000;
            emailVerzender.DeliveryMethod = SmtpDeliveryMethod.Network;
            emailVerzender.UseDefaultCredentials = false;

            // Verstuur het e-mailbericht
            {
                // Bij een beveiligde mailserver --> credentials opgeven
                emailVerzender.Credentials = new NetworkCredential("richarddejongh1995@gmail.com", "Rambogek1!");
                // Verstuur de email
                emailVerzender.Send(emailBoodschap);
            }
        }

        public void SendShortMessage(String Email, Int64 OrderID) {
            // Maak het e-mailbericht
            MailMessage emailBoodschap = new MailMessage();
            emailBoodschap.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            // Stel afzender in
            emailBoodschap.From = Sender;

            // Voeg geadresseerden toe
            emailBoodschap.To.Add(Email);

            // Onderwerp
            emailBoodschap.Subject = Subject;

            // Bericht
            emailBoodschap.Body = "Dear Visitor of CinemA, " + Environment.NewLine + "Your purchase has been succesfull. " 
                + Environment.NewLine + Environment.NewLine + "Your order code is: " + OrderID + Environment.NewLine + 
                "You can enter this code at the touchscreen in the CinemA to get your tickets." + Environment.NewLine 
                + Environment.NewLine + "Kind regards, CinemA";

            // Stel prioriteit in
            emailBoodschap.Priority = MailPriority.Normal;

            // Voeg eventueel bijlagen toe
            //emailBoodschap.Attachments.Add(new Attachment("C:\\EenBijlage.txt"));

            SmtpClient emailVerzender = new SmtpClient("smtp.gmail.com", 587);
            emailVerzender.EnableSsl = true;
            emailVerzender.Timeout = 10000;
            emailVerzender.DeliveryMethod = SmtpDeliveryMethod.Network;
            emailVerzender.UseDefaultCredentials = false;

            // Verstuur het e-mailbericht
            {
                // Bij een beveiligde mailserver --> credentials opgeven
                emailVerzender.Credentials = new NetworkCredential("richarddejongh1995@gmail.com", "Rambogek1!");
                // Verstuur de email
                emailVerzender.Send(emailBoodschap);
            }
        }

        public void sentOrderPDF(String Email, Int64 OrderID, String PDFPad) {
            // Maak het e-mailbericht
            MailMessage emailBoodschap = new MailMessage();
            emailBoodschap.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            // Stel afzender in
            emailBoodschap.From = Sender;

            // Voeg geadresseerden toe
            emailBoodschap.To.Add(Recipient);

            // Onderwerp
            emailBoodschap.Subject = Subject;

            // Bericht
            emailBoodschap.Body = "Dearest CinemA bioscopen visitor, " + Environment.NewLine + "Our tickets have been succesfully ordered "
                + Environment.NewLine + Environment.NewLine + "The ordercode is: " + OrderID + Environment.NewLine +
                "You can print your ticket at the ticketmachine. " + Environment.NewLine
                + Environment.NewLine + "Or you can print your tickets at home, by printing the PDF attached to this email"
                + Environment.NewLine + "Till soon at CinemA bioscopen";

            // Stel prioriteit in
            emailBoodschap.Priority = MailPriority.Normal;

            // Voeg eventueel bijlagen toe
            emailBoodschap.Attachments.Add(new Attachment(PDFPad));

            SmtpClient emailVerzender = new SmtpClient("smtp.gmail.com", 587);
            emailVerzender.EnableSsl = true;
            emailVerzender.Timeout = 10000;
            emailVerzender.DeliveryMethod = SmtpDeliveryMethod.Network;
            emailVerzender.UseDefaultCredentials = false;

            // Verstuur het e-mailbericht
            {
                // Bij een beveiligde mailserver --> credentials opgeven
                emailVerzender.Credentials = new NetworkCredential("richarddejongh1995@gmail.com", "Rambogek1!");
                // Verstuur de email
                emailVerzender.Send(emailBoodschap);
            }
        }
    }
}