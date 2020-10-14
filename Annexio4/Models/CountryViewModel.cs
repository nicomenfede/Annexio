using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Annexio4.Models
{
    public class CountryViewModel
    {
        public string Name { get; set; }
        public string Capital { get; set; }
        public string Population { get; set; }
        public IEnumerable<Currency> Currencies { get; set; }
        public IEnumerable<Language> Languages { get; set; }
        public IEnumerable<string> Borders { get; set; }
    }
}