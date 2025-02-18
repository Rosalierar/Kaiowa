using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCure : MonoBehaviour
{
    [SerializeField] GameObject cura0;
    [SerializeField] GameObject cura1;
    [SerializeField] GameObject cura2;
    [SerializeField] GameObject cura3;
    [SerializeField] GameObject cura4;

    int whoFall = 5;

    FallHealth fallHealth;
    Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.Find("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSevenFive();
    }

    void CheckSevenFive()
    {
        if (playerHealth.health <= 75)
        {
            switch (whoFall) { 
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        }
    }
}
