using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPlayerPrefsButton : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameController _gameController;
    [SerializeField] private GameObject _scoreBoard;
    [SerializeField] private float _timeToResetClickCounter;
    [SerializeField] private float _timeToCallToggleBoard;

    private int _mouseClick;
    private float _timeCounter;

    private void Update()
    {
        if(_mouseClick == 1)
        {
            _timeCounter += Time.deltaTime;

            if(_timeCounter >= _timeToResetClickCounter)
            {
                _timeCounter = 0;
                _mouseClick = 0;
            }
        }
    }

    public void ClickButton()
    {
        _mouseClick++;

        if( _mouseClick == 2)
        {
            _animator.SetTrigger("Reset");
            _gameController.ResetAllPlayerPrefs();
            _mouseClick = 0;
            _timeCounter = 0;

            Invoke(nameof(CallToggleScoreBoard), _timeToCallToggleBoard);
        }
    }

    public void CallToggleScoreBoard()
    {
        _scoreBoard.SetActive(false);
        _scoreBoard.SetActive(true);
    }
}
