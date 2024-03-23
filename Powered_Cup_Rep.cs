using System;
using XRL.Rules;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{

    [Serializable]
    public class PoweredCupReputation : IPoweredPart
    {
        public PoweredCupReputation()
        {
            WorksOnEquipper = true;
            ChargeUse = 50;
            NameForStatus = "FriendlyCup";
        }

        public override bool SameAs(IPart p)
        {
            return false;
        }

        public override bool WantEvent(int ID, int cascade)
        {
            return
                base.WantEvent(ID, cascade)

                || ID == GetWaterRitualReputationAmountEvent.ID
                // || ID == WaterRitualStartEvent.ID
            ;
        }
        
        public override bool HandleEvent(GetWaterRitualReputationAmountEvent E)
        {
            if (E.Actor == ParentObject.Equipped
                && !E.SpeakingWith.HasIntProperty("PerfectCupReputation")
                && E.Actor.IsPlayer()
                && E.Faction == E.Record.faction
                && IsReady(UseCharge:true)
                )
            {
                E.SpeakingWith.SetIntProperty("PerfectCupReputation", 1);
                E.Amount += 50;
            }
            return base.HandleEvent(E);
        }
        


    }

}
