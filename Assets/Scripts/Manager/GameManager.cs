using TetrisBlast.Grid;
using TMPro;
using UnityEngine.UI;

namespace TetrisBlast.Manager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    

    public class GameManager : MonoBehaviour
    {
        public List<GameObject> heartImage;
        public TetrisStorage tetris;

        public int Score
        {
            get => GridManager.GlobalAccess.score;
        }
        public TextMeshProUGUI scoreText;
        
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

        public void Update()
        {
            scoreText.text = Score.ToString();
        }
    }

}