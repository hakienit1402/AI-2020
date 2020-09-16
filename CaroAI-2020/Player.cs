using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaroAI_2020
{
    public class Player
    {
        private String name;
        public string Name { get => name; set=> name = value; }
 
        private Image color;
        public Image Color { get => color; set => color = value; }
         public Player(String name, Image color)
         {
            this.name = name;
            this.color = color;
         }

        public Player()
        {
        }
    }
}
