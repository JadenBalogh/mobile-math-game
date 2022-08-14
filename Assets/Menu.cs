using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Menu : MonoBehaviour
{
    [SerializeField] private float fadeTime = 0.2f;

    private CanvasGroup canvasGroup;
    private Coroutine fadeCR;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetVisible(bool visible)
    {
        canvasGroup.blocksRaycasts = visible;
        canvasGroup.interactable = visible;

        if (fadeCR != null) StopCoroutine(fadeCR);
        fadeCR = StartCoroutine(FadeMenu(visible));
    }

    private IEnumerator FadeMenu(bool visible)
    {
        float fadeTarget = visible ? 1f : 0f;
        float fadeTimer = 0f;

        while (fadeTimer < fadeTime)
        {
            canvasGroup.alpha = Mathf.Lerp(1f - fadeTarget, fadeTarget, fadeTimer / fadeTime);
            fadeTimer += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = fadeTarget;
    }
}
