using System;
using JetBrains.Annotations;
using TetrisBlast.TetrisShapes;
using UnityEngine.Serialization;

namespace TetrisBlast.Grid
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GridCore : MonoBehaviour
    {
        [field: SerializeField] public Vector2 gridCorePosition { get; set; }  = new Vector2();
        [field: SerializeField] public bool isFull { get; set; } = false;
        [field: SerializeField] [CanBeNull] public TetrisCore shapeCore { get; set; } = null;

        public void Start()
        {
            
        }

        public void AddCore([CanBeNull] TetrisCore core)
        {
            var p = core != null;
            isFull = p;
            shapeCore = core;
            var grid_core_position = gridCorePosition;
            grid_core_position.y = core.position.y;
        }

        public void Reset()
        {
            isFull = false;
            shapeCore = null;
        }
    }
}