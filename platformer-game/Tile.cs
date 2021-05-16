using System;
using Raylib_cs;

namespace platformer_game
{
    public class Tile
    {
        public Rectangle rec;
        // new Rectange(x, y, w, h);

        public string type;
        // "grass", "dirt"

        public bool hasEnemy = false;

        public Tile(int x, int y, int w, int h, string t)
        {
            rec = new Rectangle(x, y, w, h);
            type = t;
        }
    }
}
