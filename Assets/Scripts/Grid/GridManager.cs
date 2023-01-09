
using System;
using System.Drawing;
using JetBrains.Annotations;
using TetrisBlast.TetrisShapes;
using UnityEngine.Analytics;


namespace TetrisBlast.Grid
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GridManager : MonoBehaviour
    {
        public static GridManager GlobalAccess { get; private set; } = null;
        [field: SerializeField] public GridData gridData { get; private set; } = new GridData();
        [field: SerializeField] public GridCore gridcore { get; private set; } = new GridCore();
        public GameObject grid;

        List<GridCore> currentCoreList = new List<GridCore>();

        private GameObject currentGrid;

        public void Awake()
        {
            GlobalAccess = this;

        }

        public void Start()
        {
        }

        public void Update()
        {
            FindToCompletedLine(gridcore);
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
                    var core = inst.GetComponent<GridCore>();
                    currentCoreList.Add(core);
                }

                gridData.Ystorage.Add(y, currentCoreList);
            }
        }

        public void FindToCompletedLine(GridCore core)
        {
            if (core.coordinates.Xcordinates.Contains(0))
            {
                if (core.coordinates.Xcordinates.Count == 8)
                {
                    core.gameObject.SetActive(false);
                }
            }



        }
    }




    [Serializable]
        public class GridData: IGridData
        {
            [field: SerializeField] public Vector2 gridSize { get; private set; } = new Vector2( 9f,9f);
            [field: SerializeField] public Vector2 gridOffset { get; } = new Vector2();
           

            [field: SerializeField] public Dictionary<int, List<GridCore>> Ystorage { get; set; } =
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
    

