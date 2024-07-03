using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlappyBird : MonoBehaviour
{
    public Action OnPlayerDeath;

    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _birdGameObject;
    [SerializeField] private float _birdWeight;
    [SerializeField] private float _flapForce;
    [SerializeField] private float _rotationRate;
    [SerializeField] private float _flapMaxAngle;
    [SerializeField] private float _heightLimit;
    [Tooltip("Offset to add at Y position when the bird touches the ground and be better aligned with collider position.")]
    [SerializeField] private float _positionOffsetY;

    private Rigidbody2D _rigidBody2D;
    private bool _isDead = false;

    private int _startGameHash = Animator.StringToHash("StartGame");
    private int _deathGameHash = Animator.StringToHash("Death");

    private void OnEnable()
    {
        if(_animator == null)
            _animator = _birdGameObject.GetComponent<Animator>();

        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_isDead)
            return;

        transform.position = new Vector2(transform.position.x,
            Mathf.Clamp(transform.position.y, transform.position.y, _heightLimit));

        if (_rigidBody2D.velocity.y != 0)
        {
            float lowerLimit = 360f - _flapMaxAngle;

            int direction = _rigidBody2D.velocity.y >= 0 ? 1 : -1;
            _birdGameObject.transform.localEulerAngles += new Vector3(0f, 0f, _rotationRate * direction);

            if (direction < 0 &&
            _birdGameObject.transform.eulerAngles.z < lowerLimit &&
            _birdGameObject.transform.eulerAngles.z > _flapMaxAngle)
                _birdGameObject.transform.localEulerAngles = new Vector3(0f, 0f, lowerLimit);

            else if (direction > 0 && _birdGameObject.transform.eulerAngles.z > _flapMaxAngle)
                _birdGameObject.transform.localEulerAngles = new Vector3(0f, 0f, _flapMaxAngle);
        }
    }

    public void StartGame()
    {
        _animator.SetTrigger(_startGameHash);
        _rigidBody2D.gravityScale = _birdWeight;
        Flap();
    }
    
    public void Flap()
    {
        _rigidBody2D.velocity = Vector2.zero;
        _rigidBody2D.totalForce = Vector2.zero;
        _rigidBody2D.AddForce(Vector2.up * _flapForce, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!_isDead && collision.gameObject.tag == "Damageable")
        {
            _isDead = true;
            _birdGameObject.transform.localEulerAngles = Vector3.zero;
            _rigidBody2D.velocity = Vector2.zero;
            _rigidBody2D.totalForce = Vector2.zero;

            _birdGameObject.transform.localPosition = new Vector2(0f, _positionOffsetY);

            _animator.SetTrigger(_deathGameHash);
            OnPlayerDeath?.Invoke();
        }
    }
}
