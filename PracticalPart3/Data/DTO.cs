using PracticalPart3.Models;
using System.Globalization;

namespace PracticalPart3.Data
{
    public class DTO : List<DataCenter>
    {

        public static DTO DataCenterList = new();

        /// <summary>
        /// Method that loads data from excel file and adds it to the DTO list
        /// </summary>
        public DTO LoadData()
        {
            DataCenterList.Clear();

            try
            {
                string filePath = @"C:\\Users\\Jadan\\source\\repos\\PracticalPart3\\PracticalPart3\\DataCentreAvailability.csv";
                using (StreamReader sr = new StreamReader(filePath))
                {
                    
                    string line;
                    int count = 0;
                    while ((line = sr.ReadLine()) != null && count <= 100)
                    {
                        if (count == 0)
                        {
                            count++;
                            continue;
                        }
                        else
                        {
                            DataCenter data = new DataCenter();

                            string[] record = line.Split(",");
                            data.Id = int.Parse(record[0]);
                            data.FiscalYear = record[1];
                            data.FiscalPeriod = record[2];
                            data.Month = record[3];
                            data.InformationDate = record[4];
                            data.Branch = record[5];
                            data.Service = record[6];
                            data.SscClient = record[7];
                            data.MetricName = record[8];
                            data.Value = double.Parse(record[9]);
                            data.MetricType = record[10];

                            DataCenterList.Add(data);
                        }

                        count++;
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return DataCenterList;
        }
        
    }
}
