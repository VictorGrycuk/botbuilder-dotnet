using System;
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
            ConfigurationOptions configurationOptions =
                new ConfigurationOptions(ForbidSpacesInProjectName, RequireBotFile, RequireEndpoints,
                ForbidEndpoints, RequireLuisKey, RequireQnAMakerKey);

            string errorMsg = string.Empty;
            try
            {

                if (!BotValidatorHelper.BotFileIsValid(ProjectPath, configurationOptions, out errorMsg))
                {
                    Log.LogMessage(MessageImportance.High, $"Process found these errors ===> {errorMsg}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.LogErrorFromException(ex);
                return false;
            }

            return true;
        }
    }
}
