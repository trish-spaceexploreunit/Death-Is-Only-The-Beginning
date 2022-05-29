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
    [SerializeField] float groundYOffset = 1.0f;
    int timesRespawned;
    [SerializeField] Animator anim;
    [SerializeField] Sprite newSprite;
    bool respawning;
    bool spawnGround = true;
    CultistController playerController;
    LevelDataScript data;
    
    // Start is called before the first frame update
    void Start()
    {
        respawnLocation = transform.position;
        anim.SetBool("suicide", false);
        playerController = GetComponent<CultistController>();
        data = FindObjectOfType<LevelDataScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !respawning)
        {
            playerController.toggleControls();
            anim.SetBool("suicide", true);
            spawnGround = true;
            groundSpawnLocation = transform.position + new Vector3 (0, groundYOffset, 0);
            timesRespawned++;
            int maxRespawns = data.levelLives;
            if (timesRespawned <= maxRespawns)
            {
                Invoke("SpawnPlatform", delay/2);
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
            spawnGround = true;
            groundSpawnLocation = transform.position;
            timesRespawned++;
            int maxRespawns = data.levelLives;
            if (timesRespawned <= maxRespawns)
            {
                Invoke("SpawnPlatform", delay/2);
                Invoke("RespawnSprite", delay);
            }
            else
            {
                RespawnStage();
            }
        }
        else if (other.CompareTag("Checkpoint"))
        {
            SpriteRenderer sp = other.GetComponent<SpriteRenderer>();
            sp.sprite = Resources.Load<Sprite>("CheckpointOn");

            respawnLocation = transform.position;
        }
        else if (other.CompareTag("Finish"))
        {
            other.GetComponent<Animator>().SetBool("finished", true);
            Invoke("LoadNextlevel", 1.0f);
         }
    }

    void LoadNextlevel()
    {
        SceneManager.LoadScene(data.nextSceneIndex);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            playerController.toggleControls();
            anim.SetBool("enemyTouch", true);
            respawning = true;
            spawnGround = false;
            timesRespawned++;
            int maxRespawns = data.levelLives;
            if (timesRespawned <= maxRespawns)
            {
                Invoke("RespawnSprite", delay/2);
            }
            else
            {
                Invoke("RespawnStage", delay/2);
            }
        }
    }

    void SpawnPlatform()
    {
        if (spawnGround)
        {
            Instantiate(deathGround, groundSpawnLocation, Quaternion.identity);
        }
    }
    void RespawnSprite()
    {
        transform.position = respawnLocation;
        anim.SetBool("suicide", false);
        anim.SetBool("enemyTouch", false);
        respawning = false;
        playerController.toggleControls();
    }

    void RespawnStage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        anim.SetBool("suicide", false);
    }
}
