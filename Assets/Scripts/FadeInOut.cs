using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public bool _fadeOut;
    public Animator _animator;

    private int _fadeOutHash = Animator.StringToHash("FadeOut");

    public void FadeOut()
    {
        _animator.SetTrigger(_fadeOutHash);
    }
}
