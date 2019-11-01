using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MessageInfo
{
    public int leftImage;
    public int rightImage;
    public string message;
}

public class DialogController : MonoBehaviour
{
    public static DialogController Instance;

    public List<Sprite> Characters = new List<Sprite>();
    public TMPro.TextMeshProUGUI Message;
    public GameObject Continue;
    public UnityEngine.UI.Image Left;
    public UnityEngine.UI.Image Right;

    List<MessageInfo> _playingSequence = new List<MessageInfo>();

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void PlaySequence(List<MessageInfo> messages)
    {
        if (messages.Count < 1) return;
        GameInput.Instance?.Pause();
        gameObject.SetActive(true);
        foreach(var msg in messages)
        {
            _playingSequence.Add(msg);
        }
        SetupMessage();
    }

    void SetupMessage()
    {
        if (_playingSequence.Count < 1) return;
        var msg = _playingSequence[0];
        Message.text = msg.message;
        Continue.SetActive(_playingSequence.Count < 2);
        Left.gameObject.SetActive(msg.leftImage >= 0);
        if (msg.leftImage >= 0)
        {
            Left.sprite = Characters[msg.leftImage];
        }
        Right.gameObject.SetActive(msg.rightImage >= 0);
        if (msg.rightImage >= 0)
        {
            Right.sprite = Characters[msg.rightImage];
        }
        delta = 1f;
    }

    float delta = 1f;
    void Update()
    {
        delta -= Time.unscaledDeltaTime;
        if (delta < 0f && Input.anyKeyDown)
        {
            _playingSequence.RemoveAt(0);
            SetupMessage();
            if (_playingSequence.Count < 1)
            {
                gameObject.SetActive(false);
                GameInput.Instance?.Resume();
            }
        }
    }
}
