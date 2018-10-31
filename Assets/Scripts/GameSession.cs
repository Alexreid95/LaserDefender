using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{

    int score = 0;


	void Awake ()
    {
        SetUpSingleton();
	}

    private void SetUpSingleton()
    {
        int countGameSession = FindObjectsOfType(GetType()).Length;
        if (countGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score;
    }

    public void AddStarToScore(int pointsForStar)
    {
        score += pointsForStar;
    }

    public void AddHitToScore(int pointsForHit)
    {
        score += pointsForHit;
    }

    public void AddDeathToScore(int pointsForDeath)
    {
        score += pointsForDeath;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }


}
