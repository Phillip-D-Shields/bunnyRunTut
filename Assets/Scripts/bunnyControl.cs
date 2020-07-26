using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
// * needed for UI text box
using UnityEngine.UI;

public class bunnyControl : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Animator myAnimation;
    private Collider2D myCollider;
    public Text scoreText;
    public float bunnyJumpForce = 500f;
    private float bunnyHurtTime = -1;
    private float startTime;
    

    //* Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimation = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        startTime = Time.time;
    }

    //* Update is called once per frame
    void Update()
    {
        //! bunny is running
        if (bunnyHurtTime == -1)
        {
            //* bunny wants to jump
            if (Input.GetButtonUp("Jump"))
            {
                myRigidbody.AddForce(transform.up * bunnyJumpForce);
            }
            //* running animation starts as bunny is falling
            myAnimation.SetFloat("vVelocity", myRigidbody.velocity.y);
            //* jumping animation maintains until bunny lands
            //* myAnimation.SetFloat("vVelocity", Mathf.Abs(myRigidbody.velocity.y));
            scoreText.text = (Time.time - startTime).ToString("0.0");
        }
        else
        {
            // ! bunny has collided
            if (Time.time > bunnyHurtTime + 3)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //! set collider only for items in the Enemy Layer
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            // ! stop the prefab spawner
            foreach (PrefabSpawn spawnPoint in FindObjectsOfType<PrefabSpawn>())
            {
                spawnPoint.enabled = false;
            }
            // ! stop the prefabs already spawned
            foreach (MoveLeft MoveLefter in FindObjectsOfType<MoveLeft>())
            {
                MoveLefter.enabled = false;
            }

            // ! change the bunnyHurtTime to trigger final events
            bunnyHurtTime = Time.time;
            // * trigger bunnyHurt animation
            myAnimation.SetBool("bunnyHurt", true);
            // * stop the bunnys movement and velocity
            myRigidbody.velocity = Vector2.zero;
            // * trigger one last jump and fall off the screen
            myRigidbody.AddForce(transform.up * bunnyJumpForce);
            // * remove collision from bunny and ground
            myCollider.enabled = false;
        }
    }
}
