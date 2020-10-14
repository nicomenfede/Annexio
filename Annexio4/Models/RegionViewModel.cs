using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Annexio4.Models
{
    public class RegionViewModel
    {
        public string Name { get; set; }
        public int Population { get; set; }
        public IEnumerable<string> Countries { get; set; }
        public IEnumerable<string> Subregions { get; set; }
    }
}