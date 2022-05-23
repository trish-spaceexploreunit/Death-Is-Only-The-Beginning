using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    Vector3 respawnLocation;
    Vector3 groundSpawnLocation;
    [SerializeField] GameObject deathGround;
    [SerializeField] float delay = 1.0f;
    int timesRespawned;
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        respawnLocation = transform.position;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            groundSpawnLocation = transform.position;
            timesRespawned++;
            LevelDataScript data = FindObjectOfType<LevelDataScript>();
            int maxRespawns = data.levelLives;
            if (timesRespawned <= maxRespawns)
            {
                Invoke("RespawnSprite", delay);
            }
            else
            {
                Invoke("RespawnStage", delay);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Respawn"))
        {
            groundSpawnLocation = transform.position;
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
        Instantiate(deathGround, groundSpawnLocation, Quaternion.identity);
        transform.position = respawnLocation;
        anim.SetBool("suicide", false);
    }

    void RespawnStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
