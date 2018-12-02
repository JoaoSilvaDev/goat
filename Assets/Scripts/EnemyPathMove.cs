﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathMove : Enemy
{
	int index = 0;
	Vector2 _input = Vector2.zero;
	public List<Vector3> directions = new List<Vector3>();

    bool _isMoving = false;
    Vector3 startPos, endPos;
    private float _time;
    public float speed;
	
	public override void Move()
	{
		index++;

        if(index > directions.Count-1)
            index = 0;
        
        Debug.Log(_input);

		_input = directions[index];
		StartCoroutine(Move(transform));
	}

	IEnumerator Move(Transform entity)
    {
        _isMoving = true;
        startPos = entity.position;
        _time = 0.0f;

        endPos = new Vector3(startPos.x + _input.x, startPos.y + _input.y, startPos.z);
        Debug.Log(startPos + " " + endPos);

        while (_time < 1.0f)
        {
            _time += Time.deltaTime * speed;
            entity.position = Vector3.Lerp(startPos, endPos, _time);
            yield return null;
        }

        _isMoving = false;
        yield return 0;
    }
}
