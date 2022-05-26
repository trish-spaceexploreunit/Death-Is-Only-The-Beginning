using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollText : MonoBehaviour
{

    [SerializeField] GameObject[] textList;
    [SerializeField] float fadeSpeed = 0.5f;
    int textIndex = 0;
    bool introTextCompleted = false;
    bool fadeIn = false;
    bool fadeOut = false;
    TextMeshProUGUI currText = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !introTextCompleted && !fadeIn && !fadeOut)
        {
            fadeIn = true;
            PlayIntroText();
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && !introTextCompleted && fadeIn)
        {
            fadeIn = false;
            fadeOut = true;
        }
        else if (fadeIn && currText)
        {
            FadeInIntroText(currText, fadeSpeed);
        }
        else if (fadeOut && currText)
        {
            FadeOutIntroText(currText, fadeSpeed * 2);
        }
    }

    void PlayIntroText()
    {
        currText = textList[textIndex].GetComponent<TextMeshProUGUI>();
        if (textIndex == textList.Length - 1)
        {
                introTextCompleted = true; 
        }
        textIndex++;
    }
    

    void FadeInIntroText(TextMeshProUGUI text, float fadeInSpeed)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime * fadeInSpeed));
    }

    void FadeOutIntroText(TextMeshProUGUI text, float fadeInSpeed)
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * fadeInSpeed));
        if (text.color.a <= 0)
        {
            fadeOut = false;
        }
    }
}
