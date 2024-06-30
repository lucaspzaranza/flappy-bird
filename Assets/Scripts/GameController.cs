using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private FlappyBird _flappyBird;
    [SerializeField] private Scroller _floorScroller;
    [SerializeField] private GameObject _flash;
    [SerializeField] private bool _gameStarted;
    [SerializeField] private bool _gameOver;

    private void OnEnable()
    {
        if(_flappyBird != null)
        {
            _flappyBird.OnPlayerDeath += GameOver;
        }
    }

    private void OnDisable()
    {
        if (_flappyBird != null)
        {
            _flappyBird.OnPlayerDeath -= GameOver;
        }
    }

    public void StartGame()
    {
        _flappyBird.StartGame();
        _gameStarted = true;
    }

    private void Update()
    {
        if (!_gameStarted)
            return;

        if(Input.GetMouseButtonDown(0) && _flappyBird != null)
            _flappyBird.Flap();
    }

    public void GameOver()
    {
        _gameStarted = false;
        _gameOver = true;
        _floorScroller.StopOffset();

        Instantiate(_flash);
    }
}
