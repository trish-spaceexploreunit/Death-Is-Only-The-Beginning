using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistController : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    private float endJump = 0f;
    [SerializeField] float jumpTime = 2f;
    [SerializeField] float fallSpeed = -5f;
    [SerializeField] float defaultJumpHeight = .5f;
    bool inAir;
    bool playerControl = true;

    void Update()
    {
        if (playerControl)
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
            if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) & !inAir)
            {
                inAir = true;
                endJump = Time.time + jumpTime;
            }
            if (Time.time < endJump) 
            {
                transform.Translate(0, defaultJumpHeight, 0);
            }
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

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            inAir = true;
        }
    }

    public void toggleControls()
    {
        playerControl = !playerControl;
        GetComponent<Rigidbody2D>().constraints = playerControl?  RigidbodyConstraints2D.FreezeRotation : RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }   
}
