namespace TetrisBlast.Manager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        public List<GameObject> heartImage;
        public TetrisStorage tetris;
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void NewShape()
        {
            if (tetris.heart < 3)
            {
                Destroy(tetris.createShape);
                tetris.CreateToShape();
                heartImage[tetris.heart].SetActive(false);
                tetris.heart++;
            }
        }
    }

}