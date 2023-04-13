using System;
using System.Collections.Generic;

namespace PR_Top_Service_MVC.Models
{
    public partial class Postulation
    {
        public int IdPostulation { get; set; }
        public string ProfessionalTitles { get; set; } = null!;
        public string Certifications { get; set; } = null!;
        public string WorkExperience { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Address { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int IdProfessional { get; set; }

        public virtual Profesional? IdProfessionalNavigation { get; set; } = null!;
    }
}
