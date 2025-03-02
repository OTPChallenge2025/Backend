using BTCodeCraftersOTP.Controllers;
using FakeItEasy;
using Microsoft.Extensions.Logging;

namespace BTCodeCrafters.Tests
{
    public class OTPManagerControllerTests
    {
        //Generate OTP:
        [Fact]
        public void GenerateOTP_Generating_Correctly()
        {
            // Using the AAA testing method:
            // Arrange: Define preconditions; Inputs, Dependencies, etc... ;
            // Act:     Run specific operation;
            // Assert:  Check the results to see if they are what you expected;

            // Arrange:
            var lengthEncryptedTest = 24;
            var fakeLogger = A.Fake<ILogger<OTPManagerController>>();
            var controller = new OTPManagerController(fakeLogger);

            //Act:
            var encrypted = controller.GenerateOTP().Value;
            var lengthEncrypted = encrypted.ToString().Length;

            //Assert:
            //Test same number of digits:
            Assert.True(lengthEncryptedTest == lengthEncrypted);
            Assert.NotNull(lengthEncrypted);
        }

        //Login:
        [Fact]
        public void LoginWithOTP_CorrectInput()
        {
            //Arrange:
            // For checking: encryptedMessage = "ZVMFPMTFQWIW";
            var encryptedMessage = "OkPsfFVk7nzy7FTJnNd4tg ==";
            var fakeLogger = A.Fake<ILogger<OTPManagerController>>();
            var controller = new OTPManagerController(fakeLogger);

            //Write in file the decrypted value:
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter("../EncryptedOTP.txt");
                writer.WriteLine("ZVMFPMTFQWIW");
                //For checking: OkPsfFVk7nzy7FTJnNd4tg ==
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                writer.Close();
            }

            //Act:
            var result = controller.LoginWithOTP(encryptedMessage);

            //Assert:
            Assert.NotNull(result);
            Assert.True(result.Value.Equals("OTP is correct."));

            //Empty the file:
            writer = null;
            try
            {
                writer = new StreamWriter("../EncryptedOTP.txt");
                writer.WriteLine("");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                writer.Close();
            }
        }
    }
}