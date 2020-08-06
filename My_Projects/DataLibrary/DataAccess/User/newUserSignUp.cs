using DataLibrary.Models.User;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DataLibrary.DataAccess.User
{
    public class newUserSignUp
    {
        //string sql = @"insert into dbo.Patient_User (UserName, Password, FirstName, LastName, Age, Height, Weight, Diabetic, HBP, Smoke, Alcohol, PatientID)
        //                   value (@UserName, @Password, @FirstName, @LastName, @Age, @Height, @Weight, @Diabetic, @HBP, @Smoke, @Alcohol, @PatientID);";
        public static int addUser(Models.User.newUserSignUp newUser)
        {
            using (SqlConnection cnn = new SqlConnection(DataLibrary.DataAccess.SQLDataAccess.GetConnectionString()))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "prNewUserSignUp";
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = newUser.UserName;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = newUser.Password;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = newUser.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = newUser.LastName;
                    cmd.Parameters.Add("@Age", SqlDbType.Int, 0).Value = newUser.Age;
                    cmd.Parameters.Add("@Height", SqlDbType.Int, 0).Value = newUser.Height;
                    cmd.Parameters.Add("@Weight", SqlDbType.Int, 0).Value = newUser.Weight;
                    cmd.Parameters.Add("@Diabetic", SqlDbType.Bit, 0).Value = newUser.Diabetic;
                    cmd.Parameters.Add("@HBP", SqlDbType.Bit, 0).Value = newUser.HBP;
                    cmd.Parameters.Add("@Smoke", SqlDbType.Bit, 0).Value = newUser.Smoke;
                    cmd.Parameters.Add("@Alcohol", SqlDbType.Bit, 0).Value = newUser.Alcohol;
                
                    //ADD CHECK TO SEE IF USERNAME ALREADY EXISTS IF IT DOES REDIRECT
                    cnn.Open();

                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                    int patientID = -1;
                    if (reader.Read())
                    {
                        patientID = reader.GetInt32(reader.GetOrdinal("PatientID"));
                    }
                    return patientID;
                }
            }
        }
    }
}
