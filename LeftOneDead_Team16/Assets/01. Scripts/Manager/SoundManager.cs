using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : Singleton<SoundManager>
{
    // 볼륨,피치 조절
    [SerializeField][Range(0f, 1f)] private float masterVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
    [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
    [SerializeField][Range(0f, 1f)] private float musicVolume;

    public AudioClip[] bgmCilps; // 씬별 bgm

    private AudioSource musicAudioSource;

    [SerializeField] private AudioMixer masterMixer;

    // 사운드소스 프리팹
    public SoundSource soundSourcePrefab;

    public enum SoundType
    {
        BGM,
        SFX
    }

    protected override void Awake()
    {
        base.Awake();

        musicAudioSource = GetComponent<AudioSource>();
        musicAudioSource.volume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);
        musicAudioSource.loop = true;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBGM(scene.buildIndex);
    }

    public void PlayBGM(int sceneIdx)
    {
        if (musicAudioSource.clip == bgmCilps[sceneIdx]) return; // 중복 재생 방지

        musicAudioSource.Stop(); // 기존 BGM 정지
        musicAudioSource.clip = bgmCilps[sceneIdx];
        musicAudioSource.Play();
    }
    public static void PlayClip(AudioClip clip)
    {
       SoundSource obj = Instantiate(Instance.soundSourcePrefab);
       SoundSource soundSource = obj.GetComponent<SoundSource>();
       soundSource.Play(clip, Instance.soundEffectVolume, Instance.soundEffectPitchVariance);
    }
    // master 볼륨 저장
    public void SetMasterVolume(float volume)
    {
       
        float dB = Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1f)) * 20f;
        masterMixer.SetFloat("MasterVolume", dB);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    // bgm 볼륨 저장
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicAudioSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
        PlayerPrefs.Save();
    }
    // sfx 볼륨 저장
    public void SetSFXVolume(float volume)
    {
        soundEffectVolume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }
}
