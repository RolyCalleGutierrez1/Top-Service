using System;
using System.Collections.Generic;

namespace PR_Top_Service_MVC.Models
{
    public partial class Costumer
    {
        public Costumer()
        {
            Quotations = new HashSet<Quotation>();
        }

        public int IdCostumer { get; set; }
        public string Address { get; set; } = null!;

        public virtual Person IdCostumerNavigation { get; set; } = null!;
        public virtual ICollection<Quotation> Quotations { get; set; }
    }
}
