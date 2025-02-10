using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    EnemyMovement enemyMovement;

    EnemyData enemyData;

    public Health playerHealth;

    CircleCollider2D circleCollider;
    Animator anim;

    //Inimigos atirando
    public GameObject bullet;
    public Transform bulletPos;
    private GameObject Player;

    public Transform playerTransform;

    private float timer;

    //fogo humano ataque
    public bool isRight = true;
    [SerializeField] private float timerHumanFire;
    [SerializeField] bool canAtk = true;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
        enemyData = GetComponent<EnemyData>();
        circleCollider = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(enemyData.character[0]);
        
        // SE FOR O FOGO ARBUSTO
        if (enemyData.character[0])
        {
           
            //COLOCAR O VOID DO METODO
        }

        // SE FOR O FOGO VOADOR
        if (enemyData.character[1])
        {
            float distance = Vector2.Distance(transform.position, Player.transform.position);
            if (distance < 35)
            {
                timer += Time.deltaTime;
                if (timer > 3)
                {
                    timer = 0;
                    shooting();
                }
            }
        }

        // SE FOR O FOGO HUMANOID
        if (enemyData.character[2])
        {
           if (!canAtk)
            {
                TimeForFireHumam();
            }
            //COLOCAR O VOID DO METODO
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 localDamage = (transform.position - collision.transform.position).normalized;
            Debug.Log("tomei dano");
            playerHealth.TakeDamage(enemyData.enemyData[1], localDamage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && circleCollider.gameObject.CompareTag("FogoHumanoide"))
        {
            Debug.Log("cOLIDIO CircleCollider");
            Attack();
        }
        if (collision.gameObject.tag == "Player")
        {
            Vector2 localDamage = (transform.position - collision.transform.position).normalized;
            Debug.Log("tomei dano");
            playerHealth.TakeDamage(enemyData.enemyData[1], localDamage);
        }
    }
    /// <summary>
    /// ///////////////// FOGO ARRUSTO
    /// </summary>
    /// 

    void AttackfireBush()
    {

    }









    /// <summary>
    /// ///////////////// FOGO HUMANOIDE
    /// </summary>
    /// 
    public void ArmazenarLocalizacaoPlayer(Transform player)
    {
        playerTransform = player;
    }

    public void ArmazenarFacingRight(bool facing)
    {
        isRight = facing;
    }
    void Attack()
    {
        Debug.Log(canAtk);
        if (canAtk && ((gameObject.transform.position.x < playerTransform.transform.position.x && isRight) || (gameObject.transform.position.x > playerTransform.transform.position.x && !isRight)))
        {
            Debug.Log("Entrou no can atk iiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii");
            anim.SetTrigger("isShot");

            canAtk = false;
        }
    }

    void TimeForFireHumam()
    {
        timerHumanFire += Time.deltaTime;

        if (timerHumanFire > 3)
        {
            timerHumanFire = 0;
            canAtk = true;
        }
    }

    /// <summary>
    /// ///////////////// FOGO VOADOR
    /// </summary>
    void shooting()
    {
        anim.SetTrigger("isShot");
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
