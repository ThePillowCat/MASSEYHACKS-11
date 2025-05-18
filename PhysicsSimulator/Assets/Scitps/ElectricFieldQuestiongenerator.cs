using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ElectricFieldQuestionGenerator : MonoBehaviour
{

    [SerializeField] ElectricFieldQuestionsSO[] templates;
    public PhysicsQuestionInstance generateRandomQuestion()
    {
        ElectricFieldQuestionsSO template = templates[Random.Range(0, templates.Length)];


        float r = Random.Range(template.minRadius, template.maxRadius);
        float c1 = Random.Range(template.minCharge1, template.maxCharge1);
        float c2 = Random.Range(template.minCharge2, template.maxCharge2);
        float f = Random.Range(template.minForce, template.maxForce);

        float answer;

        answer = template.Solve(template.unknown, r, c1, c2, f);



        string questionStr = template.questionText.Replace("{r}", r.ToString("F2"))
                                                  .Replace("{c1}", c1.ToString("F2"))
                                                  .Replace("{c2}", c2.ToString("F2"))
                                                  .Replace("{f}", f.ToString("F2"));

        return new PhysicsQuestionInstance
        {
            formattedQuestion = questionStr,
            correctAnswer = answer
        };





    }
    // Start is called before the first frame update




}
