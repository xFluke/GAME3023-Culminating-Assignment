using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSaver : MonoBehaviour
{
    public GameObject obj;
    
    public void Save()
    {
        float xPos = obj.transform.position.x;
        float yPos = obj.transform.position.y;
        float zPos = obj.transform.position.z;
        PlayerPrefs.SetFloat("X", xPos);
        PlayerPrefs.SetFloat("Y", yPos);
        PlayerPrefs.SetFloat("Z", zPos);
        PlayerPrefs.Save();
    }

    public void Load()
    {
        float xPos = PlayerPrefs.GetFloat("X", 0.0f);
        float yPos = PlayerPrefs.GetFloat("Y", 0.0f);
        float zPos = PlayerPrefs.GetFloat("Z", 0.0f);
        obj.transform.SetPositionAndRotation(new Vector3(xPos, yPos, zPos), obj.transform.rotation);
    }
}
