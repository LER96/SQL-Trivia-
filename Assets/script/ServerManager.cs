using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class Question
{
    public string question_text;
    public int id;
    public string ans1_text;
    public string ans2_text;
    public string ans3_text;
    public string ans4_text;
    public int correct_ans_id;
}

public class ServerManager : MonoBehaviour
{
    List<int> questionId = new List<int>();
    private static int lastRandomNumber;
    private string current_json;
    private Question current_question;

    // Start is called before the first frame update
    void Start()
    {
        GetQuestion(1);
        InsertPlayer("ioeriewnf");
        UpdateScore("liron", 20);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetQuestion(int QID)
    {
        StartCoroutine(GetRequest("https://localhost:44307/api/Questions/" + QID));
    }

    public void InsertPlayer(string name)
    {
        StartCoroutine(InsertedPlayer("https://localhost:44307/api/InsertPlayers?name=" + name));
    }

    public void UpdateScore(string name,int score)
    {
        StartCoroutine(UpdatedScore("https://localhost:44307/api/UpdatePlayerScore?score=" + score +"&name="+ name));
    }

    IEnumerator GetRequest(string uri)
    {
        current_json = "";
        current_question = null;

        UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.responseCode == 200)
            {
                Debug.Log("Received: " + webRequest.downloadHandler.text);
                current_json = webRequest.downloadHandler.text;

                if (current_json != null && current_json.Length > 0)
                {
                    current_question = JsonUtility.FromJson<Question>(current_json);
                }
                else
                {
                    current_question = null;
                }
            }
        }
    }

    IEnumerator InsertedPlayer(string uri)
    {
        current_json = "";
        UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.responseCode == 200)
            {
                Debug.Log("Received: " + webRequest.downloadHandler.text);
                current_json = webRequest.downloadHandler.text;
                if (current_json == "1")
                {
                    Debug.Log("success");
                }
                else
                {
                    Debug.Log("Error");
                }
            }
        }
    }

    IEnumerator UpdatedScore(string uri)
    {
        current_json = "";
        UnityWebRequest webRequest = UnityWebRequest.Get(uri);
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.responseCode == 200)
            {
                Debug.Log("Received: " + webRequest.downloadHandler.text);
                current_json = webRequest.downloadHandler.text;
                if (current_json == "1")
                {
                    Debug.Log("success");
                }
                else
                {
                    Debug.Log("Error");
                }
            }

        }
    }
}

