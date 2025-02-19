using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.AnimatedValues;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class BossAttackBullet : MonoBehaviour
{
    [SerializeField] LayerMask layer;
    [SerializeField] int distance;
    [SerializeField] Transform rayPosition;
    RaycastHit2D hit;

    //CLASS
    MoveBulletBoss moveBulletBoss;
    BossMovement bossMovement;
    Animator animBoss;

    //POSITIONS
    [SerializeField] Transform SpawnBulletTransform;
    [SerializeField] Vector2[] directionBullet = new Vector2[8]; //DIRECTIONS BULLETS (0 = UP, 1 = 45º, 2 = RIGHT, 3 = 135º, 4 = DOWN, 5 = -135º 6 = LEFT, 7 = -45º 

    //GAME OBJECTS
    [SerializeField] GameObject bulletBossPrefab;

    //CONTROLLERS
    [SerializeField] bool isInstantiate = false;
    [SerializeField] int index = 0;

    [SerializeField] float timeForAtkBullet;
    [SerializeField] bool isCountDown;

    [SerializeField] bool hasCollision;
    bool isFacingRight;

    bool animationPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        bossMovement = gameObject.GetComponent<BossMovement>();
        AtribuirDirecoes();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCountDown && timeForAtkBullet < 8)
        {
            timeForAtkBullet += Time.deltaTime;
            Debug.Log("eMcOUNTdOWN");
            if (timeForAtkBullet >= 8)
            {
                isCountDown = false;
            }

        }
        else if (hasCollision)
        {
            Debug.Log("vAI iNSTANCIAR!");
            EnviarInformacoes();
        }

        DerectionCollision();
        CheckCollision();

    }
    void DerectionCollision()
    {
        isFacingRight = bossMovement.isFacingRight;
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;

        hit = Physics2D.Raycast(rayPosition.position, direction, distance, layer);
        Debug.DrawRay(rayPosition.position, direction * distance, Color.blue);
    }
    void CheckCollision()
    {
        if (hit.collider.name == "Player")
        {
            Debug.Log("Colidiu Para Ataque Bullet");
            hasCollision = true;
        }
    }

    void AtribuirDirecoes()
    {
        Debug.Log("Atribui as Direcoes");
        directionBullet[0] = transform.up; //CIMA
        directionBullet[1] = (transform.up + transform.right).normalized; //45
        directionBullet[2] = transform.right; //DIREITA
        directionBullet[3] = (transform.right + -transform.up).normalized; //135
        directionBullet[4] = -transform.up; //BAIXO
        directionBullet[5] = (-transform.right + -transform.up).normalized; //-135
        directionBullet[6] = -transform.right; //ESQUERDA
        directionBullet[7] = (transform.up + -transform.right).normalized; //-45
    }

    void EnviarInformacoes()
    {
        if (!isInstantiate && !isCountDown)
        {
            /*// Verifique se a animação já foi tocada
            if (!animationPlayed)
            {
                animBoss.SetTrigger("isAtkBullet");
                animationPlayed = true; // Garantir que a animação só seja chamada uma vez
            }*/

            bossMovement.canMove = false;
            Debug.Log("Spawndando:" + index);
            GameObject bulletBoss = Instantiate(bulletBossPrefab, SpawnBulletTransform.position, transform.rotation);
            MoveBulletBoss moveBulletBoss = bulletBoss.GetComponent<MoveBulletBoss>();
            bulletBoss.transform.SetParent(SpawnBulletTransform);
            moveBulletBoss.GetDirections(directionBullet, index);
            isInstantiate = true;
            
            
            StartCoroutine(CountDown());
        }
    }

    IEnumerator CountDown()
    {
        if (index + 1 < 8)
        {
            isCountDown = false;
            index++;

            Debug.Log("Proximo ataque/iNSTANCIA é: " + index);
        }
        else
        {
            Debug.Log("Instanciou todos!");
            timeForAtkBullet = 0;
            index = 0;
            isCountDown = true;
            bossMovement.canMove = true;
            animationPlayed = false;
        }

        yield return new WaitForSeconds(0.2f);

        isInstantiate = false;
    }
    
}
