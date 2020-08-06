using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataLibrary.DataAccess.User
{
    public class user
    {
        public static DataLibrary.Models.User.newUserSignUp validateUser(Models.User.newUserSignUp user)
        {

            using (SqlConnection cnn = new SqlConnection(DataLibrary.DataAccess.SQLDataAccess.GetConnectionString()))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "prGetUser";
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = user.UserName;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = user.Password;
                    
                    cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                    Models.User.newUserSignUp userInfo = new Models.User.newUserSignUp();
                    if (reader.Read())
                    {                        
                        userInfo.UserName = reader.GetString(reader.GetOrdinal("UserName"));
                        userInfo.Password = reader.GetString(reader.GetOrdinal("Password"));
                        userInfo.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        userInfo.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                        userInfo.Age = reader.GetInt32(reader.GetOrdinal("Age"));
                        userInfo.Height = reader.GetInt32(reader.GetOrdinal("Height"));
                        userInfo.Weight = reader.GetInt32(reader.GetOrdinal("Weight"));
                        userInfo.Diabetic = reader.GetBoolean(reader.GetOrdinal("Diabetic"));
                        userInfo.HBP = reader.GetBoolean(reader.GetOrdinal("HBP"));
                        userInfo.Smoke = reader.GetBoolean(reader.GetOrdinal("Smoke"));
                        userInfo.Alcohol = reader.GetBoolean(reader.GetOrdinal("Alcohol"));
                        userInfo.PatientID = reader.GetInt32(reader.GetOrdinal("PatientID"));

                        return userInfo;
                    }

                    return null;
                        
                    
                }
            }
        }

        //check to see if they can change name if not already taken
        public static void updateUserAccount(Models.User.newUserSignUp updateInfo)
        {

            using (SqlConnection cnn = new SqlConnection(DataLibrary.DataAccess.SQLDataAccess.GetConnectionString()))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "prUpdateUserAccount";
                    cmd.Parameters.Add("@UserName", SqlDbType.VarChar, 50).Value = updateInfo.UserName;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = updateInfo.Password;
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50).Value = updateInfo.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50).Value = updateInfo.LastName;
                    cmd.Parameters.Add("@Age", SqlDbType.Int, 0).Value = updateInfo.Age;
                    cmd.Parameters.Add("@Height", SqlDbType.Int, 0).Value = updateInfo.Height;
                    cmd.Parameters.Add("@Weight", SqlDbType.Int, 0).Value = updateInfo.Weight;
                    cmd.Parameters.Add("@Diabetic", SqlDbType.Bit, 0).Value = updateInfo.Diabetic;
                    cmd.Parameters.Add("@HBP", SqlDbType.Bit, 0).Value = updateInfo.HBP;
                    cmd.Parameters.Add("@Smoke", SqlDbType.Bit, 0).Value = updateInfo.Smoke;
                    cmd.Parameters.Add("@Alcohol", SqlDbType.Bit, 0).Value = updateInfo.Alcohol;
                    cmd.Parameters.Add("@PatientID", SqlDbType.Int, 0).Value = updateInfo.PatientID;
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void DeleteUserAccount(int patientID)
        {

            using (SqlConnection cnn = new SqlConnection(DataLibrary.DataAccess.SQLDataAccess.GetConnectionString()))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "prDeleteUserAccount";
                    cmd.Parameters.Add("@PatientID", SqlDbType.Int, 0).Value = patientID;
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<Models.User.User_history> GetHistory(int PatientID)
        {
            using (SqlConnection cnn = new SqlConnection(DataLibrary.DataAccess.SQLDataAccess.GetConnectionString()))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "prUser_History";
                    cmd.Parameters.Add("@PatientID", SqlDbType.Int, 0).Value = PatientID;
                    cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    var list = new List<Models.User.User_history>();

                    
                    while (reader.Read())
                    {
                        Models.User.User_history history = new Models.User.User_history();
                        history.PatientID = (int)reader[0];
                        history.Age = (int)reader[1];
                        history.Weight = (int)reader[2];
                        history.Diabetic = (bool)reader[3];
                        history.HBP = (bool)reader[4];
                        history.Smoke = (bool)reader[5];
                        history.Alcohol = (bool)reader[6];
                        history.Symptom = reader[7].ToString();
                        history.Name = reader[8].ToString();
                        list.Add(history);
                    }

                    return list;
                }
            }
        }
    }
}
