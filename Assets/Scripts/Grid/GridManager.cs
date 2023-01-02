
using System;
using JetBrains.Annotations;


namespace TetrisBlast.Grid
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [ExecuteInEditMode]
    public class GridManager : MonoBehaviour
    {
        public static GridManager GlobalAccess { get; private set; } = null;
        [field: SerializeField] public GridData gridData { get; private set; } = new GridData();
        
        private GameObject currentGrid;

        public void Awake()
        {
            GlobalAccess = this;
        }

        public void ReBuild()
        {
            if (currentGrid != null)
                DestroyImmediate(currentGrid);

            CreateToGrid(gridData);
        }

        public void CreateToGrid(IGridData gridData)
        {
            var size = gridData.GetGridSize();
            var coreData = gridData.GetCoreData();
            var offset = gridData.gridOffset;
            var coreSize = coreData.size;
            var grid = new GameObject();
            grid.name = "Grid";
            grid.transform.position = Vector2.zero;

            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++)
                {
                    var pos = offset + new Vector2(coreSize.x * x, coreSize.y * y);
                    var inst = Instantiate(coreData.getGridCorePrefab, pos, Quaternion.identity);
                    inst.transform.SetParent(grid.transform);
                    inst.name = $"Grid {y} {x}";
                }
            }
        }

        [Serializable]
        public class GridData: IGridData
        {
            [field: SerializeField] public Vector2 gridSize { get; private set; } = new Vector2( 9f,9f);
            [field: SerializeField] public Vector2 gridOffset { get; } = new Vector2();
            [field: SerializeField]
            public Dictionary<int, List<GridCore>> storage { get; private set; } =
                new Dictionary<int, List<GridCore>>();


            [field: SerializeField] private GridCoreData _core_data = new GridCoreData();
            
            public Vector2 GetGridSize() => gridSize;
            public IGridCoreData GetCoreData() => _core_data;
            
            
            [Serializable]
            public class GridCoreData : IGridCoreData
            {
                [field: SerializeField] public Vector2 size { get; set; } = new Vector2();
                [field: SerializeField] [CanBeNull] public GameObject _grid_core = null;
                public GameObject getGridCorePrefab => _grid_core;
            }
        }

    }
    

}