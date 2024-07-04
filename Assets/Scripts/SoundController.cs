using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _score;
    [SerializeField] private AudioClip _hit;
    [SerializeField] private AudioClip _gameOver;
    [SerializeField] private AudioClip _board;
    [SerializeField] private AudioClip _flap;
    [SerializeField] private AudioClip _resetPlayerPrefs;

    public void PlayFlapAudio()
    {
        _audioSource.PlayOneShot(_flap);
    }

    public void PlayScoreAudio()
    {
        _audioSource.PlayOneShot(_score);
    }

    public void PlayHitAudio()
    {
        _audioSource.PlayOneShot(_hit);
    }

    public void PlayGameOverAudio()
    {
        _audioSource.PlayOneShot(_gameOver);
    }

    public void PlayBoardAudio()
    {
        _audioSource.PlayOneShot(_board);
    }

    public void PlaResetPlayerPrefsAudio()
    {
        _audioSource.PlayOneShot(_resetPlayerPrefs);
    }
}
