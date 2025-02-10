using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBush : MonoBehaviour
{
    ControlScene controlScene;
    public Health playerHealth;


    //public GameObject left, right, upleft, upright;//DirectionLeft, DirectioRight, DirectionUpLeft, DirectionUpRight;

    private Rigidbody2D rig;
    public float speed;
    private float timer;
    private float timerspawn;
    bool reiniciar = false;

    GameObject[] bulletsFireBush;

    Rigidbody2D[] rigbullets;

    void Start()
    {

    }
    
    void InicializandoBullets()
    {
        for (int i = 0; i < bulletsFireBush.Length; i++)
        {
            if (i == 0) //Virar direita
            {
               rigbullets[i].velocity = transform.right * speed;
            }
            if (i == 1) // Virar Esquerda
            {
                rigbullets[i].velocity = -transform.right * speed;
            }
            if (i == 2) // cima esq
            {
                float angle = 15f; // Ângulo de 15 graus
                float radians = angle * Mathf.Deg2Rad; // Converter o ângulo para radianos

                // Calcular a direção usando seno e cosseno, mas invertendo a direção X
                Vector2 direction = new Vector2(-Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

                rigbullets[i].velocity = direction * speed;
            }
            if (i == 3) // cima dir
            {
                float angle = 15f; // Ângulo de 15 graus
                float radians = angle * Mathf.Deg2Rad; // Converter o ângulo para radianos

                // Calcular a direção usando seno e cosseno
                Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

                rigbullets[i].velocity = direction * speed;
            }
        }
    }
    void Update()
    {
        /*
        timer += Time.deltaTime;

        if (timer > 10)
        {
            Destroy(gameObject);
        }
        if (reiniciar)
        {
            timerspawn += Time.deltaTime;
        }
        CarregarCena();
        */
    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
            playerHealth.health -= 20;
            playerHealth.slider.value = playerHealth.health;
            if (playerHealth.health <= 0)
            {
                reiniciar = true;
                StartCoroutine(playerHealth.TimeForRespawn());
                Debug.Log("Morri!");
                playerHealth.textRespawnGameObj.SetActive(true);
            }
            if (playerHealth.health > 0)
                Destroy(gameObject);
        }

    }
    void CarregarCena()
    {
        if (timerspawn > 2)
        {
            playerHealth.controlScene.LoadScene();
        }
    }
    */
}
