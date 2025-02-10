using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyData;

public class CollectCages : MonoBehaviour
{
    public Animator anim;
    BoxCollider2D boxCollider2D;
    ControlScene controlScene;

    //Definindo o evento de jaulas abertas
    public delegate void OnCollectCage();
    public event OnCollectCage CollectedCage;

    bool close;
    bool tocou;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("JaulasAbertas", 0);

        anim = GetComponent<Animator>();

        boxCollider2D = GetComponent<BoxCollider2D>();

        controlScene = GameObject.FindGameObjectWithTag("SceneController").GetComponent<ControlScene>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && tocou)
        {
            PlayerPrefs.SetInt("JaulasAbertas", PlayerPrefs.GetInt("JaulasAbertas", 0) + 1);

            Debug.Log("Prefebs: " + PlayerPrefs.GetInt("JaulasAbertas"));

            boxCollider2D.enabled = false;
            anim.SetTrigger("open");
            CollectedCage?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Estou proximo da jaula");
        if (collision.CompareTag("Player"))
            tocou = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Sai da proximidade da jaula");
        if (collision.CompareTag("Player"))
            tocou = false;
    }
    
}
