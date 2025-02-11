using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnabledMovePlayer : MonoBehaviour
{
    public bool canFight = false;

    ControlSound controlSound;
    Cronometro cronometro;
    PlayerLogic playerLogic;

    [SerializeField] private GameObject[] paredes = new GameObject[2];
    [SerializeField] private GameObject manObject;

    Animator anim;
    int locomoverHash;

    // Start is called before the first frame update
    void Start()
    {
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
        if (collision.gameObject.CompareTag("Player"))
        {
            cronometro.progressao = 1;
            anim.SetTrigger(locomoverHash);
            playerLogic.dirX = 0;
            playerLogic.podesemover = false;
            //paredes[0].SetActive(false);
        }
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            controlSound.SoundOnOff(1);
            canFight = true;
        }
            
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CarasMals"))
        {
            cronometro.progressao = 2;
            playerLogic.podesemover = true;
            paredes[1].SetActive(false);
            Destroy(manObject);
            paredes[0].SetActive(false);
        }
    }
}
