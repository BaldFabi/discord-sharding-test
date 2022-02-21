using System.Reflection;

using Discord;
using Discord.Commands;
using Discord.WebSocket;



namespace ShardingBot.Services
{
    public class CommandHandler
    {
        #region Fields

        private readonly DiscordShardedClient _botClient;
        private readonly CommandService _commandService;

        #endregion

        #region Constructors

        public CommandHandler(DiscordShardedClient client, CommandService commandService)
        {
            _botClient = client;
            _commandService = commandService;
        }

        #endregion

        #region Methods

        public async Task InitializeCommands()
        {
            _botClient.MessageReceived += HandleCommand;
            await _commandService.AddModulesAsync(Assembly.GetEntryAssembly(), null);
        }

        #endregion

        #region Events

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

        #endregion
    }
}