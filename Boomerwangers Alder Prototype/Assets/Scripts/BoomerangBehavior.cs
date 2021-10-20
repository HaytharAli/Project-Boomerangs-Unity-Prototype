using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangBehavior : MonoBehaviour
{
    Rigidbody rigid_body;
    Transform playerTransform;
    BoomerangToss toss;
    public float boomerangAccell = 5f;
    Vector3 target = Vector3.zero;
    bool returning = false;

    void FixedUpdate()
    {
        Seek(target);
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
            Vector3 currentVector = rigid_body.velocity;

            desiredVector = desiredVector.normalized;
            currentVector = currentVector.normalized;

            Vector3 appliedVector = (desiredVector - currentVector);
            appliedVector = appliedVector.normalized * boomerangAccell;
            rigid_body.AddForce(appliedVector);
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

    public void initBoomerang(Transform player, Rigidbody boomerigid, BoomerangToss bt)
    {
        playerTransform = player;
        rigid_body = boomerigid;
        toss = bt;
    }

    private void OnCollisionEnter(Collision collision)
    {
        returning = true;
        if(collision.transform.name == "Player")
        {
            toss.resetBoomers();
            Destroy(this.gameObject);            
        }
    }
}
