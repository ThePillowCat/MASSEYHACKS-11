using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ProjectileMotion : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Rigidbody rb;

    [SerializeField] GameObject arrow1; 
    [SerializeField] GameObject arrow2;


    private Vector3 pos;

    private float velo;


    private void Start()
    {
        arrow1.SetActive(true);
        arrow2.SetActive(false);
        pos = transform.position;
        rb.isKinematic = true;
    }


    // Update is called once per frame
    public void OnLaunch()
    {
        arrow2.SetActive(true);
        arrow1.SetActive(false);
        rb.isKinematic = false;
        velo = TextBroadcast.instance.value;
        rb.velocity = new Vector3(-velo*Mathf.Cos(Mathf.Deg2Rad * (180 + SetRotation.instance.rotation)), velo * Mathf.Sin(Mathf.Deg2Rad * (180 + SetRotation.instance.rotation)), 0);
    }

    public void OnRetry()
    {
        arrow1.SetActive(true);
        arrow2.SetActive(false);
        rb.velocity = Vector3.zero;
        transform.position = pos;
        rb.isKinematic = true;
    }

}
