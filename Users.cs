using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RPS
{
    public class PlayerData
    {
        public string Name { get; set; }
        public int Win { get; set; }
        public int Loss { get; set; }
        public int Tie { get; set; }

        public override string ToString()
        {
            return String.Format("Name: {0}, Win: {1}, Loss: {2}, Tie: {3}", Name, Win, Loss, Tie);
        }

        // public User(string name, int win, int loss, int tie)
        // {
        //     Name = name;
        //     Win = win;
        //     Loss = loss;
        //     Tie = tie;
        // }
    }






    //   might have to be deleted
    /*
    public static class ReadPlayerData
    {
        public static List<User> PlayerData(string fileName) {
            List<User> output = new List<User>();
            using(StreamReader csvFile = new StreamReader("player_log.csv")) 
            {
                string line = csvFile.ReadLine();
                int lineNum = 1;
                while((line = csvFile.ReadLine()) != null) 
                {
                    lineNum++;
                    var PL = line.Split(",");
                    try {
                        User pd = new User(PL[0]);
                        pd.Name = PL[1];
                        pd.Win = Int32.Parse(PL[2]);
                        pd.Loss = Int32.Parse(PL[3]);
                        pd.Tie = Int32.Parse(PL[4]);
                        


                        output.Add(sd);
                    }
                    catch(ArgumentException ex){ 
                        Console.WriteLine($"Skipping line { lineNum }\n\tError loading { ex.ParamName } record: { ex.Message }");
                    }
                    catch(FormatException ex) {
                        Console.WriteLine($"Skipping line { lineNum }\n\tError loading Rating/Price record: { ex.Message }");
                    }
                    catch(Exception ex){
                        System.Console.WriteLine($"Unknown error on line { lineNum}\n\t{ ex.Message }\n\t{ ex.StackTrace }");
                    }

                }
            }

            return output;
        }
    }*/
    

}