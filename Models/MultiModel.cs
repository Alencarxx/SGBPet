using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SGB_Beta1.Models
{
    public class MultiModel
    {
        public List<Saidas> Saidas { get; set; }
        public List<ContasApagar> ContasApagar { get; set; }
        public List<VendasPorData> VendasPorData { get; set; }
    }
}