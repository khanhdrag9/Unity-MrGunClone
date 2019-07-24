using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    SpriteRenderer sr = null;
    Color colorTo = Color.white;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        colorTo = sr.color;
    }

    public void ColorTo(Color to, float duration)
    {
        //sr.color = colorTo;
        StopAllCoroutines();
        StartCoroutine(ChangeColor(to, duration));
    }

    IEnumerator ChangeColor(Color to, float duration)
    {
        colorTo = to;
        Color cur = sr.color;
        for(float t = 0f; t <= 255f; t += Time.deltaTime / duration)
        {
            Color newColor = Color.Lerp(cur, to, t);
            sr.color = newColor;
            yield return new WaitForEndOfFrame();
            //yield return null;
        }
    }

}
