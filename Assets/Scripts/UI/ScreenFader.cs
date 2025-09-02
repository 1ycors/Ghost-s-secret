using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 0.1f;

    public IEnumerator FadeOut() 
    {
        yield return Fade(0f, 1f); //из прозрачного к черному
    }
    public IEnumerator FadeIn() 
    {
        yield return Fade(1f, 0f); //наоборот
    }
    private IEnumerator Fade( float startAlpa, float endAlpha) 
    {
        float elapsed = 0f;
        Color color = fadeImage.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            color.a = Mathf.Lerp(startAlpa, endAlpha, t);
            fadeImage.color = color;
            yield return null;
        }
    } 
}
