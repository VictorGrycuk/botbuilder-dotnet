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
            var requireEndpoints = args[1];
            var forbidEndpoints = args[2];
            var forbidSpacesInProjectName = args[3];
            var requireBotFile = args[4];
            var requireLuisKey = args[5];
            var requireQnAMakerKey = args[6];

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
