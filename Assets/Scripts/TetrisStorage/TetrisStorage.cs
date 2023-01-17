using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TetrisStorage : MonoBehaviour
{
    [field: SerializeField] public static TetrisStorage  GloballAccess { get; private set; } = null;
   [field: SerializeField] public List<GameObject> tetrisPrefabs = new List<GameObject>();
   [field: SerializeField] public List<GameObject> shapeStroge = new List<GameObject>();

    [field: SerializeField] public GameObject shape;

    public void Awake()
    {
        GloballAccess = this;
    }

    public void Start()
    {
        CreateToShape();
    }
    
    public void CreateToShape()
   {
       var randIndex = Random.Range(0, tetrisPrefabs.Count);
       shape = tetrisPrefabs[randIndex];
        shapeStroge.Add(Instantiate(shape,transform.position,shape.transform.rotation));
   }
    
}
