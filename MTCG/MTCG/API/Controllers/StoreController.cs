﻿using System.Collections.Generic;
using MTCG.Components.DataManagement.DB;
using MTCG.Components.DataManagement.Schemas;
using Newtonsoft.Json.Linq;
using WebService_Lib;
using WebService_Lib.Attributes;
using WebService_Lib.Attributes.Rest;
using WebService_Lib.Server;

namespace MTCG.API.Controllers
{
    [Controller]
    public class StoreController
    {
        [Autowired]
        private readonly AuthCheck auth = null!;

        [Autowired] 
        private readonly PostgresDatabase db = null!;
        
        [Post("/packages")]
        public Response AddPackages(AuthDetails? user, Dictionary<string, object>? payload)
        {
            // Check parameters
            if (!(user is { } userDetails) || payload == null)
                return Response.Status(Status.BadRequest);
            return AddPackage(user, payload);
        }
        
        [Post("/transactions/packages")]
        public Response AcquirePackages(AuthDetails? user)
        {
            // Check parameters
            if (!(user is { } userDetails) )
                return Response.Status(Status.BadRequest);
            return AcquirePackage(user);
        }
        

        private Response AddPackage(AuthDetails user, Dictionary<string, object> payload)
        {
            // Package needs to consists of 5 cards
            if (!(payload["array"] is JArray rawCards) || rawCards.Count != 5)
                return Response.Status(Status.BadRequest);
            // Get user and check if its an admin account
            var userSchema = db.GetUser(user.Username);
            if (userSchema is null) return Response.Status(Status.BadRequest);
            if (userSchema.Role != Role.Admin) return Response.Status(Status.Forbidden);
            // Parse given cards
            var cards = CardSchema.ParseRequest(rawCards);
            // Check if all cards were correctly parsed
            if (cards.Count != 5) return Response.Status(Status.BadRequest);
            // Add package and return corresponding response
            var result = db.AddPackage(cards);
            return Response.Status(result ? Status.Created : Status.Conflict);
        }
        
        private Response AcquirePackage(AuthDetails user)
        {
            var packageCost = 5;
            var userStats = db.GetUserStats(user.Username);
            if (userStats is null) 
                return Response.Status(Status.BadRequest);
            if (userStats.Coins - packageCost < 0) 
                return Response.Status(Status.BadRequest);
            return Response.Status(db.AcquirePackage(user.Username, packageCost) 
                ? Status.Created : Status.BadRequest);
        }
        
        
    }
}