using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackBullet : MonoBehaviour
{
    //CLASS
    MoveBulletBoss moveBulletBoss;
    BossMovement bossMovement;

    //POSITIONS
    [SerializeField] Transform SpawnBulletTransform;
    [SerializeField] Vector2[] directionBullet = new Vector2[8]; //DIRECTIONS BULLETS (0 = UP, 1 = 45º, 2 = RIGHT, 3 = 135º, 4 = DOWN, 5 = -135º 6 = LEFT, 7 = -45º 

    //GAME OBJECTS
    [SerializeField] GameObject bulletBossPrefab;

    //CONTROLLERS
    bool isInstantiate = false;
    int index = 0;

    float timeForAtkBullet;
    bool isCountDown;

    // Start is called before the first frame update
    void Start()
    {
        AtribuirDirecoes();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCountDown)
            timeForAtkBullet += Time.deltaTime;
        else EnviarInformacoes();
    }

    void AtribuirDirecoes()
    {
        directionBullet[0] = transform.up; //CIMA
        directionBullet[1] = (transform.up + transform.right).normalized; //45
        directionBullet[2] = transform.right; //DIREITA
        directionBullet[3] = (transform.right + -transform.up).normalized; //135
        directionBullet[4] = -transform.up; //BAIXO
        directionBullet[5] = (-transform.right + -transform.up).normalized; //-135
        directionBullet[6] = -transform.right; //ESQUERDA
        directionBullet[7] = (-transform.up + transform.right).normalized; //-45
    }

    void EnviarInformacoes()
    {
        if (!isInstantiate && timeForAtkBullet > 8)
        {
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
            timeForAtkBullet = 0;
            isCountDown = false;
            index++;
        }
        else
        {
            index = 0;
            isCountDown = true;
        }

        yield return new WaitForSeconds(0.5f);

        isInstantiate = true;
    }
    
}
