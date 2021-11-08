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
    public float BoomerangLaunchForce = 1000f;






    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            if (!thrown)
            {
                //Determine Angle and Position to start the projectile
                Quaternion projRot = Quaternion.Euler(cameraTransform.localRotation.x, this.transform.localRotation.y, 0); //Get the Camera's Vertical Rotation, and the body's Horizontal Rotation
                Vector3 projectileSpacing = cameraTransform.position + cameraTransform.forward * 1f;

                //Make the boomerang and set up the refernces!
                boomerangCurrent = Instantiate(boomerangPrefab, cameraTransform.position + projectileSpacing, cameraTransform.rotation);
                boomerangRigidbody = boomerangCurrent.GetComponent<Rigidbody>();
                boomerangBehavior = boomerangCurrent.GetComponent<BoomerangBehavior>();
                boomerangBehavior.initBoomerang(this);
               
                //Add forces to send it flying
                boomerangRigidbody.AddForce(boomerangCurrent.transform.localRotation * Vector3.forward * BoomerangLaunchForce);
                thrown = true;
            }
            else
            {
                //Move to wherever the camera is pointing
                RaycastHit hit;
                if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    boomerangBehavior.updateTarget(hit.point);
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
        Debug.Log("Deleted!");
    }
}

