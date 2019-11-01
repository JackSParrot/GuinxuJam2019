using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    private void Awake()
    {
        Instance = this;
    }

    public Vector3 mousePosition = Vector3.zero;
    bool _paused = false;

    public bool GetMouseButtonDown(int idx)
    {
        if (_paused) return false;
        return Input.GetMouseButtonDown(idx);
    }
    public void Pause()
    {
        _paused = true;
        Time.timeScale = 0f;
    }
    public void Resume()
    {
        _paused = false;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_paused)
        {
            mousePosition = Input.mousePosition;
        }
    }
}
