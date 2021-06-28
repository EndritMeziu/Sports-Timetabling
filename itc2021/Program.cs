using itc2021.Deserializer;
using itc2021.Deserializer.Classes;
using System;
using System.IO;
using itc2021.HelperClasses;
using System.Linq;
using Google.OrTools.Sat;
using System.Collections.Generic;

namespace itc2021
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDeserializer deserializer = new XmlDeserializer();
            var obj = deserializer.DeserializeXml<Instance>(@"C:\Users\USER\Desktop\AI Project\SportsTimeTabling\Test Instances EM\ITC2021_Early_2.xml");

            int numTeams = obj.Resources.Teams.Team.Count;
            int numSlots = obj.Resources.Slots.Slot.Count;

            CpModel model = new CpModel();
            
            // Variables.
            IntVar[,,] x = new IntVar[numTeams, numTeams, numSlots];
            // x[i, j, k ] is an array of 0-1 variables, which will be 1
            for (int i = 0; i < numTeams; ++i)
            {
                for (int j = 0; j < numTeams; ++j)
                {
                    for (int k = 0; k < numSlots; ++k)
                    {
                        x[i, j, k] = model.NewIntVar(0, 1, $"Team_{i}_plays_Team_{j}_inslot_{k}");
                    }
                }
            }

            //Constraints
            //No team plays against itself
            for (int i = 0; i < numTeams; i++)
            {
                IntVar[] vars = new IntVar[numSlots];
                for (int j = 0; j < numSlots; j++)
                {
                    vars[j] = x[i,i, j];
                }
                model.Add(LinearExpr.Sum(vars) + LinearExpr.Sum(vars) <= 2);
                model.Add(LinearExpr.Sum(vars) == 0);
            }

            //////Each team plays exaclty once in each round
            for (int k = 0; k < numSlots; k++)
            {
                for (int i = 0; i < numTeams; i++)
                {
                    IntVar[] vars = new IntVar[numTeams*2];
                    for (int j = 0; j < numTeams; j++)
                    {
                        vars[j] = x[i, j, k];
                    }
                    for (int j = 0; j < numTeams; j++)
                    {
                        vars[j+numTeams] = x[j, i, k];
                    }
                    model.Add(LinearExpr.Sum(vars) == 1);
                }
            }

            //////Each team plays exactly numTeams - 1 home games
            for (int i = 0; i < numTeams; i++)
            {
                IntVar[] vars = new IntVar[numTeams * numSlots];
                for (int j = 0; j < numTeams; j++)
                {
                    for (int k = 0; k < numSlots; k++)
                    {
                        vars[j * numSlots + k] = x[i, j, k];
                    }
                }
                model.Add(LinearExpr.Sum(vars) == numTeams - 1);
            }

            ////Each team plays only 1H 1A against each of the other teams
            for (int i = 0; i < numTeams; i++)
            {
                for (int j = 0; j < numTeams; j++)
                {
                    if (i != j)
                    {
                        IntVar[] vars = new IntVar[numSlots];
                        for (int k = 0; k < numSlots; k++)
                        {
                            vars[k] = x[i, j, k];
                        }
                        model.Add(LinearExpr.Sum(vars) == 1);
                    }
                }
            }

            if (obj.Structure.Format.GameMode.Text == "P")
            {
                //Phased constraint tofix
                for (int i = 0; i < numTeams-1; i++)
                {
                    for (int j = i + 1; j < numTeams; j++)
                    {
                        IntVar[] vars = new IntVar[(numTeams - 1) * (numTeams - 1)];
                        int count = 0;
                        for (int k = 0; k < numTeams - 1; k++)
                        {
                            vars[count++] = x[i, j, k];
                            vars[count++] = x[j, i, k];
                        }
                        model.Add(LinearExpr.Sum(vars) == 1);
                    }
                }
            }


            ////CA1 constraints
            var CA1Constraints = obj.Constraints.CapacityConstraints.CA1?.Where(x => x.Type == "HARD").ToList();
            foreach (var element in CA1Constraints)
            {
                var teams = CapacityConstraintsHelper.processTeams(element);
                var slots = CapacityConstraintsHelper.processSlots(element);
                foreach (var team in teams)
                {
                    IntVar[] vars = new IntVar[slots.Count*numTeams];
                    int count = 0;
                    foreach (var slot in slots)
                    {
                        for (int i = 0; i < numTeams; i++)
                        {
                            if (i != int.Parse(team))
                            {
                                if (element.Mode == "H")
                                    vars[count] = x[int.Parse(team), i, int.Parse(slot)];
                                else
                                    vars[count] = x[i, int.Parse(team), int.Parse(slot)];
                            }
                            count++;
                        }
                    }
                    model.Add(LinearExpr.Sum(vars) >= int.Parse(element.Min));
                    model.Add(LinearExpr.Sum(vars) <= int.Parse(element.Max));
                }
            }

            ////CA2 constraints
            var CA2Constrainst = obj.Constraints.CapacityConstraints.CA2?.Where(x => x.Type == "HARD").ToList();
            foreach (var element in CA2Constrainst)
            {
                var teams1 = CapacityConstraintsHelper.processTeams1(element);
                var teams2 = CapacityConstraintsHelper.processTeams2(element);
                var slots = CapacityConstraintsHelper.processSlots(element);
                foreach (var team1 in teams1)
                {
                    IntVar[] vars = new IntVar[slots.Count * teams2.Count*2];
                    int count = 0;
                    foreach (var slot in slots)
                    {
                        foreach (var team2 in teams2)
                        {
                            if (element.Mode1 == "H")
                                vars[count++] = x[int.Parse(team1), int.Parse(team2), int.Parse(slot)];
                            else if (element.Mode1 == "A")
                                vars[count++] = x[int.Parse(team2), int.Parse(team1), int.Parse(slot)];
                            else
                            {
                                vars[count++] = x[int.Parse(team1), int.Parse(team2), int.Parse(slot)];
                                vars[count++] = x[int.Parse(team2), int.Parse(team1), int.Parse(slot)];
                            }
                        }
                    }
                    model.Add(LinearExpr.Sum(vars) >= int.Parse(element.Min));
                    model.Add(LinearExpr.Sum(vars) <= int.Parse(element.Max));
                }
            }

            ////CA3 constraints
            var CA3Constrains = obj.Constraints.CapacityConstraints.CA3?.Where(x => x.Type == "HARD").ToList();
            foreach(var element in CA3Constrains)
            {
                var teams1 = CapacityConstraintsHelper.processTeams1(element);
                var teams2 = CapacityConstraintsHelper.processTeams2(element);
                int intp = int.Parse(element.Intp);
                for (int i = 0; i < teams1.Count; i++)
                {
                    for (int l = 0; l <= 2 * (numTeams - 1) - intp; l++)
                    {
                        IntVar[] varsH = new IntVar[teams2.Count * intp];
                        IntVar[] varsA = new IntVar[teams2.Count * intp];
                        int countH = 0;
                        int countA = 0;
                        for (int j = 0; j < teams2.Count; j++)
                        {
                            for (int k = l; k < l + intp; k++)
                            {
                                if(element.Mode1 == "H")
                                {
                                    varsH[countH++] = x[int.Parse(teams1[i]), int.Parse(teams2[j]), k];
                                }
                                else if(element.Mode1 == "A")
                                {
                                    varsH[countA++] = x[int.Parse(teams1[i]), int.Parse(teams2[j]), k];
                                }
                                else if(element.Mode2 == "HA")
                                {
                                    varsH[countH++] = x[int.Parse(teams1[i]), int.Parse(teams2[j]), k];
                                    varsA[countA++] = x[int.Parse(teams1[i]), int.Parse(teams2[j]), k];
                                }
                            }
                        }

                        if(element.Type == "H")
                        {
                            model.Add(LinearExpr.Sum(varsH) <= int.Parse(element.Max));
                            model.Add(LinearExpr.Sum(varsH) >= int.Parse(element.Min));
                        }
                        else if(element.Type == "A")
                        {
                            model.Add(LinearExpr.Sum(varsA) <= int.Parse(element.Max));
                            model.Add(LinearExpr.Sum(varsA) >= int.Parse(element.Min));
                        }
                        else if(element.Type == "HA")
                        {
                            model.Add(LinearExpr.Sum(varsH) + LinearExpr.Sum(varsA) <= int.Parse(element.Max));
                            model.Add(LinearExpr.Sum(varsH) + LinearExpr.Sum(varsA) >= int.Parse(element.Min));
                        }
                    }
                }
            }


            ////CA4 constraints
            var CA4Constraints = obj.Constraints.CapacityConstraints.CA4?.Where(x => x.Type == "HARD").ToList();
            foreach (var element in CA4Constraints)
            {
                var teams1 = CapacityConstraintsHelper.processTeams1(element);
                var teams2 = CapacityConstraintsHelper.processTeams2(element);
                var slots = CapacityConstraintsHelper.processSlots(element);
                if (element.Mode2 == "GLOBAL")
                {
                    IntVar[] vars = new IntVar[teams1.Count * slots.Count * (teams2.Count * 2)];
                    int count = 0;
                    foreach (var team1 in teams1)
                    {
                        foreach (var slot in slots)
                        {
                            foreach (var team2 in teams2)
                            {
                                if (team1 != team2)
                                {
                                    if (element.Mode1 == "H")
                                        vars[count++] = x[int.Parse(team1), int.Parse(team2), int.Parse(slot)];
                                    else if (element.Mode1 == "A")
                                        vars[count++] = x[int.Parse(team2), int.Parse(team1), int.Parse(slot)];
                                    else
                                    {
                                        vars[count++] = x[int.Parse(team1), int.Parse(team2), int.Parse(slot)];
                                        vars[count++] = x[int.Parse(team2), int.Parse(team1), int.Parse(slot)];
                                    }
                                }

                            }
                        }
                    }
                    model.Add(LinearExpr.Sum(vars) >= int.Parse(element.Min));
                    model.Add(LinearExpr.Sum(vars) <= int.Parse(element.Max));
                }
                else
                {
                    foreach (var slot in slots)
                    {
                        IntVar[] vars = new IntVar[teams1.Count * (teams2.Count * 2)];
                        int count = 0;
                        foreach (var team1 in teams1)
                        {
                            foreach (var team2 in teams2)
                            {
                                if (element.Mode1 == "H")
                                    vars[count++] = x[int.Parse(team1), int.Parse(team2), int.Parse(slot)];
                                else if (element.Mode1 == "A")
                                    vars[count++] = x[int.Parse(team2), int.Parse(team1), int.Parse(slot)];
                                else
                                {
                                    vars[count++] = x[int.Parse(team1), int.Parse(team2), int.Parse(slot)];
                                    vars[count++] = x[int.Parse(team2), int.Parse(team1), int.Parse(slot)];
                                }
                            }
                        }
                        model.Add(LinearExpr.Sum(vars) >= int.Parse(element.Min));
                        model.Add(LinearExpr.Sum(vars) <= int.Parse(element.Max));
                    }
                }
            }

            ////GA1 constraints
            var GA1Constraints = obj.Constraints.GameConstraints.GA1?.Where(x => x.Type == "HARD").ToList();
            foreach (var element in GA1Constraints)
            {
                var meetings = GameConstraintsHelper.processMeetings(element);
                var slots = GameConstraintsHelper.processSlots(element);
                IntVar[] vars = new IntVar[meetings.Count * slots.Count];
                int count = 0;
                foreach (var meeting in meetings)
                {
                    int team1 = int.Parse(meeting.Split(',')[0]);
                    int team2 = int.Parse(meeting.Split(',')[1]);
                    foreach (var slot in slots)
                    {
                        vars[count++] = x[team1, team2, int.Parse(slot)];
                    }
                }
                model.Add(LinearExpr.Sum(vars) >= int.Parse(element.Min));
                model.Add(LinearExpr.Sum(vars) <= int.Parse(element.Max));
            }


            ////Break Contraints
            IntVar[,] h = new IntVar[numTeams, 2*(numTeams-1)];
            IntVar[,] a = new IntVar[numTeams, 2*(numTeams-1)];

            h[0, 0] = model.NewIntVar(0, 0, "0,0");
            a[0, 0] = model.NewIntVar(0, 0, "0,0");
            
            for (int i = 0; i < numTeams; i++)
            {
                for (int k = 1; k < 2 * (numTeams - 1); k++)
                {
                    h[i,k] = model.NewIntVar(0, 1, $"{i},{k}");
                    a[i,k] = model.NewIntVar(0, 1, $"{i},{k}");
                    IntVar[] varsh = new IntVar[2 * numTeams];
                    int counth = 0;
                    int counta = 0;
                    IntVar[] varsa = new IntVar[2 * numTeams];
                    for (int j = 0; j < numTeams; j++)
                    {
                        varsh[counth++] = x[i,j,k - 1];
                        varsh[counth++] = x[i, j, k];

                        varsa[counta++] = x[j, i, k - 1];
                        varsa[counta++] = x[j, i, k];
                    }

                    model.Add(LinearExpr.Sum(varsh) - 1 <= h[i, k]);
                    model.Add(LinearExpr.Sum(varsa) - 1 <= a[i, k]);

                }
            }

            //BR1 Constraint
            var BR1Constraints = obj.Constraints.BreakConstraints.BR1?.Where(x => x.Type == "HARD").ToList();
            foreach (var element in BR1Constraints)
            {
                var teams = BreakConstraintsHelper.processTeams(element);
                var slots = BreakConstraintsHelper.processSlots(element);
                foreach (var team in teams)
                {
                    //Constraint constraint = solver.MakeConstraint(0, int.Parse(element.Intp), "");
                    IntVar[] vars = new IntVar[slots.Count *2];
                    int count = 0;
                    foreach (var slot in slots)
                    {
                        vars[count++] = h[int.Parse(team), int.Parse(slot)];
                        vars[count++] = a[int.Parse(team), int.Parse(slot)];
                    }
                    model.Add(LinearExpr.Sum(vars) <= int.Parse(element.Intp));
                }
            }


            //BR2 Constraint
            var BR2Constraints = obj.Constraints.BreakConstraints.BR2?.Where(x => x.Type == "HARD").ToList();
            foreach(var element in BR2Constraints)
            {
                var teams = BreakConstraintsHelper.processTeams(element);
                var slots = BreakConstraintsHelper.processSlots(element);
                IntVar[] vars = new IntVar[teams.Count * (slots.Count*2)];
                int count = 0;
                foreach(var team in teams)
                {
                    foreach(var slot in slots)
                    {
                        vars[count++] = h[int.Parse(team), int.Parse(slot)];
                        vars[count++] = a[int.Parse(team), int.Parse(slot)];
                    }
                }
                model.Add(LinearExpr.Sum(vars) <= int.Parse(element.Intp));
            }

            //FA2 Constraint
            var FA2Constraints = obj.Constraints.FairnessConstraints.FA2?.Where(x => x.Type == "HARD").ToList();
            foreach(var element in FA2Constraints)
            {
                var teams = FairnessConstraintsHelper.processTeams(element);
                var slots = FairnessConstraintsHelper.processSlots(element);
                for (int i = 0; i < teams.Count; i++)
                {
                    for (int j = i + 1; j < teams.Count; j++)
                    {
                        for (int k = 0; k < slots.Count; k++)
                        {
                            IntVar[] varsSum = new IntVar[(k + 1) * numTeams];
                            IntVar[] varsDif = new IntVar[(k + 1) * numTeams];
                            int countSum = 0;
                            int countDif = 0;
                            for (int k2 = 0; k2 <= k; k2++)
                            {
                                for (int j2 = 0; j2 < numTeams; j2++)
                                {
                                    varsSum[countSum++] = x[int.Parse(teams[i]), j2, k2];
                                    varsDif[countDif++] = x[int.Parse(teams[j]), j2, k2];                                    
                                }
                            }
                            model.Add(LinearExpr.Sum(varsSum) - LinearExpr.Sum(varsDif) <= int.Parse(element.Intp));
                            model.Add(LinearExpr.Sum(varsSum) - LinearExpr.Sum(varsDif) >= -int.Parse(element.Intp));

                        }

                    }
                }
            }

            var SE1Constraints = obj.Constraints.SeparationConstraints.SE1?.Where(x => x.Type == "HARD").ToList();
            foreach(var element in SE1Constraints)
            {
                var teams = SeparationConstrainstsHelper.processTeams(element);
                for (int i = 0; i < teams.Count-1; i++)
                {
                    for (int j = i + 1; j < teams.Count; j++)
                    {
                        for (int k = 0; k < numSlots - int.Parse(element.Min); k++)
                        {
                            int countH = 0;
                            int countA = 0;
                            IntVar[] varsH = new IntVar[numSlots];
                            IntVar[] varsA = new IntVar[numSlots];
                            for(int k2 = k+1; k2< k+1+int.Parse(element.Min);k2++)
                            {
                                varsH[countH++] = x[i, j, k2];
                                varsA[countA++] = x[j, i, k2];
                            }
                            model.Add(LinearExpr.Sum(varsH) == 0).OnlyEnforceIf(x[j, i, k]);
                            model.Add(LinearExpr.Sum(varsA) == 0).OnlyEnforceIf(x[i, j, k]);
                        }
                    }
                }
            }
 

            CpSolver solver = new CpSolver();
            CpSolverStatus status = solver.Solve(model);
            Console.WriteLine($"Solve status: {status}");

            // Print solution.
            // Check that the problem has a feasible solution.
            if (status == CpSolverStatus.Optimal || status == CpSolverStatus.Feasible)
            {
                for (int k = 0; k < numSlots; k++)
                {
                    for (int i = 0; i < numTeams; i++)
                    {
                        for (int j = 0; j < numTeams; j++)
                        {
                            if (solver.Value(x[i, j, k]) > 0.5)
                            {
                                Console.WriteLine($"[Team {i} vs Team {j}] Round: {k}");
                            }

                        }
                    }
                    Console.WriteLine("----------------");
                }
            }

            printSolution(x, numSlots, numTeams, solver) ;


        }

        public static void printSolution(IntVar[,,] x,int numSlots,int numTeams, CpSolver solver)
        {
            string solution = "<Solution>\n";
            solution += "\t<MetaData>\n";
            solution += "\t\t<InstanceName>" + "ITC2021_Test3.xml" + "</InstanceName>\n";
            solution += "\t\t<SolutionName>" + "ITC2021_Test3Solution" + "</SolutionName>\n";
            solution += "\t\t<ObjectiveValue/>\n";
            solution += "\t</MetaData>\n";

            solution += "\t<Games>\n";
            for (int k = 0; k < numSlots; k++)
            {
                for (int i = 0; i < numTeams; i++)
                {
                    for (int j = 0; j < numTeams; j++)
                    {
                        if (solver.Value(x[i, j, k]) > 0.5)
                        {
                            solution += "\t\t<ScheduledMatch home=\"" + i + "\" away=\"" + j + "\" slot=\"" + k + "\"/>\n";
                        }

                    }
                }
            }
            solution += "\t</Games>\n";
            solution += "</Solution>\n";
            Console.WriteLine(solution);
        }
    }
}
