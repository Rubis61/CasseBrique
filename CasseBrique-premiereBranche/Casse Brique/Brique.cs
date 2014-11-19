using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Casse_Brique
{
    public class Brique : GameObject
    {
        public Brique(Game1 game, int vitesse, int dirX, int dirY) 
            : base(game, vitesse, dirX, dirY)
        {

        }
    }
}