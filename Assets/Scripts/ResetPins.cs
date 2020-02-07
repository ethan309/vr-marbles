using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ResetPins : MonoBehaviour
{
    private GameObject[] marbles;

    public int winningScore;
    public int score;

    public Text scoreDisplayText;
    public Text endgameMessage;

    // Start is called before the first frame update
    void Awake()
    {
        marbles = GameObject.FindGameObjectsWithTag("Standing Marble");
        endgameMessage.text = "";
    }

    void UpdateScore(int newScore)
    {
        score = newScore;
        scoreDisplayText.text = "Score: " + score.ToString();
        if (score >= winningScore)
        {
            endgameMessage.text = "Please Reset Marbles";
        }
        else
        {
            endgameMessage.text = "";
        }
    }

    private void FixedUpdate()
    {
        int newScore = 0;
        foreach (GameObject marble in marbles)
        {
            if (marble.GetComponent<Resetable>().hit)
            {
                newScore += 1;
            }
        }
        UpdateScore(newScore);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("Triggerred");
        if (other.gameObject.CompareTag("Player"))
        {
            score = 0;
            foreach (GameObject marble in marbles)
            {
                marble.GetComponent<Resetable>().ToggleReset(true);
            }
        }
    }
}
