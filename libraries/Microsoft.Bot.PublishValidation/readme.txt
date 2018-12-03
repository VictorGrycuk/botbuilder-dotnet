-- Microsoft.Bot.PublishValidation --

This is a tool for validating a bot configuration prior to publishing it. By default, the validation checks the following:
- Existence of a '.bot' file
- Internal structure of the configuration file:
    - No spaces in Project's name
    - Production endpoint existence
    - No Development endpoint exists

You can also set to check the following:
- Existence of LUIS key
- Existence of QnA Maker key

In order to add this extra validations, you can create your own Target in your '.csproj' file by adding the following code:

------------------------------------------------------------------------------
<Target Name="ValidateBotForPublish" BeforeTargets="OnlyPublish">
    <BotConfigCheckerTask
        ProjectPath="$(MSBuildProjectDirectory)"
        ForbidSpacesInProjectName="true"
        RequireBotFile="true"
        RequireEndpoints="production"
        ForbidEndpoints="development"
        RequireLuisKey="true"
        RequireQnAMakerKey="true"/>
</Target>
------------------------------------------------------------------------------

IMPORTANT! This target won't prevent the default validation to run. For achieving that you should also add the following property:

------------------------------------------------------------------------------
<PropertyGroup>
    <AvoidDefaultPublishValidation>true</AvoidDefaultPublishValidation>
</PropertyGroup>
------------------------------------------------------------------------------

The default Target inside this Task has a condition, which checks the existence of this tag 'AvoidDefaultPublishValidation', and if it's set to true, the default validation explained before won't run.