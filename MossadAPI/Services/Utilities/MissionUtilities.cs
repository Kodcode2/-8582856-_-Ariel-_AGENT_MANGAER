using MossadAPI.Models;


namespace MossadAPI.Services.Utilities
{
    public static class MissionUtilities
    {
        public static int CalculateTime(TargetLocation targetLocation, AgentLocation agentLocation)
        {
            int distance = CalculateDistance(targetLocation, agentLocation);
            int time = distance / 5;
            return time;
        }

        public static int CalculateDistance(TargetLocation targetLocation, AgentLocation agentLocation)
        {
            return (int)Math.Sqrt(Math.Pow(targetLocation.X - agentLocation.X, 2) + Math.Pow(targetLocation.Y - agentLocation.Y, 2));
        }

        public static Dictionary<string, int> CalculateMovement(TargetLocation targetLocation, AgentLocation agentLocation)
        {
            Dictionary<string, int> movements = new Dictionary<string, int>()
            {
                {"x", 0},
                {"y", 0}
            };
            
            if (targetLocation.X > agentLocation.X)
            {
                movements["x"] += 1;
            }
            else if (targetLocation.X < agentLocation.X)
            {
                movements["x"] -= 1;
            }
            if (targetLocation.Y > agentLocation.Y)
            {
                movements["x"] += 1;
            }
            else if (targetLocation.Y < agentLocation.Y)
            {
                movements["x"] -= 1;
            }
            return movements;
        }
    }
}
