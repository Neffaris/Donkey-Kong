using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameObject currentPlatform;
    public GameObject Donkey_Kong;
    public GameObject Barrel_Spawner;
    public Transform Mario;

    public GameObject Barrel;
     private new Rigidbody2D rigidbody;
     public float Movement_Speed = 4f;
    public float Jump_Force = 4f;
    public float Distance_between_Mario_and_Barrel;

    public RaycastHit2D hit;

    [SerializeField] private CapsuleCollider2D playerCollider;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        if (Input.GetButtonDown("Jump") && Mathf.Abs(rigidbody.velocity.y) < 0.01f) 
        {
            rigidbody.AddForce(new Vector2(0, Jump_Force), ForceMode2D.Impulse);
            Debug.Log("Mario skacze");
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            if (currentPlatform != null)
            {
                StartCoroutine(DisableCollision());
            }
        }
        //Wykrywanie beczki raycastem
        float ray_Distance = 1;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, ray_Distance);

        if(hit.collider != null)
        {
            if(hit.collider.gameObject.CompareTag("Barrel"))
            { 
                hit.collider.transform.GetComponent<Obstacle>().OnPlayerJumpsOver();
            }
            else
            {   
                // Specjalnie nic sie nie dzieje
                // Debug.Log("Promień(Raycast) dotyka coś innego niż beczka");
            }
        }
        //Raycast
        Debug.DrawRay(transform.position, Vector2.down * ray_Distance, Color.green);

        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Movement_Speed * Time.deltaTime;

        if (movement > 0f) {
            transform.eulerAngles = Vector3.zero;
        }
        else if (movement < 0f) {
            transform.eulerAngles = new Vector3(0, 180f, 0f);
        }
    }
    // void FixedUpdate()
    // {
    //     float ray_Distance = 1;
    //     RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, ray_Distance);

    //     if(hit.collider != null)
    //     {
    //         if(hit.collider.gameObject.CompareTag("Barrel"))
    //         { 
    //             Debug.Log("Mario przeskakuje nad beczką i dostaje 100 punktów");
    //             hit.collider.transform.GetComponent<Obstacle>().OnPlayerJumpsOver();
    //         }
    //         else
    //         {   
    //             // Specjalnie nic sie nie dzieje
    //             // Debug.Log("Promień(Raycast) dotyka coś innego od beczki");
    //         }
    //     }
    //     //Raycast
    //     Debug.DrawRay(transform.position, Vector2.down * ray_Distance, Color.green);
    // }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentPlatform = collision.gameObject;
        }
        else if(collision.gameObject.CompareTag("Objective"))
        {
            enabled = false;
            Destroy(Barrel_Spawner); // Beczki przestają się pojawiać
            // Barrel_Spawner.SetActive(false); dlaczego to nie działa?
            Donkey_Kong.GetComponent<BoxCollider2D>().enabled = false; //Donkey Kong spada
            FindObjectOfType<Game_Manager>().Level_Complete();
        }
        else if (collision.gameObject.CompareTag("Barrel"))
        {
            enabled = false;
            FindObjectOfType<Game_Manager>().Level_Failed();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWayPlatform"))
        {
            currentPlatform = null;
        }
    }

    private IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = currentPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(2.5f);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
    
}
