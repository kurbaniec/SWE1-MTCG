﻿using MTCG.Cards.Basis;
using MTCG.Cards.Basis.Monster;
using MTCG.Cards.DamageUtil;

namespace MTCG.Cards.Specialities.Concrete
{
    public class Afraid : ISpeciality
    {
        public void Apply(ICard self, ICard other, IDamage damage)
        {
            // Used for: "Goblins are too afraid of Dragons to attack"
            if (self is IMonsterCard selfMonster && selfMonster.MonsterType == MonsterType.Goblin &&
                other is IMonsterCard otherMonster && otherMonster.MonsterType == MonsterType.Dragon)
            {
                damage.SetNoDamage();
            }
        }
    }
}