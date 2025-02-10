using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemies : MonoBehaviour
{
    ControlScene controlScene;
    public Health playerHealth;

    [SerializeField] int valueDamage;

    private GameObject player;
    private Rigidbody2D rig;
    public float Force;
    private float timer;
    private float timerspawn;
    bool reiniciar = false;
    
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rig.velocity = new Vector2(direction.x, direction.y).normalized * Force;

        transform.rotation = Quaternion.Euler(0,0, 0);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerLogic playerLogic = GameObject.FindWithTag("Player").GetComponent<PlayerLogic>();
            playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();

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

    /*void CarregarCena()
    {
        if (timerspawn > 2)
        {
            playerHealth.controlScene.LoadScene();
        }
    }*/
}
