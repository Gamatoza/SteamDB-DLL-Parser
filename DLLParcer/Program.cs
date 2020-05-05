using DLLParcer.Core;
using DLLParcer.Core.SteamDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DLLParcer {
    class Program {

        static void Main(string[] args)
        {
            List<string> ListTitles = new List<string>();
            Dictionary<string, string> DLC = new Dictionary<string, string>();
            ParserWorker<string[]> parser;
            parser = new ParserWorker<string[]>(
                    new HabraParser()
                );

            parser.OnCompleted += (arg)=> {
                Console.WriteLine("All works done!");
                foreach (var item in ListTitles)
                {

                    List<string> arr = new List<string>(item.Split('\n'));
                    arr = arr.Distinct().ToList();
                    DLC.Add(arr[1], arr[2]);
                }
                
                Console.WriteLine("Done. Enter to create the ini file and exit");

            };
            parser.OnNewData += (arg1, arg2) => {
                ListTitles.AddRange(arg2);
            };
            Console.WriteLine("Enter the game id");
            string id = Console.ReadLine();
            parser.Settings = new HabraSettings(id);
            parser.Start();
            //parser.Abort();
            Thread.Sleep(600);
            Console.ReadLine();

            string str = "";
            str += "[steam]\n";
            str += "appid = " + id + "\n";
            str += "unlockall = false\n";
            str += "orgapi = steam_api_o.dll\n";
            str += "orgapi64 = steam_api64_o.dll\n";
            str += "extraprotection = false\n";
            str += "forceoffline = false\n";
            str += "\n";
            str += "[steam_misc]\n";
            str += "disableuserinterface = false\n";
            str += "\n";
            str += "[dlc]\n";
            foreach (var item in DLC)
            {
                str += $"{item.Key} = {item.Value}\n";
            }
            using (FileStream fstream = new FileStream($"cream_api.ini", FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(str);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
            }
        }
    }
}
