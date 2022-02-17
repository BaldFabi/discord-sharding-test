using System.Reflection;

using Discord.Commands;
using Discord.WebSocket;



namespace ShardingBot.Handlers
{
    public class CommandHandler
    {
        private readonly DiscordShardedClient _botClient;
        private readonly CommandService _commandService;

        public CommandHandler(DiscordShardedClient client, CommandService commandService)
        {
            _botClient = client;
            _commandService = commandService;
        }

        public async Task InitializeCommands()
        {
            _botClient.MessageReceived += HandleCommand;
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), null);
        }

        private async Task HandleCommand(SocketMessage msg)
        {
            var message = msg as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            if (!(message.HasCharPrefix('!', ref argPos) ||
                  message.HasMentionPrefix(_botClient.CurrentUser, ref argPos)) ||
                  message.Author.IsBot) return;


            var context = new CommandContext(_botClient, message);

            try
            {
                var commandTask = await _commandService.ExecuteAsync(
                    context: context,
                    argPos: argPos,
                    services: null
                );
            }
            catch (Exception e)
            {
                await context.Channel.SendMessageAsync($"Error: \n {e.InnerException}");
            }

        }
    }
}