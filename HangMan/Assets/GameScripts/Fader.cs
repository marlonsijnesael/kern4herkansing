using UnityEngine;
using System.Collections;
 
public class Fader : MonoBehaviour
{
    public Color endColor;
    public Color rightColor;
    public Color wrongColor;
    public float lerpTime;

    public UnityEngine.UI.RawImage image;

    private void Awake()
    {
        //Fade();
    }

    public void FadeWrong()
    {
        StartCoroutine(DoLerp(wrongColor));
    }

    public void FadeRight()
    {
        StartCoroutine(DoLerp(rightColor));
    }

    private IEnumerator DoLerp(Color startCol)
    {
        image.gameObject.SetActive(true);
        float timeElapsed = 0f;
        float totalTime = lerpTime;

        while (timeElapsed < totalTime)
        {
            timeElapsed += Time.deltaTime;
            image.color = Color.Lerp(startCol, endColor, timeElapsed / totalTime);
            yield return null;
        }
        image.gameObject.SetActive(false);

    }
}