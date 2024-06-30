using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatEffect : MonoBehaviour
{
    [SerializeField] private float _upperLimit;
    [SerializeField] private float _lowerLimit;
    [SerializeField] private float _duration;

    void OnEnable()
    {
        Float();
    }

    private void OnDisable()
    {
        StopFloat();
    }

    public void Float()
    {
        transform.DOMoveY(_upperLimit, _duration).SetLoops(-1, LoopType.Restart);
    }

    public void StopFloat()
    {
        transform.DOKill();
    }
}
