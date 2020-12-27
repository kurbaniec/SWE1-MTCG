﻿using System.Collections.Generic;
using MTCG.DataManagement.Schemas;

namespace MTCG.DataManagement.DB
{
    public interface IDatabase
    {
        void CreateDatabaseIfNotExists();
        
        bool AddPackage(string admin, List<CardSchema> cards);
        bool AcquirePackage(string username);

        bool AddUser(UserSchema user);
        UserSchema? GetUser(string username);
        StatsSchema? GetUserStats(string username);
        
        bool EditUserProfile(string username, string bio, string image);
        
        List<CardSchema> GetUserDeck(string username);
        List<CardSchema> GetUserCards(string username);
        
        List<StatsSchema> GetScoreboard();

        List<StoreSchema> GetTradingDeals();
        bool AddTradingDeal(string username, StoreSchema deal);
        bool Trade(string username, string myDeal, string otherDeal);

        
    }
}