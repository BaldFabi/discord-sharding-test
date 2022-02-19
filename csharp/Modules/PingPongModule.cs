using System;
using System.Threading.Tasks;
using System.IO;

using Discord.Commands;
using Discord;



namespace ShardingBot.Modules
{
    public class PingPongModule : ModuleBase<CommandContext>
    {
        #region Commands

        [Command("ping")]
        [Summary("Simple \"Ping-Pong\" command")]
        public async Task PingCommand()
        {   
            await Context.Message.ReplyAsync("Pong!");
        }

        #endregion
    }
}