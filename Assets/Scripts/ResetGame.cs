using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    public FadeInOut _fadeInOutAnimation;

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ResetGameByReloadingScene()
    {
        SceneManager.LoadScene(0);
    }

    public void FadeOut()
    {
        _fadeInOutAnimation.FadeOut();
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
