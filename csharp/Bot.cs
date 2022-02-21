using ShardingBot.Services;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace ShardingBot
{
    public class Bot
    {
        #region Fields

        private DiscordShardedClient _botClient;
        private CommandHandler? _commandHandler;

        #endregion

        #region Constructors

        public Bot()
        {
            _botClient = new DiscordShardedClient();
        }

        #endregion

        #region Methods

        public async Task StartBot()
        {
            RegisterClientEvents();

            await _botClient.LoginAsync(TokenType.Bot, "MY_BOT_TOKEN");
            await _botClient.StartAsync();

            _commandHandler = new CommandHandler(_botClient, new CommandService());
            await _commandHandler.InitializeCommands();

            await Task.Delay(-1);
        }

        private void RegisterClientEvents()
        {
            _botClient.ShardReady += OnShardReady;
            _botClient.Log += OnClientLog;
        }

        private async void AddGlobalCommands(DiscordSocketClient guildClient)
        {
            var globalCommand = new SlashCommandBuilder()
            {
                Name = "slash-ping",
                Description = "Simple \"Ping-Pong\" lash-command",
            };

            await guildClient.CreateGlobalApplicationCommandAsync(globalCommand.Build());
        }

        private void RegisterSlashCommandEvents(DiscordSocketClient guildClient)
        {
            guildClient.SlashCommandExecuted += OnSlashCommandExecuted;
        }

        #endregion

        #region Events

        private Task OnShardReady(DiscordSocketClient arg)
        {
            AddGlobalCommands(arg);
            RegisterSlashCommandEvents(arg);

            return Task.CompletedTask;
        }

        private Task OnClientLog(LogMessage arg)
        {
            Console.WriteLine(arg.ToString());

            return Task.CompletedTask;
        }

        private async Task OnSlashCommandExecuted(SocketSlashCommand command)
        {
            switch (command.CommandName)
            {
                case "slash-ping":
                    await command.RespondAsync("slash-pong!");
                    break;
            }
        }

        #endregion
    }
}