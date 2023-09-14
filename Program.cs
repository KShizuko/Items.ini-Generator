using System;
using System.IO;

namespace ItemsINI_Batcher
{
    internal class Program
    {
        private static string GetQualityType(string ID)
        {
            switch ((uint.Parse(ID) % 10))
            {
                case 3:
                    return "NORMALV1";

                case 4:
                    return "NORMALV2";

                case 5:
                    return "NORMALV3";

                case 6:
                    return "REFINED";

                case 7:
                    return "UNIQUE";

                case 8:
                    return "ELITE";

                case 9:
                    return "SUPER";
            }
            return "FIXED";
        }

        private static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine("The output will be located at;\r\n" + currentDirectory + @"\itemtype.txt");
            Console.WriteLine("Press enter when you are ready to begin.");
            Console.ReadLine();
            if (File.Exists(currentDirectory + @"\itemtype.txt"))
            {
                TextReader TR = new StreamReader(currentDirectory + @"\itemtype.txt");
                string Items = TR.ReadToEnd();
                TR.Close();
                Items = Items.Replace("\r", "");
                string[] AllItems = Items.Split('\n');

                string ItemName;
                string Quality;
                string ID;
                int count = 0;
                StreamWriter Writer = new StreamWriter(new FileStream(currentDirectory + @"\Items.ini", FileMode.Create));
                Writer.WriteLine("[Items]");

                foreach (string _item in AllItems)
                {
                    string _item_ = _item.Trim();
                    if (_item_.Length >= 2)
                    {
                        if (_item_.IndexOf("//", 0, 2) != 0)
                        {
                            string[] data = _item_.Split(' ');
                            if (data.Length >= 37)
                            {
                                ID = data[0].Trim();
                                ItemName = data[1].Trim();
                                Quality = GetQualityType(ID);
                                Writer.WriteLine(ItemName.ToUpper() + Quality + "=" + ID);
                            }
                            count++;
                            Console.WriteLine("Written: {0} items to Items.ini", count);
                        }
                    }
                }
                Writer.Flush();
                Writer.Close();
                Console.WriteLine("Finished, press enter to exit.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("itemtype.txt not found");
                Console.ReadLine();
            }
        }
    }
}

