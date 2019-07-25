//CreditCardProcessing App

using System.Collections.Generic;
using Xunit;
using CreditCardProcessing;
using System;

namespace CreditCardProcessingUnitTests
{
    public class CreditCardProcessorUnitTests
    {
        [Fact]
        public void Validate_Parse_Strings()
        {
            // Arrange
            string testInput = "Add Tom 4111111111111111 $1000\r\nAdd Lisa 5454545454545454 $3000\r\nAdd Quincy 1234567890123456 $2000\r\nCharge Tom $500\r\nCharge Tom $800\r\nCharge Lisa $7\r\nCredit Lisa $100\r\nCredit Quincy $200";
            List<string> correctUserNamesList = new List<string>()
            {
                "Tom",
                "Lisa",
                "Quincy"
            };
            SortedDictionary<string, string> correctUserBalances = new SortedDictionary<string, string>()
            {
                {"Lisa", "$-93" },
                {"Quincy", "Error" },
                {"Tom", "$500" }
            };

            Dictionary<string, string> correctCreditCards = new Dictionary<string, string>()
            {
                {"Tom", "4111111111111111"},
                {"Lisa", "5454545454545454"},
                {"Quincy", "1234567890123456"}
            };

            Dictionary<string, int> correctCreditLimits = new Dictionary<string, int>()
            {
                { "Tom", 1000},
                { "Lisa", 3000},
                { "Quincy", 2000}
            };

            // Act
            CreditCardProcessor.ParseString(testInput);

            // Assert
            Assert.Equal(correctUserNamesList, CreditCardProcessor.userNames);
            Assert.Equal(correctUserBalances, CreditCardProcessor.userBalances);
            Assert.Equal(correctCreditCards, CreditCardProcessor.creditCards);
            Assert.Equal(correctCreditLimits, CreditCardProcessor.creditLimits);
        }

        [Fact]
        public void Invalid_Parse_Strings()
        {
            // Arrange
            string testInput = "Add Bob 4111111111111111 $1000\r\nAdd Steve 5454545454545454 $3000\r\nAdd Rob 1234567890123456 $2000\r\nCharge Bob $500\r\nCharge Steve $800\r\nCharge Rob $7\r\nCredit Lisa $100\r\nCredit Bob $200";
            List<string> incorrectUserNamesList = new List<string>();
            SortedDictionary<string, string> incorrectUserBalances = new SortedDictionary<string, string>();
            Dictionary<string, string> incorrectCreditCards = new Dictionary<string, string>();
            Dictionary<string, int> incorrectCreditLimits = new Dictionary<string, int>();

            // Act
            CreditCardProcessor.ParseString(testInput);

            // Assert
            Assert.NotEqual(incorrectUserNamesList, CreditCardProcessor.userNames);
            Assert.NotEqual(incorrectUserBalances, CreditCardProcessor.userBalances);
            Assert.NotEqual(incorrectCreditCards, CreditCardProcessor.creditCards);
            Assert.NotEqual(incorrectCreditLimits, CreditCardProcessor.creditLimits);
        }

        [Fact]
        public void String_Successfully_Converted_To_Int()
        {
            // Arrange
            string withDollarSign = "$1000";
            string withComma = "1,000";
            string withDollarSignAndComma = "$1,000";

            // Act
            int dollarSignConversion = CreditCardProcessor.CurrencyStringConvertToInt(withDollarSign);
            int commaConversion = CreditCardProcessor.CurrencyStringConvertToInt(withComma);
            int dollarSignAndCommaConversion = CreditCardProcessor.CurrencyStringConvertToInt(withDollarSignAndComma);

            // Assert
            Assert.Equal(1000, dollarSignConversion);
            Assert.Equal(1000, commaConversion);
            Assert.Equal(1000, dollarSignAndCommaConversion);
        }

        [Fact]
        public void String_Converted_To_Int_Throws_Exception()
        {
            // Arrange
            string withDollarSign = "!!@!@";

            // Act
            Exception ex = Assert.Throws<System.IO.IOException>(() => CreditCardProcessor.CurrencyStringConvertToInt(withDollarSign));

            // Assert
            Assert.Equal("The handle is invalid", ex.Message);
        }
    }
}
