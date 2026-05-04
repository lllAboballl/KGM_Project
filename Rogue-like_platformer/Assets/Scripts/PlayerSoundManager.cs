using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{

    [Header("Slashing SFX")]
    [SerializeField] AudioClip slashingClip;
    [SerializeField, Range(0, 1)] float slashingVolume = 1f;

    [Header("Shooting SFX")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField, Range(0, 1)] float shootingVolume = 1f;

    [Header("Damage SFX")]
    [SerializeField] AudioClip damageClip;
    [SerializeField, Range(0, 1)] float damageVolume = 1f;

    [Header("Walking SFX")]
    [SerializeField] AudioClip walkingClip;
    [SerializeField, Range(0, 1)] float walkingVolume = 1f;

    [Header("Jumping SFX")]
    [SerializeField] AudioClip jumpingClip;
    [SerializeField, Range(0, 1)] float jumpingVolume = 1f;
    [SerializeField] AudioClip landingClip;
    [SerializeField, Range(0, 1)] float landingVolume = 1f;

    public void PlaySlashingSFX()
    {
        PlayAudioClip(slashingClip, slashingVolume);
    }

    public void PlayShootingSFX()
    {
        PlayAudioClip(shootingClip, shootingVolume);
    }

    public void PlayDamageSFX()
    {
        PlayAudioClip(damageClip, damageVolume);
    }

    public void PlayWalkingSFX()
    {
        PlayAudioClip(walkingClip, walkingVolume);
    }

    public void PlayJumpingSFX()
    {
        PlayAudioClip(jumpingClip, jumpingVolume);
    }

    public void PlayLandingSFX()
    {
        PlayAudioClip(landingClip, landingVolume);
    }



    void PlayAudioClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
        }
    }


}
