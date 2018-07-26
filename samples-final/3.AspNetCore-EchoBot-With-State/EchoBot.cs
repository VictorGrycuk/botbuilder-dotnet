// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading.Tasks;
using Microsoft.Bot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace AspNetCore_EchoBot_With_State
{
    public class EchoBot : IBot
    {
        private readonly IStatePropertyAccessor<EchoState> accessor;

        public EchoBot(IStatePropertyAccessor<EchoState> accessor)
        {
            this.accessor = accessor;
        }
        
        /// <summary>
        /// Every Conversation turn for our EchoBot will call this method. In here
        /// the bot checks the Activty type to verify it's a message, bumps the 
        /// turn conversation 'Turn' count, and then echoes the users typing
        /// back to them. 
        /// </summary>
        /// <param name="context">Turn scoped context containing all the data needed
        /// for processing this conversation turn. </param>        
        public async Task OnTurnAsync(ITurnContext context)
        {
            // This bot is only handling Messages
            if (context.Activity.Type == ActivityTypes.Message)
            {
                // Get the conversation state from the turn context
                var state = await context.GetConversationState<EchoState>(accessor);

                // Bump the turn count. 
                state.TurnCount++;

                // Echo back to the user whatever they typed.
                await context.SendActivityAsync($"Turn {state.TurnCount}: You sent '{context.Activity.Text}'");
            }
        }
    }


    /// <summary>
    /// Extension class for obtaining the ConversationState using the IStatePropertyAccessor.
    /// </summary>
    public static class ITurnContextExtensions
    {
        public static Task<T> GetConversationState<T>(this ITurnContext turnContext, IStatePropertyAccessor<T> accessor)
        {
            return accessor.GetAsync(turnContext);
        }
    }
}
