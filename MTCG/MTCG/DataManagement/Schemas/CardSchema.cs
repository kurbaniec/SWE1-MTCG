﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace MTCG.DataManagement.Schemas
{
    public class CardSchema
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Damage { get; set; }
        public string? PackageId { get; set; }
        public string? UserId { get; set; }
        public string? StoreId { get; set; }
        public bool InDeck { get; set; }

        // Used when inserting new cards
        public CardSchema(string id, string name, double damage)
        {
            Id = id;
            Name = name;
            Damage = damage;
        }
        
        public CardSchema(string id, string name, double damage, string? packageId, string? userId, string? storeId, bool inDeck)
        {
            Id = id;
            Name = name;
            Damage = damage;
            PackageId = packageId;
            UserId = userId;
            StoreId = storeId;
            InDeck = inDeck;
        }

        public static List<CardSchema> ParseRequest(JArray array)
        {
            // Parse Cards
            // See: https://stackoverflow.com/a/41810862/12347616
            var cards = new List<CardSchema>();
            foreach (var jToken in array)
            {
                if (!(jToken is JObject item)) continue;
                if (item["Id"] == null || item["Name"] == null || item["Damage"] == null) continue;
                try
                {
                    // Convert values
                    var id = item.GetValue("Id").ToObject<string>();
                    var name = item.GetValue("Name").ToObject<string>();
                    var damage = item.GetValue("Damage").ToObject<double>();
                    cards.Add(new CardSchema(id, name, damage));
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            return cards;
        }
    }
}