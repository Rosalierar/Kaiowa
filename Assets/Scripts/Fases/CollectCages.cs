using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyData;

public class CollectCages : MonoBehaviour
{
    public Animator anim;
    BoxCollider2D boxCollider2D;
    ControlScene controlScene;

    // Definindo o evento de jaulas abertas
    public delegate void OnCollectCage();
    public event OnCollectCage CollectedCage;

    bool close;
    bool tocou;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        boxCollider2D = GetComponent<BoxCollider2D>();

        controlScene = GameObject.FindGameObjectWithTag("SceneController").GetComponent<ControlScene>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && tocou)
        {
            boxCollider2D.enabled = false;
            anim.SetTrigger("open");
            //spriteRenderer.sprite = sprites[1];
            CollectedCage?.Invoke();
            //StartCoroutine(Collect());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Estou proximo da jaula");
        tocou = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Sai da proximidade da jaula");
        tocou = false;
    }
    
}
