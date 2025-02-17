using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBush : MonoBehaviour
{
    [SerializeField] int valueDamage = 25;

    private float timerBush;

    public float speed = 20;

    public Rigidbody2D rig;

    Transform bulletPos;

    bool bulletsInstance = false;

    int whoSpawn;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        // Chamando a inicialização da direção
        InicializandoBullets();
    }

    private void Update()
    {
        timerBush += Time.deltaTime;

        if (timerBush > 3)
        {
            Destroy(gameObject);
        }
    }

    void InicializandoBullets()
    {
        if (whoSpawn == 0) // Direita
        {
            rig.velocity = transform.right * speed;
            Debug.Log("Bullet moving right");
        }
        else if (whoSpawn == 1) // Esquerda
        {
            rig.velocity = -transform.right * speed;
            Debug.Log("Bullet moving left");
        }
        else if (whoSpawn == 2) // Cima à esquerda
        {
            float angle = 15f;
            float radians = angle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(-Mathf.Cos(radians), Mathf.Sin(radians)).normalized;
            rig.velocity = direction * speed;
            Debug.Log("Bullet moving up left");
        }
        else if (whoSpawn == 3) // Cima à direita
        {
            float angle = 15f;
            float radians = angle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;
            rig.velocity = direction * speed;
            Debug.Log("Bullet moving up right");
        }
    }

    public void GetInformationsBulletpos(int localWhereSpawn, Transform shotPos)
    {
        whoSpawn = localWhereSpawn;
        bulletPos = shotPos;

        // Assim que as informações da posição e direção forem passadas, inicializa o movimento
        InicializandoBullets();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerLogic playerLogic=GameObject.FindWithTag("Player").GetComponent<PlayerLogic>(); 
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

            if (playerHealth.health > 0)
                Destroy(gameObject);
        }
    }
}
