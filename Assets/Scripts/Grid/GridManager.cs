
using System;
using System.Drawing;
using System.Linq;
using System.Xml.XPath;
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
        
        private GameObject currentGrid;
       
        public void Awake()
        {
            GlobalAccess = this;
            CreateToGrid(gridData);

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
            var listgrids = new List<GridCore>();
            
                for (int x = 0; x < size.x; x++)
                {
                    var pos = offset + new Vector2(coreSize.x * x, coreSize.y * y);
                    var inst = Instantiate(coreData.getGridCorePrefab, pos, Quaternion.identity);
                    inst.transform.SetParent(grid.transform);
                    inst.name = $"Grid {y} {x}";
                    var gridInfo = inst.GetComponent<GridCore>();
                    gridInfo.info = new Coordinates(y, x);

                    listgrids.Add(gridInfo);
                }

                gridData.storage.Add(y, listgrids);
            }
        }

        public void FindsCompleteGridCore(params Coordinates[] coordinates)
        {
            var xCoordinates = new List<int>();
            var yCoordinates = new List<int>();

            foreach (var cordinate in coordinates)
            {
                if(!yCoordinates.Contains(cordinate.key)) yCoordinates.Add(cordinate.key);
                if(!xCoordinates.Contains(cordinate.orderX)) xCoordinates.Add(cordinate.orderX);
            }
            
            CheckGridCoreWithX(xCoordinates);
            CheckGridCoreWithY(yCoordinates);
        }

        public void CheckGridCoreWithY(List<int> yCoordinates)
        {
            
            for (int key = 0; key < gridData.storage.Count; key++)
            {
                if (yCoordinates.Contains(key))
                {
                var isLineTheCompleted = false;

                    for (int order = 0; order < gridData.storage[key].Count; order++)
                    {
                        isLineTheCompleted = true;
                        
                        if (!gridData.storage[key][order].isFull)
                        {
                            isLineTheCompleted = false;
                            break;
                            
                        }
                    }

                    if (isLineTheCompleted)
                    {
                        LineExplosion(key);
                    }
                }
                
              
            }
            
        }

        public void CheckGridCoreWithX(List<int> xCoorditanes)
        {
            var completedList = new Dictionary<int, List<GridCore>>();

            foreach (var coreId in xCoorditanes)
            {
                if (!completedList.ContainsKey(coreId))
                {
                    completedList.Add(coreId , new List<GridCore>());
                }

                foreach (var line in gridData.storage.Values)
                {
                    if (line.Count > coreId)
                    {
                        completedList[coreId].Add(line[coreId]);
                    }
                }
            }

            if (completedList.Count > 0)
            {
                foreach (var currentKey in xCoorditanes)
                {
                    var list = completedList[currentKey];

                    foreach (var gridCore in list )
                    {
                        if (!gridCore.isFull)
                        {
                            completedList.Remove(currentKey);
                            break;
                        }
                    }
                }
            }
            
            RowExplosion(completedList);

        }
        
        void LineExplosion(int key)
        {
            var selected = gridData.storage[key];
            foreach (var VARIABLE in selected)
            {
                Destroy(VARIABLE.shapeCore.gameObject);
                VARIABLE.isFull = false;
            }
        }

        void RowExplosion(Dictionary<int, List<GridCore>> grids)
        {
            foreach (var VARIABLE in grids)
            {
                if (VARIABLE.Value.Count >= 9)
                {
                    foreach (var grid in VARIABLE.Value)
                    {
                        Destroy(grid.shapeCore.gameObject);
                        grid.isFull = false;
                    }
                }
            }
        }
       
    }




    [Serializable]
        public class GridData: IGridData
        {
            [field: SerializeField] public Vector2 gridSize { get; private set; } = new Vector2( 9f,9f);
            [field: SerializeField] public Vector2 gridOffset { get; } = new Vector2();
           

            [field: SerializeField] public Dictionary<int, List<GridCore>> storage { get; set; } =
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
    

