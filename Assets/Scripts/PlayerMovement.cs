using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;

    public float playerSpeed = 2;
    public float jumpForce = 2;
    public float raycastLength;

    public bool isGrounded;
    public LayerMask groundLayerMask;

    public Transform respawnPoint;

    private SpriteRenderer spriteRenderer;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        respawnPoint.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontal * playerSpeed, rb.velocity.y);
        horizontal = Input.GetAxis("Horizontal");


        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        }

        if (rb.velocity.x != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, raycastLength, groundLayerMask);
        Debug.DrawRay(transform.position, Vector3.down * 0.5f, Color.green);
        anim.SetBool("isGrounded", isGrounded);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Cointag") 
        {
            Destroy(other.gameObject);
        }


        else
        if (other.tag == "Respawn") 
        {
            Respawn();
        }

    }
    void Respawn()
    {
        transform.position = respawnPoint.position;
    }

}