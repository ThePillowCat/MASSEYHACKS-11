using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum ProjectileVariable
{
    Distance,
    Time,
    InitialVelocity,
    FinalVelocity,
    Acceleration
}


[CreateAssetMenu(fileName = "NewProjectileQuestions", menuName = "Projectile Questions Template")]

public class ProjectileQuestionsSO : ScriptableObject
{
    // Start is called before the first frame update

    [TextArea(3, 10)]
    public string questionText;

    public ProjectileVariable unknown;
    public ProjectileVariable unused;

    public float minDist;
    public float maxDist;

    public float minTime;
    public float maxTime;

    public float minVelocity;
    public float maxVelocity;

    public float minAcceleration;
    public float maxAcceleration;

    public float SolveNoVf(ProjectileVariable variable, float dist, float time, float vi, float accel)
    {
        switch (variable)
        {
            case ProjectileVariable.Distance:
                return vi * time + 0.5f * accel * time * time;
            case ProjectileVariable.InitialVelocity:
                return (dist - 0.5f * accel * time * time) / time;
            case ProjectileVariable.Acceleration:
                return 2 * (dist - vi * time) / (time * time);

            default:
                return -1;
        }
    }
    public float SolveNoVi(ProjectileVariable variable, float dist, float time, float vf, float accel)
    {
        switch (variable)
        {
            case ProjectileVariable.Distance:
                return vf * time - 0.5f * accel * time * time;
            case ProjectileVariable.FinalVelocity:
                return (dist + 0.5f * accel * time * time) / time;
            case ProjectileVariable.Acceleration:
                return 2 * (dist + vf * time) / (time * time);
            default:
                return -1;
        }
    }
    public float SolveNoDist(ProjectileVariable variable, float time, float vi, float vf, float accel)
    {
        switch (variable)
        {
            case ProjectileVariable.InitialVelocity:
                return -accel * time + vf;
            case ProjectileVariable.FinalVelocity:
                return accel * time - vf;
            case ProjectileVariable.Acceleration:
                return (vf - vi)/time;
            case ProjectileVariable.Time:
                return (vf - vi) * accel;
            default:
                return -1;
        }
    }
    public float SolveNoTime(ProjectileVariable variable, float dist, float vi, float vf, float accel)
    {
        switch (variable)
        {
            case ProjectileVariable.InitialVelocity:
                return Mathf.Sqrt(vf*vf - 2 * accel * dist);
            case ProjectileVariable.FinalVelocity:
                return Mathf.Sqrt(vi * vi + 2 * accel * dist);
            case ProjectileVariable.Acceleration:
                return (vf*vf - vi * vi)/(2 * dist);
            case ProjectileVariable.Distance:
                return (vf * vf - vi * vi) / (2 * accel); ;
            default:
                return -1;
        }
    }

    public float SolveNoAccel(ProjectileVariable variable, float dist, float vi, float vf, float time)
    {
        switch (variable)
        {
            case ProjectileVariable.InitialVelocity:
                return dist/time * 2 - vf;
            case ProjectileVariable.FinalVelocity:
                return dist/time * 2 - vi; ;
            case ProjectileVariable.Time:
                return (2*dist)/(vi+vf);
            case ProjectileVariable.Distance:
                return (vi+vf)*time/2; ;
            default:
                return -1;
        }
    }

}
