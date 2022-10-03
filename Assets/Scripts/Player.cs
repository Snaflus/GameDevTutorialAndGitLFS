using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] //makes them visible in inspector panel
    private float moveForce = 10f;
    [SerializeField]
    private float jumpForce = 11f;

    private bool isGrounded;
    private string GROUND_TAG = "Ground"; //tag for the ground
    
    private float movementX;
    private Rigidbody2D myBody;
    private SpriteRenderer spriteRenderer;
    private Animator myAnimator;

    private string WALK_ANIMATION = "Walk"; //name of the boolean defined in animator

    private string ENEMY_TAG = "Enemy";

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        AnimatePlayer();
        PlayerJump();
    }

    void FixedUpdate()
    {
        
    }

    void PlayerMove()
    {
        movementX = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;
    }

    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) //solution uses collision
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag(ENEMY_TAG))
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) //solution uses IsTrigger tag on Collider2D
    {
        if (collision.CompareTag(ENEMY_TAG))
            Destroy(gameObject);
    }

    void AnimatePlayer()
    {
        if (movementX > 0)
        {
            myAnimator.SetBool(WALK_ANIMATION, true);
            spriteRenderer.flipX = false;
        }
        else if (movementX < 0)
        {
            myAnimator.SetBool(WALK_ANIMATION, true);
            spriteRenderer.flipX = true;
        }
        else
        {
            myAnimator.SetBool(WALK_ANIMATION, false);
        }
    }
}
