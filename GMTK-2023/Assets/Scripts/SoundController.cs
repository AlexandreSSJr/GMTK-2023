using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    private bool soundState = true;
    [SerializeField] private AudioSource backgroundMusic;

    [SerializeField] private Sprite soundOn;
    [SerializeField] private Sprite soundOff;

    [SerializeField] private Image muteImage;
    public void soundOnOff()
    {
        soundState = !soundState;
        backgroundMusic.enabled = soundState;

        if (soundState)
        {
            muteImage.sprite = soundOn;
        }
        else
        {
            muteImage.sprite = soundOff;
        }
    }

    public void MusicVolume (float value)
    {
        backgroundMusic.volume = value;
    }
}
