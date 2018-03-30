using System;
using System.Configuration;
using System.IO;
using CSVReader;

namespace CSVGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generate a demo CSV file
            GenerateCSV();
            // View the CSV file generated above
            RetrieveCSV();
        }

        static void GenerateCSV()
        {
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

            try
            {
                string filepth = ConfigurationManager.AppSettings.Get("FilePath");
                FileStream fs = new FileStream(filepth, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);

                string record = "";

                for (int i = 0; i < 1000; i++)
                {
                    record += String.Format("{0},Name{1},{2},name{3}@email.com;", i, i, i, i);

                }

                watch.Start();
                sw.Write(record);
                sw.Flush();
                watch.Stop();
                sw.Close();

                System.Diagnostics.Debug.WriteLine("Done within {0}", watch.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex.Message);
            }
        
        }

        static void RetrieveCSV()
        {
             ReadCSV.Read();
             ReadCSV.DisplayConsole();
        }
    }
}