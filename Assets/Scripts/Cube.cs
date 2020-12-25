using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] GameObject prefabCubeEffect;
    [Header("Scene Settings")]
    public List<Vector3> scenePos = new List<Vector3>();
    public void EffectHitCube()
    {
        int Count=0;
        List<GameObject> list = GameManager.instance.cubeEffectList;
        GameObject particleSystem = GameManager.instance.particleObjects.transform.GetChild(0).gameObject;
        if (GameManager.instance.cubeEffectList.Count > 0)
        {
            /* Checking if Cube Effect is inactive - we can reuse IT */
            foreach(var value in list)
            {
                if(!value.active)
                {
                    value.SetActive(true);
                    value.transform.position = transform.position;
                    break;
                }
            }
            /* Checking if there is Full - We need to create Once */
            for(int i=0;i<list.Count;i++)
            {
                if (list[i].gameObject.active)
                {
                    Count++;
                }
                if(Count == list.Count)
                {
                    GameObject effect = Instantiate(prefabCubeEffect);
                    effect.transform.position = transform.position;
                    effect.transform.SetParent(particleSystem.transform);
                    list.Add(effect);
                    break;
                }
            }
        }
        /* If list dont have one effect - Then create it and use it once */
        else
        {
            GameObject effect = Instantiate(prefabCubeEffect);
            effect.transform.SetParent(particleSystem.transform);
            list.Add(effect);
            effect.transform.position = transform.position;
        }
    }
}
