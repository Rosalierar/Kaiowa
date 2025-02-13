using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackBasic : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    
    Vector2 directionRight;
    Vector2 directionLeft;
    [SerializeField] int distance;
    [SerializeField] Transform rayPosition;
    RaycastHit2D hitRight;
    RaycastHit2D hitLeft;

    Animator animBoss;

    // Start is called before the first frame update
    void Start()
    {
        directionLeft = Vector2.left;
        directionRight= Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {
        DetctionCollision();
    }

    void DetctionCollision()
    {
        hitLeft = Physics2D.Raycast(rayPosition.position, directionLeft, distance, layer);
        Debug.DrawRay(rayPosition.position, directionLeft * distance, Color.cyan);

        hitRight = Physics2D.Raycast(rayPosition.position, directionRight, distance, layer);
        Debug.DrawRay(rayPosition.position, directionRight * distance, Color.blue);

        if (hitLeft.collider.name == "Player" || hitRight.collider.name == "Player")
        {
            Debug.Log("Hit: " + hitLeft.collider.tag);
            animBoss.SetTrigger("isAtkBasic");
        }
    }
}
