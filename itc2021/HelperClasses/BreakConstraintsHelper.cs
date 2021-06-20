using itc2021.Deserializer.Classes.ConstraintClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace itc2021.HelperClasses
{
    public static class BreakConstraintsHelper
    {
        public static List<string> processTeams(BR1 bR1)
        {
            List<string> teams = new List<string>();
            if(bR1.Teams.Contains(';'))
            {
                return bR1.Teams.Split(';').ToList();
            }
            teams.Add(bR1.Teams);
            return teams;
        }

        public static List<string> processSlots(BR1 bR1)
        {
            List<string> slots = new List<string>();
            if(bR1.Slots.Contains(';'))
            {
                return bR1.Slots.Split(';').ToList();
            }
            slots.Add(bR1.Slots);
            return slots;
        }
    }
}
