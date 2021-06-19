using itc2021.Deserializer.Classes.ConstraintClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace itc2021.HelperClasses
{
    public static class CapacityConstraintsHelper
    {
        #region Capacity Constraints CA1
        public static List<string> processSlots(CA1 cA1)
        {
            List<string> slots = new List<string>();
            if(cA1.Slots.Contains(';'))
            {
                return cA1.Slots.Split(';').ToList();
            }
            slots.Add(cA1.Slots);
            return slots;
        }

        public static List<string> processTeams(CA1 cA1)
        {
            List<string> teams = new List<string>();
            if(cA1.Teams.Contains(';'))
            {
                return cA1.Teams.Split(';').ToList();
            }
            teams.Add(cA1.Teams);
            return teams;
        }
        #endregion

        #region Capacity Constraints CA2
        public static List<string> processTeams1(CA2 cA2)
        {
            List<string> teams = new List<string>();
            if(cA2.Teams1.Contains(';'))
            {
                return cA2.Teams1.Split(';').ToList();
            }
            teams.Add(cA2.Teams1);
            return teams;
        }

        public static List<string> processTeams2(CA2 cA2)
        {
            List<string> teams = new List<string>();
            if (cA2.Teams2.Contains(';'))
            {
                return cA2.Teams2.Split(';').ToList();
            }
            teams.Add(cA2.Teams2);
            return teams;
        }

        public static List<string> processSlots(CA2 cA2)
        {
            List<string> slots = new List<string>();
            if(cA2.Slots.Contains(';'))
            {
                return cA2.Slots.Split(';').ToList();
            }
            slots.Add(cA2.Slots);
            return slots;
        }
        #endregion

        #region Capacity Constraints CA4
        public static List<string> processTeams1(CA4 cA4)
        {
            List<string> teams = new List<string>();
            if (cA4.Teams1.Contains(';'))
            {
                return cA4.Teams1.Split(';').ToList();
            }
            teams.Add(cA4.Teams1);
            return teams;
        }

        public static List<string> processTeams2(CA4 cA4)
        {
            List<string> teams = new List<string>();
            if (cA4.Teams2.Contains(';'))
            {
                return cA4.Teams2.Split(';').ToList();
            }
            teams.Add(cA4.Teams2);
            return teams;
        }

        public static List<string> processSlots(CA4 cA4)
        {
            List<string> slots = new List<string>();
            if (cA4.Slots.Contains(';'))
            {
                return cA4.Slots.Split(';').ToList();
            }
            slots.Add(cA4.Slots);
            return slots;
        }
        #endregion
    }
}
