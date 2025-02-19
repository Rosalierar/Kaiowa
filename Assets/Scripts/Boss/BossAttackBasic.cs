using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class BossAttackBasic : MonoBehaviour
{
    [SerializeField] int valueDamage = 35;

    //CLASS
    BossMovement bossMovement;

    //PROPRIETIES
    PolygonCollider2D polygon;

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
        animBoss = GetComponent<Animator>();
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

        } 
        //SE JA ATACOU ESPERA 6 SEGUNDOS PARA ATACAR DE NOVO
        if (isAtkBasic && countDown <= 6 && !hasCollision)
        {
            countDown += Time.deltaTime;
           
            if (countDown > 6)
            {
                countDown = 0;
                isAtkBasic = false;
            }
        }
    }

    IEnumerator StopMoveForATK()
    {
        polygon = GetComponent<PolygonCollider2D>();

        bossMovement.canMove = false;
        Debug.Log("Parou para atacar!");
        isAtkBasic = true;
        animBoss.SetTrigger("isAtkBasic");

        AnimatorStateInfo stateInfo = animBoss.GetCurrentAnimatorStateInfo(0);
        {
            if (stateInfo.normalizedTime >= 0.7f)  // normalizedTime varia de 0 a 1
            {
                polygon.enabled = true;
            }
        }

        yield return new WaitForSeconds(2f);

        polygon.enabled = false;
        bossMovement.canMove = true;
        hasCollision = false;

        Debug.Log("Terminou o atacar!");
    }

    //DAR DANO AO ATACAR
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CapsuleCollider2D capsuleCollider2D = collision.GetComponent<CapsuleCollider2D>();

        if (collision.gameObject.CompareTag("Player") && isAtkBasic)
        {
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
        }
    }

}
