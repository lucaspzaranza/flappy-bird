using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public static Action OnPlayerPassedPipe;
    public static Action<Pipe> OnPipeReachedDeadzone;

    public float _speed;

    private bool _stop = false;

    void Update()
    {
        if (_stop)
            return;

        transform.Translate(Vector2.left * _speed * Time.deltaTime);
    }

    public void ResumeMovement()
    {
        _stop = false;
    }

    public void StopMovement()
    {
        _stop = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            OnPlayerPassedPipe?.Invoke();
        else if (other.tag == "Deadzone")
            OnPipeReachedDeadzone?.Invoke(this);
    }
}
