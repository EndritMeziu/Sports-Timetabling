using itc2021.Deserializer;
using itc2021.Deserializer.Classes;
using System;
using System.IO;
using Google.OrTools.LinearSolver;
using itc2021.HelperClasses;
using System.Linq;

namespace itc2021
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDeserializer deserializer = new XmlDeserializer();
            var obj = deserializer.DeserializeXml<Instance>(@"C:\Users\USER\Desktop\AI Project\SportsTimeTabling\Test Instances EM\ITC2021_Test5.xml");

            
            int numTeams = obj.Resources.Teams.Team.Count;
            int numSlots = obj.Resources.Slots.Slot.Count;
            
                
            Solver solver = Solver.CreateSolver("SCIP");

            
            // Variables.
            // x[i, j, k ] is an array of 0-1 variables, which will be 1
            Variable[,,] x = new Variable[numTeams, numTeams,numSlots];
            for (int i = 0; i < numTeams; ++i)
            {
                for (int j = 0; j < numTeams; ++j)
                {
                    for (int k = 0; k < numSlots; ++k)
                    {
                        x[i, j, k] = solver.MakeIntVar(0, 1, $"worker_{i}_task_{j}");
                    }
                }
            }

            //Constraints

            //No team plays against itself
            for (int i = 0; i < numTeams; i++)
            {
                Constraint constraint = solver.MakeConstraint(0, 0, "");
                for (int j = 0; j < numSlots; j++)
                {
                    constraint.SetCoefficient(x[i, i, j], 1);
                }
            }

            ////Each team plays exaclty once in each round
            for (int k = 0; k < numSlots; k++)
            {
                for (int i = 0; i < numTeams; i++)
                {
                    Constraint constraint = solver.MakeConstraint(1, 1, "");
                    for (int j = 0; j < numTeams; j++)
                    {
                        constraint.SetCoefficient(x[i, j, k], 1);
                        constraint.SetCoefficient(x[j, i, k], 1);
                    }
                }
            }

            ////Each team plays exactly numTeams - 1 home games
            for (int i = 0; i < numTeams; i++)
            {
                Constraint constraint = solver.MakeConstraint(numTeams - 1, numTeams - 1, "");
                for (int j = 0; j < numTeams; j++)
                {
                    for (int k = 0; k < numSlots; k++)
                    {
                        constraint.SetCoefficient(x[i, j, k], 1);
                    }
                }
            }

            //Each team plays only 1H 1A against each of the other teams
            for (int i = 0; i < numTeams; i++)
            {
                for (int j = 0; j < numTeams; j++)
                {
                    if (i != j)
                    {
                        Constraint constraint = solver.MakeConstraint(1, 1, "");
                        for (int k = 0; k < numSlots; k++)
                        {
                            constraint.SetCoefficient(x[i, j, k], 1);
                        }
                    }
                }
            }

            ////Phased constraint tofix
            //for (int i = 0; i < numTeams; i++)
            //{
            //    Constraint constraint = solver.MakeConstraint(numTeams - 1, numTeams - 1, "");
            //    for (int j = 0; j < numTeams; j++)
            //    {
            //        for (int k = 0; k < numTeams - 1; k++)
            //        {
            //            constraint.SetCoefficient(x[i, j, k], 1);
            //            constraint.SetCoefficient(x[j, i, k], 1);
            //        }
            //    }
            //}



            //CA1 constraints
            var CA1Constraints = obj.Constraints.CapacityConstraints.CA1?.Where(x => x.Type == "HARD").ToList();
            foreach (var element in CA1Constraints)
            {
                var teams = CapacityConstraintsHelper.processTeams(element);
                var slots = CapacityConstraintsHelper.processSlots(element);
                foreach (var team in teams)
                {
                    Constraint constraint = solver.MakeConstraint(int.Parse(element.Min), int.Parse(element.Max), "");
                    foreach (var slot in slots)
                    {
                        for (int i = 0; i < numTeams; i++)
                        {
                            if (i != int.Parse(team))
                            {
                                if (element.Mode == "H")
                                    constraint.SetCoefficient(x[int.Parse(team), i, int.Parse(slot)], 1);
                                else
                                    constraint.SetCoefficient(x[i, int.Parse(team), int.Parse(slot)], 1);
                            }
                        }
                    }
                }
            }

            //CA2 constraints
            var CA2Constrainst = obj.Constraints.CapacityConstraints.CA2?.Where(x => x.Type == "HARD").ToList();
            foreach(var element in CA2Constrainst)
            {
                var teams1 = CapacityConstraintsHelper.processTeams1(element);
                var teams2 = CapacityConstraintsHelper.processTeams2(element);
                var slots = CapacityConstraintsHelper.processSlots(element);
                foreach(var team1 in teams1)
                {
                    Constraint constraint = solver.MakeConstraint(int.Parse(element.Min), int.Parse(element.Max), "");
                    foreach(var slot in slots)
                    {
                        foreach(var team2 in teams2)
                        {
                            if (element.Mode1 == "H")
                                constraint.SetCoefficient(x[int.Parse(team1), int.Parse(team2), int.Parse(slot)], 1);
                            else if(element.Mode1 == "A")
                                constraint.SetCoefficient(x[int.Parse(team2), int.Parse(team1), int.Parse(slot)], 1);
                            else
                            {
                                constraint.SetCoefficient(x[int.Parse(team1), int.Parse(team2), int.Parse(slot)], 1);
                                constraint.SetCoefficient(x[int.Parse(team2), int.Parse(team1), int.Parse(slot)], 1);
                            }

                        }
                    }
                }
            }

            //CA3 constraints --- todo

            //CA4 constraints
            var CA4Constraints = obj.Constraints.CapacityConstraints.CA4?.Where(x => x.Type == "HARD").ToList();
            foreach (var element in CA4Constraints)
            {
                var teams1 = CapacityConstraintsHelper.processTeams1(element);
                var teams2 = CapacityConstraintsHelper.processTeams2(element);
                var slots = CapacityConstraintsHelper.processSlots(element);
                if (element.Mode2 == "GLOBAL")
                {
                    Constraint constraint = solver.MakeConstraint(int.Parse(element.Min), int.Parse(element.Max), "");
                    foreach (var team1 in teams1)
                    {
                        foreach (var slot in slots)
                        {
                            foreach (var team2 in teams2)
                            {
                                if (team1 != team2)
                                {
                                    if (element.Mode1 == "H")
                                        constraint.SetCoefficient(x[int.Parse(team1), int.Parse(team2), int.Parse(slot)], 1);
                                    else if (element.Mode1 == "A")
                                        constraint.SetCoefficient(x[int.Parse(team2), int.Parse(team1), int.Parse(slot)], 1);
                                    else
                                    {
                                        constraint.SetCoefficient(x[int.Parse(team1), int.Parse(team2), int.Parse(slot)], 1);
                                        constraint.SetCoefficient(x[int.Parse(team2), int.Parse(team1), int.Parse(slot)], 1);
                                    }
                                }

                            }
                        }
                    }
                }
                else
                {
                    foreach(var slot in slots)
                    {
                        Constraint constraint = solver.MakeConstraint(int.Parse(element.Min), int.Parse(element.Max), "");
                        foreach(var team1 in teams1)
                        {
                            foreach(var team2 in teams2)
                            {
                                if(element.Mode1 == "H")
                                    constraint.SetCoefficient(x[int.Parse(team1), int.Parse(team2), int.Parse(slot)], 1);
                                else if(element.Mode1 == "A")
                                    constraint.SetCoefficient(x[int.Parse(team2), int.Parse(team1), int.Parse(slot)], 1);
                                else
                                {
                                    constraint.SetCoefficient(x[int.Parse(team1), int.Parse(team2), int.Parse(slot)], 1);
                                    constraint.SetCoefficient(x[int.Parse(team2), int.Parse(team1), int.Parse(slot)], 1);

                                }
                            }
                        }
                    }
                }
            }

            //GA1 constraints
            var GA1Constraints = obj.Constraints.GameConstraints.GA1?.Where(x => x.Type == "HARD").ToList();
            foreach (var element in GA1Constraints)
            {
                var meetings = GameConstraintsHelper.processMeetings(element);
                var slots = GameConstraintsHelper.processSlots(element);
                Constraint constraint = solver.MakeConstraint(int.Parse(element.Min), int.Parse(element.Max), "");
                foreach (var meeting in meetings)
                {
                    int team1 = int.Parse(meeting.Split(',')[0]);
                    int team2 = int.Parse(meeting.Split(',')[1]);
                    foreach (var slot in slots)
                    {
                        constraint.SetCoefficient(x[team1, team2, int.Parse(slot)], 1);
                    }
                }
            }

            Solver.ResultStatus resultStatus = solver.Solve();
            
            if(resultStatus == Solver.ResultStatus.FEASIBLE || resultStatus == Solver.ResultStatus.OPTIMAL)
            {
                for (int k = 0; k < numSlots; k++)
                {
                    for (int i = 0; i < numTeams; i++)
                    {
                        for (int j = 0; j < numTeams; j++)
                        {
                            if (x[i, j, k].SolutionValue() > 0.5)
                            {
                                Console.WriteLine($"[Team {i} vs Team {j}] Round: {k}");
                            }

                        }
                    }

                    Console.WriteLine("----------------");
                }
            }


        }
    }
}
