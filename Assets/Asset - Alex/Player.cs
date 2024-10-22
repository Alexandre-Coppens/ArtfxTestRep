using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float jumpForce = 3f;
    [SerializeField] private Vector2 movement;
    [SerializeField] private bool canJump = true;
    [SerializeField] bool isFloored = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Vector2.zero;
        movement.y = rb.velocity.y;
        if (Input.GetKey(KeyCode.LeftArrow)) { movement.x -= 1; }
        if (Input.GetKey(KeyCode.RightArrow)) { movement.x += 1; }
        if (Input.GetKey(KeyCode.Space) && isFloored) { movement.y = jumpForce; }
        movement.x *= speed;
        rb.velocity = movement;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isFloored = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isFloored = false;
    }
}
