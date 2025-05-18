using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{


    [SerializeField] GameObject centerObject;

    [SerializeField] float multiplier;

    private Transform customPivot;

    private void Start()
    {
        customPivot = centerObject.transform;

    }


    public void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, TextBroadcast.instance.value * multiplier);

        transform.position = customPivot.position;

        //cylinder.transform.position = customPivot.position;
        transform.localEulerAngles = new Vector3(SetRotation.instance.rotation, transform.localEulerAngles.y, transform.localEulerAngles.z);
        
    }
}
