using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._3DGamekitLite.Scripts.Game.Player
{
    public class Inventory : MonoBehaviour
    {
        private int Food=0, Water=0, MedKit=0, Pikaxe=0;

        public int get(string item) {
            if(item == "Food")
                return Food;
            if (item == "Water")
                return Water;
            if (item == "MedKit")
                return MedKit;
            if (item == "Pikaxe")
                return Pikaxe;

            return -1;

        }
        public void set(string item, int value)
        {
            if (item == "Food")
                Food = value;
            if (item == "Water")
                Water = value;
            if (item == "MedKit")
                MedKit = value;
            if (item == "Pikaxe")
                Pikaxe = value;

        }

    }
}
