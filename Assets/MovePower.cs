using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePower : MonoBehaviour
{
    public float speed = 20f;

    public float time;

    public Rigidbody2D rbPoder;
    // Start is called before the first frame update
    void Start()
    {
        rbPoder.velocity = -transform.right * speed;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if (time > 5)
        {
            Debug.Log("Destroido depois de 10 min");
            Destroy(gameObject);
        }
    }
}
