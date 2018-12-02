using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathMove : Enemy
{
	int index = -1;
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

		_input = directions[index] * 0.95f;
		StartCoroutine(Move(transform));
	}

    IEnumerator Move(Transform entity)
    {
        _isMoving = true;
        startPos = entity.position;
        _time = 0.0f;

        endPos = new Vector3(startPos.x + _input.x, startPos.y + _input.y, startPos.z);

        while (_time < 1.0f)
        {
            _time += Time.deltaTime * speed;
            GetComponent<Animator>().SetBool("jump", true);
            entity.position = Vector3.Lerp(startPos, endPos, _time);
            yield return null;
        }

        if (index + 1 > directions.Count - 1)
        {
            if (directions[0].x > 0)
                GetComponent<SpriteRenderer>().flipX = true;
            if (directions[0].x < 0)
                GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            if (directions[index + 1].x > 0)
                GetComponent<SpriteRenderer>().flipX = true;
            if (directions[index + 1].x < 0)
                GetComponent<SpriteRenderer>().flipX = false;
        }

        GetComponent<Animator>().SetBool("jump", false);
        _isMoving = false;
        yield return 0;
    }
}
