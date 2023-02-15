using UnityEngine.UI;

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
        public GameObject Onaudio;
        public GameObject OffAudio;
        public AudioSource buttonSound;
        public bool isActive;

        public void Start()
        {
            isActive = true;
            buttonSound = GetComponent<AudioSource>();
        }

        public void PlayButton()
        {
            SceneManager.LoadScene(1);
            buttonSound.Play();
        }

        public void SoundButton()
        {
            audioButton.transform.DOMoveX(900f, 1);
            buttonSound.Play();
        }

        public void AudioButton()
        {
            buttonSound.Play();
            if (isActive)
            {
                Onaudio.SetActive(false);
                OffAudio.SetActive(true);
                AudioListener.volume = 0f;
                isActive = false;
            }
            else
            {
                Onaudio.SetActive(true);
                OffAudio.SetActive(false);
                AudioListener.volume = 1f;
                isActive = true;
            }

        }
    }

}