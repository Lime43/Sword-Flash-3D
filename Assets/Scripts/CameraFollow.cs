using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Camera Follow Settings")]
    [SerializeField] Transform cameraFinish;
    [SerializeField] Transform objectFollow;
    [SerializeField] Vector3 smoothtime,vec,distance;
    public bool Follow;
    [Header("Limit Camera Settings")]
    [SerializeField] bool limitCamera;
    [SerializeField] Vector3 minDistance,maxDistance;
    private void Start()
    {
        _CameraFollowObject();
    }
    public void _CameraFinish()
    {
        StartCoroutine(C__CameraFinish());
    }

    private IEnumerator C__CameraFinish()
    {
        float _time = 0.0f;
        Quaternion currentQuaternion = transform.rotation;
        Quaternion targetQuaternion = cameraFinish.rotation;
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = cameraFinish.position;

        while(_time < 1.0f)
        {
            _time += Time.deltaTime / 5.0f;
            transform.rotation = Quaternion.Lerp(currentQuaternion, targetQuaternion , _time);
            transform.position = Vector3.Lerp(currentPosition, targetPosition, _time);
            yield return null;
        }
    }

    public void _CameraFollowObject()
    {
        StartCoroutine(C_CameraFollowObject());
    }
    private IEnumerator C_CameraFollowObject()
    {
        while(!GameManager.instance.isFinish&&Follow)
        {
            float posX = Mathf.SmoothDamp(gameObject.transform.position.x, objectFollow.transform.position.x, ref vec.x,
            smoothtime.x * Time.deltaTime);
            float posY = Mathf.SmoothDamp(gameObject.transform.position.y, objectFollow.transform.position.y, ref vec.y,
                smoothtime.y * Time.deltaTime);
            float posZ = Mathf.SmoothDamp(gameObject.transform.position.z, objectFollow.transform.position.z, ref vec.z,
                smoothtime.z * Time.deltaTime);
            if (limitCamera)
            {
                /* Position Y */
                posY = Mathf.Clamp(posY, minDistance.y, maxDistance.y);
            }
            transform.position = new Vector3(posX, posY, posZ)+ distance;
            yield return new WaitForFixedUpdate();
            //yield return null;
        }
    }
}
