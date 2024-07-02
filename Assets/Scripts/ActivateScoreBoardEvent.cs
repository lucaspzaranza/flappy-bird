using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateScoreBoardEvent : MonoBehaviour
{
    [SerializeField] private GameObject _scoreBoard;

    public void ActivateScoreBoard()
    {
        _scoreBoard.SetActive(true);
    }
}
