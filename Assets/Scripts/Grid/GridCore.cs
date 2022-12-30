using System;
using UnityEngine.Serialization;

namespace TetrisBlast.Grid
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GridCore : MonoBehaviour

    {
        public float Id { get; set; } = -1f;
        public bool isFull { get; } = false;
    }
}