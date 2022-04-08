using System.Collections.Generic;
using ASCIITableGenerator;

namespace Task3.Help
{
    public static class Table
    {
        public static ASCIITable Create(List<string> move)
        {
            List<string[]> lines = new List<string[]>();
            List<string> header = new List<string>();
            header.Add(" Helper ");
            header.AddRange(move);
            lines.Add(header.ToArray());
            for (int i = 0; i < move.Count; i++)
            {
                List<string> line = new List<string>();
                line.Add(move[i]);
                for (int j = 0; j < move.Count; j++)
                {
                    bool? gameResult = Game.CheckGameResult(i, j);
                    if (gameResult == null)
                    {
                        line.Add(" DRAW ");
                    }
                    else
                    {
                        line.Add(gameResult == true ? " WIN " : " LOSE ");
                    }
                }
                lines.Add(line.ToArray());
            }
            return new ASCIITable(lines);
        }
    }
}