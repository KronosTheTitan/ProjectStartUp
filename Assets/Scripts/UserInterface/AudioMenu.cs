using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    
    [Header("Master")]
    [SerializeField] private TMP_Text masterVolumeNumber;
    [SerializeField] private Slider masterVolumeSlider;
    public void SetMasterVolume()
    {
        masterVolumeNumber.text = masterVolumeSlider.value.ToString();
        mixer.SetFloat("masterVolume", masterVolumeSlider.value - 80);
    }
    
    [Header("Music")]
    [SerializeField] private TMP_Text musicVolumeNumber;
    [SerializeField] private Slider musicVolumeSlider;
    public void SetMusicVolume()
    {
        musicVolumeNumber.text = musicVolumeSlider.value.ToString();
        mixer.SetFloat("musicVolume", musicVolumeSlider.value - 80);
    }
    
    [Header("Effects")]
    [SerializeField] private TMP_Text effectsVolumeNumber;
    [SerializeField] private Slider effectsVolumeSlider;
    public void SetEffectsVolume()
    {
        effectsVolumeNumber.text = effectsVolumeSlider.value.ToString();
        mixer.SetFloat("effectsVolume", effectsVolumeSlider.value - 80);
    }
    
    [Header("Voice")]
    [SerializeField] private TMP_Text voiceVolumeNumber;
    [SerializeField] private Slider voiceVolumeSlider;
    public void SetVoiceVolume()
    {
        voiceVolumeNumber.text = voiceVolumeSlider.value.ToString();
        mixer.SetFloat("voiceVolume", voiceVolumeSlider.value - 80);
    }
}