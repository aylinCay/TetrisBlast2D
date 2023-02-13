using System;
using TetrisBlast.Grid;

namespace TetrisBlast.Vfx
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class VfxExplosion : MonoBehaviour
    {
        public GridColor color;
        public ParticleSystem vfx_Effect;

        public void Start()
        {
            VfxManager.GloballAccess.explosions.Add(color,this);
        }
    }

}