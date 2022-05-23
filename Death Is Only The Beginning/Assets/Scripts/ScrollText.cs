using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollText : MonoBehaviour
{

    [SerializeField] GameObject[] textList;
    //[SerializeField] float fadeSpeed = 0.5f;
    bool introTextCompleted = false;
    GameObject currText;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        currText = textList[index];
        currText.SetActive(true);
        index++;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {   if (!introTextCompleted)
            {
                index++; 
                if (index==textList.Length)
                {
                    introTextCompleted = true;
                    return;
                }
                currText.SetActive(false);
                currText = textList[index];
                currText.SetActive(true);
            }   
        }
    }

}