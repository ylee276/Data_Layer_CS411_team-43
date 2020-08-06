using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace DataLibrary.DataAccess.User
{
    public class symptoms
    {
        public static List<Models.User.user_symptoms> GetAllSymptoms()
        {
            using (SqlConnection cnn = new SqlConnection(DataLibrary.DataAccess.SQLDataAccess.GetConnectionString()))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "prGetAllSymptoms";

                    cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    var list = new List<Models.User.user_symptoms>();


                    while (reader.Read())
                    {
                        Models.User.user_symptoms symptoms = new Models.User.user_symptoms();
                        symptoms.symptom = reader[0].ToString();
                        symptoms.Id = (int)reader[1];
                        symptoms.Checked = false;
                        list.Add(symptoms);
                    }

                    return list;
                }
            }
        }

        public static int StoreUserSymptoms(List<int> symptomsIDs, int patientUserID)
        {
            using (SqlConnection cnn = new SqlConnection(DataLibrary.DataAccess.SQLDataAccess.GetConnectionString()))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "prStoreUserSymptoms";

                    cmd.Parameters.Add("@patientUserID", SqlDbType.Int, 0).Value = patientUserID;
                    for (int i = 1; i <= symptomsIDs.Count; i++)
                    {
                        string curSymptomParam = String.Concat("@symptom", i);
                        cmd.Parameters.Add(curSymptomParam, SqlDbType.Int, 0).Value = symptomsIDs[i-1];
                    }

                    cnn.Open();
                    int symptomID = Convert.ToInt32(cmd.ExecuteScalar());
                    return symptomID;
                }
            }
        }

        public static DataSet GetUserDataForDiseaseCalc(int symptomID)
        {
            DataSet ds = new DataSet();
            using (SqlConnection cnn = new SqlConnection(DataLibrary.DataAccess.SQLDataAccess.GetConnectionString()))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "prUserDataForDiseaseCalc";

                    cmd.Parameters.Add("@symptomID", SqlDbType.Int, 0).Value = symptomID;

                    cnn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(ds);
                    }                      
                }
                return ds;

            }
        }

        public static Hashtable getDiseaseToSymptom(List<Models.User.user_symptoms> list)
        {
            Hashtable hashTable = new Hashtable();
            DataSet ds = new DataSet();
            try
            {

                using (SqlConnection cnn = new SqlConnection(DataLibrary.DataAccess.SQLDataAccess.GetConnectionString()))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "prgetDiseaseToSymptom";
                        cmd.Parameters.Add("@symptomID", SqlDbType.Int, 0);
                        cnn.Open();
                        foreach (Models.User.user_symptoms symptoms in list)
                        {
                            List<int> diseaseList = new List<int>();

                            
                            cmd.Parameters["@symptomID"].Value = symptoms.Id;
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(ds);
                            }

                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                diseaseList.Add(Convert.ToInt32(dr["DiseaseID"]));
                            }
                            hashTable.Add(symptoms.Id, diseaseList);
                            ds.Clear();
                        }

                        return hashTable;
                    }
                }
            }
            catch (Exception ex)
            {
                string message = "Failed to pull disease to symptom data";
                // Logger.Error(LogSource, "SaveGlobalOrderDays", string.Empty, message, string.Empty, ex);
                throw new Exception(message, ex);
            }
        }

        //Extra function_1 converting top 3 diseaseID into Disease Name
        public static Models.Disease.Precaution GetprecautionsAndDiseaseInfo(int diseaseID)
        {
            using (SqlConnection cnn = new SqlConnection(DataLibrary.DataAccess.SQLDataAccess.GetConnectionString()))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "prgetFinal_disease_Name";
                    cmd.Parameters.Add("@DiseaseID", SqlDbType.Int, 0).Value = diseaseID;
                    cnn.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                    Models.Disease.Precaution diseaseInfo = new Models.Disease.Precaution();
                    if (reader.Read())
                    {
                        diseaseInfo.Disease = reader.GetString(reader.GetOrdinal("Disease"));
                        diseaseInfo.precaution_1 = reader.GetString(reader.GetOrdinal("Precaution_1"));
                        diseaseInfo.precaution_2 = reader.GetString(reader.GetOrdinal("Precaution_2"));
                        diseaseInfo.precaution_3 = reader.GetString(reader.GetOrdinal("Precaution_3"));
                        diseaseInfo.precaution_4 = reader.GetString(reader.GetOrdinal("Precaution_4"));


                    }

                    return diseaseInfo;
                }
            }
        }
    }
}
