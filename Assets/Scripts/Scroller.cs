using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    [SerializeField] private float _offsetRate;
    private Renderer _renderer;

    private bool _stop;

    void Start()
    {
        _renderer = GetComponent<Renderer>();

        if (!_renderer)
            Debug.LogError("Scroller Renderer not found. Please insert the script in a Game Object with a Renderer component.");

        MoveOffset();
    }

    void Update()
    {
        if (_stop)
            return;

        _renderer.material.mainTextureOffset += new Vector2(_offsetRate * Time.deltaTime, 0f);
    }

    public void MoveOffset()
    {
        _stop = false;
    }

    public void StopOffset()
    {
        _stop = true;
    }
}