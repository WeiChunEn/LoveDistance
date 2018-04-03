using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_area : MonoBehaviour
{
    public Transform start_point;
    public Transform end_point;
    public float _fDistance;
    public float _fStart_x ;
    public float _fSpeed = 5.0f;

	
	void Start ()
    {
        _fStart_x = transform.position.x;
    }
	
	
	void Update ()
    {
        _fDistance = Mathf.PingPong(Time.time * _fSpeed, Vector3.Distance(start_point.position, end_point.position));
        transform.position = new Vector3(_fStart_x + _fDistance,transform.position.y, transform.position.z);
	}
}
