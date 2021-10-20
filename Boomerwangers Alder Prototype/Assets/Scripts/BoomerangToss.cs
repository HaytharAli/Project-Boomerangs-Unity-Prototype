using UnityEngine;

public class BoomerangToss : MonoBehaviour
{
    //-----Boomerang References-----//
    public GameObject boomerangPrefab; //Refernce to the Prefab we want to instantiate
    GameObject currentBoomerang; //Reference to the Boomerang Currently in the scene. Determined at runtime, should be reset to NULL when the Boomerang dies
    Rigidbody boomerangRigidbody; //Determined at runtime, reset to Null at Runtime
    BoomerangBehavior boomerangBehavior;
    
    //-----Player References-----//
    public Transform camTransform;

    //-----Boomerang Properties-----//
    bool thrown = false; //Keeps track of whether the wang is thrown
    public float BoomerangLaunchForce = 1000f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (!thrown)
            {
                //Determine Angle and Position to start the projectile
                Quaternion projRot = Quaternion.Euler(camTransform.localRotation.x, this.transform.localRotation.y, 0); //Get the Camera's Vertical Rotation, and the body's Horizontal Rotation
                Vector3 projectileSpacing = projRot * Vector3.forward * 2.7f; //This doesn't work correctly and I don't know why
                
                //Make the boomerang and set up the refernces!
                currentBoomerang = Instantiate(boomerangPrefab, camTransform.position + projectileSpacing, camTransform.rotation);
                boomerangRigidbody = currentBoomerang.GetComponent<Rigidbody>();
                boomerangBehavior = currentBoomerang.GetComponent<BoomerangBehavior>();
                boomerangBehavior.initBoomerang(this.transform, boomerangRigidbody, this);
               
                //Add forces to send it flying
                boomerangRigidbody.AddForce(currentBoomerang.transform.localRotation * Vector3.forward * BoomerangLaunchForce);
                thrown = true;
            }
            else
            {
                //Move to wherever the camera is pointing
                RaycastHit hit;
                if (Physics.Raycast(camTransform.position, camTransform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    boomerangBehavior.updateTarget(hit.point);
                }

            }
        }
    }

    public void resetBoomers()
    {
        currentBoomerang = null;
        boomerangBehavior = null;
        boomerangRigidbody = null;

        thrown = false;
        Debug.Log("Deleted!");
    }
}

