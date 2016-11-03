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
    public class Mailer : IMailer
    {
        static string Recipient = "richarddejongh1995@gmail.com";
        static MailAddress Sender = new MailAddress("noreply@CinemAbioscopen.nl", "CinemA bioscopen");
        static string Subject = "CinemA bioscopen";

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
            emailBoodschap.Body = "Hallo," + subscriber.Name + ", " + Environment.NewLine + "Bijgaande de nieuwsbrief voor de komende weken"
                + Environment.NewLine + "Tot snel bij CinemA bioscopen";

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
            emailBoodschap.Body = "Beste bezoeker van CinemA bioscopen, " + Environment.NewLine + "Uw kaarten zijn succesvol gereserveerd. " 
                + Environment.NewLine + Environment.NewLine + "Uw code voor uw bestelling is: " + OrderID + Environment.NewLine + 
                "Deze code kunt u invullen bij de touchscreens in de bioscoop om uw kaartjes te laten afdrukken." + Environment.NewLine 
                + Environment.NewLine + "Tot snel bij CinemA bioscopen";

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
            emailBoodschap.To.Add(Email);

            // Onderwerp
            emailBoodschap.Subject = Subject;

            // Bericht
            emailBoodschap.Body = "Beste bezoeker van CinemA bioscopen, " + Environment.NewLine + "Uw kaarten zijn succesvol besteld. "
                + Environment.NewLine + Environment.NewLine + "Uw code voor uw bestelling is: " + OrderID + Environment.NewLine +
                "Deze code kunt u invullen bij de touchscreens in de bioscoop om uw kaartjes te laten afdrukken." + Environment.NewLine
                + Environment.NewLine + "Daarnaast is er een PDF bijgevoegd zodat u de kaartjes kan uitprinten" 
                + Environment.NewLine + "Tot snel bij CinemA bioscopen";

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