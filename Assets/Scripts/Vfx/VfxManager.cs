using System;
using System.Collections;
using System.Collections.Generic;
using TetrisBlast.Grid;
using TetrisBlast.Vfx;
using UnityEngine;

public class VfxManager : MonoBehaviour
{
    public static VfxManager GloballAccess;
    public GridColor selected = GridColor.Red;
    public AudioSource explosionSound;

    public Dictionary<GridColor, VfxExplosion> explosions = new Dictionary<GridColor, VfxExplosion>();
    public void Awake()
    {
        GloballAccess = this;
        
    }

    public void Start()
    {
        explosionSound = GetComponent<AudioSource>();
    }

    public void Explosion(ExplosionDirection dir , Vector3 pos)
    {
        if (explosions.ContainsKey(selected))
        {
            var s = explosions[selected];
            s.transform.position = new Vector3(pos.x, pos.y, s.transform.position.z);
            if (dir == ExplosionDirection.Vertical)
            {
                s.transform.rotation = Quaternion.Euler(-90,0,0);
                
            }
            else
            {
                s.transform.rotation = Quaternion.Euler(0,-90,-90);
            }
            s.vfx_Effect.Play();
        }
    }
    
}

public enum ExplosionDirection
{
    Vertical = 0,
    Horizontal = 1
};
