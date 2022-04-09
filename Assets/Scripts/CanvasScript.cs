using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] Image fadeImage;
    private void Start()
    {
    }

    public void SetToBlack()
    {
        Color fadeImageColor = new Color(0, 0, 0, 1f);
        fadeImage.color = fadeImageColor;
    }

    public IEnumerator fadeToBright(float fadeDuration)
    {
        Color initialColor = fadeImage.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeImage.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }
        fadeImage.gameObject.SetActive(false);
    }

    public IEnumerator fadeToDarkness(float fadeDuration)
    {
        fadeImage.gameObject.SetActive(true);
        Color initialColor = fadeImage.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeImage.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }
    }
}

