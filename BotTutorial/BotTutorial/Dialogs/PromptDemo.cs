using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace BotTutorial.Dialogs
{
    [Serializable]
    public class PromptDemo : IDialog<object>
    {
        public string name { get; set; }
        public long age { get; set; }
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Thanks for using the bot. " +
                "Please inform some data:");
            context.Wait(GetNameAsync);
        }

        private Task GetNameAsync(IDialogContext context, IAwaitable<IMessageActivity> activity)
        {
            PromptDialog.Text(
                context: context,
                resume: ResumeGetName,
                prompt: "Please entrer your name",
                retry: "Sorry I didn't understand"
                );
            return Task.CompletedTask;
        }
        private async Task ResumeGetName(IDialogContext context, IAwaitable<string> result)
        {
            name = await result;
            PromptDialog.Number(
                context: context,
                resume: ResumeGetAge,
                prompt: $"{name}, please entrer your Age",
                retry: "Sorry I didn't understand",
                attempts: 3,
                min: 18,
                max: 50
                );
        }

        private async Task ResumeGetAge(IDialogContext context, IAwaitable<long> result)
        {
            age = await result;
            PromptDialog.Confirm(
                context: context,
                resume: ResumeConfirm,
                prompt: $"Your name is *{name}* and your age is *{age}*, right?",
                retry: "Sorry I didn't understand",
                options: new string[] {"Yeah", "Nah"},
                promptStyle: PromptStyle.PerLine

                );
        }

        private async Task ResumeConfirm(IDialogContext context, IAwaitable<bool> result)
        {
           if(await result)
            {
                await context.PostAsync($"**{name}**, You are registered!");
            }
            else
            {
                await context.PostAsync("tá de brinks?");
                context.Done(string.Empty);
            }
        }

    }
}