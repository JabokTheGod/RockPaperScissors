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
    public class Program
    {
        private static List<PlayerData> playerDataList = new List<PlayerData>();
        static void Main(string[] args)
        {
            //string MenuChoice = "";
            //Prints Menu Choices
            string PlayerLogFilePath = string.Empty;

            Console.WriteLine("Welcome to Rock, Paper, Scissors!\n\n1. Start New Game\n2. Load Game\n3. Quit\n\nEnter choice: ");
            string MenuChoice = Console.ReadLine();
            if (MenuChoice == "Start New Game")
            {
                int userWin = 0;
                int compWin = 0;
                int Playtie = 0;
                bool loop = true;

                Console.WriteLine("What is your name?");
                string playerName = Console.ReadLine();
                Console.WriteLine("Hello " + playerName + ". Let's play!\n");
                int roundNum = 1;
                while(loop == true)
                { 
                    
                    Console.WriteLine("Round " + roundNum + "\n\n1. Rock\n2. Paper\n3. Scissors\n\nWhat will it be?");
                    string[] Choices = new string[3]{"Rock","Paper","Scissors"};
                    
                    string userPick = Console.ReadLine();
                    //Console.WriteLine("You chose " + userPick + ". The computer chose " + Comp + ". You " + WL + "!");
                
                  
                    //computer random pick
                    Random Comp = new Random();
                    int abc = Comp.Next(0, 3);
                    if (userPick == "Rock" && Choices[abc] == "Scissors")
                    {
                        Console.WriteLine("You chose Rock. The computer chose Scissors. You Win!");
                        userWin += 1;
                    }
                    else if (userPick == "Rock" && Choices[abc] == "Paper")
                    {
                        Console.WriteLine("You chose Rock. The computer chose Paper. You Lose!");
                        compWin += 1;
                    }
                    else if (userPick == "Paper" && Choices[abc] == "Rock")
                    {
                        Console.WriteLine("You chose Paper. The computer chose Rock. You Win!");
                        userWin += 1;
                    }
                    else if (userPick == "Paper" && Choices[abc] == "Scissors")
                    {
                        Console.WriteLine("You chose Paper. The computer chose Scissors. You Lose!");
                        compWin += 1;
                    }
                    else if (userPick == "Scissors" && Choices[abc] == "Paper")
                    {
                        Console.WriteLine("You chose Scissors. The computer chose Paper. You Win!");
                        userWin += 1;
                    }
                    else if (userPick == "Scissors" && Choices[abc] == "Rock")
                    {
                        Console.WriteLine("You chose Scissors. The computer chose Rock. You Lose!");
                        compWin += 1;
                    }
                    else
                    {
                        Console.WriteLine("Chose the same.");
                        Playtie += 1;
                        roundNum += 1;
                        continue;
                    }
                    roundNum += 1;
                    Console.WriteLine("What would you like to do?\n\n1. Play Again\n2. View Player Statistics\n3. View Leaderboard\n4. Quit\n\nEnter choice: ");
                    string UP = Console.ReadLine();
                    if (UP == "Play Again")
                    {
                        loop = true;
                        continue;
                    }
                    else if (UP == "View Player Statistics")
                    {
                        Console.WriteLine(playerName + ", here are your game play statistics…\n");
                        Console.WriteLine("Wins: " + userWin);
                        Console.WriteLine("Losses: " + compWin);
                        Console.WriteLine("Ties: " + Playtie);
                        int ratio = (userWin / compWin);
                        Console.WriteLine("\nWin/Loss Ratio: " + ratio);
                        continue;
                    }
                    else if(UP == "View Leaderboard")
                    {
                        if (File.Exists(PlayerLogFilePath))
                        {
                            if(ReadPlayerData(PlayerLogFilePath))
                            {
                                //exception for creating file
                                try
                                {
                                    // Open the text file using a stream reader.
                                    using (var reader = new StreamReader("player_log.csv"))
                                    {
                                        var line = reader.ReadLine();
                                        var values = line.Split(',');
                                    }
                                }
                                catch (IOException e)
                                {
                                    Console.WriteLine("The file could not be read:");
                                    Console.WriteLine(e.Message);
                                } 
                            }
                            else
                            {
                                Console.WriteLine($"Player Log does not exist at path: " , PlayerLogFilePath);
                            }
                            
                            
                        }
                        Console.WriteLine("Top 10 Winning Players");
                        //var TopTen = from player in playerDataList orderby player.Name select new() ; //working on
                        //where song.Name.ToCharArray().Length > 85 orderby song.Year descending select song;
                    }
                    else
                    {
                        return;
                    }
                }
    
            }
            else if (MenuChoice == "Load Game")
            {
                Console.WriteLine("What is your name?");
                string playerName = Console.ReadLine();
                /*
                if (playerName == playerDataList[Name])
                {

                }
                */
                Console.WriteLine("Welcome back " + playerName + ". Let's play!");
                // Needs code for if name cant be found
                //Console.WriteLine(playerName + ", your game could not be found.");
            }
            else
            {
                return;
            }

        }
    

        static bool ReadPlayerData(string filePath)
        {
            Console.WriteLine($"Reading data from: " , filePath);

            try 
            {
                int NumItemsInRow = 0;
                string[] lineNumbers = File.ReadAllLines(filePath);
                for (int i = 0; i < lineNumbers.Length; i++)
                {
                    string lineNumber = lineNumbers[i];
                    string[] value = lineNumber.Split('\t');
                    
                    if (i == 0)
                    {
                        NumItemsInRow = value.Length;
                    }
                    else
                    {
                        if (NumItemsInRow != value.Length)
                        {
                            Console.WriteLine($"Row " , lineNumber , " contains " , value.Length , " values. It should contain " , NumItemsInRow , ".");
                            return false;
                        }
                        else 
                        {
                            try
                            {   
                                PlayerData playerData = new PlayerData();
                                playerData.Name = value[0];
                                playerData.Win = Int32.Parse(value[1]);
                                playerData.Loss = Int32.Parse(value[2]);
                                playerData.Tie = Int32.Parse(value[3]);
                                playerDataList.Add(playerData);
                            }
                            catch (InvalidCastException)
                            {
                                Console.WriteLine($"Row " , i , " contains invalid value.");
                                return false;
                            }
                        }
                    }
                }
                Console.WriteLine($"Data reading success.");
                return true;
            } 
            catch(Exception) 
            {
                Console.WriteLine("Error in reading data from csv file.");
                throw;
            } 
        }
    }
}
