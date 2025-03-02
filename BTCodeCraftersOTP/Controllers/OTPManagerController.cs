using Microsoft.AspNetCore.Mvc;
using RandomString4Net;

namespace BTCodeCraftersOTP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OTPManagerController : ControllerBase
    {
        private readonly ILogger<OTPManagerController> _logger;

        private string OTP;
        private string savedOTP;
        private string secretKey;

        public OTPManagerController(ILogger<OTPManagerController> logger)
        {
            _logger = logger;
            OTP = "";
            secretKey = "rktlqtuixakparuo";
        }

        // Generate OTP:
        [HttpGet]
        [Route("generateOTP")]
        public JsonResult GenerateOTP()
        {
            _logger.LogInformation("GenerateOTP was called.");

            //Example: OTP = "DJASKFLHKAJD";
            OTP = RandomString.GetString(Types.ALPHABET_UPPERCASE, 12);
            savedOTP = OTP;

            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter("../EncryptedOTP.txt");
                writer.WriteLine(OTP);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                writer.Close();
            }

            Console.WriteLine("Decrypted as: " + OTP + " ;");

            // Encrypted OTP: 24 characters:
            // abcdefghijklmnop 16 characters lower letters;
            var encrypted = EncryptAndDecryptPassword.Encrypt(OTP, secretKey);
            Console.WriteLine("Encrypted as: " + encrypted + " ;");

            return new JsonResult(encrypted);
        }

        // Log in:
        [HttpPost]
        [Route("loginWithOTP")]
        public JsonResult LoginWithOTP([FromBody] string encryptedOTP)
        {
            _logger.LogInformation("LoginWithOTP was called.");
            Console.WriteLine("New encrypted as: " + encryptedOTP + " ;");

            //Get the encrypted OTP and decrypt it:
            var newDecrypted = EncryptAndDecryptPassword.Decrypt(encryptedOTP, secretKey);
            Console.WriteLine("New decrypted as: " + newDecrypted + " ;");

            string dataFromFile = "";
            StreamReader reader = null;
            try
            {
                reader = new StreamReader("../EncryptedOTP.txt");

                dataFromFile = reader.ReadLine();
                savedOTP = dataFromFile;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                reader.Close();
            }
            Console.WriteLine("Old decrypted as: " + savedOTP + " ;");

            // Check if they are the same:
            if (newDecrypted.Equals(savedOTP) == true && newDecrypted.Equals("") == false)
            {
                // Login if they are the same:
                OTP = "";
                savedOTP = "";

                StreamWriter writer = null;
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

                return new JsonResult("OTP is correct.");
            }
            else
            {
                return new JsonResult("OTP is incorrect.");
            }
        }

        [HttpPost]
        [Route("GetCurrentTime")]
        public JsonResult GetCurrentTime([FromBody] int time)
        {
            ResetAfterTime.setCurrentTime(time);

            return new JsonResult("Received time.");
        }
    }
}