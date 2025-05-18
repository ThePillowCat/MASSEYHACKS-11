using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class gravity : MonoBehaviour
{
    // Start is called before the first frame update
    float gravityConst = 9.81f;

    [SerializeField] Slider slider;
    [SerializeField] TMP_InputField inputField;

    private void Start()
    {
        inputField.text = (gravityConst).ToString();
        Physics.gravity = new Vector3(0, -gravityConst, 0);
    }

    public void UpdateSlider()
    {
        gravityConst = slider.value;
        inputField.text = (gravityConst).ToString();
        Physics.gravity = new Vector3(0, -gravityConst, 0);

    }

    public void UpdateTextInfo()
    {
        gravityConst = float.Parse(inputField.text);

        if (gravityConst < 0) gravityConst = 0;
        else if (gravityConst > 50) gravityConst = 50;

        inputField.text = gravityConst.ToString();

        Physics.gravity = new Vector3(0, -gravityConst, 0);
        slider.value = gravityConst;

    }
}
