using System.Collections.Generic;
using System.Resources;
using UnityEngine;

/// <summary>
/// 사운드 관리 클래스.
/// BGM과 SFX 재생, 볼륨 조절, 설정 로드를 담당합니다.
/// </summary>
public class SoundManager : MonoBehaviour
{
    // PlayerPrefs에 저장될 키 상수
    const string BGM_VOLUME_KEY = "BGM_VOLUME";
    const string SFX_VOLUME_KEY = "SFX_VOLUME";
    const string BGM_ON_KEY = "BGM_ON";
    const string SFX_ON_KEY = "SFX_ON";

    // 리소스 경로 포맷
    const string SOUND_RESOURCE_PATH_FORMAT = "Sounds/{0}";

    ResourcesManager _resourcesManager; // 리소스 로드를 위한 ResourceManager
    AudioSource _bgmSource; // BGM 재생용 AudioSource
    List<AudioSource> _sfxSourceList; // SFX 재생용 AudioSource 리스트
    int _sfxSourceCount = 10; // 동시에 재생 가능한 SFX AudioSource의 수

    public float BGMVolume { get; private set; } // BGM 볼륨
    public float SFXVolume { get; private set; } // SFX 볼륨
    public bool BGMOn { get; private set; } // BGM 활성화 여부
    public bool SFXOn { get; private set; } // SFX 활성화 여부


    /// <summary>
    /// 초기화 작업을 수행합니다.
    /// AudioSource를 생성하고 설정을 로드합니다.
    /// </summary>
    public void Initialize(ResourcesManager resourcesManager)
    {
        _resourcesManager = resourcesManager;

        // BGM AudioSource 생성
        GameObject bgmGo = new GameObject("BGM");
        GameObject.DontDestroyOnLoad(bgmGo);
        _bgmSource = bgmGo.AddComponent<AudioSource>();
        _bgmSource.playOnAwake = false;
        _bgmSource.loop = true;
        _bgmSource.spatialBlend = 0.0f; // 2D 사운드로 설정(BGM은 거리에 무관하게 일정한 볼륨을 들린다.)

        // SFX AudioSource 리스트 초기화
        _sfxSourceList = new List<AudioSource>();
        for (int i = 0; i < _sfxSourceCount; i++)
        {
            GameObject sfxGo = new GameObject($"SFX_{i}");
            GameObject.DontDestroyOnLoad(sfxGo);
            AudioSource sfxAs = sfxGo.AddComponent<AudioSource>();
            sfxAs.playOnAwake = false;
            _sfxSourceList.Add(sfxAs);
        }

        // 설정 로드
        LoadSettings();
    }

    /// <summary>
    /// 사운드 설정을 로드합니다.
    /// </summary>
    private void LoadSettings()
    {
        BGMVolume = PlayerPrefs.GetFloat(BGM_VOLUME_KEY, 1.0f);
        SFXVolume = PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1.0f);
        BGMOn = PlayerPrefs.GetInt(BGM_ON_KEY, 1) == 1;
        SFXOn = PlayerPrefs.GetInt(SFX_ON_KEY, 1) == 1;

        _bgmSource.mute = !BGMOn;
        foreach (AudioSource sfxSource in _sfxSourceList)
        {
            sfxSource.mute = !SFXOn;
        }
    }

    /// <summary>
    /// BGM 볼륨을 설정합니다.
    /// </summary>
    /// <param name="volume">설정할 볼륨 값 (0.0 ~ 1.0)</param>
    public void SetBGMVolume(float volume)
    {
        BGMVolume = Mathf.Clamp01(volume);
        _bgmSource.volume = BGMVolume;
        PlayerPrefs.SetFloat(BGM_VOLUME_KEY, BGMVolume);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// SFX 볼륨을 설정합니다.
    /// </summary>
    /// <param name="volume">설정할 볼륨 값 (0.0 ~ 1.0)</param>
    public void SetSFXVolume(float volume)
    {
        SFXVolume = Mathf.Clamp01(volume);
        foreach (AudioSource sfxSource in _sfxSourceList)
        {
            sfxSource.volume = SFXVolume;
        }
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, SFXVolume);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// BGM 활성화 여부를 설정합니다.
    /// </summary>
    /// <param name="isOn">BGM 활성화 여부</param>
    public void ToggleBGM(bool isOn)
    {
        BGMOn = isOn;
        _bgmSource.mute = !BGMOn;
        PlayerPrefs.SetInt(BGM_ON_KEY, BGMOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// SFX 활성화 여부를 설정합니다.
    /// </summary>
    /// <param name="isOn">SFX 활성화 여부</param>
    public void ToggleSFX(bool isOn)
    {
        SFXOn = isOn;
        foreach (AudioSource sfxSource in _sfxSourceList)
        {
            sfxSource.mute = !SFXOn;
        }
        PlayerPrefs.SetInt(SFX_ON_KEY, SFXOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 경로를 통해 BGM을 재생합니다.
    /// </summary>
    /// <param name="path">BGM 파일 경로</param>
    public void PlayBGM(string path)
    {
        AudioClip clip = GetAudioClip(path);
        PlayBGM(clip);
    }

    /// <summary>
    /// 주어진 AudioClip으로 BGM을 재생합니다.
    /// </summary>
    /// <param name="clip">재생할 AudioClip</param>
    public void PlayBGM(AudioClip clip)
    {
        if (_bgmSource.clip == clip)
            return;

        _bgmSource.clip = clip;
        _bgmSource.volume = BGMVolume;
        _bgmSource.Play();
    }

    /// <summary>
    /// BGM 재생을 정지합니다.
    /// </summary>
    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    /// <summary>
    /// BGM 재생을 일시 정지합니다.
    /// </summary>
    public void PauseBGM()
    {
        _bgmSource.Pause();
    }

    /// <summary>
    /// BGM 재생을 다시 시작합니다.
    /// </summary>
    public void UnPauseBGM()
    {
        _bgmSource.UnPause();
    }
    /// <summary>
    /// 경로를 통해 SFX를 재생합니다.
    /// </summary>
    /// <param name="path">SFX 파일 경로</param>
    /// <param name="is3D">3D 사운드 여부</param>
    /// <param name="position">3D 사운드 위치</param>
    public void PlaySFX(string path, bool is3D = false, Vector3 position = default)
    {
        AudioClip clip = GetAudioClip(path);
        PlaySFX(clip, is3D, position);
    }

    /// <summary>
    /// 주어진 AudioClip으로 SFX를 재생합니다.
    /// </summary>
    /// <param name="clip">재생할 AudioClip</param>
    /// <param name="is3D">3D 사운드 여부</param>
    /// <param name="position">3D 사운드 위치</param>
    public void PlaySFX(AudioClip clip, bool is3D = false, Vector3 position = default)
    {
        if (!SFXOn) return;

        AudioSource availableSource = GetAvailableSFXSource();
        if (availableSource != null)
        {
            availableSource.clip = clip;
            availableSource.volume = SFXVolume;
            availableSource.spatialBlend = is3D ? 1.0f : 0.0f; // 3D 여부 설정
            if (is3D)
            {
                availableSource.transform.position = position;
            }
            availableSource.Play();
        }
    }

    /// <summary>
    /// 사용 가능한 SFX AudioSource를 가져옵니다.
    /// </summary>
    /// <returns>사용 가능한 AudioSource</returns>
    AudioSource GetAvailableSFXSource()
    {
        foreach (AudioSource source in _sfxSourceList)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }
        return null;
    }

    /// <summary>
    /// 경로를 통해 AudioClip을 로드합니다.
    /// </summary>
    /// <param name="path">오디오 파일 경로</param>
    /// <returns>로드된 AudioClip</returns>
    AudioClip GetAudioClip(string path)
    {
        return _resourcesManager.LoadResource<AudioClip>(string.Format(SOUND_RESOURCE_PATH_FORMAT, path));
    }

    /// <summary>
    /// 모든 BGM 및 SFX 재생을 정리합니다.
    /// </summary>
    public void Clear()
    {
        _bgmSource.Stop();
        _bgmSource.clip = null;
        foreach (AudioSource audioSource in _sfxSourceList)
        {
            audioSource.Stop();
            audioSource.clip = null;
        }
    }
}