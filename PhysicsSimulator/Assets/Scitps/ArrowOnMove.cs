using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowOnMove : MonoBehaviour
{


    [SerializeField] GameObject centerObject;
    [SerializeField] Rigidbody rb;

    [SerializeField] float multiplier;

    private Transform customPivot;

    private void Start()
    {
        customPivot = centerObject.transform;

    }


    public void Update()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.y * rb.velocity.y)* multiplier);

        transform.position = customPivot.position;

        //cylinder.transform.position = customPivot.position;
        transform.localEulerAngles = new Vector3(Mathf.Atan2(-rb.velocity.y, Mathf.Abs(rb.velocity.x)) * Mathf.Rad2Deg, transform.localEulerAngles.y, transform.localEulerAngles.z);

    }
}
