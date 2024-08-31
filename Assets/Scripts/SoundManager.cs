using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private AudioClipRefsSO _audioClipRefsSO;

    private float _masterVolume;
    private float _soundVolume;

    private void Awake()
    {
        Instance = this;

        _masterVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.MasterVolume, 1f);
        _soundVolume = PlayerPrefs.GetFloat(PlayerPrefsKeys.SoundEffectsVolume, 1f);
    }
    
    private void Start()
    {
        PlayerShooting.Instance.OnShoot += PlayerShooting_OnShoot;
        PlayerShooting.Instance.OnStartReloading += PlayerShooting_OnStartReloading;
    }

    public void PlayBulletHitSound(Vector3 position)
    {
        PlaySound(_audioClipRefsSO.BulletHit, position);
    }

    public void PlayEnemyDeadSound(Vector3 position)
    {
        PlaySound(_audioClipRefsSO.EnemyDead, position);
    }

    private void PlayerShooting_OnShoot(object sender, EventArgs e)
    {
        PlayerShooting playerShooting = PlayerShooting.Instance;
        PlaySound(_audioClipRefsSO.PlayerShoot, playerShooting.gameObject.transform.position);
    }

    private void PlayerShooting_OnStartReloading(object sender, EventArgs e)
    {
        PlayerShooting playerShooting = PlayerShooting.Instance;
        PlaySound(_audioClipRefsSO.PlayerReloading, playerShooting.gameObject.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * _masterVolume * _soundVolume);
    }
}