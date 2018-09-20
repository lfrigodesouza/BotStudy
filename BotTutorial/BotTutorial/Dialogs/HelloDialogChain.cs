using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.Bot.Builder.Dialogs;

namespace BotTutorial.Dialogs
{
    public class HelloDialogChain
    {
        public static readonly IDialog<string> dialog = Chain.PostToChain()
            .Select(x => x.Text)
            .Switch(
            Chain.Case(
                new Regex("^Hello", RegexOptions.IgnoreCase),
                (context, text) => Chain.Return("Welcome to Bot Application").PostToUser()
                ), Chain.Case(
                new Regex("How are you", RegexOptions.IgnoreCase),
                (context, text) => Chain.Return("I'm fine, thanks").PostToUser()
                ), Chain.Default<string, IDialog<string>>(
                (context, text) => Chain.Return("I can't understand you").PostToUser()
                )
            ).Unwrap();

    }
}