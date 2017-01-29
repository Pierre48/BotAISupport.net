using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;

namespace AISupportBot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
#pragma warning disable CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        public async Task StartAsync(IDialogContext context)
#pragma warning restore CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        {
            context.Wait<IMessageActivity>(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            await context.PostAsync($"Bonjour. Quelle application ?");
            PromptDialog.Choice(context, ResumeAfter, new List<string> { "Zac", "Zb", "Giraf AVC", "OscarNG", "GirafAPC" },"Pour quelle application ?");

            //context.Wait<IMessageActivity>(MessageReceivedAsync);
        }

        private async Task ResumeAfter(IDialogContext context, IAwaitable<object> result)
        {
            var message = await result;
            await context.PostAsync($"Vous avez choisi l'application : {message}");

            if (string.Equals("Zac",message.ToString(),StringComparison.InvariantCultureIgnoreCase))
            {
                context.Call(new ZacRootDialog(), ResumeAfter);
            }

        }
    }
}