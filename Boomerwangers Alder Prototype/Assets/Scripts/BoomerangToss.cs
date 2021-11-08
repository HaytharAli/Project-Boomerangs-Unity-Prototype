using UnityEngine;

public class BoomerangToss : MonoBehaviour
{
    //-----Boomerang References-----//
    public GameObject boomerangPrefab; //Refernce to the Prefab we want to instantiate
    GameObject boomerangCurrent; //Reference to the Boomerang Currently in the scene. Determined at runtime, should be reset to NULL when the Boomerang dies
    Rigidbody boomerangRigidbody;
    BoomerangBehavior boomerangBehavior; //Reference to the Boomerang's own script
    
    //-----Player References-----//
    public Transform cameraTransform;

    //-----Boomerang Properties-----//
    bool thrown = false; //Keeps track of whether the wang is thrown
    public float projectileLaunchForce = 1000f;
    public float projectileSpacing = 1f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (!thrown)
            {
                Vector3 projectilePosition = cameraTransform.position + cameraTransform.forward * projectileSpacing;

                //Make the boomerang and set up the refernces!
                boomerangCurrent = Instantiate(boomerangPrefab, projectilePosition, cameraTransform.rotation);
                boomerangRigidbody = boomerangCurrent.GetComponent<Rigidbody>();
                boomerangBehavior = boomerangCurrent.GetComponent<BoomerangBehavior>();
                boomerangBehavior.initBoomerang(this, this.transform);

                //Add forces to send it flying
                boomerangRigidbody.AddForce(boomerangCurrent.transform.localRotation * Vector3.forward * projectileLaunchForce);
                thrown = true;
            }
            else
            {
                //Move to wherever the camera is pointing
                RaycastHit hit;
                if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        boomerangBehavior.updateTarget(hit.transform);
                    }
                    else
                    {
                        boomerangBehavior.updateTarget(hit.point);
                    }
                }
            }
        }
    }

    public void resetBoomers()
    {
        boomerangCurrent = null;
        boomerangBehavior = null;
        boomerangRigidbody = null;

        thrown = false;
    }
}

