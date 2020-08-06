using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models.User
{
    public class user_symptoms
    {
        public string symptom { get; set; }
        public bool Checked
        {
            get;
            set;
        }
        public int Id
        {
            get;
            set;
        }

    }
}
