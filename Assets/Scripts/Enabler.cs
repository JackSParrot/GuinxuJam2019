using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enabler : MonoBehaviour
{
    List<GameObject> ToEnable = new List<GameObject>();
    public List<MessageInfo> Messages = new List<MessageInfo>();

    void Start()
    {
        ToEnable.Clear();
        for(int i = 0; i < transform.parent.childCount; ++i)
        {
            ToEnable.Add(transform.parent.GetChild(i).gameObject);
        }
        ToEnable.Remove(gameObject);
        foreach(var go in ToEnable)
        {
            if (go != null)
                go.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            foreach(var go in ToEnable)
            {
                if(go != null)
                go.SetActive(true);
            }
            gameObject.SetActive(false);
            DialogController.Instance.PlaySequence(Messages);
        }
    }
}
