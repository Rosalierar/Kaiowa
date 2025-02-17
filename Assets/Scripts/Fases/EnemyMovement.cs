using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class EnemyMovement : MonoBehaviour
{
    public bool humanFireFacingRight;

    SpawnEnemy spawnEnemy;
    EnemyData enemyData;
    bool[] whoEnemy = new bool[4];

    public Transform playerTransform;
    public bool isChasing;

    public float chaseDistance;
    public float playerDistance;

    //Pontos de patrulha dos inimigos
    public bool[] Phase;
    public int qualPatrolPont;

    public Transform[] armazenarLocalizacaoDePatrolPointPrimeiroFase1;
    public Transform[] armazenarLocalizacaoDePatrolPointSegundoFase1;

    public Transform[] armazenarLocalizacaoDePatrolPointPrimeiroFase2;
    public Transform[] armazenarLocalizacaoDePatrolPointSegundoFase2;

    public Transform[] patrolPoints;

    public float moveSpeedEnemy;
    public int patrolDestination;
    ////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        enemyData = GetComponent<EnemyData>();

        for (int i = 0; i < enemyData.character.Length; i++)
        {
            if (enemyData.character[i])
                whoEnemy[i] = true;
            else
                whoEnemy[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (whoEnemy[0])
        {
            MovementBushFire();
        }
        if (whoEnemy[1] || whoEnemy[2])
        {
            MovementEnemies();
        }
    }

    /// <summary>
    /// ///////////////// FOGO ARBUSTO MOVIMENTO
    /// </summary>
    /// 
    private void MovementBushFire()
    {
        if (isChasing)
        {
            //se o jogador esta no lado esquerdo
            if (transform.position.x > playerTransform.position.x)
            {
                //virar personagem
                transform.localScale = new Vector3(1, 1, 1);
                //perseguir o personagem
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeedEnemy * Time.deltaTime);
                //transform.position += Vector3.left * moveSpeedEnemy * Time.deltaTime;
            }
            //se o jogador esta no lado direito
            if (transform.position.x < playerTransform.position.x)
            {
                //virar personagem
                transform.localScale = new Vector3(-1, 1, 1);
                //perseguir o personagem
                transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeedEnemy * Time.deltaTime);
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
    public void ArmazenarLocalizacaoPlayer(Transform player)
    {
       playerTransform = player;
    }
     /// <summary>
    /// ///////////////// FOGO VOADOR E  HUMANOIDE MOVIEMENTO
    /// </summary>
    private void MovementEnemies()
    {
        Debug.Log("Face Inimiga" + humanFireFacingRight);

        // Move o inimigo em direção ao ponto de patrulha atual
        Transform target = patrolPoints[patrolDestination]; // Pega o ponto de patrulha atual
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeedEnemy * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.2f) // Definindo uma distância de "chegada"
        {
            // Alterna entre o ponto 0 e o ponto 1
            patrolDestination = (patrolDestination + 1) % 2;
            // Atualiza a direção do inimigo para que ele olhe na direção certa
            if (patrolDestination == 0)
            {
                humanFireFacingRight = true;
                transform.localScale = new Vector3(1, 1, 1); // Olha para a direita
            }
            else
            {
                humanFireFacingRight = false;
                transform.localScale = new Vector3(-1, 1, 1); // Olha para a esquerda
            }
            EnemyDamage enemyDamage = GetComponent<EnemyDamage>();
            enemyDamage.ArmazenarFacingRight(humanFireFacingRight);
        }
    }
    //metodo para pegar o gameobject do player para fazer a perseguicao

    public void ArmazenarPatrolPoints(int qualoPatrolPont, bool[] phase, Transform[] fase1Primeiro, Transform[] fase1Segundo, Transform[] fase2Primeiro, Transform[] fase2Segundo)
    {
        if (phase[0]) // Fase 1
        {
            patrolPoints[0] = fase1Primeiro[qualoPatrolPont];
            patrolPoints[1] = fase1Segundo[qualoPatrolPont];
        }
        else if (phase[1]) // Fase 2
        {
            patrolPoints[0] = fase2Primeiro[qualoPatrolPont];
            patrolPoints[1] = fase2Segundo[qualoPatrolPont];
        }
    }
}
