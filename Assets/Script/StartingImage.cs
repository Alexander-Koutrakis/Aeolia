using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartingImage : MonoBehaviour
{
    private void Start()
    {
        Image image = GetComponent<Image>();
        StartCoroutine(FadeAndDestroy(image));
    }

    private IEnumerator FadeAndDestroy(Image image)
    {
        Color fadedColor = new Color(1, 1, 1, 0);
        Color startColor = image.color;
        float t = 0;
        while (t < 1)
        {
            image.color = Color.Lerp(startColor, fadedColor, t);
            t += Time.deltaTime / 2;
            yield return null;
        }

        Destroy(gameObject);
    }
}
