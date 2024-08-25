namespace MossadAPI.Services.Utilities
{
    public static class AgentUtilities
    {
        public static Dictionary<string, int> CalculateMovement(string direction, int x, int y)
        {
            Dictionary<string, (int, int)> directions = new Dictionary<string, (int, int)>
            {
                { "n", (0, 1) },
                { "nw", (-1, 1) },
                { "ne", (1, 1) },
                { "w", (-1, 0) },
                { "e", (1, 0) },
                { "sw", (-1, -1) },
                { "s", (0, -1) },
                { "se", (1, -1) },
            };

            Dictionary<string, int> result = new Dictionary<string, int>
            {
                { "x", x + directions[direction].Item1 },
                { "y", y + directions[direction].Item2 },
            };
            return result;
        }
    }
}
