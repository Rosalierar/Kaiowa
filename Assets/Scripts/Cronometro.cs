using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cronometro : MonoBehaviour
{
    [SerializeField] private TMP_Text txtTime; 
    [SerializeField] private float timeValue;

    [SerializeField] private string decOrInc;

    PlayerLogic playerLogic;
    PlayerDamage playerDamage;

    [SerializeField] public float contador = 0f;
    [SerializeField] public float valorMaximo;
    bool comecou = false;

    public int progressao = 0;
    public int sec;

    //contador para utilizar poder
    public bool[] CountDown= new bool[2]; 
    public float[] Recharge = new float[2];
    public float[] RechargeTimer = new float[2];

    // Start is called before the first frame update
    void Start()
    {
        playerLogic = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLogic>();
        playerDamage = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDamage>();

        InvokeRepeating(decOrInc, 1f,1f);
        //tempo que dura a animacao do ataque
        RechargeTimer[0] = 0.2f;
        RechargeTimer[1] = 2f;

        //atribuindo uma variavel que sera mudada para o valor inicial
        Recharge[0] = RechargeTimer[0];
        Recharge[1] = RechargeTimer[1];

        CountDown[0] = false;
        CountDown[1] = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (comecou)
            Tempo();

        if (Input.GetKeyDown(KeyCode.L) && !CountDown[1])
        {
            StartCoroutine(Recarga(KeyCode.L, 1));
        }
        if (Input.GetKeyDown(KeyCode.K) && !CountDown[0])
        {
            StartCoroutine(Recarga(KeyCode.K, 0));
        }

    }
    IEnumerator Recarga(KeyCode tecla, int indiceRecarga)
    {
        yield return null; 
        //Marcar a recarga em andamento
        CountDown[indiceRecarga] = true;

        //parar totalmente o jogador
        //playerLogic.rb.velocity = Vector2.zero;
        //playerLogic.podesemover = false;

        while (Recharge[indiceRecarga] > 0f)
        {
            Recharge[indiceRecarga] -= Time.unscaledDeltaTime; 
            yield return null;
        }

        // Quando a recarga acabar
        Recharge[indiceRecarga] = RechargeTimer[indiceRecarga];
        //playerLogic.podesemover = true; 
        CountDown[indiceRecarga] = false;

        playerDamage.atk[1] = false;
        playerDamage.atk[0] = false;

        Debug.Log("Recarga finalizada para a tecla " + tecla);
        Debug.Log("Recarga finalizada  " + CountDown[indiceRecarga]);
        
    }

    public void Tempo()
    {
        contador += Time.deltaTime;
    }
    public void ContadorCutscene(bool comecar)
    {
        comecou = comecar;
    }

    private void DecreaseTime()
    {
        if (timeValue < 0f) return;

        if (timeValue > 0f) timeValue--;

        else timeValue = 0f;

        DisplayTime(timeValue);
    }

    private void IncreseTime()
    {
        if (timeValue < 0f) return;

        timeValue++;

        DisplayTime(timeValue);

    }

    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt (timeToDisplay / 60);
        float seconds = Mathf.FloorToInt (timeToDisplay % 60);

        txtTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    IEnumerator CountdownTimer(int seconds)
    {
        while (seconds > 0)
        {
            yield return new WaitForSeconds(1);
            seconds--;
            sec = seconds;
        }
    }
    public void StartCourt(int seconds)
    {
        StartCoroutine(CountdownTimer(seconds));
    }
}
