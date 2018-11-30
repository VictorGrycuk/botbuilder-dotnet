this is a readme

add this:

<Target Name="ValidateBotForPublish" BeforeTargets="PrepareForPublish">
    <BotValidationTask 
        ForbidSpacesInProjectName="true" 
        RequireBotFile="true" 
        RequireEndpoints="production" 
        ForbidEndpoints="development" 
        RequireLuisKey="true" 
        RequireQnAMakerKey="true"/>
</Target>
