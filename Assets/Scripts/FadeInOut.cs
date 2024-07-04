using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public bool _fadeOut;
    public bool _isStartFadeInOut;
    public Animator _animator;

    private int _fadeOutHash = Animator.StringToHash("FadeOut");
    private int _fadeInOutHash = Animator.StringToHash("FadeInOut");

    private void OnEnable()
    {
        if (_isStartFadeInOut)
            _animator.SetTrigger(_fadeInOutHash);
    }

    public void FadeOutAnimation()
    {
        _animator.SetTrigger(_fadeOutHash);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
