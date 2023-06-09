﻿using System;
using System.Collections.Generic;

namespace PR_Top_Service_MVC.Models
{
    public partial class Service
    {
        public int IdService { get; set; }
        public int IdAdmin { get; set; }
        public int IdProfessional { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
        public byte Status { get; set; } 

        public virtual Admin? IdAdminNavigation { get; set; } = null!;
        public virtual Profesional? IdProfessionalNavigation { get; set; } = null!;
        public virtual Receipt? Receipt { get; set; }
    }
}
