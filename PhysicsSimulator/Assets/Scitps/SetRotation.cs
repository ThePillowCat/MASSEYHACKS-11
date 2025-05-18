using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetRotation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Slider slider;
    [SerializeField] TMP_InputField inputField;

    public static SetRotation instance;

    public float rotation = 0f;

    private float rotationTmp;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);

        }
        instance = this;
    }

    public void UpdateSlider()
    {
        rotation = -slider.value;
        inputField.text = (-rotation).ToString();

    }

    public void UpdateTextInfo()
    {
        rotationTmp = float.Parse(inputField.text);

        if (rotationTmp < 0) rotationTmp = 0;
        else if (rotationTmp > 90) rotationTmp = 90;

        inputField.text = rotationTmp.ToString();

        rotation = -rotationTmp;
        slider.value = -rotation;

    }

}
