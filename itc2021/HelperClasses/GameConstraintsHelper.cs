using itc2021.Deserializer.Classes.ConstraintClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace itc2021.HelperClasses
{
    public static class GameConstraintsHelper
    {
        public static List<string> processMeetings(GA1 gA1)
        {
            List<string> meetingsPairs = new List<string>();
            gA1.Meetings = gA1.Meetings.Remove(gA1.Meetings.Length - 1,1);
            if(gA1.Meetings.Contains(';'))
            {
                return gA1.Meetings.Split(';').ToList();
            }
            meetingsPairs.Add(gA1.Meetings);
            return meetingsPairs;
        }

        public static List<string> processSlots(GA1 gA1)
        {
            List<string> slots = new List<string>();
            if(gA1.Slots.Contains(';'))
            {
                return gA1.Slots.Split(';').ToList();
            }
            slots.Add(gA1.Slots);
            return slots;
        }
    }
}
