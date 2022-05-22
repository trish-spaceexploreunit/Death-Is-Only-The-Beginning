using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    Vector3 respawnLocation;
    [SerializeField] GameObject deathGround;
    int timesRespawned;
    
    // Start is called before the first frame update
    void Start()
    {
        respawnLocation = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Respawn"))
        {
            timesRespawned++;
            LevelDataScript data = FindObjectOfType<LevelDataScript>();
            int maxRespawns = data.levelLives;
            if (timesRespawned <= maxRespawns)
            {
                RespawnSprite();
            }
            else
            {
                RespawnStage();
            }
        }
    }

    void RespawnSprite()
    {
        //move character model back to start
        //ensure the camera resets
        //can't just reload the scene because we need to potentially add a platform
        Instantiate(deathGround, transform.position, Quaternion.identity);
        transform.position = respawnLocation;
        
    }

    void RespawnStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
