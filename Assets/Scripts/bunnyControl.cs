using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class bunnyControl : MonoBehaviour
{

    private Rigidbody2D myRigidbody;
    private Animator myAnimation;
    public float bunnyJumpForce = 500f;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Jump"))
        {
            myRigidbody.AddForce(transform.up * bunnyJumpForce);
        }
        // running animation starts as bunny is falling
        myAnimation.SetFloat("vVelocity", myRigidbody.velocity.y);
        // jumping animation maintains until bunny lands
        // myAnimation.SetFloat("vVelocity", Mathf.Abs(myRigidbody.velocity.y));
    }
}
