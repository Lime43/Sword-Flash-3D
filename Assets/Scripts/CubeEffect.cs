using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEffect : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(delay());
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
