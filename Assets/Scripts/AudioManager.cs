using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Camera MainCamera;
    [SerializeField] [Range(0, 1)] float jumpSoundVolume;
    public AudioClip JumpSound;
    [SerializeField][Range(0, 1)] float scoreVolume;
    public AudioClip Score;
    public AudioClip DeathSound;
    public AudioClip ButtonHover;
    public AudioClip GameOver;
    public AudioClip ButtonClick;

    private void Awake()
    {
        instance = this;
    }
    public static AudioManager GetInstance()
    {
        return instance;
    }

    public void PlayJumpSound()
    {
        AudioSource.PlayClipAtPoint(JumpSound, MainCamera.transform.position, jumpSoundVolume);
    }
    public void PlayScoreSound()
    {
        AudioSource.PlayClipAtPoint(Score, MainCamera.transform.position, scoreVolume);
    }
    public void PlayDeathSound()
    {
        AudioSource.PlayClipAtPoint(DeathSound, MainCamera.transform.position);
    }
    public void PlayGameOverSound()
    {
        AudioSource.PlayClipAtPoint(GameOver, MainCamera.transform.position);
    }

    public void ButtonHoverSound()
    {
        AudioSource.PlayClipAtPoint(ButtonHover, MainCamera.transform.position);
    }
    public void ButtonClickSound()
    {
        AudioSource.PlayClipAtPoint(ButtonClick, MainCamera.transform.position);
    }
}
