using System;
using System.Collections.Generic;

namespace PR_Top_Service_MVC.Models
{
    public partial class Profesional
    {
        public Profesional()
        {
            Postulations = new HashSet<Postulation>();
            Services = new HashSet<Service>();
        }

        public int IdProfesional { get; set; }
        public string Ocupation { get; set; } = null!;
        public DateTime Birthdate { get; set; }

        public virtual Person IdProfesionalNavigation { get; set; } = null!;
        public virtual ICollection<Postulation> Postulations { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
