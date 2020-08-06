using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataLibrary.Logic.User
{
    public class symptoms
    {
        public static List<Models.User.user_symptoms> GetAllSymptoms()
        {
            return DataAccess.User.symptoms.GetAllSymptoms();
        }



        public static int StoreUserSymptoms(List<int> symptomsIDs, int patientUserID)
        {
            int symptomID = DataAccess.User.symptoms.StoreUserSymptoms(symptomsIDs, patientUserID);
            return symptomID;
        }

        public static List<Models.Disease.Precaution> helperPrecautions(int symptomID)
        {
            List<Models.User.user_symptoms> list = GetAllSymptoms();
            List<Models.Disease.Precaution> precautionsList = calcDisesase(symptomID, list);
            return precautionsList;
        }


        public static List<Models.Disease.Precaution> calcDisesase(int symptomID, List<Models.User.user_symptoms> list)
        {
           
            int patientID;
            int age;
            int weight;
            bool diabetic;
            bool HBP;
            bool smoker;
            bool alcoholic;
            DataSet ds;
            List<Models.Disease.Precaution> precautionsList = new List<Models.Disease.Precaution>();

            ds = DataAccess.User.symptoms.GetUserDataForDiseaseCalc(symptomID);
            Hashtable hashTable = DataAccess.User.symptoms.getDiseaseToSymptom(list);

            //needed for calculating weight couldnt get to that part
            DataRow dr = ds.Tables[0].Rows[0];
            patientID = Convert.ToInt32(dr["PatientID"]);
            age = Convert.ToInt32(dr["Age"]);
            weight = Convert.ToInt32(dr["Weight"]);
            diabetic = Convert.ToBoolean(dr["Diabetic"]);
            HBP = Convert.ToBoolean(dr["HBP"]);
            smoker = Convert.ToBoolean(dr["Smoke"]);
            alcoholic = Convert.ToBoolean(dr["Alcohol"]);

            List<int> final_List = new List<int>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                int selecetedSymptom = int.Parse(row["symptomID"].ToString());

                foreach (int item in ((List<int>)hashTable[selecetedSymptom]))
                {
                    final_List.Add(item);
                }
            }

            if(final_List.Count <= 0)
            {
                precautionsList.Add(new Models.Disease.Precaution { errorMessage = "No Results Found" });
                return precautionsList;
            }

            var most1 = -1;
            var most2 = -1;
            var most3 = -1;
            if (final_List.Count >= 3)
            {
                most1 = final_List.GroupBy(i => i).OrderByDescending(grp => grp.Count())
                            .Select(grp => grp.Key).First();
                final_List.RemoveAll(item => item == most1);
                most2 = final_List.GroupBy(i => i).OrderByDescending(grp => grp.Count())
                            .Select(grp => grp.Key).First();
                final_List.RemoveAll(item => item == most2);
                most3 = final_List.GroupBy(i => i).OrderByDescending(grp => grp.Count())
                            .Select(grp => grp.Key).First();
                final_List.RemoveAll(item => item == most3);
            }
            else
            {

                precautionsList.Add(new Models.Disease.Precaution { errorMessage = "Not Enough Information for Diagonosis" });
                return precautionsList;
            }
            precautionsList.Add(DataAccess.User.symptoms.GetprecautionsAndDiseaseInfo(most1));
            precautionsList.Add(DataAccess.User.symptoms.GetprecautionsAndDiseaseInfo(most2));
            precautionsList.Add(DataAccess.User.symptoms.GetprecautionsAndDiseaseInfo(most3));

            return precautionsList;
        }
    }
}
