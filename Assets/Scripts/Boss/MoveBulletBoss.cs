using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MoveBulletBoss : MonoBehaviour
{
    [SerializeField] Rigidbody2D rbBullet;
    
    //DIRECTIONS BULLETS (0 = UP, 1 = 45º, 2 = RIGHT, 3 = 135º, 4 = DOWN, 5 = -135º 6 = LEFT, 7 = -45º 
    Vector2 directionBullet;
    [SerializeField] float speedBullet;


    // Start is called before the first frame update
    void Start()
    {
        rbBullet.velocity = directionBullet * speedBullet;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDirections(Vector2[] directions, int index)
    {
        for (int i = 0; i < directions.Length; i++)
        { 
            if (i == index)
            {
                directionBullet = directions[i];
            }
        }
    }

    private void OnBecameInvisible()
    {
        Debug.Log("Sai da Camera" + directionBullet);
        Destroy(gameObject);
    }
}
