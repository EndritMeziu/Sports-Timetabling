﻿using itc2021.Deserializer.Classes.ConstraintClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace itc2021.HelperClasses
{
    public static class CapacityConstraintsHelper
    {
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
    }
}
