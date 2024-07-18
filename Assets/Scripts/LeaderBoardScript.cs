using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Unity.VisualScripting;

public class LeaderBoardScript : MonoBehaviour
{
    private Dictionary<int, int> scores;

    [SerializeField] TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        scores = new Dictionary<int, int>();
        text.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void add_player_score(int id, int score)
    {
        scores.Add(id, score);
        text.text = "";

        int min = scores[id];
        int min_id = id;
        // Find winner
        foreach (var item in scores)
        {
            if (item.Value < min)
            {
                min = item.Value;
                min_id = item.Key;
            }
        }

        foreach (var item in scores)
        {
            if (item.Key == min_id)
            {
                text.text += "Player #" + item.Key + " = " + item.Value + "Strokes <--- WINNER!\n";
            }
            else
            {
                text.text += "Player #" + item.Key + " = " + item.Value + "Strokes\n";
            }
        }
    }
}
