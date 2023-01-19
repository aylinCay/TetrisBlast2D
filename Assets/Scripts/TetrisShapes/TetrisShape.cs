using TetrisBlast.Grid;

namespace TetrisBlast.TetrisShapes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;

    public class TetrisShape : MonoBehaviour
    {
        public static TetrisShape GloballAccess { get; private set; } = null;
        public List<TetrisCore> cores = new List<TetrisCore>();
        public List<TetrisCore> successCore = new List<TetrisCore>();
        public List<TetrisCore> failCore = new List<TetrisCore>();
        public List<Coordinates> cordinatesInfo = new List<Coordinates>();

        public bool isSelected;
        public Vector3 moveOffset;
        public GridCore mainGrid = null;
        public bool isLocated;
        public bool isSettleDown = false;
        public Color shapeColor;
        public Color linear;
        private Vector3 selectedPosition;
        public Vector3 pos;
        public Color allShapeColor;

        public void Awake()
        {
            GloballAccess = this;
            linear = shapeColor.linear;
            
        }

        public void Start()
        {
            if (mainGrid != null && failCore.Count == 0)
            {
                Complete();
            }
        }

        public void NotifyCore(TetrisCore core, bool isSuccess = false)
        {
            var isExitsSuccess = successCore.Contains(core);
            var isExitsFail = failCore.Contains(core);

            if (isSuccess)
            {
                if (core.isPivotCore) mainGrid = core.currentGridCore;
                if (!isExitsSuccess) successCore.Add(core);
                if (isExitsFail) failCore.Remove(core);
                return;
            }

            if (core.isPivotCore) mainGrid = null;
            if (isExitsSuccess) successCore.Remove(core);
            if (!isExitsFail) failCore.Add(core);
        }
        
        private void Update()
        {
            if (isSelected)
            {
                var inputPosition = Input.mousePosition;
                inputPosition.z = Camera.main.farClipPlane;
                 pos = Camera.main.ScreenToWorldPoint(inputPosition);
                pos.z = 0f;

                transform.position = Vector3.Lerp(transform.position, pos + moveOffset, .1f);

                if (Input.GetButtonUp("Fire1"))
                {
                    isSelected = false;
                    if (mainGrid != null && failCore.Count == 0)
                    {
                        transform.position = mainGrid.transform.position;
                        isLocated = true;
                        isSettleDown = true;
                        Complete();
                        allShapeColor = this.shapeColor.linear;
                        GridManager.GlobalAccess.FindsCompleteGridCore(cordinatesInfo.ToArray());
                        return;
                    }
                    
                    transform.position = selectedPosition;
                }
            }
           
        }

        public void Complete()
        {
            foreach (var VARIABLE in cores)
            {
                VARIABLE.SetToGrid();
            }
        }

        public void CoresReset()
        {
            isLocated = false;
            foreach (var VARIABLE in cores)
            {
                VARIABLE.Reset();
            }
        }

        public void OnSelect(bool isSelect, TetrisCore core)
        {
            if (isLocated)
            return;
            
            isSelected = isSelect;
            selectedPosition = transform.position;
            //moveOffset = (selectedPosition - pos);
            //moveOffset.x = 0f;
            //moveOffset.y = moveOffset.y == 0 ? 1f : Mathf.Abs(moveOffset.y);
            moveOffset.y = 1f;
            moveOffset.x = 0;
            Debug.Log("selected" + selectedPosition.y + "core" + core.transform.position.y + "move" + moveOffset.y);
           
        }

    }

}