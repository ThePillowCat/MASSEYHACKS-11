using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeArrow : MonoBehaviour
{


    [SerializeField] GameObject centerObject;

    [SerializeField] float multiplier;

    private Vector3 customPivot;

    private void Start()
    {
        customPivot = centerObject.transform.position;

    }


    public void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, centerObject.transform.position.x - customPivot.x);

        //cylinder.transform.position = customPivot.position;
        //transform.localEulerAngles = new Vector3(SetRotation.instance.rotation, transform.localEulerAngles.y, transform.localEulerAngles.z);

    }
}
