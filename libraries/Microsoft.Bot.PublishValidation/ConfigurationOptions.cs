using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Bot.PublishValidation
{
    public class ConfigurationOptions
    {
        public bool ForbidSpacesInProjectName { get; set; }
        public bool RequireBotFile { get; set; }
        public string RequireEndpoints { get; set; }
        public string ForbidEndpoints { get; set; }
        public bool RequireLuisKey { get; set; }
        public bool RequireQnAMakerKey { get; set; }

        public ConfigurationOptions(string ForbidSpacesInProjectName, string RequireBotFile,
            string RequireEndpoints, string ForbidEndpoints,
            string RequireLuisKey, string RequireQnAMakerKey)
        {
            this.ForbidSpacesInProjectName = ParseConfigOption(ForbidSpacesInProjectName);
            this.RequireBotFile = ParseConfigOption(RequireBotFile);
            this.RequireLuisKey = ParseConfigOption(RequireLuisKey);
            this.RequireQnAMakerKey = ParseConfigOption(RequireQnAMakerKey);

            this.ForbidEndpoints = ForbidEndpoints;
            this.RequireEndpoints = RequireEndpoints;
        }

        private bool ParseConfigOption(string configOption)
        {
            bool result = false;

            if (bool.TryParse(configOption, out result))
                return result;

            return false;
        }
    }
}
