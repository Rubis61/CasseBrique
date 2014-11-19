﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Casse_Brique
{
    public class Brique : GameObject
    {
        public bool IsInfinite { get; set; }
        public int Vie { get; set; }
        //public Bonus Bonus { get; set; }

        public Brique(Game1 game, int vitesse, int dirX, int dirY) 
            : base(game, vitesse, dirX, dirY)
        {
            
        }
    }
}