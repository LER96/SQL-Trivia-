using UnityEngine;

[System.Serializable]
public class Question : MonoBehaviour
{
    public string question_text;
    public int id;
    public string ans1_text;
    public string ans2_text;
    public string ans3_text;
    public string ans4_text;
    public int correct_ans_id;
}

