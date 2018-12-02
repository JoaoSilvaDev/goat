using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	int currentPoint = 0;
	Vector3 targetPos = Vector3.zero;
	List<Vector3>[] points = new List<Vector3>();

	void Start()
	{
		targetPos = transform.position;
	}
	void Move()
	{}

	void NextPoint()
	{
		currentPoint++;
	}

	
}
