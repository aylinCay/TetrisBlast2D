namespace TetrisBlast.Manager
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.Serialization;


    public class MenuManager : MonoBehaviour
    {
        public GameObject audioButton;

        public void Start()
        {

        }

        public void PlayButton()
        {
            SceneManager.LoadScene(1);
        }

        public void SoundButton()
        {
            audioButton.transform.DOMoveX(2, 1);
        }

        public void AudioButton()
        {

        }
    }

}