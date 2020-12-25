using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class InitLevel : MonoBehaviour
{
    [Header("Level Scene")]
    public bool ResetScene;
    public int Level;
    public bool MoveToLevel;
    [Header("Setup Level")]
    public bool isGenerate;
    public bool isSure;
    public int LevelGenerate;
    public int MaxLevelGenerate;
    [Header("Objects")]
    public GameObject cubeObjects;
    public GameObject groundObjects;
    public GameObject player;
    public GameObject finish;
    public GameObject cylinderObjects;
    public GameObject enemyObjects;
    public GameObject enemyWeaponObjects;
    public GameObject enemyGunObjects;
    private void Update()
    {
        _MoveLevel();
        _InitLevel();
        _ResetScene();
    }
    private void _ResetScene()
    {
        if (ResetScene)
        {
            Debug.Log("Reset Scene");
            PlayerPrefs.DeleteAll();
        }
    }
    private void _ConditionList(List<Vector3> list)
    {
        if (list.Count == 0)
            list.Add(Vector3.zero);
        else
        {
            while (list.Count < LevelGenerate)
                list.Add(Vector3.zero);
        }
    }
    private void _InitLevel()
    {
        if (isGenerate)
        {
            Debug.Log("Are you sure ?");
            if(LevelGenerate < MaxLevelGenerate)
            {
                _ConditionAddListScenePos();
                if (isSure)
                {
                    Debug.Log("Level Generate");
                    _GenerateLevel();
                }
            }
            else
            {
                Debug.Log("Level Generate Out of Range <" + MaxLevelGenerate);
            }
        }
    }
    private void _ConditionAddListScenePos()
    {
        List<Vector3> list;
        /* First List Ground */
        GameObject objects = groundObjects;
        for (int i = 0; i < objects.transform.childCount; i++)
        {
            GameObject value = objects.transform.GetChild(i).gameObject;
            list = value.GetComponent<Ground>().scenePos;
            _ConditionList(list);
        }
        /* Second List Ground */
        objects = cubeObjects;
        for(int i=0;i < objects.transform.childCount;i++)
        {
            GameObject value = objects.transform.GetChild(i).gameObject;
            list = value.GetComponent<Cube>().scenePos;
            _ConditionList(list);
        }
        /* Third List Cylinder */
        objects = cylinderObjects;
        for(int i=0;i<cylinderObjects.transform.childCount;i++)
        {
            GameObject value = cylinderObjects.transform.GetChild(i).gameObject;
            list = value.GetComponent<Cylinder>().scenePos;
            _ConditionList(list);
        }
        /* Fourth List Enemy */
        objects = enemyObjects;
        for(int i =0;i<objects.transform.childCount;i++)
        {
            GameObject value = objects.transform.GetChild(i).gameObject;
            list = value.GetComponent<Enemy>().scenePos;
            _ConditionList(list);
        }
        /* Fifth  List EnemyWeapon */
        objects = enemyWeaponObjects;
        for(int i=0;i<objects.transform.childCount;i++)
        {
            GameObject value = objects.transform.GetChild(i).gameObject;
            list = value.GetComponent<Enemy>().scenePos;
            _ConditionList(list);
        }
        /* Sixth List EnemyGun */
        objects = enemyGunObjects;
        for (int i = 0; i < objects.transform.childCount; i++)
        {
            GameObject value = objects.transform.GetChild(i).gameObject;
            list = value.GetComponent<Enemy>().scenePos;
            _ConditionList(list);
        }
        /*  ------------------ Only  Objects ------------------ */
        /* Player List Scene */
        objects = player;
        list = objects.GetComponent<Player>().scenePos;
        _ConditionList(list);
        /* Finish Objects */
        objects = finish;
        list = objects.GetComponent<Finish>().scenePos;
        _ConditionList(list);
    }

    private void _GenerateLevel()
    {
            /* Cube objects */
        GameObject objects = cubeObjects;
        for(int i=0;i<objects.transform.childCount;i++)
        {
            GameObject value = objects.transform.GetChild(i).gameObject;
            if (value.active)
                value.GetComponent<Cube>().scenePos[LevelGenerate - 1] = value.transform.position;
            else
                value.GetComponent<Cube>().scenePos[LevelGenerate - 1] = Vector3.zero;
        }
            /* Ground Objects */
        objects = groundObjects;
        for(int i= 0;i<objects.transform.childCount;i++)
        {
            GameObject value = objects.transform.GetChild(i).gameObject;
            if (value.active)
                value.GetComponent<Ground>().scenePos[LevelGenerate - 1] = value.transform.position;
            else
                value.GetComponent<Ground>().scenePos[LevelGenerate - 1] = Vector3.zero;
        }
            /* Cylinder Objects */
        objects = cylinderObjects;
        for(int i=0;i<objects.transform.childCount;i++)
        {
            GameObject value = objects.transform.GetChild(i).gameObject;
            if (value.active)
                value.GetComponent<Cylinder>().scenePos[LevelGenerate - 1] = value.transform.position;
            else
                value.GetComponent<Cylinder>().scenePos[LevelGenerate - 1] = Vector3.zero;
        }
            /* Enemy Objects */
        objects = enemyObjects;
        for (int i = 0; i < objects.transform.childCount; i++)
        {
            GameObject value = objects.transform.GetChild(i).gameObject;
            if (value.active)
                value.GetComponent<Enemy>().scenePos[LevelGenerate - 1] = value.transform.position;
            else
                value.GetComponent<Enemy>().scenePos[LevelGenerate - 1] = Vector3.zero;
        }
            /* Enemy Weapon Objects */
        objects = enemyWeaponObjects;
        for (int i = 0; i < objects.transform.childCount; i++)
        {
            GameObject value = objects.transform.GetChild(i).gameObject;
            if (value.active)
                value.GetComponent<Enemy>().scenePos[LevelGenerate - 1] = value.transform.position;
            else
                value.GetComponent<Enemy>().scenePos[LevelGenerate - 1] = Vector3.zero;
        }
            /* Enemy Gun Objects */
        objects = enemyGunObjects;
        for (int i = 0; i < objects.transform.childCount; i++)
        {
            GameObject value = objects.transform.GetChild(i).gameObject;
            if (value.active)
                value.GetComponent<Enemy>().scenePos[LevelGenerate - 1] = value.transform.position;
            else
                value.GetComponent<Enemy>().scenePos[LevelGenerate - 1] = Vector3.zero;
        }
        /* Player Objects */
        objects = player;
        objects.GetComponent<Player>().scenePos[LevelGenerate - 1] = objects.transform.position;
        /* Finish Objects */
        objects = finish;
        objects.GetComponent<Finish>().scenePos[LevelGenerate - 1] = objects.transform.position;
    }

    private void _MoveLevel()
    {
        if(MoveToLevel)
        {
            Debug.Log("Move to Level" + Level);
            /* Setup Player-Prefs */
                PlayerPrefs.SetInt("Scene",Level-1);
            /* There are 4 objects that have ScenePos : Cylinder , Cube , Finish , Ground , Player */
                /* There are 3 list : Cube , Ground , Cylinder */
                _LoopList();
                /* Player , Finish Pos */
                    /* Player */
                GameObject obj = player;
                obj.transform.position = obj.GetComponent<Player>().scenePos[Level - 1];
                    /* Finish */
                obj = finish;
                obj.transform.position = obj.GetComponent<Finish>().scenePos[Level - 1];
        }
    }
    private void _LoopList()
    {
        GameObject obj = cubeObjects;
        for(int i=0;i<obj.transform.childCount;i++)
        {
            GameObject value = obj.transform.GetChild(i).gameObject;
            value.transform.position = value.GetComponent<Cube>().scenePos[Level - 1];
            if (value.transform.position == Vector3.zero)
            {
                Debug.Log("Vector3 zero " + value.gameObject);
                value.SetActive(false);
            }
            else value.SetActive(true);
        }
        obj = groundObjects;
        for(int i=0;i< obj.transform.childCount;i++)
        {
            GameObject value = obj.transform.GetChild(i).gameObject;
            value.transform.position = value.GetComponent<Ground>().scenePos[Level - 1];
            if (value.transform.position == Vector3.zero)
            {
                Debug.Log("Vector3 zero " + value.gameObject);
                value.SetActive(false);
            }
            else value.SetActive(true);
        }
        obj = cylinderObjects;
        for(int i=0;i<obj.transform.childCount;i++)
        {
            GameObject value = obj.transform.GetChild(i).gameObject;
            value.transform.position = value.GetComponent<Cylinder>().scenePos[Level - 1];
            if (value.transform.position == Vector3.zero)
            {
                Debug.Log("Vector3 zero " + value.gameObject);
                value.SetActive(false);
            }
            else value.SetActive(true);
        }
        obj = enemyObjects;
        for(int i=0;i<obj.transform.childCount;i++)
        {
            GameObject value = obj.transform.GetChild(i).gameObject;
            value.transform.position = value.GetComponent<Enemy>().scenePos[Level - 1];
            if (value.transform.position == Vector3.zero)
            {
                Debug.Log("Vector3 zero " + value.gameObject);
                value.SetActive(false);
            }
            else value.SetActive(true);
        }
        obj = enemyWeaponObjects;
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject value = obj.transform.GetChild(i).gameObject;
            value.transform.position = value.GetComponent<Enemy>().scenePos[Level - 1];
            if (value.transform.position == Vector3.zero)
            {
                Debug.Log("Vector3 zero " + value.gameObject);
                value.SetActive(false);
            }
            else value.SetActive(true);
        }
        obj = enemyGunObjects;
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject value = obj.transform.GetChild(i).gameObject;
            value.transform.position = value.GetComponent<Enemy>().scenePos[Level - 1];
            if (value.transform.position == Vector3.zero)
            {
                Debug.Log("Vector3 zero " + value.gameObject);
                value.SetActive(false);
            }
            else value.SetActive(true);
        }
    }
}
