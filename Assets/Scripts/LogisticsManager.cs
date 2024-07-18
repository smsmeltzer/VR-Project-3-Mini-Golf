using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogisticsManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI lifeTotalText;
    [SerializeField] GameObject iLoseText;
    [SerializeField] GameObject otherLoseText;

    [SerializeField] private int currentHit = 5;
    private string hitText = "Life Total = ";

    public bool collisionFound = false;
    public bool lost = false;
    private bool endGame = false;

    // Update is called once per frame
    void Update()
    {
        if (!endGame)
        {
            if (collisionFound)
            {
                currentHit--;
                lifeTotalText.text = hitText + currentHit.ToString();

                collisionFound = false;

                if (currentHit < 1)
                {
                    lost = true;
                    iLoseText.SetActive(true);
                }
            }
        }
    }

    public void resetGameplay()
    {
        lost = false;
        endGame = true;
    }

    public void otherLost()
    {
        otherLoseText.SetActive(true);
    }

    public void showOnScreen(string str)
    {
        lifeTotalText.text = str;
    }
}
