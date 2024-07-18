using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI strokesText;
    [SerializeField] TextMeshProUGUI totalText;

    private int Score1 = 0;
    private int Score2 = 0;
    private int Score3 = 0;
    public int total_score = 0;

    // Start is called before the first frame update
    void Start()
    {
        strokesText.text = "Strokes: 0";
        totalText.text = "Total Strokes: 0";
    }
    public void set_course_score(int num, int score)
    {
        if (num == 1)
        {
            Score1 = score;
        }
        else if (num == 2)
        {
            Score2 = score;
        }
        else if(num == 3)
        {
            Score3 = score;
        }
        totalText.text = "Total Strokes: " + get_total_score();
    }

    public bool is_finished()
    {
        if (Score1 != 0 && Score2 != 0 && Score3 != 0)
        {
            return false;
        }
        return true;
    }

    public void set_num_strokes(int num)
    {
        strokesText.text = "Strokes: " + num;
    }

    public void set_total_strokes(int num)
    {
        totalText.text = "Total Strokes: " + num;
    }

    public int get_total_score()
    {
        return Score1 + Score2 + Score3;
    }
}
