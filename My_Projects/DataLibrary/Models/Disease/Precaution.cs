using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models.Disease
{
    public class Precaution
    {
        public string Disease { get; set; }
        public string precaution_1 { get; set; }
        public string precaution_2 { get; set; }
        public string precaution_3 { get; set; }
        public string precaution_4 { get; set; }

        public string errorMessage { get; set; }
    }
}
