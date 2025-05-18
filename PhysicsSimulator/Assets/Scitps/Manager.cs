using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.ProBuilder;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI name;
    [SerializeField]
    private TextMeshProUGUI projMotionAssign;
    [SerializeField]
    private TextMeshProUGUI eceAssign;

    public string resultName;
    public string[] saved;

    void Start()
    {
        saved = PlayerPrefs.GetString("stringData").Split(',');
        name.text = "Hello, " + saved[1] + "!";
        projMotionAssign.text = "You have " + saved[2] + " Projectile Motion Assignments";
        eceAssign.text = "You have " + saved[3] + " Electric Force Assignments!";
    }

    IEnumerator DoSomethingAfterDelay()
    {
        // Wait for 0.1 seconds (100 milliseconds)
        yield return new WaitForSeconds(0.1f);

        // This code runs after 100ms
        name.text = "Hello, " + saved[1] + "!";
        projMotionAssign.text = "You have " + saved[2] + " Projectile Motion Assignments";
        eceAssign.text = "You have " + saved[3] + " Electric Force Assignments!";
    }
}
