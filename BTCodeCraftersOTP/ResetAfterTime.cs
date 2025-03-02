namespace BTCodeCraftersOTP
{
    public class ResetAfterTime : BackgroundService
    {
        private ILogger<ResetAfterTime> _logger;

        public ResetAfterTime(ILogger<ResetAfterTime> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string dataFromFile = "";

            StreamWriter writer1 = null;
            try
            {
                writer1 = new StreamWriter("../EncryptedOTP.txt");
                writer1.WriteLine("");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                writer1.Close();
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                if (dataFromFile.Equals("") != true)
                {
                    Console.WriteLine("OTP: " + dataFromFile + " ;");

                    // How much waiting time for the OTP:
                    var howMuchDelay = 30;
                    for (int i = 0; i < howMuchDelay; i++)
                    {
                        Console.WriteLine("Seconds left: " + (30 - i));
                        await Task.Delay(1 * 1000, stoppingToken);
                    }

                    //Empty the OTP:
                    StreamWriter writer2 = null;
                    try
                    {
                        writer2 = new StreamWriter("../EncryptedOTP.txt");
                        writer2.WriteLine("");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        writer2.Close();
                    }

                    // Reset data:
                    dataFromFile = "";
                }
                else
                {
                    StreamReader reader = null;
                    try
                    {
                        reader = new StreamReader("../EncryptedOTP.txt");

                        dataFromFile = reader.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        reader.Close();
                    }
                    Console.WriteLine("File data is empty.");

                    var howMuchDelay = 1;
                    await Task.Delay(howMuchDelay * 1000, stoppingToken);
                }
            }
        }
    }
}