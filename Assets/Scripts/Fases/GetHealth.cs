using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHealth : MonoBehaviour
{
    Health playerhealth;

    Animator animator;
    [SerializeField] int vidaParaRegenerar;
    [SerializeField] bool close;

    // Start is called before the first frame update
    void Start()
    {
        playerhealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (close)
        {
            if (Input.GetKeyUp(KeyCode.J))
            {
                //vida + 20 (exemplo) <= 100
                if (playerhealth.health + vidaParaRegenerar < 100) {
                    playerhealth.health += vidaParaRegenerar;
                    playerhealth.slider.value = playerhealth.health;

                    Debug.Log("recuperei vida");
                    Destroy(gameObject);
                }
                else if (playerhealth.health + vidaParaRegenerar > 100) {
                    playerhealth.health = 100;
                    playerhealth.slider.value = playerhealth.health;

                    Debug.Log("recuperei vida");
                    Destroy(gameObject);
                }
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Estou proximo da vida");
            close = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Sai da proximidade da vida");
        close = false;
    }
}
