﻿using MTCG.Cards.Basis;
using MTCG.Cards.Basis.Monster;
using MTCG.Cards.Basis.Spell;
using MTCG.Cards.DamageUtil;

namespace MTCG.Cards.Specialities.Types.Destruction
{
    public class DrownKnight : ISpeciality, IDestruction
    {
        public void Apply(ICard other, IDamage damage)
        {
            // Used for: "The armor of Knights is so heavy that WaterSpells make them drown them instantly."
            if (other is IMonsterCard otherMonster && otherMonster.MonsterType == MonsterType.Knight)
            {
                (this as IDestruction).Destroy(damage);
            }
        }
    }
}