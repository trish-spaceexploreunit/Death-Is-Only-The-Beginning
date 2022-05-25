using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] bool moveLeft;
    [SerializeField] float movementSpeed;
    [SerializeField] SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float frameAdjustedMovementSpeed = movementSpeed * Time.deltaTime;
        transform.Translate(moveLeft ? -frameAdjustedMovementSpeed : frameAdjustedMovementSpeed, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
        {
            moveLeft = !moveLeft;
            sprite.flipX = !sprite.flipX;
        }
    }
}
