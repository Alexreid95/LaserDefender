using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution : MonoBehaviour
{
    [SerializeField] int width = 608; // or something else
    [SerializeField] int height = 1080; // or something else
    [SerializeField] bool isFullScreen = false; // should be windowed to run in arbitrary resolution
    [SerializeField] int desiredFPS = 60; // or something else

    // Use this for initialization
    void Start()
    {
        Screen.SetResolution(width, height, isFullScreen, desiredFPS);
        // Update is called once per frame
    }

    void Update ()
    {
		
	}
}
