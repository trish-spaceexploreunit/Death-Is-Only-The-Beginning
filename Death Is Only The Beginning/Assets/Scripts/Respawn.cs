using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    Vector3 respawnLocation;
    [SerializeField] GameObject deathGround;
    
    // Start is called before the first frame update
    void Start()
    {
        respawnLocation = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Respawn"))
        {
            RespawnSprite();
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
}
