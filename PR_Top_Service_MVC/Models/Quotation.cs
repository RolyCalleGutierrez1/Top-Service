using System;
using System.Collections.Generic;

namespace PR_Top_Service_MVC.Models
{
    public partial class Quotation
    {
        public int IdQuotation { get; set; }
        public int IdCostumer { get; set; }
        public DateTime Date { get; set; }
        public string Service { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Status { get; set; } = null!;

        public virtual Costumer IdCostumerNavigation { get; set; } = null!;
    }
}
