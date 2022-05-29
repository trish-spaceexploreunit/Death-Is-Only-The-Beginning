using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartDisplay : MonoBehaviour
{
    LevelDataScript data;
    [SerializeField] GameObject heartUIObject;
    List<GameObject> heartList;
    [SerializeField] GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        data = FindObjectOfType<LevelDataScript>();
        for (int i = 0; i < data.levelLives; i++)
        {
            // object, position, rotation, parent
            //Vector3 newPosition = heartUIObject.transform.position + new Vector3(i*30, 0, 0);
            heartList.Add(Instantiate(heartUIObject, canvas.transform));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveHeart()
    {
        GameObject toDelete = heartList[heartList.Count-1];
        heartList.RemoveAt(heartList.Count-1);
        Destroy(toDelete);
    }
}
