using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestionsUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] PhysicsQuestionGenerator generator;
    [SerializeField] TMP_Text questionText;
    [SerializeField] TMP_Text response;
    [SerializeField] Button submitButton;
    [SerializeField] TMP_InputField answer;

    [SerializeField] float clearTime;


    private PhysicsQuestionInstance currentQuestion;
    void Start()
    {
        LoadQuestion();
    }

    private void LoadQuestion()
    {
        currentQuestion = generator.generateRandomQuestion();
        questionText.text = currentQuestion.formattedQuestion;
        answer.text = "";
        response.text = "";

    }

    public void CheckAnswer()
    {
        if (float.TryParse(answer.text, out float studentAnswer))
        {
            float tolerance = 5f;
            if (tolerance >= Mathf.Abs(studentAnswer - currentQuestion.correctAnswer))
            {
                response.text = "Correct!";
            }
            else
            {
                response.text = $"Incorrect. Correct answer: {currentQuestion.correctAnswer:F2}";
            }

            Invoke(nameof(LoadQuestion), clearTime);
        }
        else
        {
            response.text = "Please enter a valid number.";
        }
    }
}
