using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{

    [Header("Attacking SFX")]
    [SerializeField] AudioClip attackingClip;
    [SerializeField, Range(0, 1)] float attackingVolume = 1f;

    [Header("Shooting SFX")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField, Range(0, 1)] float shootingVolume = 1f;

    [Header("Damage SFX")]
    [SerializeField] AudioClip damageClip;
    [SerializeField, Range(0, 1)] float damageVolume = 1f;

    [Header("Jumping SFX")]
    [SerializeField] AudioClip jumpingClip;
    [SerializeField, Range(0, 1)] float jumpingVolume = 1f;
    [SerializeField] AudioClip landingClip;
    [SerializeField, Range(0, 1)] float landingVolume = 1f;

    [Header("Charging SFX")]
    [SerializeField] AudioClip chargingClip;
    [SerializeField, Range(0, 1)] float chargingVolume = 1f;


    public void PlayAttackingSFX()
    {
        PlayAudioClip(attackingClip, attackingVolume);
    }

    public void PlayShootingSFX()
    {
        PlayAudioClip(shootingClip, shootingVolume);
    }

    public void PlayDamageSFX()
    {
        PlayAudioClip(damageClip, damageVolume);
    }

    public void PlayJumpingSFX()
    {
        PlayAudioClip(jumpingClip, jumpingVolume);
    }

    public void PlayLandingSFX()
    {
        PlayAudioClip(landingClip, landingVolume);
    }

    public void PlayChargingSFX()
    {
        PlayAudioClip(chargingClip, chargingVolume);
    }

    void PlayAudioClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
        }
    }


}
