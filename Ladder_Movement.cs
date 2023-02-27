using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder_Movement : MonoBehaviour
{
    private float vertical;
    private float speed = 2.2f;
    private bool isLadder;
    private bool isClimbing;

    
    [SerializeField] 
    private new Rigidbody2D rigidbody;

    void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (isLadder && Mathf.Abs(vertical) > 0f)
        {
            isClimbing = true;
        }
    }
    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rigidbody.gravityScale = 0f;
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, vertical * speed);
        }
        else
        {
            rigidbody.gravityScale = 4f;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = true;
            Debug.Log("Mario używa drabiny");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isLadder = false;
            isClimbing = false;
            Debug.Log("Mario przestaje używać drabiny lub ją mija");
        }
    }
}
