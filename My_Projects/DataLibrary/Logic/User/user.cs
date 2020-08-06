using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Logic.User
{
    public class user
    {
        public static Models.User.newUserSignUp validateUser(Models.User.newUserSignUp user)
        {
            Models.User.newUserSignUp userInfo = DataLibrary.DataAccess.User.user.validateUser(user);
            return userInfo;
        }

        public static void updateUserAccount(Models.User.newUserSignUp updateInfo)
        {
           DataAccess.User.user.updateUserAccount(updateInfo);
           
        }
        public static void DeleteUserAccount(int patientID)
        {
            DataAccess.User.user.DeleteUserAccount(patientID);

        }

        public static List<Models.User.User_history> GetHistory(int PatientID)
        {
            return DataAccess.User.user.GetHistory(PatientID);
        }
    }
}
