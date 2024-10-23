using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private Vector2 movement;
    [SerializeField] private bool canJump = true;
    [SerializeField] bool isFloored = false;

    private Animator animator;
    [SerializeField] private Button respawnButton;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector2.zero;
        movement.y = rb.velocity.y;
        if (Input.GetKey(KeyCode.LeftArrow)) { movement.x -= 1; GetComponent<SpriteRenderer>().flipX = false; }
        if (Input.GetKey(KeyCode.RightArrow)) { movement.x += 1; GetComponent<SpriteRenderer>().flipX = true; }
        if (Input.GetKey(KeyCode.Space) && isFloored) { movement.y = jumpForce; animator.SetTrigger("Jumping"); }
        movement.x *= speed;
        rb.velocity = movement;
        Animations();

        if (transform.position.y < -10)
        {
            if(respawnButton.transform.localScale.x != 1) { respawnButton.transform.localScale = new Vector3(1,1,0); }
            if (Input.GetKeyDown(KeyCode.Space)) { Respawn(); }
        }
    }

    private void Animations()
    {
        animator.SetFloat("Velocity", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("FallVelocity", rb.velocity.y);
    }

    public void Respawn()
    {
        rb.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        respawnButton.transform.localScale = new Vector3(0, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isFloored = true;
        animator.SetBool("Grounded", true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isFloored = false;
        animator.SetBool("Grounded", false);
    }
}
