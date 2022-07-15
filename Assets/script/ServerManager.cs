using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

[System.Serializable]
public class ServerManager : MonoBehaviour
{
    public string current_json;
    private Question current_question;
    [SerializeField] GameObject questionOBJ;

    public TMP_Text questionText;

    // Start is called before the first frame update
    void Start()
    {
        current_question = questionOBJ.GetComponent<Question>();
        UI();
        FirstQuestion();
        //InsertPlayer("ioeriewnf");
        //UpdateScore("liron", 20);
    }

    public void UI()
    {
        questionText.text = "First question: " + current_question.question_text;
    }

    public void FirstQuestion()
    {
        GetQuestion(1);
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
                questionText.text = webRequest.downloadHandler.text;
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

