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

    public int whoFall = 5;

    FallHealth fallHealth;
    Health playerHealth;
    GetHealth getHealth;

    public bool pegouCura;

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
        if (playerHealth.health < 75)
        {
            switch (whoFall) { 
                case 0:
                    if (cura0 == null)
                    {
                        Destroy(gameObject);
                    }
                    break;
                case 1:
                    if (cura1 == null)
                    {
                        cura0.SetActive(true);
                        getHealth = cura0.GetComponent<GetHealth>();
                        whoFall--;
                    }
                    break;
                case 2:
                    if (cura2 == null)
                    {
                        cura1.SetActive(true);
                        getHealth = cura1.GetComponent<GetHealth>();
                        whoFall--;
                    }
                    break;
                case 3:
                    if (cura3 == null)
                    {
                        cura2.SetActive(true);
                        getHealth = cura2.GetComponent<GetHealth>();
                        whoFall--;
                    }
                    break;
                case 4:
                    if(cura4 == null)
                    {
                        cura3.SetActive(true);
                        getHealth = cura3.GetComponent<GetHealth>();
                        whoFall--;
                    }
                    break;
                case 5:
                    cura4.SetActive(true);
                    getHealth = cura4.GetComponent<GetHealth>();
                    whoFall--;
                    break;
            }
        }
    }
}
