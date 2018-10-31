using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    float speedOfSpin;
    [SerializeField] float minspeed = 360f;
    [SerializeField] float maxspeed = 500f;


    // Update is called once per frame
    void Update ()
    {
        speedOfSpin = Random.Range(minspeed, maxspeed);
        transform.Rotate(0, 0, speedOfSpin * Time.deltaTime);
	}
}
