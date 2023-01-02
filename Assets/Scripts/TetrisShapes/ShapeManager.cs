namespace TetrisBlast.TetrisShapes
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [UnityEngine.ExecuteInEditMode]
    public class ShapeManager : MonoBehaviour
    {
        public static ShapeManager GloballAccess { get; private set; } = null;
        public LayerMask coreLayerMask;
        public LayerMask mask;


        private void Awake()
        {
            GloballAccess = this;
        }
        
        public void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                var mousePosition = Input.mousePosition;
                mousePosition.z = Camera.main.nearClipPlane;
                var pos = Camera.main.ScreenToWorldPoint(mousePosition);
                var hit = Physics2D.Raycast(pos, Vector3.back, 50f, mask);

                if (hit.collider != null)
                {
                    var shapeCore = hit.transform.GetComponent<TetrisCore>();
                    shapeCore.Select();
                }
            }

            if (TetrisShape.GloballAccess.isSettleDown)
            {
              TetrisStorage.GloballAccess.CreateToShape();
            }
            
        }
    }
}