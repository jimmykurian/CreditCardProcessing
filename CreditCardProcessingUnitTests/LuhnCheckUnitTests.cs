//CreditCardProcessing App

using CreditCardProcessing;
using Xunit;

namespace CreditCardProcessingUnitTests
{
    public class LuhnCheckUnitTests
    {
        [Fact]
        public void Validate_Check_Digits()
        {
            // Act
            Assert.True(LuhnCheck.CheckSum("0"));
            Assert.False(LuhnCheck.CheckSum("1234567890123456"));
            Assert.True(LuhnCheck.CheckSum("5454545454545454"));
            Assert.True(LuhnCheck.CheckSum("4111111111111111"));
            Assert.False(LuhnCheck.CheckSum("TEST"));
        }

    }
}
