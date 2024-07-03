using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedalGlowEffect : MonoBehaviour
{
    [SerializeField] private GameObject _glow;
    [SerializeField] private Vector2 _offset;
    [SerializeField] private float _radius;
    [SerializeField] private float _interval;

    public void InstantiateGlows(int numOfGlows)
    {
        for (int i = 0; i < numOfGlows; i++)
        {
            float randomInterval = Random.Range(0f, _interval);
            Invoke(nameof(InstantiateGlowGameObject), randomInterval);
        }
    }

    private void InstantiateGlowGameObject()
    {
        Vector2 pos = Random.insideUnitCircle * _radius;
        pos = new Vector2(pos.x + _offset.x, pos.y + _offset.y);
        Instantiate(_glow, pos, Quaternion.identity, transform);
    }
}
