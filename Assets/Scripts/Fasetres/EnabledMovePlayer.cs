using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledMovePlayer : MonoBehaviour
{
    BoxCollider2D boxCollider2D;

    public bool canFight = false;

    ControlSound controlSound;
    Cronometro cronometro;
    PlayerLogic playerLogic;

    [SerializeField] private GameObject cam1;
    [SerializeField] private GameObject cam2;
    [SerializeField] private GameObject[] paredes = new GameObject[2];
    [SerializeField] private GameObject manObject;

    Animator anim;
    int locomoverHash;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();

        controlSound = GameObject.FindWithTag("SoundController").GetComponent<ControlSound>();
        cronometro = GameObject.FindGameObjectWithTag("Cronometro").GetComponent<Cronometro>();
        playerLogic = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLogic>();
        anim = GameObject.FindGameObjectWithTag("CarasMals").GetComponent<Animator>();

        locomoverHash = Animator.StringToHash("Locomover");
        cronometro.progressao = 0;
        controlSound.SoundOnOff(0);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && cronometro.progressao != 1)
        {
            cronometro.progressao = 1;
            anim.SetTrigger(locomoverHash);
            playerLogic.dirX = 0;
            playerLogic.podesemover = false;
            paredes[0].SetActive(false);
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            controlSound.SoundOnOff(1);
            canFight = true;
            paredes[0].SetActive(true);

            playerLogic.rb.velocity = Vector2.zero;
            playerLogic.podesemover = false;
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CarasMals"))
        {
            cronometro.progressao = 2;
            playerLogic.rb.velocity = Vector2.zero;
            playerLogic.podesemover = true;
            paredes[1].SetActive(false);
            paredes[0].SetActive(false);
            Destroy(manObject);
        }
    }
}
