using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ajuda : MonoBehaviour
{
    public bool clicouJ=false;
    public float CountHelp;

    public GameObject textoajuda;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.J))
        {
            CountHelp += Time.deltaTime;
        }
        else
        {
            clicouJ = true;
        }

        if (CountHelp > 15)
            textoajuda.SetActive(true);

        if (clicouJ)
            textoajuda.SetActive(false);
    }
}
