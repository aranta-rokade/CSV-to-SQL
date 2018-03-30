using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace CSVReader
{
    public class ReadCSV
    {
        public static List<Customer> list = new List<Customer>();   

        public static void Read()
        {
            string filepth = ConfigurationManager.AppSettings.Get("FilePath");
            FileStream fs = new FileStream(filepth, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            string line;
            StringBuilder sb = new StringBuilder();
            line  = sr.ReadToEnd();

            String[] record = line.Split(new char[] { ';' });

            for (int i = 0; i < record.Length - 1; i++)
			{
                string[] values = record[i].Split(new char[] { ',' });
                Customer Cobj = new Customer() { CID = int.Parse(values[0]), CName = values[1], BillNo = int.Parse(values[2]), Email = values[3] };
                list.Add(Cobj);
			}
        }

        public static void DisplayConsole()
        {
            int j = 0;
            foreach (Customer item in list)
            {
                j++;
                Console.WriteLine("{0} {1} {2} {3}", item.CID, item.CName, item.BillNo, item.Email);
                if (j % 100 == 0)
                {
                    Console.WriteLine("Press Enter for more");
                    Console.ReadLine();
                }
            }

            Console.Read();
        }

    }
}
