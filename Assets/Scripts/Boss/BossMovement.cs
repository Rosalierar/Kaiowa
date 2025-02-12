using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    //CLASS
    Animator animatorBoss;
    Transform playerTransform;

    //CONTROLLER POSITION
    [SerializeField] private bool isChasing;
    [SerializeField] private int chaseDistance;

    //MOVEMENT
    [SerializeField] private float moveSpeedBoss;
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        MoveLogic();
    }

    public void MoveLogic()
    {
        if (isChasing && canMove)
        {
            //se o jogador esta no lado esquerdo
            if (transform.position.x > playerTransform.position.x)
            {
                //virar personagem
                transform.localScale = new Vector3(3, 3, 3);
                //perseguir o personagem
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeedBoss * Time.deltaTime);
            }
            //se o jogador esta no lado direito
            if (transform.position.x < playerTransform.position.x)
            {
                //virar personagem
                transform.localScale = new Vector3(-3, 3, 3);
                //perseguir o personagem
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeedBoss * Time.deltaTime);
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }
        }
    }
    public void ArmazenarPlayerPosition(Transform playerPosition)
    {
        playerTransform = playerPosition;
    }
}
