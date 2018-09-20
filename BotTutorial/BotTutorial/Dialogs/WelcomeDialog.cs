using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace BotTutorial.Dialogs
{
    [Serializable]
    public class WelcomeDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(PerformActionAsync);
            return Task.CompletedTask;
        }

        private async Task PerformActionAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            if (activity.Text.Equals("Hello"))
            {
                await context.PostAsync("Welcome to the Bot Application");
            }
            else if (activity.Text.Equals("How are you?"))
            {
                await context.PostAsync("I'm fine, thanks");
            }
            else
            {
                await context.PostAsync("Sorry, I can't understand you.");
            }
        }
    }
}