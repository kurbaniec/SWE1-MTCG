﻿using System.Collections.Generic;
using MTCG.Cards.DamageUtil;

namespace MTCG.Battles.Logging
{
    public interface IBattleLog
    {
        (IPlayerLog, IPlayerLog) GetPlayerLogs(string playerAName, string playerBName);

        void RoundLog(IDamage playerADamage, IDamage playerBDamage, int cardsLeftA, int cardsLeftB);

        Dictionary<string, object> GetLog();
    }
}