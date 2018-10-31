﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{

    // Configuration parameters
    WaveConfig waveConfig;
    float moveSpeed;

    List<Transform> waypoints;
    int waypointIndex = 0;


	// Use this for initialization
	void Start ()
    {
        moveSpeed = waveConfig.GetMoveSpeed();
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPos = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position =
                Vector2.MoveTowards(transform.position, targetPos, movementThisFrame);

            if (transform.position == targetPos)
            {
                waypointIndex++;
            }
        }

        else
        {
            Destroy(gameObject);
        }
    }

}
