using System;
using Microsoft.Bot.PublishValidation.BotHelper;
using Microsoft.Build.Framework;
using TaskBuilder.Helpers;

namespace Microsoft.Bot.PublishValidation
{
    public class BotConfigCheckerTask : Microsoft.Build.Utilities.Task
    {
        public string ProjectPath { get; set; }

        public string ForbidSpacesInProjectName { get; set; }
        public string RequireBotFile { get; set; }
        public string RequireEndpoints { get; set; }
        public string ForbidEndpoints { get; set; }
        public string RequireLuisKey { get; set; }
        public string RequireQnAMakerKey { get; set; }

        public override bool Execute()
        {
            LoggerHelper logHelper = new LoggerHelper(Log.LogError, Log.LogWarning);
            ConfigurationOptions configurationOptions =
                new ConfigurationOptions(ForbidSpacesInProjectName, RequireBotFile, RequireEndpoints,
                ForbidEndpoints, RequireLuisKey, RequireQnAMakerKey);

            string errorMsg = string.Empty;
            try
            {
                Log.LogMessage(MessageImportance.High, string.Format("Project Path ===> {0}", ProjectPath));
                Log.LogMessage(MessageImportance.High, "Starting to read Bot's file...");

                if (!BotValidatorHelper.BotFileIsValid(ProjectPath, configurationOptions, out errorMsg))
                {
                    Log.LogMessage(MessageImportance.High, string.Format("Process found these errors ===> {0}", errorMsg));
                }
            }
            catch (Exception ex)
            {
                Log.LogWarning("Error handled");
                Log.LogErrorFromException(ex);
                return false;
            }

            return true;
        }
    }
}
