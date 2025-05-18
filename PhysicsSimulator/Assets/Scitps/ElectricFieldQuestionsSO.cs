using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ElectricVariable
{
    Radius,
    Force,
    Charge1,
    Charge2
};

public class ElectricFieldQuestionsSO : ScriptableObject
{
    // Start is called before the first frame update

    [TextArea(3, 10)]
    public string questionText;

    public ElectricVariable unknown;
    public ElectricVariable unused;

    public float minRadius;
    public float maxRadius;

    public float minForce;
    public float maxForce;

    public float minCharge1;
    public float maxCharge1;

    public float minCharge2;
    public float maxCharge2;

    private float coul = 8.99e9f;

    public float Solve(ElectricVariable variable, float radius, float charge1, float charge2, float force)
    {
        switch (variable)
        {
            case ElectricVariable.Radius:
                return Mathf.Sqrt(coul * charge1 * charge2 / force);
            case ElectricVariable.Charge1:
                return (force * radius * radius/(coul*charge2));
            case ElectricVariable.Charge2:
                return (force * radius * radius / (coul * charge1));
            case ElectricVariable.Force:
                return (charge1*charge2*coul)/(radius*radius);

            default:
                return -1;
        }
    }
}
