using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBehavior : MonoBehaviour
{
    //References to this objects own components
    Rigidbody _rigidbody;
    Transform playerTransform;
    //Reference to the Boomerang Throwing Script from the asshat who threw it
    BoomerangToss toss;

    //Boomerang Properties
    public float boomerangAccell = 5f;
    Vector3 target = Vector3.zero;
    bool returning = false;

    private void Start()
    {
        playerTransform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    //If you read this comment, https://www.youtube.com/watch?v=ovkC8Eb_7mE
    void FixedUpdate()
    {
        Seek(target);
    }

    //Uses Seek Steering Behaviour to send the Boomerang flying towards the target vector. 
    //Automatically updated the target to be the player and auto seeks if returning.
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Returning Boomerwang");
            returnBoomerang();
        }
    }

    public void Seek(Vector3 newTarget)
    {
        if (Input.GetMouseButton(0) || returning)
        {
            if (returning)
            {
                target = playerTransform.position;
            }

            Vector3 desiredVector = target - this.transform.position;
            Vector3 currentVector = _rigidbody.velocity;

            desiredVector = desiredVector.normalized;
            currentVector = currentVector.normalized;

            Vector3 appliedVector = (desiredVector - currentVector);
            appliedVector = appliedVector.normalized * boomerangAccell;
            _rigidbody.AddForce(appliedVector);
        }
    }

    public void updateTarget(Vector3 newTarget)
    {
        if (!returning)
        {
            target = newTarget;
        }
        else
        {
            target = playerTransform.position;
        }
    }

    public void initBoomerang(BoomerangToss bt)
    {
        toss = bt;
    }

    private void OnCollisionEnter(Collision collision)
    {
        returning = true;
        if(collision.transform.name == "Player")
        {
            returnBoomerang();            
        }
    }

    private void returnBoomerang()
    {
        toss.resetBoomers();
        Destroy(this.gameObject);
    }
}
