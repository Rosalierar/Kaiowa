using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossControlMovement : MonoBehaviour
{
    //COTROLLER MOVEMENT
    [SerializeField] bool canWalk = true;

    //TIMER MOVEMENT
    float timeStopMovement = 20f;
    float timeCanMove = 5f;
    [SerializeField] float timeMovement;

    //SCRIPT MOVEMENT
    BossMovement bossMovement;
    Rigidbody2D rbBoss;

    //VALUE ATTACK
    int valueDamage = 20;

    // Start is called before the first frame update
    void Start()
    {
        bossMovement = GetComponent<BossMovement>();
        rbBoss = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        StopMove(canWalk);
    }

    void StopMove(bool whatDo)
    {
        timeMovement += Time.deltaTime;

        if (whatDo)
        {
            //nao vai poder se mexer assim que o tempo ultrapassar
            if (timeMovement >= timeStopMovement)
            {
                rbBoss.velocity = Vector2.zero;
                bossMovement.canMove = false;

                timeMovement = 0;
                canWalk = false;
            }
        }
        else
        {
            //vai voltar a se mexer
            if (timeMovement >= timeCanMove)
            {
                bossMovement.canMove = true;
                timeMovement = 0;
                canWalk = true;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerLogic playerLogic = GameObject.FindWithTag("Player").GetComponent<PlayerLogic>();
            Health playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();

            Debug.Log("Dano vindo da direita?" + playerLogic.isKnockRight);
            playerLogic.kBCount = playerLogic.kBTime;

            if (collision.transform.position.x <= transform.position.x)
            {
                Debug.Log("Dano vindo da direita?" + playerLogic.isKnockRight);
                playerLogic.isKnockRight = true;
            }
            if (collision.transform.position.x > transform.position.x)
            {
                Debug.Log("Dano vindo da direita?" + playerLogic.isKnockRight);
                playerLogic.isKnockRight = false;
            }

            playerHealth.TakeDamage(valueDamage);
        }
    }
}
