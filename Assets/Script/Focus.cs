using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Focus : MonoBehaviour
{
    public static Focus Instance;
    [SerializeField] private GameObject focusGameobject;
    private Image fadeImage;
    private Image focusImage;
    private TMP_Text focusText;
    private CanvasGroup canvasGroup;
    private const float fadingTime=1f;
    private Queue<IEnumerator> CoroutineStack = new Queue<IEnumerator>();
    private bool running;

    private void Awake()
    {
        Instance = this;
        fadeImage = focusGameobject.GetComponent<Image>();
        focusImage = focusGameobject.GetComponentsInChildren<Image>(true)[1];
        focusText = focusGameobject.GetComponentInChildren<TMP_Text>(true);
        canvasGroup=focusGameobject.GetComponent<CanvasGroup>();
    }

    private IEnumerator FadeCanvasGroup(float duration,bool fadeIn)
    {

        running = true;
        float targetAlpha;
        float t = 0f;
        if (fadeIn)
        {
            targetAlpha = 1;
        }
        else
        {
            targetAlpha = 0;
        }

        while (t<1f)
        {
            canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, targetAlpha,t);
            t += Time.deltaTime / duration;
            yield return null;
        }
        canvasGroup.alpha = targetAlpha;
        running = false;
    }
    private IEnumerator ScaleFocusImage(float duration,bool scaleUp)
    {

        running = true;
        Vector2 targetScale;
        float t = 0f;
        if (scaleUp)
        {
            targetScale = Vector2.one;
        }
        else
        {
            targetScale = Vector2.zero;
        }

        while (t < 1f)
        {
            focusImage.rectTransform.localScale = Vector2.Lerp(focusImage.rectTransform.localScale, targetScale, t);
            t += Time.deltaTime / duration;
            yield return null;
        }

        focusImage.rectTransform.localScale = targetScale;
        running = false;
    }
    private IEnumerator DisableGameobject()
    {
        yield return null;
        focusImage.gameObject.SetActive(false);
    }
    private IEnumerator CoroutineSequence()
    {
        while (CoroutineStack.Count > 0)
        {
            if (!running)
            {
                StartCoroutine(CoroutineStack.Dequeue());
            }
            yield return null;
        }
    }
    private void StarCoroutineSequence()
    {
        StartCoroutine(CoroutineSequence());
    }
    public void FocusItem(Item item)
    {
        fadeImage.gameObject.SetActive(true);
        focusImage.sprite = item.Sprite;
        focusText.text = item.Name;
        CoroutineStack.Enqueue(FadeCanvasGroup(fadingTime/2, true));
        CoroutineStack.Enqueue(ScaleFocusImage(fadingTime, true));
        StarCoroutineSequence();
    }
    public void UnFocusItem()
    {
        CoroutineStack.Enqueue(ScaleFocusImage(fadingTime, false));
        CoroutineStack.Enqueue(FadeCanvasGroup(fadingTime, false));
        CoroutineStack.Enqueue(DisableGameobject());
        StarCoroutineSequence();
    }

}
