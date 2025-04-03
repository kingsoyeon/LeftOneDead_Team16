using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingUI : PopupUI
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    [SerializeField] private Button[] muteButton;

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);
        bgmSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        bgmSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMasterVolume(float volume) { SoundManager.Instance.SetMasterVolume(volume); }
    public void SetSFXVolume(float volume) { SoundManager.Instance.SetSFXVolume(volume); }
    public void SetMusicVolume(float volume) { SoundManager.Instance.SetMusicVolume(volume); }

    
    // 뮤트 기능 구현 필요
}
