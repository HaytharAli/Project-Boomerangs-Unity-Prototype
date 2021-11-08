using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBehavior : MonoBehaviour
{
    //-----Boomerang Components-----//
    Rigidbody _rigidbody;
    //Reference to the Boomerang Throwing Script and Transform of the asshat who threw it
    Transform lockedTarget = null;
    bool targetLocked = false;
    Transform playerTransform;
    BoomerangToss toss;

    //Boomerang Properties
    public float boomerangAccell = 5f;
    Vector3 target = Vector3.zero;
    bool returning = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    //If you read this comment, https://www.youtube.com/watch?v=ovkC8Eb_7mE
    void FixedUpdate()
    {
        Seek(target);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            returnBoomerang();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            returning = true;
        }
    }

    public void Seek(Vector3 newTarget)
    {
        if (Input.GetMouseButton(0) || returning)
        {
            if (targetLocked)
            {
                target = lockedTarget.position;
            }
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
    }

    public void updateTarget(Transform enemyTrans)
    {
        if (!returning)
        {
            lockedTarget = enemyTrans;
            targetLocked = true;
        }
    }
    public void initBoomerang(BoomerangToss bt, Transform pt)
    {
        toss = bt;
        playerTransform = pt;
    }

    private void OnCollisionEnter(Collision collision)
    {
        returning = true;
        targetLocked = false;
        lockedTarget = null;
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
