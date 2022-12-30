using JetBrains.Annotations;

namespace TetrisBlast.Grid
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IGridCoreData
    {
        public Vector2 size { get; }
        [CanBeNull]public GameObject getGridCorePrefab { get; }
    }
}