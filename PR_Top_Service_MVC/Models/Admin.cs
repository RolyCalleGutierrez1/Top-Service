using System;
using System.Collections.Generic;

namespace PR_Top_Service_MVC.Models
{
    public partial class Admin
    {
        public Admin()
        {
            Services = new HashSet<Service>();
        }

        public int IdAdmin { get; set; }
        public string CelphoneNumber { get; set; } = null!;
        public DateTime Birthdate { get; set; }

        public virtual Person IdAdminNavigation { get; set; } = null!;
        public virtual ICollection<Service> Services { get; set; }
    }
}
