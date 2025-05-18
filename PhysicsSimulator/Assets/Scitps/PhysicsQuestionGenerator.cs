using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PhysicsQuestionInstance
{
    public string formattedQuestion;
    public float correctAnswer;
}
public class PhysicsQuestionGenerator : MonoBehaviour
{

    [SerializeField] ProjectileQuestionsSO[] templates;
    public PhysicsQuestionInstance generateRandomQuestion()
    {
        ProjectileQuestionsSO template = templates[Random.Range(0, templates.Length)];


        float d = Random.Range(template.minDist, template.maxDist);
        float vi = Random.Range(template.minVelocity, template.maxVelocity);
        float vf = Random.Range(template.minVelocity, template.maxVelocity);
        float a = Random.Range(template.minAcceleration, template.maxAcceleration);
        float t = Random.Range(template.minTime, template.maxTime);

        float answer;

        switch (template.unused)
        {
            case ProjectileVariable.Distance:
                answer = template.SolveNoDist(template.unknown, t, vi, vf, a);
                break;
            case ProjectileVariable.Time:
                answer = template.SolveNoTime(template.unknown, d, vi, vf, a);
                break;
            case ProjectileVariable.InitialVelocity:
                if (template.unknown == ProjectileVariable.Time)
                {
                    answer = -1;
                }
                else
                {
                    answer = template.SolveNoVi(template.unknown, d, t, vf, a);
                }
                break;

            case ProjectileVariable.FinalVelocity:
                if (template.unknown == ProjectileVariable.Time)
                {
                    answer = -1;
                }
                else
                {
                    answer = template.SolveNoVf(template.unknown, d, t, vi, a);
                }
                break;
            case ProjectileVariable.Acceleration:
                answer = template.SolveNoAccel(template.unknown, d, vi, vf, t);
                break;
            default:
                answer = 0;
                break;
        }



        string questionStr = template.questionText.Replace("{d}", d.ToString("F2"))
                                                  .Replace("{t}", t.ToString("F2"))
                                                  .Replace("{vi}", vi.ToString("F2"))
                                                  .Replace("{vf}", vf.ToString("F2"))
                                                  .Replace("{a}", a.ToString("F2"));

        return new PhysicsQuestionInstance
        {
            formattedQuestion = questionStr,
            correctAnswer = answer
        };




        
    }
    // Start is called before the first frame update



    
}
