using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistController : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    [SerializeField] float jumpHeight = 0.5f;
    [SerializeField] float fallSpeed = -0.1f;
    bool inAir;
    
    void Update()
    {
        float frameAdjustedSpeed = speed * Time.deltaTime;
        float frameAdjustedFallSpeed = fallSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(frameAdjustedSpeed, inAir? frameAdjustedFallSpeed:0, 0);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-frameAdjustedSpeed, inAir? frameAdjustedFallSpeed:0, 0);
        }
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) & !inAir)
        {
            inAir = true;
            transform.Translate(0, jumpHeight, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            //There may well be other things we want to check here eventually
            if (inAir)
            {
                inAir = false;
            }
        }
    }

    
}
