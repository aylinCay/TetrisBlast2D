

using TetrisBlast.Grid;
using Unity.VisualScripting;

namespace TetrisBlast.TetrisShapes
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class TetrisCore : MonoBehaviour
    {

        public bool isPivotCore;
        public TetrisShape parent;
        public SpriteRenderer coreRenderer;

        public Vector2 position;

        public GridCore currentGridCore;

        public Coordinates gridInfo;
            
        RaycastHit2D hit;
        
        private LayerMask mask => ShapeManager.GloballAccess.coreLayerMask;

      

        private void Start()
        {
            coreRenderer = GetComponent<SpriteRenderer>();
            coreRenderer.sprite = parent.sprite;
            if (parent != null)
            {
                if (!parent.cores.Contains(this))
                    parent.cores.Add(this);
                
                if (!parent.isSelected && parent.isLocated)
                {
                    GridCheck();
                }
            }
        }

        private void Update()
        {
            if (parent.isSelected && !parent.isLocated)
            {
                GridCheck();
            }

            if (parent.isSelected)
            {
                coreRenderer.sortingOrder = 2;
            }
        }

        public void GridCheck()
        {
            hit = Physics2D.Raycast(transform.position, Vector3.back, 10f, mask);

            if (hit.collider != null)
            {
                if (CheckToGrid())
                {
                    Success();
                    return;
                }

                Fail();
                return;
            }
            Fail();
        }

        public bool CheckToGrid()
        {
            GridCore nCore = null;

            if (hit.transform.TryGetComponent<GridCore>(out nCore) && !nCore.isFull)
            {
                
                currentGridCore = nCore;
                return true;
            }
            
            return false;
        }

        public void Success()
        {
            coreRenderer.sortingOrder = 1;
            parent.NotifyCore(this, true);
        }

        public void Fail()
        {
            parent.NotifyCore(this, false);
        }

        public void SetToGrid()
        {
            if (parent.isLocated)
            {
                if (!currentGridCore.isFull)
                {
                    currentGridCore.AddCore(this);
                    parent.cordinatesInfo.Add(gridInfo);
                    coreRenderer.sortingOrder = 1;
                }
            }
           
        }

        public void Reset()
        {
            if (currentGridCore != null)
            {
                if (currentGridCore.shapeCore == this)
                {
                    currentGridCore.AddCore(null);
                    parent.cordinatesInfo.Remove(gridInfo);
                }
            }
        }

        public void Select()
        {
            if (parent != null)
            {
                if (!parent.isLocated)
                {
                    parent.CoresReset();
                }
                
                parent.OnSelect(true, this);
            }
        }
    }

    }

