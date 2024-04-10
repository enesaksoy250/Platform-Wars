using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class SoundControl : MonoBehaviour
{

    [SerializeField] Slider soundSlider;
    [SerializeField] AudioSource gameMusicSource;
    [SerializeField] Slider allSoundSlider;

    void Start()
    {

        if (!PlayerPrefs.HasKey("Sound"))
        {

            PlayerPrefs.SetFloat("Sound", 1);

        }
      
        gameMusicSource.volume = PlayerPrefs.GetFloat("Sound");
        soundSlider.value = gameMusicSource.volume;
       // soundSlider.value = PlayerPrefs.GetInt("Sound");
        soundSlider.onValueChanged.AddListener(delegate { AdjustVolume(); });
        allSoundSlider.onValueChanged.AddListener(delegate { AdjustVolume2(); });
    }

    void AdjustVolume()
    {
        
        gameMusicSource.volume = soundSlider.value;
        PlayerPrefs.SetFloat("Sound",gameMusicSource.volume);
    }

    void AdjustVolume2()
    {

        gameMusicSource.volume = allSoundSlider.value;
        soundSlider.value = allSoundSlider.value;

    }

    public void VolumeDownButton()
    {

        gameMusicSource.volume -= 0.1f;
        soundSlider.value -= 0.1f;
        PlayerPrefs.SetFloat("Sound", gameMusicSource.volume);
    }

    public void VolumeBoostButton() 
    {

        gameMusicSource.volume += 0.1f;
        soundSlider.value += 0.1f;
        PlayerPrefs.SetFloat("Sound", gameMusicSource.volume);
    }

    public void StopGameMusic()
    {

        gameMusicSource.Stop();

    }


}
