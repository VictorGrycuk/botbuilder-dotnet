namespace Microsoft.Bot.PublishValidation
{
    using System;
    using Microsoft.Bot.Configuration;

    class Program
    {
        // Return codes
        private const int ERROR = 2;
        private const int OK = 0;

        public static int Main(string[] args)
        {
            Console.WriteLine("Args: " + string.Join(", ", args));

            var projectPath = args[0];

            try
            {
                var bot = BotConfiguration.LoadFromFolder(projectPath);
                Console.WriteLine($"{bot.Name} - {bot.Description}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ERROR;
            }
            
            return OK;
        }
    }
}
