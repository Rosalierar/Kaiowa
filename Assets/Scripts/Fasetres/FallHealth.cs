using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallHealth : MonoBehaviour
{
    Health playerHealth;
    [SerializeField] GameObject player;

    [SerializeField] int whoHealth;

    [SerializeField] Rigidbody2D rbHealth;

    [SerializeField] LayerMask layer;
    [SerializeField] Transform rayPosition;
    [SerializeField] int distance;
    Vector2 direction;
    RaycastHit2D hitHealth;
    // Start is called before the first frame update
    void Start()
    {
        rbHealth = GetComponent<Rigidbody2D>();
        rbHealth.gravityScale = 0f;
        playerHealth = player.GetComponent<Health>();
        direction = Vector2.down;
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerHealth();
       // RayCastHealth();
        //CheckRayCollision();
    }

    void RayCastHealth()
    {
        hitHealth = Physics2D.Raycast(rayPosition.position, direction, distance, layer);
        Debug.DrawRay(rayPosition.position, direction * distance, Color.red);
    }

    void CheckRayCollision()
    {
        if (hitHealth.collider.tag == "Chao")
        {
            Debug.Log("rbMudada");
            rbHealth.bodyType = RigidbodyType2D.Static;
        }
    }

    void CheckPlayerHealth()
    {
        if (playerHealth.health <= 50)
        {
            rbHealth.gravityScale = 0.05f;

            RayCastHealth();
            CheckRayCollision();
        }
    }
}
