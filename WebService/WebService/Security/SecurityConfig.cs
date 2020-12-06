﻿using System.Collections.Generic;
using WebService_Lib;

namespace WebService.Security
{
    [WebService_Lib.Attributes.Security]
    public class SecurityConfig : ISecurity
    {
        public AuthDetails AuthDetails(string token)
        {
            var username = token.Substring(0, token.Length - 6);
            return new AuthDetails(token, username);
        }
        private readonly HashSet<string> tokens = new HashSet<string>();
        private readonly Dictionary<string, string> users = new Dictionary<string, string>();
        public bool Authenticate(string token) => tokens.Contains(token);
        public (bool, string) Register(string username, string password) 
        {
            if (users.ContainsKey(username)) return (false, "");
            users[username] = password;
            return (true, GenerateToken(username));
        }
        public string GenerateToken(string username) => username + "-token";
        public void AddToken(string token) => tokens.Add(token);
        public void RevokeToken(string token) => tokens.Remove(token);
        public List<string> SecurePaths() => new List<string> { "/secured" };
        public bool CheckCredentials(string username, string password) 
            => users.ContainsKey(username) && users[username] == password;
    }
}