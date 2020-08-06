using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.DataAccess.User;

namespace DataLibrary.Logic.User
{
    public class newUserSignUp
    {
        public static int addUser(Models.User.newUserSignUp newUser)
        {

            int patiendID = DataAccess.User.newUserSignUp.addUser(newUser);
            return patiendID;

        }
    }
}
