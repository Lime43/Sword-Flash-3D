using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [Header("Finish Settings")]
    public GameObject middlePosition;
    [Header("Scene Settings")]
    public List<Vector3> scenePos = new List<Vector3>();
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player")
        { 
            GameManager.instance._CompleteGame();
        }
    }
}
