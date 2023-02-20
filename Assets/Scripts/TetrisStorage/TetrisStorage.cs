using System;
using System.Collections;
using System.Collections.Generic;
using TetrisBlast.Grid;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TetrisStorage : MonoBehaviour
{
    [field: SerializeField] public static TetrisStorage  GloballAccess { get; private set; } = null;
   [field: SerializeField] public List<GameObject> tetrisPrefabs = new List<GameObject>();
   [field: SerializeField] public List<GameObject> shapeStroge = new List<GameObject>();

    [field: SerializeField] public GameObject shape;
    [field: SerializeField] public GameObject createShape;
    public int heart = 0;
    public List<GameObject> heartImage;
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
       createShape = Instantiate(shape, transform.position, shape.transform.rotation);
       shapeStroge.Add(createShape);
   }

   
    
}
