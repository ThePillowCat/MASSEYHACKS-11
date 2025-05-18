using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class LoginData
{
    public string username;
    public string password;
}

[System.Serializable]
public class LoginResponse
{
    public bool success;
    public string username;
    public string name;
    public string assigned;
}

public class LoginRequest : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField usernameInput;
    [SerializeField]
    private TMP_InputField passwordInput;
    [SerializeField]
    private Button button;

    void Start()
    {
        button.onClick.AddListener(buttonPress);
    }

    IEnumerator SendLoginRequest(string uri, string json)
    {
        UnityWebRequest request = new UnityWebRequest(uri, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Error: " + request.error);
        }
        else
        {
            Debug.Log("Received: " + request.downloadHandler.text);
            LoginResponse response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);
            if (response.success)
            {
                Debug.Log("Login successful!");
                PlayerPrefs.SetString("stringData", response.username+","+response.name+","+response.assigned);
                SceneManager.LoadScene(1);
            }
        }
    }

    private void buttonPress()
    {
        Debug.Log("something happened");
        LoginData data = new LoginData { username = usernameInput.text.Trim(), password = passwordInput.text.Trim() };
        string jsonData = JsonUtility.ToJson(data);
        StartCoroutine(SendLoginRequest("http://localhost:3000/unityStudentLogin", jsonData));
    }
}
