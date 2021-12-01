using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    private bool fading;
    private Image fadeImage;
    public static SceneLoader Instance;

    private IEnumerator FadeImage(Image image,float targetAlpha,float duration)
    {
        fading = true;
        image.gameObject.SetActive(true);
        float alpha = image.color.a;

        while (alpha!=targetAlpha)
        {
            alpha = Mathf.MoveTowards(alpha, targetAlpha, Time.deltaTime/duration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            
            yield return null;
        }
        image.color = new Color(image.color.r, image.color.g, image.color.b, targetAlpha);

        if (targetAlpha == 0)
        {
            image.gameObject.SetActive(false);
        }
        fading = false;
    }

    private IEnumerator WaitToLoad(string targetScene)
    {
        yield return null;
        while (fading)
        {
            yield return null;
        }
        SceneManager.LoadScene(targetScene);
    }

    public void LoadScene(string targetScene)
    {
        StartCoroutine(FadeImage(fadeImage,1, 0.5f));
        StartCoroutine(WaitToLoad(targetScene));
    }

    public void StartScene()
    {
        StartCoroutine(FadeImage(fadeImage, 0, 0.5f));
    }

    public void Awake()
    {
        Instance = this;
        fadeImage = GetComponentInChildren<Image>(true);
    }

    public void Start()
    {
        StartScene();
    }


}
