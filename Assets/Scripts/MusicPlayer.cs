using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

	// Use this for initialization
	void Awake ()
    {
        SetUpSingleton();
	}

    private void SetUpSingleton()
    {
        int countMusicPlayers = FindObjectsOfType(GetType()).Length;
        if (countMusicPlayers > 1)
        {
            Destroy(gameObject);
        }

        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    // Update is called once per frame
    void Update ()
    {
		
	}
}
