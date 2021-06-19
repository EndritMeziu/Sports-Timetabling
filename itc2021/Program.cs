using itc2021.Deserializer;
using itc2021.Deserializer.Classes;
using System;
using System.IO;
using Google.OrTools.LinearSolver;

namespace itc2021
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDeserializer deserializer = new XmlDeserializer();
            var obj = deserializer.DeserializeXml<Instance>(@"C:\Users\USER\Desktop\AI Project\SportsTimeTabling\Test Instances EM\ITC2021_Test5.xml");

            Solver solver = Solver.CreateSolver("SCIP");

            int numTeams = obj.Resources.Teams.Team.Count;
            int numSlots = obj.Resources.Slots.Slot.Count;
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

            //Each team plays exaclty once in each round
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

            //Each team plays exactly numTeams - 1 home games
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
                        Constraint constraint = solver.MakeConstraint(1,1, "");
                        for (int k = 0; k < numSlots; k++)
                        {
                            constraint.SetCoefficient(x[i, j, k], 1);
                        }
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
