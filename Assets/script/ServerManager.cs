using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Question
{
    public string question_text;
    public int id;
    public string ans1_text;
    public string ans2_text;
    public string ans3_text;
    public string ans4_text;
    private int correct_ans_id;
}

public class ServerManager : MonoBehaviour
{
    public TMP_Text correctAnswer;
    public TMP_Text sqlScoreText;
    public TMP_Text timerText;
    [SerializeField] TMP_Text scoreGained;
    [SerializeField] InputField userName;
    private string current_json;
    private Question current_question;

    [SerializeField] TMP_Text questionText;
    [SerializeField] TMP_Text answer1;
    [SerializeField] TMP_Text answer2;
    [SerializeField] TMP_Text answer3;
    [SerializeField] TMP_Text answer4;
    [SerializeField] Button startGameButton;

    [SerializeField] GameObject questionCanvas;
    [SerializeField] GameObject endCanvas;
    [SerializeField] GameObject startCanvas;
    [SerializeField] TMP_Text endGame;

    [SerializeField] int score;
    [SerializeField] int scoreOfQuest;
    [SerializeField] int index;
    [SerializeField] float timer=0;
    [SerializeField] float maxTime;
    [SerializeField] bool ispresent;
    //float copytime;
    
    void Start()
    {
        index = 1;
    }

    private void Update()
    {
        if (ispresent)
        {
            Stopper();
        }
        timerText.text = "Timer: " + timer;

    }

    //the timer of each question
    void Stopper()
    {
        if (timer < maxTime)
        {
            timer += Time.deltaTime;
        }
        else
        {
            index++;
            NextQuest();
        }
    }

    //give next question from the chart
    void NextQuest()
    {
        if (index< 8)
        {
            timer = 0.1f;
            ispresent = true;
            GetQuestion(index);
        }
        else
        {
            EndTrivia();
        }
    }

    //check if the answer is correct
    public void GetAnswer(int answer)
    {
        int a = int.Parse(correctAnswer.text);
        scoreOfQuest = int.Parse(sqlScoreText.text);
        if(a== answer)
        {
            score += (int)(scoreOfQuest / (int)timer);
        }
        else
        {
            Debug.Log("kill him");
        }
        index++;
        NextQuest();
    }

    public void CreateUsername()
    {
        string name = userName.text;
        InsertPlayer(name);
        startGameButton.gameObject.SetActive(true);
    }

    public void StartGame()
    {
        startCanvas.SetActive(false);
        questionCanvas.SetActive(true);
        NextQuest();
    }

    public void EndTrivia()
    {
        endCanvas.SetActive(true);
        questionCanvas.SetActive(false);
        UpdateScore(userName.text, score);
        endGame.text = "" + userName.text + " Got " + score;
    }

    public void EndGame()
    {
        Application.Quit();
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
                string[] split = current_json.Split(':' , ',' , '}');
                for (int i = 0; i < split.Length; i++)
                {
                   // Debug.Log(split[i]);
                }
                questionText.text = split[1];
                answer1.text = split[5];
                answer2.text = split[7];
                answer3.text = split[9];
                answer4.text = split[11];
                correctAnswer.text = split[13];
                sqlScoreText.text = split[15];
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

