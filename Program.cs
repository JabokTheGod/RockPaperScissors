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
            string PlayerLogFilePath = "player_log.csv";

            Console.WriteLine("Welcome to Rock, Paper, Scissors!\n\n1. Start New Game\n2. Load Game\n3. Quit\n\nEnter choice: ");
            string MenuChoice = Console.ReadLine();
            int MenuChoiceInt = Int32.Parse(MenuChoice);
            
            int userWin = 0;
            int compWin = 0;
            int Playtie = 0;
            bool loop1 = true;
            bool loop2 = true;

            if (MenuChoiceInt == 1)
            {

                Console.WriteLine("What is your name?");
                string playerName = Console.ReadLine();
                Console.WriteLine("Hello " + playerName + ". Let's play!\n");
                int roundNum = 1;
                while(loop1 == true)
                { 
                    Console.WriteLine("Round " + roundNum + "\n\n1. Rock\n2. Paper\n3. Scissors\n\nWhat will it be?");
                    int[] Choices = new int[3]{1,2,3};
                    
                    string userPick = Console.ReadLine();
                    int userPickInt = Int32.Parse(userPick);
                    //Console.WriteLine("You chose " + userPick + ". The computer chose " + Comp + ". You " + WL + "!");
                  
                    //computer random pick
                    Random Comp = new Random();
                    int abc = Comp.Next(0, 3);
                    if (userPickInt == 1 && Choices[abc] == 3)
                    {
                        Console.WriteLine("You chose Rock. The computer chose Scissors. You Win!");
                        userWin += 1;
                    }
                    else if (userPickInt == 1 && Choices[abc] == 2)
                    {
                        Console.WriteLine("You chose Rock. The computer chose Paper. You Lose!");
                        compWin += 1;
                    }
                    else if (userPickInt == 2 && Choices[abc] == 1)
                    {
                        Console.WriteLine("You chose Paper. The computer chose Rock. You Win!");
                        userWin += 1;
                    }
                    else if (userPickInt == 2 && Choices[abc] == 3)
                    {
                        Console.WriteLine("You chose Paper. The computer chose Scissors. You Lose!");
                        compWin += 1;
                    }
                    else if (userPickInt == 3 && Choices[abc] == 2)
                    {
                        Console.WriteLine("You chose Scissors. The computer chose Paper. You Win!");
                        userWin += 1;
                    }
                    else if (userPickInt == 3 && Choices[abc] == 1)
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
                    while (loop2 == true)
                    {
                        Console.WriteLine("What would you like to do?\n\n1. Play Again\n2. View Player Statistics\n3. View Leaderboard\n4. Quit\n\nEnter choice: ");
                        string UP = Console.ReadLine();
                        int UPint = Int32.Parse(UP);
                        if (UPint == 1)
                        {
                            loop1 = true;
                            continue;
                        }
                        else if (UPint == 2)
                        {
                            Console.WriteLine(playerName + ", here are your game play statistics…\n");
                            Console.WriteLine("Wins: " + userWin);
                            Console.WriteLine("Losses: " + compWin);
                            Console.WriteLine("Ties: " + Playtie);
                            decimal ratio = (userWin / compWin);
                            Console.WriteLine("\nWin/Loss Ratio: " + ratio);
                            //loop1 = false;
                            loop2 = true;
                            continue;
                        }
                        else if(UPint == 3)
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
                            // using(StreamWriter writer = new StreamWriter(reportFile)) 
                            // {
                            //     writer.WriteLine(report);
                            // }
                            
                            Console.WriteLine("Global Game Stats");
                            Console.WriteLine("----------------------");
                            Console.WriteLine("----------------------");
                            var TopTenWins = (from player in playerDataList orderby player.Win select player).Take(10);
                            var TopGamePlays = (from player in playerDataList orderby player.TotalMatches descending select player).Take(5);
                            //var TotalGames = from player in playerDataList where ;
                            //var TotalGames = (from player in playerDataList orderby player.TotalMatches descending select player).Take(5);

                            //Console.WriteLine($"{TopTenWins}");
                            //Console.WriteLine($"{ TopGamePlays }");
                            Console.WriteLine("Top 10 Winning Players");
                            Console.WriteLine("----------------------");
                            foreach(var HighScore in TopTenWins)
                            {
                                Console.WriteLine($"{ HighScore.Name }: { HighScore.Win } wins");
                            }
                            Console.WriteLine("Most Played Games");
                            Console.WriteLine("----------------------");
                            foreach(var HighPlays in TopGamePlays)
                            {
                                Console.WriteLine($"{ HighPlays.Name }: { HighPlays.TotalMatches } games played");
                            }
                            Console.WriteLine("----------------------");
                            Console.WriteLine("Win/Loss Ratio: ");
                            Console.WriteLine("----------------------\n");
                            Console.WriteLine("----------------------");
                            Console.WriteLine("Total Games Played: ");
                            Console.WriteLine("----------------------");

                            //loop1 = false;
                            loop2 = true;
                            
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
            else if (MenuChoiceInt == 2)
            {
                Console.WriteLine("What is your name?");
                string playerName = Console.ReadLine();
                if (GetExisitngPlayer(playerDataList, playerName) != null)
                {
                    Console.WriteLine("Welcome back " + playerName + ". Let's play!");
                    loop1 = true;
                }
                else
                {
                    Console.WriteLine("Player does not exist.");
                    return;
                }
                
                
                // Needs code for if name cant be found
                //Console.WriteLine(playerName + ", your game could not be found.");
            }
            else
            {
                return;
            }

        }
        static PlayerData GetExisitngPlayer(List<PlayerData> playerDataList, string playerName)
        {
            foreach (PlayerData player in playerDataList)
            {
                if (player.Name.ToLower() == playerName.ToLower())
                {
                    return player;
                }
            }
            return null;
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
