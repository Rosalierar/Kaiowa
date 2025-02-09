using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassarParte3 : MonoBehaviour
{
    public GameObject Parte3;
    public GameObject Parte4;

    Cronometro Cronometro;

    // Start is called before the first frame update
    void Start()
    {
        Cronometro = GameObject.FindGameObjectWithTag("Cronometro").GetComponent<Cronometro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FecharTexto()
    {
        Parte3.SetActive(false);
        Parte4.SetActive(true);
        Cronometro.progressao = 3;
        //3
    }
}
