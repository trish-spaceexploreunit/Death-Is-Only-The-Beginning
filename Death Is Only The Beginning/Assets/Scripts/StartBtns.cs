using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartBtns : MonoBehaviour
{
    public Button begin, credits;

    // Start is called before the first frame update
    void Start()
    {
        Button btn1 = begin.GetComponent<Button>();
        Button btn2 = credits.GetComponent<Button>();

        btn1.onClick.AddListener(beginTask);
        btn2.onClick.AddListener(creditsTask);
    }

    void beginTask()
    {
        SceneManager.LoadScene(1);
    }

    void creditsTask()
    {
        SceneManager.LoadScene("Credits");
    }
}