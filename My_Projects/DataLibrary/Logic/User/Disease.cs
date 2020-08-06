using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Logic.User
{
    public class Disease
    {
        public static Models.User.AllDisease GetDisease(Models.User.AllDisease diseases)
        {
            Models.User.AllDisease diseaseX = DataLibrary.DataAccess.User.Disease.GetDisease(diseases);
            return diseaseX;
        }
    }
}
