using itc2021.Deserializer.Classes.ConstraintClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace itc2021.HelperClasses
{
    public static class FairnessConstraintsHelper
    {
        public static List<string> processTeams(FA2 fa2)
        {
            List<string> teams = new List<string>();
            if (fa2.Teams.Contains(';'))
            {
                return fa2.Teams.Split(';').ToList();
            }
            teams.Add(fa2.Teams);
            return teams;
        }

        public static List<string> processSlots(FA2 fa2)
        {
            List<string> slots = new List<string>();
            if (fa2.Slots.Contains(';'))
            {
                return fa2.Slots.Split(';').ToList();
            }
            slots.Add(fa2.Slots);
            return slots;
        }
    }
}
