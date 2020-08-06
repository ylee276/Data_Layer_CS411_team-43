using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.DataAccess.User
{
    public class Disease
    {
        public static DataLibrary.Models.User.AllDisease GetDisease(Models.User.AllDisease diseases)
        {
            using (SqlConnection cnn = new SqlConnection(DataLibrary.DataAccess.SQLDataAccess.GetConnectionString()))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "prDiseases";
                    cmd.Parameters.Add("@Disease", SqlDbType.NVarChar, 255).Value = diseases.Disease;

                    cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                    Models.User.AllDisease diseaseX = new Models.User.AllDisease();
                    if (reader.Read())
                    {
                        diseaseX.Disease = reader.GetString(reader.GetOrdinal("Disease"));
                        diseaseX.Description = reader.GetString(reader.GetOrdinal("Description"));
                        return diseaseX;
                    }
                    else
                    {
                        diseaseX.Disease = "Please Enter Some Disease Name";
                        diseaseX.Description = "None";
                        return diseaseX;
                    }



                }
            }
        }
    }
}
