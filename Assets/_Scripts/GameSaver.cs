using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSaver : MonoBehaviour
{
    public static UnityEvent OnSave = new UnityEvent();
    public static UnityEvent OnLoad = new UnityEvent();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) {
            OnSave.Invoke();
        }
    }
}
