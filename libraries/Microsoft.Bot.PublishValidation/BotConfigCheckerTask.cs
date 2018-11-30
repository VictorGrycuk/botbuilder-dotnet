namespace Microsoft.Bot.PublishValidation
{
    using Microsoft.Bot.Configuration;
    using TaskBuilder.Helpers;

    public class BotConfigCheckerTask : Build.Utilities.Task, Build.Framework.ITask
    {
        public string ProjectPath { get; set; }

        public override bool Execute()
        {
            var logHelper = new LoggerHelper(Log.LogError, Log.LogWarning);

            var bot = BotConfiguration.LoadFromFolder(ProjectPath);

            // Validate if the Project directory path is valid
            if (!DirectoryValidatorHelper.DirectoryIsValid(ProjectPath, out var resultMsg, out var logType))
            {
                logHelper.Log(resultMsg, logType);
                return false;
            }

            // Validate if there is any .bot file inside the Project Directory 
            if (!DirectoryValidatorHelper.FileExists(ProjectPath, "*.bot", out resultMsg, out logType))
            {
                logHelper.Log(resultMsg, logType);
                return false;
            }

            return true;
        }
    }
}
