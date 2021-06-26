using itc2021.Deserializer.Classes.ConstraintClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace itc2021.HelperClasses
{
    public static class SeparationConstrainstsHelper
    {
        public static List<string> processTeams(SE1 se1)
        {
            List<string> teams = new List<string>();
            if (se1.Teams.Contains(';'))
            {
                return se1.Teams.Split(';').ToList();
            }
            teams.Add(se1.Teams);
            return teams;
        }
    }
}
