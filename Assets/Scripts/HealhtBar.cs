using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealhtBar : MonoBehaviour
{
    void Update()
    {
        var rot = transform.rotation;
        rot.eulerAngles = new Vector3(rot.eulerAngles.x, 0f, 0f);
        transform.rotation = rot;
    }
}
