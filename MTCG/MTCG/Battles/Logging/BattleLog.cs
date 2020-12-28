﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using MTCG.Cards.DamageUtil;

namespace MTCG.Battles.Logging
{
    public class BattleLog : IBattleLog
    {
        private readonly Dictionary<string, object> log = new Dictionary<string, object>();
        private int counter = 1;
        private IPlayerLog playerA = null!;
        private IPlayerLog playerB = null!;

        public (IPlayerLog, IPlayerLog) GetPlayerLogs(string playerAName, string playerBName)
        {
            playerA = new PlayerLog(playerAName);
            playerB = new PlayerLog(playerBName);
            return (playerA, playerB);
        }

        public void RoundLog(IDamage playerADamage, IDamage playerBDamage, int cardsLeftA, int cardsLeftB)
        {
            if (!(playerA is { } a) || !(playerB is { } b)) return;
            var round = new Dictionary<string, object>
            {
                [a.Username] = playerA.Log, [b.Username] = playerB.Log
            };
            var result = new List<string>
            {
                $"{a.CardName}\tVS\t{b.CardName}", 
                $"{playerADamage.ToString()}\tVS\t{playerBDamage.ToString()}"
            };
            if (playerADamage.CompareTo(playerBDamage) == 0)
            {
                result.Add("Result: Draw");
            } else if (playerADamage.CompareTo(playerBDamage) > 0)
            {
                result.Add($"Result: {a.Username} Win");
                if (a.EffectLog is {} effect) result.Add(effect);
            }
            else
            {
                result.Add($"Result: {b.Username} Win");
                if (b.EffectLog is {} effect) result.Add(effect);
            }
            round["result"] = result;
            log[$"round {counter}"] = round;
            counter++;
        }

        public Dictionary<string, object> GetLog()
        {
            return log;
        }
    }
}