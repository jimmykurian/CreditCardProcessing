//CreditCardProcessing App

using System;
using System.Linq;

namespace CreditCardProcessing
{
    public class LuhnCheck
    {
        //Luhn 10 Algorithim CheckSum, takes in a string of numbers
        public static bool CheckSum(string numbers)
        {
            return numbers.All(char.IsDigit) && numbers.Reverse()
                .Select(c => c - 48)
                .Select((thisNum, i) => i % 2 == 0
                    ? thisNum
                    : ((thisNum *= 2) > 9 ? thisNum - 9 : thisNum)
                ).Sum() % 10 == 0;
        }
    }
}
