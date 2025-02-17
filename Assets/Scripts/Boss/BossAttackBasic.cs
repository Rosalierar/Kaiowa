using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttackBasic : MonoBehaviour
{
    [SerializeField] int valueDamage = 35;

    //CLASS
    BossMovement bossMovement;

    [SerializeField] LayerMask layer;
    
    [SerializeField] int distance;
    [SerializeField] Transform rayPosition;
    RaycastHit2D hit;

    Animator animBoss;

    [SerializeField] bool isAtkBasic;

    [SerializeField] float countDown;

    [SerializeField] bool hasCollision = false;

    bool isFacingRight;

    // Start is called before the first frame update
    void Start()
    {
        bossMovement = GetComponent<BossMovement>();
        isFacingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        DerectionCollision();
        AttackBasic();
        CheckCollision();
    }

    void DerectionCollision()
    {
        isFacingRight = bossMovement.isFacingRight;

        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;

        hit = Physics2D.Raycast(rayPosition.position, direction, distance, layer);
        Debug.DrawRay(rayPosition.position, direction * distance, Color.cyan);
    }

    void CheckCollision()
    {
        if (hit.collider.name == "Player")
        {
            if (countDown == 0f && !isAtkBasic)
            {
                Debug.Log("Colidiu para Ataque Basico");
                hasCollision = true;
            }
        }
        else
        {
            Debug.Log("nao Colidiu");
        }
    }

    void AttackBasic()
    {
        //ATACA QUANDO O RAY CAST DETECTA QUANDO NAO ESTA COUNDOWN
        if (hasCollision && !isAtkBasic)
        {
            //PARAR PARA REALIZAR ATAQUE
            StartCoroutine(StopMoveForATK());
            
            Debug.Log("Hit Esquerda: " + hit.collider.tag);

            animBoss.SetTrigger("isAtkBasic");
        } 
        //SE JA ATACOU ESPERA 5 SEGUNDOS PARA ATACAR DE NOVO
        if (isAtkBasic && countDown <= 5)
        {
            countDown += Time.deltaTime;
           
            if (countDown > 5)
            {
                countDown = 0;
                isAtkBasic = false;
            }
        }
    }

    IEnumerator StopMoveForATK()
    {
        Debug.Log("Parou para atacar!");

        bossMovement.canMove = false;

        yield return new WaitForSeconds(3f);

        Debug.Log("Terminou o atacar!");

        isAtkBasic = true;
        bossMovement.canMove = true;
        hasCollision = false;
    }

    //DAR DANO AO ATACAR
    private void OnTriggerEnter2D(Collider2D collision)
    {

        CapsuleCollider2D capsuleCollider2D = collision.GetComponent<CapsuleCollider2D>();

        if (collision.gameObject.CompareTag("Player") && isAtkBasic && capsuleCollider2D != null)
        {
            //animBoss.SetTrigger("isAtkBasic");

            PlayerLogic playerLogic = GameObject.FindWithTag("Player").GetComponent<PlayerLogic>();
            Health playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();

            Debug.Log("Dano vindo da direita?" + playerLogic.isKnockRight);
            //Vector2 localDamage = (transform.position - collision.transform.position).normalized;
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

            /*if (playerHealth.health > 0)
                Destroy(gameObject);*/
        }
    }

}
