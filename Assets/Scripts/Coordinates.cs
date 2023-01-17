using System;
using TetrisBlast.Grid;

namespace TetrisBlast
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class Coordinates
    {
        public int key;
        public int orderX;

        public Coordinates(int key, int x)
        {
            this.key = key;
            orderX = x;
        }
    }
}