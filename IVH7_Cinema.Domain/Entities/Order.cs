using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace IVH7_Cinema.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Order
    {
        //Size of ID = 10
        private static int Size = 10;

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int64 OrderID { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        
        public double Totaalprijs { get; set;}

        public String Status { get; set;} // ordered, paid, finalized

        private void CalculateTotaalprijs(ICollection<Ticket> tickets)
        {
            foreach (Ticket t in tickets)
            {
                Totaalprijs += t.Tariff.Price;
            }
        }
        public void GenerateRandomNumber()
        {
            string S = null;
            Random Random = new Random();
            for(int i = 0; i < Size; i++)
            {
                string x = Convert.ToString(Random.Next(1,9));
                S = S += x;
            }
            OrderID = Convert.ToInt64(S);
        }
    }
}
