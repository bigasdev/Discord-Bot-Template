using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Discord.WebSocket;
using Discord;
using Discord.Net;
using Newtonsoft.Json;
namespace discordbottemplate{
    public class DebugCommands{
        //The command handler
        public Task CommandHandler(SocketMessage message){
            //local variables for the command string and lenght
            string command = "";
            int lenghtCommand = -1;
            //Check for the prefix and if its a bot that called
            Console.WriteLine("Trying to execute a debug command");
            var s = "?";
            if(!message.Content.StartsWith(s[0]))
                return Task.CompletedTask;

            if(message.Author.IsBot)
                return Task.CompletedTask;

            if(message.Content.Contains(' '))
                lenghtCommand = message.Content.IndexOf(' ');
            else
                lenghtCommand = message.Content.Length;

            command = message.Content.Substring(1, lenghtCommand - 1);
            Console.WriteLine("The command is : "+command+" Done by: "+message.Author.ToString());

            switch(command){
                case "about":
                    message.Channel.SendMessageAsync($@"{message.Author.Mention}Your bot!");
                break;
                case "help":
                    Help(message);
                break;
                case "users":
                    GetUsers(message);
                break;
                case "guild":
                    GetGuild(message);
                break;
            }
            return Task.CompletedTask;
        }

        //The handler for the debug buttons
        public async Task DebugButtonHandler(SocketMessageComponent msg){
            switch(msg.Data.CustomId){
                    case "help":
                        await msg.RespondAsync($"{msg.User.Mention} Clicked the button and wants some help!");
                    break;
            }
        }

        //The help command
        public Task Help(SocketMessage msg){
            var eb = new EmbedBuilder{
                Title = "I'm your help!",
                Description = "Press the button to continue!"
            };
            var builder = new ComponentBuilder()
                .WithButton("HEEEEELP!", "help");

            msg.Channel.SendMessageAsync(msg.Author.Mention, false, eb.Build(), components: builder.Build());
            return Task.CompletedTask;
        }

        //Command to get the users
        public Task GetUsers(SocketMessage msg){
            if(Bot.usersManagerInstance.usersConnected.Count <= 0){
                msg.Channel.SendMessageAsync("No user connected");
                return Task.CompletedTask;
            }
            foreach(var u in Bot.usersManagerInstance.usersConnected){
                var eb = new EmbedBuilder{
                    Title = u.name,
                    Description = "Last interaction time: " + u.lastInteractedTime + "\n" + u.userState.ToString()
                };
                msg.Channel.SendMessageAsync("", false, eb.Build());
            }
            return Task.CompletedTask;
        }

        //Command to get the guild
        public Task GetGuild(SocketMessage msg){
            msg.Channel.SendMessageAsync("Guilds the bot is in : "+ Bot.InstanceClient.Guilds.Count);
            var chnl = msg.Channel as SocketGuildChannel;
            msg.Channel.SendMessageAsync("This guild id is : "+ chnl.Guild.Name + " ID: "+chnl.Guild.Id+ " Onwer: "+chnl.Guild.Owner);
            return Task.CompletedTask;
        }
    }
}