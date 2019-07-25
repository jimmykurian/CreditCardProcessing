//CreditCardProcessing App

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CreditCardProcessing
{
    public class CreditCardProcessor
    {
        /*
         I'm thinking about the benefits of maybe refactoring these public static variables to internal static methods to
         take advantage of some of the encapsulation benefts internal has. I could then modify the Test Project assembelies 
         with [InternalsVisibleTo] so that the Test files could utizlie these variables.
        */
        public static readonly List<string> userNames = new List<string>(); 
        //SortedDictionary doesn't have the best performance in .NET, but I can see that there is a 3rd Party package called C5.IntervalHeap that might provide better performance
        //while maintain Sort Order, would need to research/play aroud with because haven't used it before.
        public static readonly SortedDictionary<string, string> userBalances = new SortedDictionary<string, string>();
        public static readonly Dictionary<string, string> creditCards = new Dictionary<string, string>();
        public static readonly Dictionary<string, int> creditLimits = new Dictionary<string, int>();

        public static void Main(string[] args)
        {
            try
            {
                //Takes in Command Line args
                if (args.Any())
                {
                    using (StreamReader reader = new StreamReader(args[0]))
                    {
                        string line = reader.ReadToEnd();
                        ParseString(line);
                        foreach(KeyValuePair<string, string> keyValuePair in userBalances)
                        {
                          Console.WriteLine("{0}: {1}", keyValuePair.Key, keyValuePair.Value);
                        }
                        reader.Close();
                        Console.WriteLine("\nPress Any Key to Exit");
                        Console.ReadKey();
                    }
                }

                else
                {
                    //Takes in STDIN
                    string stdin = null;
                    if (Console.IsInputRedirected)
                    {
                        using (StreamReader reader = new StreamReader(Console.OpenStandardInput(), Console.InputEncoding))
                        {
                            stdin = reader.ReadToEnd();
                            ParseString(stdin);;
                            foreach (KeyValuePair<string, string> keyValuePair in userBalances)
                            {
                                Console.WriteLine("{0}: {1}", keyValuePair.Key, keyValuePair.Value);
                            }
                            reader.Close();
                            Console.WriteLine("\nPress Any Key to Exit");
                            Console.Read();
                        }
                    }
                }
            }

            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }

        //Method parses input string first on new line and or carriage return, then further parsing if line contains the following action/word "Add", "Charge", "Credit"
        public static void ParseString(string lineInput)
        {
            string[] lineInputArray = lineInput.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            string[] splitStrings;

            foreach (string lineInputItem in lineInputArray)
            {
                if (lineInputItem.Contains("Add") == true)
                {
                    splitStrings = lineInputItem.Split(' ');
                    CreateAccount(splitStrings);
                }

                if (lineInputItem.Contains("Charge") == true)
                {
                    splitStrings = lineInputItem.Split(' ');
                    if(userNames.Contains(splitStrings[1]))
                    {
                        ChargeAccount(splitStrings);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n{0}: Does not have an account.\n\nPress ENTER to return", splitStrings[1]);
                        Console.ReadLine();
                    }
                }

                if (lineInputItem.Contains("Credit") == true)
                {
                    splitStrings = lineInputItem.Split(' ');
                    if (userNames.Contains(splitStrings[1]))
                    {
                        CreditAccount(splitStrings);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("\n{0}: Does not have an account.\n\nPress ENTER to return", splitStrings[1]);
                        Console.ReadLine();
                    }
                }
            }
        }
        
        //Method to Create new credit card accounts
        public static void CreateAccount(string[] splitStrings)
        {
            userNames.Add(splitStrings[1]);
            if (LuhnCheck.CheckSum(splitStrings[2]) == true)
            {
                userBalances.Add(splitStrings[1], string.Format("${0}", 0.ToString()));
            }
            else
            {
                userBalances.Add(splitStrings[1], "Error");
            }

            creditCards.Add(splitStrings[1], splitStrings[2]);
            creditLimits.Add(splitStrings[1], CurrencyStringConvertToInt(splitStrings[3]));
        }

        //Method to Charge an exisiting account
        public static void ChargeAccount(string[] splitStrings)
        {
            if (LuhnCheck.CheckSum(creditCards.GetValueOrDefault(splitStrings[1])) == true)
            {
                int currentBalance = CurrencyStringConvertToInt(userBalances.GetValueOrDefault(splitStrings[1]));
                int creditLimit = creditLimits.GetValueOrDefault(splitStrings[1]);
                if ((currentBalance + CurrencyStringConvertToInt(splitStrings[2])) < creditLimit)
                {
                    userBalances[splitStrings[1]] = string.Format("${0}", (currentBalance + CurrencyStringConvertToInt(splitStrings[2])).ToString());
                }
            }
        }

        //Method to Credit an exisiting Account
        public static void CreditAccount(string[] splitStrings)
        {
            if (LuhnCheck.CheckSum(creditCards.GetValueOrDefault(splitStrings[1])) == true)
            {
                int currentBalance = CurrencyStringConvertToInt(userBalances.GetValueOrDefault(splitStrings[1]));
                userBalances[splitStrings[1]] = string.Format("${0}", (currentBalance - CurrencyStringConvertToInt(splitStrings[2])).ToString());
            }
        }

        //Method to convert Currency strings into ints, looks for $ signs and commas
        public static int CurrencyStringConvertToInt(string currencyString)
        {
            string valueWithoutDollarSign = currencyString.Replace("$", "");
            string valueWithoutComma = valueWithoutDollarSign.Replace(",", "");
            int result;

            if (int.TryParse(valueWithoutComma, out result))
            {
                return int.Parse(valueWithoutComma);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n{0}: Is not a number.\n\nPress ENTER to return", valueWithoutComma);
                Console.ReadLine();
                return result;
            }
                
        }
    }
}
