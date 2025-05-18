using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBroadcast : MonoBehaviour
{
    // Start is called before the first frame update

    public static TextBroadcast instance;

    [SerializeField] TMP_InputField field;
    [SerializeField] Slider slider;
    [HideInInspector] public float value;

    private float valueTmp;



    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            
        }
        instance = this;

    }


    private void Start()
    {
        UpdateSlider();
    }

    public void UpdateSlider()
    {
        value = slider.value;
        field.text = (value).ToString();

    }

    public void UpdateTextInfo()
    {
        valueTmp = float.Parse(field.text);

        if (valueTmp < 0) valueTmp = 0;
        else if (valueTmp > 150) valueTmp = 150;

        field.text = valueTmp.ToString();

        value = valueTmp;
        slider.value = value;

    }
}
