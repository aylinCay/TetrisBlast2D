namespace TetrisBlast.Grid
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IGridData
    {
        public Vector2 gridOffset { get; }

        public Vector2 GetGridSize();

        public IGridCoreData GetCoreData();
        
        public Dictionary<int, List<GridCore>> storage { get; set; }

    }
}