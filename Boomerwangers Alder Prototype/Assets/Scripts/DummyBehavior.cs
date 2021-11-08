using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBehavior : MonoBehaviour
{
    bool reset;
    float timer = 0f;
    Vector3 spawnPosition;
    //float health = 100f;

    Rigidbody rb;
    Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
        spawnPosition = tf.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                rb.velocity = Vector3.zero;
                tf.position = spawnPosition;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        reset = true;
        timer = 5f;
    }
}
