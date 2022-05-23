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
    bool respawning;
    CultistController playerController;
    LevelDataScript data;
    
    // Start is called before the first frame update
    void Start()
    {
        respawnLocation = transform.position;
        anim = GetComponent<Animator>();
        playerController = GetComponent<CultistController>();
        data = FindObjectOfType<LevelDataScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !respawning)
        {
            playerController.toggleControls();
            groundSpawnLocation = transform.position;
            timesRespawned++;
            int maxRespawns = data.levelLives;
            if (timesRespawned <= maxRespawns)
            {
                Invoke("RespawnSprite", delay);
            }
            else
            {
                Invoke("RespawnStage", delay);
            }
            respawning = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Respawn") && !respawning)
        {
            playerController.toggleControls();
            respawning = true;
            groundSpawnLocation = transform.position;
            timesRespawned++;
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
        else if (other.CompareTag("Checkpoint"))
        {
            respawnLocation = transform.position;
        }
        else if (other.CompareTag("Finish"))
        {
            SceneManager.LoadScene(data.nextSceneIndex);
        }
    }

    void RespawnSprite()
    {
        Instantiate(deathGround, groundSpawnLocation, Quaternion.identity);
        transform.position = respawnLocation;
        anim.SetBool("suicide", false);
        respawning = false;
        playerController.toggleControls();
    }

    void RespawnStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
