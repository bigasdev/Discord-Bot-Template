using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Discord.WebSocket;
using Discord;
using Discord.Net;
using Newtonsoft.Json;

namespace discordbottemplate{
    public class Commands{
        //The command handler
        public Task CommandHandler(SocketMessage message){
            //local variables for the command string and lenght
            string command = "";
            int lenghtCommand = -1;
            //Check for the prefix and if its a bot that called
            Console.WriteLine("Trying to execute a command");
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
                case "erase":
                    EraseUsers();
                break;
            }
            return Task.CompletedTask;
        }
        //The handler for misc stuff when a button is pressed
        public async Task ButtonClickHandler(SocketMessageComponent msg){
            await Bot.usersManagerInstance.AddUser(msg.User.Id.ToString(), msg.User.Username);
            Bot.usersManagerInstance.GetUser(msg.User.Id.ToString()).lastInteractedTime = DateTime.Now.ToString("h:mm:ss tt");
            Bot.usersManagerInstance.GetUser(msg.User.Id.ToString()).userState = UserState.Interacting;
        }

        //The handler for buttons
        public async Task ButtonHandler(SocketMessageComponent msg){
            await ButtonClickHandler(msg);
            switch(msg.Data.CustomId){
                    case "help":
                        await msg.RespondAsync($"{msg.User.Mention} Clicked the button and wants some help!");
                    break;
            }
        }
        //Command to erase the users
        public Task EraseUsers(){
            Bot.usersManagerInstance.DisposeAll();
            return Task.CompletedTask;
        }
    }
}