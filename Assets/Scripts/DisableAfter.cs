using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAfter : MonoBehaviour
{
    public float time = 1f;

    void Update()
    {
        if ((time -= Time.deltaTime) < 0f) gameObject.SetActive(false);
    }
}
