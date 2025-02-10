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
        if (whoSpawn == 0) // Direita
        {
            rig.velocity = transform.right * speed;
            Debug.Log("Bullet moving right");
        }
        if (whoSpawn == 1) // Esquerda
        {
            rig.velocity = -transform.right * speed;
            Debug.Log("Bullet moving left");
        }
        if (whoSpawn == 2) // Cima à esquerda
        {
            float angle = 15f;
            float radians = angle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(-Mathf.Cos(radians), Mathf.Sin(radians)).normalized;
            rig.velocity = direction * speed;
            Debug.Log("Bullet moving up left");
        }
        if (whoSpawn == 3) // Cima à direita
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
    }
}
