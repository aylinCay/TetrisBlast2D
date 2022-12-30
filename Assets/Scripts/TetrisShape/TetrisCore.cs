using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisCore : MonoBehaviour,ITetrisCore
{
   public bool isClick { get; set; }
   public Vector3 gap;
   
   public void OnMouseDown()
   {
       isClick = true;

      
   }

   public void OnMouseDrag()
   {
       
   }
}
