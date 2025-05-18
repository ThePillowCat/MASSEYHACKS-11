using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisableRestart : MonoBehaviour
{
    // Start is called before the first frame update

    // Update is called once per frame

    [SerializeField] Button fire;

    public void OnFireClick()
    {
        fire.interactable = false;
    }

    public void OnRestartClick()
    {
        fire.interactable=true;
    }
}
