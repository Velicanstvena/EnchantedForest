using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    private Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 GetBasePosition()
    {
        return currentPosition;
    }

}
