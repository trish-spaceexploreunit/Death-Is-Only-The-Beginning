using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultistController : MonoBehaviour
{
    [SerializeField] float speed = 0.5f;
    [SerializeField] float jumpHeight = 0.5f;
    [SerializeField] float fallSpeed = -0.1f;
    bool inAir;
    public Animator anim;
    bool playerControl = true;
    float defaultGravityScale = 1.5f; //IF YOU EDIT THE GRAVITY IN THE RIGID BODY YOU MUST ALSO EDIT IT HERE
    
    void Start() 
    {
        anim = GetComponent<Animator>();
    }

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
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) & !inAir)
            {
                inAir = true;
                transform.Translate(0, jumpHeight, 0);
            }
            if (Input.GetKey(KeyCode.R))
            {
                //Die and spawn a platform?
                anim.SetBool("suicide", true);
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

    public void toggleControls()
    {
        playerControl = !playerControl;
        GetComponent<Rigidbody2D>().gravityScale = playerControl ? defaultGravityScale : 0.0f;
    }   
}
