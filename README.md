# **Credit Card Processing App**

*Language used: C# .NET Core 2.1 IDE Used: Visual Studio 2019 Enterprise*

## How to run application:

1.  Navigate to: *CreditCardProcessing\CreditCardProcessing\bin\Release\netcoreapp2.1\win10-x64*
    
2.  In Command Prompt or Terminal you can run either commands: 
> *Command Line Args:* `CreditCardProcessing.exe test.txt`
> *STDIN:* `CreditCardProcessing.exe. < test.txt`

I provide a text file named *test.txt* in that directory like the one in the directions that will display the requested output when executed with CreditCardProcessing.exe 

## Re-Releasing EXE and Running Tests in Visual Studio and Command Line:

You can obviously open this project in Visual Studio and run both application and unit tests in the IDE via the Visual Studio Test Explorer.
  
If you have .NET Core installed locally you can re-release the executable via command line when you are in the following directory: *CreditCardProcessing\CreditCardProcessing*

then run the following command: `dotnet publish -c Release -r win10-x64`

It will re-release the exe into to the *bin\Release\netcoreapp2.1\win10-x64.*

We can run tests via the command line by navigating to the test project in the: *CreditCardProcessing\CreditCardProcessingUnitTests*

and then we can run the following command: `dotnet test`

## Program Design Thoughts

Why I chose C# and .NET Core First major design reason that I decided on was to create this application in C# and .NET Core 2.1. I decided on C# because it was the language that I had the most experience in currently. I also knew from looking at the requirements that a lot of string manipulation would most likely be invovled so I knew that C# had a lot of built-in/out-of-the-box tools to assist with that.

I chose .NET Core over .NET Framework because if the application needs to run across multiple platforms such as Windows, Linux and macOS, .NET Core will allow to. Also from what I have read the MacOS version of Visual Studio and Visual Studio Code can now run .NET Core applications.

## Intial Design Steps

First design steps were to read the requirements and do a quick diagram sketch on a piece of paper to see how many difference components I needed to implement. Next step was the mock out some basic tests for some of methods that 100% I would need such as CurrencyConversionToInt and LuhnCheck.

I decided to use a SortedDictionary to story UserBalances to be able to preserve the alphabetical ordering of required for the display of the accounts later.

I also decided to have my LuhnCheck take in a string of numbers instead of BIGINT. I thought this would be more benenficial as if the LuhnCheck failed I could later story the balance as the string "Error" which out simplify my output later.
