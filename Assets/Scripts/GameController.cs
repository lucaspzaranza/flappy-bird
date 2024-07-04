using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public static class PlayerPrefsStrings
{
    public const string TopScore = "TopScore";
    public const string RankingPositionName = "RankingPositionName";
    public const string RankingScore = "RankingScore";
}

public class GameController : MonoBehaviour
{
    public const int NumOfRankings = 10;

    [SerializeField] private FlappyBird _flappyBird;
    [SerializeField] private Scroller _floorScroller;
    [SerializeField] private GameObject _flash;
    [SerializeField] private GameObject _pipesPrefab;
    [SerializeField] private GameObject _inGameMenu;
    [SerializeField] private GameObject _gameOverMenu;    
    [SerializeField] private GameObject _resetGameAnimation;    
    [SerializeField] private GameObject _medalGlows;    
    [SerializeField] private Text _score;
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private bool _gameStarted;
    [SerializeField] private int _numOfPipes;
    [SerializeField] private float _pipeInitialXPosition;
    [SerializeField] private float _pipeDistance;
    [SerializeField] private float _yMinPos;
    [SerializeField] private float _yMaxPos;
    [SerializeField] private float _timeToInvokeGameOverMenu;
    [SerializeField] private LinkedList<Pipe> _pipes;
    [SerializeField] private bool _resetTopScore;
    [SerializeField] private bool _resetPlayerPrefs;

    [SerializeField] private int _scoreValue;
    public int ScoreValue => _scoreValue;

    private string _playerName;
    public string PlayerName => _playerName;

    private void OnEnable()
    {
        if(_flappyBird != null)
        {
            _flappyBird.OnPlayerDeath += GameOver;
        }

        Pipe.OnPlayerPassedPipe += Score;
        Pipe.OnPipeReachedDeadzone += SwapPipesPosition;
    }

    private void OnDisable()
    {
        if (_flappyBird != null)
        {
            _flappyBird.OnPlayerDeath -= GameOver;
        }

        Pipe.OnPlayerPassedPipe -= Score;
        Pipe.OnPipeReachedDeadzone -= SwapPipesPosition;
    }

    public void StartGame()
    {
        _playerName = _nameInput.text;

        InstantiatePipes();
        if(_flappyBird.isActiveAndEnabled)
        {
            _flappyBird.StartGame();
            _gameStarted = true;
        }
    }

    private void Update()
    {
        if(_resetTopScore)
        {
            ResetTopScore();
            _resetTopScore = false;
        }

        if (_resetPlayerPrefs)
        {
            ResetPlayerPrefs();
            _resetPlayerPrefs = false;
        }

        if (!_gameStarted)
            return;

        if((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && _flappyBird != null)
            _flappyBird.Flap();
    }

    private void InstantiatePipes()
    {
        float pos = _pipeInitialXPosition;
        _pipes = new LinkedList<Pipe>();

        for (int i = 0; i < _numOfPipes; i++)
        {
            Vector2 pipePos = new Vector2(pos, GetNewRandomYPipePosition());
            GameObject pipe = Instantiate(_pipesPrefab, pipePos, Quaternion.identity);
            _pipes.AddLast(pipe.GetComponent<Pipe>());
            pos += _pipeDistance;
        }
    }

    private void Score()
    {
        _scoreValue++;
        _score.text = _scoreValue.ToString();
    }

    private float GetNewRandomYPipePosition()
    {
        return Random.Range(_yMinPos, _yMaxPos);
    }

    private void SwapPipesPosition(Pipe pipe)
    {
        Pipe firstPipe = _pipes.First.Value;
        Pipe lastPipe = _pipes.Last.Value;

        float newX = lastPipe.gameObject.transform.position.x + _pipeDistance;
        float newY = GetNewRandomYPipePosition();
        firstPipe.gameObject.transform.position = new Vector2(newX, newY);

        _pipes.RemoveFirst();
        _pipes.AddLast(firstPipe);
    }

    public void GameOver()
    {
        _gameStarted = false;
        Instantiate(_flash);
        _floorScroller.StopOffset();
        _inGameMenu.SetActive(false);

        RegisterScore();

        Invoke(nameof(ActivateGameOverMenu), _timeToInvokeGameOverMenu);

        foreach (Pipe pipe in _pipes)
        {
            pipe.StopMovement();
        }
    }

    private void RegisterScore()
    {
        if (_scoreValue > GetTopScore())
            SetTopScore(_scoreValue);

        bool insertedNewScore = false;

        for (int i = 1; i < NumOfRankings + 1; i++)
        {
            string currentPlayerPrefName = PlayerPrefs.GetString($"{PlayerPrefsStrings.RankingPositionName + i}");
            int playerScore = PlayerPrefs.GetInt($"{PlayerPrefsStrings.RankingScore + i}");

            if(!insertedNewScore && _scoreValue > playerScore && !string.IsNullOrEmpty(PlayerName))
            {
                ReorderPlayerPrefsScores(PlayerName, _scoreValue, i);
                i++;
                insertedNewScore = true;
            }
        }
    }

    private void ReorderPlayerPrefsScores(string playerName, int score, int index)
    {
        for (int i = NumOfRankings + 1; i >= index; i--)
        {
            string previousPlayerName = PlayerPrefs.GetString($"{PlayerPrefsStrings.RankingPositionName + (i - 1)}");
            int previousPlayerScore = PlayerPrefs.GetInt($"{PlayerPrefsStrings.RankingScore + (i - 1)}");

            PlayerPrefs.SetString($"{PlayerPrefsStrings.RankingPositionName + i}", previousPlayerName);
            PlayerPrefs.SetInt($"{PlayerPrefsStrings.RankingScore + i}", previousPlayerScore);

            if(i == index)
            {
                PlayerPrefs.SetString($"{PlayerPrefsStrings.RankingPositionName + index}", playerName);
                PlayerPrefs.SetInt($"{PlayerPrefsStrings.RankingScore + index}", score);
            }
        }
    }

    public int GetTopScore()
    {
        return PlayerPrefs.GetInt(PlayerPrefsStrings.TopScore);
    }

    public void SetTopScore(int newVal)
    {
        PlayerPrefs.SetInt(PlayerPrefsStrings.TopScore, newVal);
    }

    public void ResetTopScore()
    {
        SetTopScore(0);
        print("Top Score successfully reseted.");
    }

    public void ResetPlayerPrefs()
    {
        for (int i = 1; i < NumOfRankings + 1; i++)
        {
            PlayerPrefs.SetString($"{PlayerPrefsStrings.RankingPositionName + i}", string.Empty);
            PlayerPrefs.SetInt($"{PlayerPrefsStrings.RankingScore + i}", 0);
        }

        print("Player Prefs successfully reseted.");
    }

    private void ActivateGameOverMenu()
    {
        _gameOverMenu.SetActive(true);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
    }

    public void CallResetGame()
    {
        _medalGlows.SetActive(false);
        Instantiate(_resetGameAnimation);
    }
}
