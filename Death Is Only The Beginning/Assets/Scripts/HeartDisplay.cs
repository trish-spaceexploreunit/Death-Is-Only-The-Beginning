using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartDisplay : MonoBehaviour
{
    LevelDataScript data;
    [SerializeField] GameObject[] heartUIObjects;

    // Start is called before the first frame update
    void Start()
    {
        data = FindObjectOfType<LevelDataScript>();
        for (int i = heartUIObjects.Length; i > data.levelLives; i--)
        {
            heartUIObjects[i-1].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveHeart(int index)
    {
        heartUIObjects[index].SetActive(false);
    }
}
