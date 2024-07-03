using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private Text _score;
    [SerializeField] private Text _topScore;
    [SerializeField] private float _interval;
    [SerializeField] private GameObject _newTopScore;
    [SerializeField] private GameObject _simpleMedal;
    [SerializeField] private GameObject _bronzeMedal;
    [SerializeField] private GameObject _silverMedal;
    [SerializeField] private GameObject _goldMedal;
    [SerializeField] private GameObject _okButton;
    [SerializeField] private GameObject _scoreButton;
    [SerializeField] private GameObject _medalGlows;

    private GameController _controller;

    private void Awake()
    {
        if(_controller == null)    
            _controller = FindObjectOfType<GameController>();
    }

    public IEnumerator ActivateScoreIncrementAnimation()
    {
        if (_controller == null)
            yield return null;

        int topScore = _controller.GetTopScore();
        _topScore.text = topScore.ToString();
        for (int i = 0; i <= _controller.ScoreValue; i++)
        {
            yield return new WaitForSeconds(_interval);

            _score.text = i.ToString();

            if(i > topScore)
            {
                _topScore.text = _score.text;
                _newTopScore.SetActive(true);
                _controller.SetTopScore(i);
            }
        }

        _okButton.SetActive(true);
        _scoreButton.SetActive(true);

        MedalActivation();
    }

    public void MedalActivation()
    {
        if (_controller == null)
            return;

        int score = _controller.ScoreValue;

        if(score >= 10 && score < 20)
            _simpleMedal.SetActive(true);
        else if(score > 10)
        {
            int numOfGlows = 0;
            if(score >= 20 && score < 30)
            {
                _bronzeMedal.SetActive(true);
                numOfGlows = 2;
            }
            else if (score >= 30 && score < 40)
            {
                numOfGlows = 3;
                _silverMedal.SetActive(true);
            }
            else if (score >= 40)
            {
                numOfGlows = 4;
                _goldMedal.SetActive(true);
            }

            _medalGlows.SetActive(true);
            MedalGlowEffect glowEffect = _medalGlows.GetComponent<MedalGlowEffect>();
            if (glowEffect != null)
            {
                glowEffect.InstantiateGlows(numOfGlows);
            }
        }
    }
}
