using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SendMailByExcel
{
    class Program
    {
        static void Main(string[] args)
        {
            DB db = new DB();
            DataTable dt = new DataTable();
            List<string> successList = new List<string>();
            List<string> errorList = new List<string>();

            dt = db.GetData();
            
            foreach (DataRow row in dt.Rows)
            {
                string mail = row["mail"].ToString();


                if (!successList.Contains(mail))
                {
                    if (Mail.SendMail(mail))
                    {
                        successList.Add(mail);
                        db.UpdateStatusSuccess(mail);
                    }
                    else
                    {
                        errorList.Add(mail);
                    }
                }
            }

            report(successList, errorList);
            
            Console.WriteLine("Press any key to stop the services");
            Console.ReadKey();
        }

        private static void report(List<string> successList, List<string> errorList)
        {
            Console.WriteLine($"成功筆數:{successList.Count}");
            foreach (var item in successList)
            {
                Console.WriteLine($"{item} 成功寄出通知");
            }

            Console.WriteLine();

            Console.WriteLine($"失敗筆數:{errorList.Count}");
            foreach (var item in errorList)
            {
                Console.WriteLine($"{item} 寄出失敗");
            }
        }
    }
}
