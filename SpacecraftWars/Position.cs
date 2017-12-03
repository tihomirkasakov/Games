namespace SpacecraftWars
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Position
    {
        public int X;
        public int Y;
        public string Elements;

        public Position(int x, int y,string elements)
        {
            this.X = x;
            this.Y = y;
            this.Elements = elements;
        }
    }
}
