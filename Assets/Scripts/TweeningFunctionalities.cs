using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TweeningFunctionalities : MonoBehaviour
{
    [SerializeField] private float _fadeOutDuration;

    public void ImageFadeOut(GameObject objectToFade)
    {
        var spriteRenderer = objectToFade.GetComponent<Image>();

        if (spriteRenderer == null)
            return;

        spriteRenderer.DOFade(0f, _fadeOutDuration);
    }
}
