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
        public static GameManager GloballAccess;
        public List<GameObject> heartImage;
        public TetrisStorage tetris;
        public GameObject scorePanel;
        public TextMeshProUGUI newScore;
        public TextMeshProUGUI highScoreText;
        public TextMeshProUGUI scoreTextPanel;
        public AudioSource buttonSound;
        public TextMeshProUGUI secondText;
        public TextMeshProUGUI scoreText;
        public int higScore;
        private int _score;
        private float _second = 120;
        
        public void Awake()
        {
            GloballAccess = this;
        }

        public void Start()
        {
            buttonSound = GetComponent<AudioSource>();

        }

        public void Update()
        {
           CoutDown(1);
        }

        public void NewShape()
        {
            if (tetris.heart < 2)
            {
                tetris.heart++;
                TetrisStorage.GloballAccess.shapeStroge.Remove(tetris.createShape);
                Destroy(tetris.createShape);
                tetris.CreateToShape();
                heartImage[tetris.heart].SetActive(false);
                buttonSound.Play();
                CoutDown(10);
            }
            else
            {
                ScoreUpdate();
            }

            if (_second < 1)
            {
                ScoreUpdate();
            }
            
        }

        public void ScoreUpdate()
        {
            
            if (_score > higScore)
            {
                newScore.text = "NewScore";
                higScore = _score;
               PlayerPrefs.SetInt("High Score" , higScore); 

            }
            else
            {
                newScore.text = "GameOver";
            }

            highScoreText.text = PlayerPrefs.GetInt("High Score").ToString();
            scoreTextPanel.text = _score.ToString();
            scorePanel.SetActive(true);
            _second = 0;

        }

        public void NextButton1()
        {
            buttonSound.Play();
            SceneManager.LoadScene(2);
        }

        public void NextButton2()
        {
            SceneManager.LoadScene(3);
        }

        public void CoutDown(float second)
        {
            _second -= Time.deltaTime * second;
            secondText.text = _second.ToString("0");
        }

        public void AddScore(int score)
        {
            _score += score;
            scoreText.text = _score.ToString();
        }
    }

}