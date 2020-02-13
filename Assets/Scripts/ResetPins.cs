using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ResetPins : MonoBehaviour
{
    private GameObject[] marbles;

    private int winningScore;
    private int score;

    public TextMeshProUGUI playerScoreDisplayText;
    // public Text endgameMessage;

    List<Vector2> GenerateRingPoints(int marbleCount) {
        List<Vector2> points = new List<Vector2>();
        while(points.Count < marbleCount) {
            const int ringSize = 5;
            const float minSeparation = 0.5F;
            Vector2 newPoint = UnityEngine.Random.insideUnitCircle * ringSize;
            if(!points.Any(point => Vector3.Distance(point, newPoint) < minSeparation))
            {
                points.Add(newPoint);
            }
        }
        return points;
    }

    // Start is called before the first frame update
    void Start()
    {
        marbles = GameObject.FindGameObjectsWithTag("Standing Marble");
        winningScore = marbles.Count();
        //endgameMessage.text = "";
    }

    void UpdateScore(int newScore)
    {
        score = newScore;
        playerScoreDisplayText.text = "Score: " + score.ToString() + "/" + winningScore.ToString();
        if (score >= winningScore)
        {
            playerScoreDisplayText.text = "Score: " + score.ToString() + "/" + winningScore.ToString() + "\n" + "GAME OVER. PLEASE RESET MARBLES.";
        }
        else
        {
            //print("NEW SCORE: " + score);
            //endgameMessage.text = "";
        }
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            score = 0;
            foreach (GameObject marble in marbles)
            {
                marble.GetComponent<Resetable>().ToggleReset(true);
            }
            List<Vector2> newPoints = GenerateRingPoints(marbles.Count());
            for (int i = 0; i < marbles.Count(); i++)
            {
                marbles[i].GetComponent<Resetable>().TriggerRearrange(newPoints[i].x, newPoints[i].y);
            }
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
        if (other.gameObject.CompareTag("Player"))
        {
            score = 0;
            foreach (GameObject marble in marbles)
            {
                marble.GetComponent<Resetable>().ToggleReset(true);
            }
            List<Vector2> newPoints = GenerateRingPoints(marbles.Count());
            for (int i = 0; i < marbles.Count(); i++)
            {
                marbles[i].GetComponent<Resetable>().TriggerRearrange(newPoints[i].x, newPoints[i].y);
            }
        }
    }
}
