using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingScore : MonoBehaviour
{
    [SerializeField] private int _rankingPosition;

    // Assign them via Inspector
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _score;

    private void OnEnable()
    {
        if(_name != null)
            _name.text = PlayerPrefs.GetString($"{PlayerPrefsStrings.RankingPositionName + _rankingPosition}");

        if(_score != null)
            _score.text = PlayerPrefs.GetInt($"{PlayerPrefsStrings.RankingScore + _rankingPosition}").ToString();
    }
}
