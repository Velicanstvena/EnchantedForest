using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Pathfinding pathfinding;
    private Pathfinding2 pathfinding2;

    private void Start()
    {
        PlayerPrefsController.SetMasterVolume(0.2f);
        PlayerPrefsController.SetDifficulty(1);
        Debug.Log(PlayerPrefsController.GetDifficulty());
        Debug.Log(PlayerPrefsController.GetMasterVolume());
    }

    private void Update()
    {
        
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera) 
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }
}
