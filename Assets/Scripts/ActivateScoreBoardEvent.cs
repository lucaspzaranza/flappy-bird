using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScoreBoardEvent : MonoBehaviour
{
    [SerializeField] private GameObject _scoreBoard;
    [SerializeField] private SoundController _soundController;

    public void ActivateScoreBoard()
    {
        _soundController.PlayBoardAudio();
        _scoreBoard.SetActive(true);
    }
}
