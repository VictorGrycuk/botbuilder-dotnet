// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Samples.Echo;
using Microsoft.Bot.Schema;

namespace Console_EchoBot_With_State
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the EchoBot. Type something to get started.");
            // Create the Conversation Sate
            var conversationState = new ConversationState(new MemoryStorage());
            // Create the property which will manage the ConversationState
            var accessor = conversationState.CreateProperty<EchoState>("ConversationState", () => new EchoState());
            // Create the Console Adapter, and add Conversation State 
            // to the Bot. The Conversation State will be stored in memory. 
            var adapter = new ConsoleAdapter()
                .Use(conversationState);

            // Create the instance of our Bot.
            var echoBot = new EchoBot(accessor);

            // Connect the Console Adapter to the Bot. 
            adapter.ProcessActivity(
                async (context) => await echoBot.OnTurnAsync(context)).Wait();
        }
    }

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
                var state = await context.GetConversationState(accessor);
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

    /// <summary>
    /// Class for storing conversation state. 
    /// </summary>
    public class EchoState
    {
        public int TurnCount { get; set; } = 0;
    }
}
