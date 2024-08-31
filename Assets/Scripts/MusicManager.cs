using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    
    private AudioSource _audioSource;

    private void Awake()
    {
        Instance = this;
        
        _audioSource = GetComponent<AudioSource>();

        float _masterVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.MasterVolume, 1f);
        float _musicVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.MusicVolume, 1f);
        
        _audioSource.volume = _masterVolume * _musicVolume;
    }

    public void ChangeMusicVolume()
    {
        float _masterVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.MasterVolume, 1f);
        float _musicVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.MusicVolume, 1f);
        
        _audioSource.volume = _masterVolume * _musicVolume;
    }
}