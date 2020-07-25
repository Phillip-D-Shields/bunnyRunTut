using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class bunnyControl : MonoBehaviour
{

    private Rigidbody2D myRigidbody;
    public float bunnyJumpForce = 500f;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonUp("Jump"))
        {
            myRigidbody.AddForce(transform.up * bunnyJumpForce);
        }
    }
}
