using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePlatform : Platform {
    public bool isRotating;
    public float targetRotation;

    public override void Activate()
    {
        //isRotating = true;
        //if (trans.rotation.eulerAngles.z != 0.0f)
        //    targetRotation = trans.eulerAngles.z - 90.0f;
        //else
        //    targetRotation = 270.0f;

        animator.SetTrigger("Rotate");

    }

    public override void Update()
    {
        //Debug.Log(trans.eulerAngles.z);
        //if (isRotating && (trans.rotation.eulerAngles.z > targetRotation || trans.rotation.eulerAngles.z == 0.0f))
        //{
        //    trans.Rotate(Vector3.back * Time.deltaTime * 120.0f);
        //    Debug.Log("Here!");
        //}
        //else if (trans.rotation.eulerAngles.z < targetRotation)
        //{
        //    isRotating = false;
        //}


    }

}
