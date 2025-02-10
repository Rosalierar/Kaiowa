using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBush : MonoBehaviour
{
    ControlScene controlScene;
    public Health playerHealth;

    private float timer;
    public float speed = 20;

    private Rigidbody2D rig;
    //public GameObject left, right, upleft, upright;//DirectionLeft, DirectioRight, DirectionUpLeft, DirectionUpRight;

    bool reiniciar = false;

    Transform bulletPos;

    bool bulletsInstance = false;

    int whoSpawn;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        InicializandoBullets();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }
    void InicializandoBullets()
    {
        if (whoSpawn == 0) //Virar direita
        {
            rig.velocity = transform.right * speed;
        }
        if (whoSpawn == 1) // Virar Esquerda
        {
            rig.velocity = -transform.right * speed;
        }
        if (whoSpawn == 2) // cima esq
        {
            float angle = 15f; // �ngulo de 15 graus
            float radians = angle * Mathf.Deg2Rad; // Converter o �ngulo para radianos

            // Calcular a dire��o usando seno e cosseno, mas invertendo a dire��o X
            Vector2 direction = new Vector2(-Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

            rig.velocity = direction * speed;
        }
        if (whoSpawn == 3) // cima dir
        {
            float angle = 15f; // �ngulo de 15 graus
            float radians = angle * Mathf.Deg2Rad; // Converter o �ngulo para radianos

            // Calcular a dire��o usando seno e cosseno
            Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

            rig.velocity = direction * speed;
        }
    }
    public void GetInformationsBulletpos(int localWhereSpawn, Transform shotPos)
    {
        whoSpawn = localWhereSpawn;
        bulletPos = shotPos;
    }
}
