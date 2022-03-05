using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Discord.WebSocket;
using Discord;
using Discord.Net;
using Newtonsoft.Json;

namespace discordbottemplate{
    public class UsersManager{
        public List<User> usersConnected = new List<User>();

        public Task AddUser(string Id, string name){
            if(GetUser(Id) != null)return Task.CompletedTask;
            var u = new User(Id, name);
            Console.WriteLine("Added new user to the list: "+u.Id);
            usersConnected.Add(u);
            return Task.CompletedTask;
        }
        public User GetUser(string Id){
            foreach(var u in usersConnected){
                if(u.Id == Id){
                    Console.WriteLine("Found the user : "+Id);
                    return u;
                }
            }
            return null;
        }
        public void DisposeAll(){
            Console.WriteLine("Erasing the users");
            usersConnected.Clear();
        }
    }
    public class User{
        public string name;
        public string Id;
        public string lastInteractedTime;
        public UserState userState = UserState.Idle;

        public User(string Id, string name){
            this.Id = Id;
            this.name = name;
        }
    }
}
public enum UserState{
    Idle,
    Interacting, 
    Buy,
    Sell
}