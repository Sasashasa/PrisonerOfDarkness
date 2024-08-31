using UnityEngine;

[CreateAssetMenu]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] PlayerShoot;
    public AudioClip[] BulletHit;
    public AudioClip[] EnemyDead;
    public AudioClip[] PlayerReloading;
}