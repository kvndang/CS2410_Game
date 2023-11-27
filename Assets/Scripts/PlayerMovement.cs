using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    public Tilemap obstacles;
    public SpriteRenderer sprite;
    private Rigidbody2D player;
    [SerializeField] private float speed;
    [SerializeField] private float velocity;
    public Transform groundCheck;
    public LayerMask groundLayer;
    

    // Start is called before the first frame update
    void Start()
    {
        //get the RigidBody references from Unity Editor
        player = GetComponent<Rigidbody2D>(); 

    }

    // Update is called once per frame
    void Update()
    {
        //This is so we can track the player's velocity
        velocity = player.velocity.x;
        //Input.GetAxis() allows us to use the A or D keys for horizontal movement. 
        //Return value is -1 or 1 for left and right respectively. Needs a speed multiplier.
        float horizontal = Input.GetAxisRaw("Horizontal");
        player.velocity = new Vector2(horizontal * speed, player.velocity.y);
        
        if(horizontal <= -1)
        {
            sprite.flipX = true;
        }
        else if(horizontal >= 1)
        {
            sprite.flipX = false;
        }
        //Jump key checker
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
            player.velocity = new Vector2(player.velocity.x, speed);
    }
    
    /// <summary>
    /// Creates a circle at player's feet. If it collides with the ground layer, then it will return true
    /// </summary>
    /// <returns></returns>
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

}
