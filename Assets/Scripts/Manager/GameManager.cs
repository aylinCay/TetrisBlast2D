using System;
using TetrisBlast.Grid;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
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
        public int higScore;
        public GameObject scorePanel;
        public TextMeshProUGUI newScore;
        public TextMeshProUGUI highScoreText;
        public TextMeshProUGUI scoreTextPanel;
        public AudioSource buttonSound;
       

        public int Score
        {
            get => GridManager.GlobalAccess.score;
        }
        public TextMeshProUGUI scoreText;
        
        public void NewShape()
        {
            if (tetris.heart < 2)
            {
                tetris.heart++;
                Destroy(tetris.createShape);
                tetris.CreateToShape();
                heartImage[tetris.heart].SetActive(false);
                buttonSound.Play();
            }
            else
            {
             ScoreUpdate();
            }
            
        }

        public void ScoreUpdate()
        {
            
            if (Score > higScore)
            {
                newScore.text = "NewScore";
                higScore = Score;
               PlayerPrefs.SetInt("High Score" , higScore); 

            }
            else
            {
                newScore.text = "GameOver";
            }

            highScoreText.text = PlayerPrefs.GetInt("High Score").ToString();
            scoreTextPanel.text = Score.ToString();
            scorePanel.SetActive(true);
        }

        public void RepLay()
        {
            buttonSound.Play();
            SceneManager.LoadScene(1);
        }

        public void Update()
        {
            scoreText.text = Score.ToString();
        }

        public void Start()
        {
            buttonSound = GetComponent<AudioSource>();
        }
    }

}