using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowManolo : MonoBehaviour
{
    public Transform toFollow;
    public float speed;

    Vector3 _offset;
    void Awake()
    {
        _offset = transform.position - toFollow.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp( transform.position, toFollow.position + _offset, Time.deltaTime * speed);
    }
}
