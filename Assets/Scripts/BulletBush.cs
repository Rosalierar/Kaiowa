using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBush : MonoBehaviour
{
    ControlScene controlScene;
    public Health playerHealth;
    //public GameObject left, right, upleft, upright;//DirectionLeft, DirectioRight, DirectionUpLeft, DirectionUpRight;
    private Rigidbody2D rig;
    public float Force;
    private float timer;
    private float timerspawn;
    bool reiniciar = false;
    GameObject[] bulletsFireBush;
    Rigidbody2D[] rigbullets;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        /*
        left = GameObject.FindGameObjectWithTag("DirectionLeft");

        Vector3 directionl = left.transform.position - transform.position;

        rig.velocity = new Vector2(directionl.x, directionl.y).normalized * Force;

        transform.rotation = Quaternion.Euler(0, 0, 0);
        */
    }
    /*
    void InicializandoBullets()
    {
        for (int i = 0; i < bulletsFireBush.Length; i++)
        {
            if (i == 0) //Virar direita
            {
               rigbullets[i].velocity = transform.right * Force;
            }
            if (i == 1) // Virar Esquerda
            {
                rigbullets[i].velocity = -transform.right * Force;
            }
            if (i == 2) // cima esq
            {
                //rigbullets[i].velocity = ;
            }
            if (i == 3) // cima dir
            {
                //rigbullets[i].velocity = ;
            }
            
        }
    }
    */
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
