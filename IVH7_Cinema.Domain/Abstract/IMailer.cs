using System.Collections.Generic;
using IVH7_Cinema.Domain.Entities;
using System;

namespace IVH7_Cinema.Domain.Abstract
{
    public interface IMailer
    {
        void SendMessage(Subscriber subscriber);

        void SendShortMessage(String Mailer, Int64 OrderID);
        //void SendShortMessage(string email, Int64 OrderID);

        void sentOrderPDF(String Email, Int64 OrderID, String PDFPad);
    }
}
