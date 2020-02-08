using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Linq;

public class ResetPins : MonoBehaviour
{
    private GameObject[] marbles;

    public int winningScore;
    public int score;

    // public Text scoreDisplayText;
    // public Text endgameMessage;

    List<Vector2> GenerateRingPoints(int marbleCount) {
        List<Vector2> points = new List<Vector2>();
        while(points.Count < marbleCount) {
            Vector2 newPoint = UnityEngine.Random.insideUnitCircle * 5;
            if(!points.Any(point => Vector3.Distance(point, newPoint) < 0.5))
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
        //endgameMessage.text = "";
    }

    void UpdateScore(int newScore)
    {
        score = newScore;
        //scoreDisplayText.text = "Score: " + score.ToString();
        if (score >= winningScore)
        {
            //endgameMessage.text = "Please Reset Marbles";
        }
        else
        {
            //endgameMessage.text = "";
        }
    }

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            print("Triggerred (keyboard)");
            score = 0;
            foreach (GameObject marble in marbles)
            {
                marble.GetComponent<Resetable>().ToggleReset(true);
            }
            print("HIT HERE");
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
        print("Triggerred");
        if (other.gameObject.CompareTag("Player"))
        {
            score = 0;
            foreach (GameObject marble in marbles)
            {
                marble.GetComponent<Resetable>().ToggleReset(true);
            }
            print("HIT HERE");
            List<Vector2> newPoints = GenerateRingPoints(marbles.Count());
            for (int i = 0; i < marbles.Count(); i++)
            {
                marbles[i].GetComponent<Resetable>().TriggerRearrange(newPoints[i].x, newPoints[i].y);
            }
        }
    }
}
