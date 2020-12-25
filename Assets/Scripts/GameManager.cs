using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton classManager
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion 

    [Header("References")]
    public CanvasUI canvasUI;
    public CameraFollow cameraFollow;
    public Player player;
    public GameObject cubeObjects;
    public GameObject particleObjects;
    public GameObject groundObjects;
    public GameObject finishEffect;
    public GameObject cylinderObjects;
    public GameObject enemyObjects;
    public GameObject enemyweaponObjects;
    public GameObject enemyGunObjects;
    public GameObject bulletObjects;
    [Header("List Settings")]
    public List<GameObject> cubeEffectList  = new List<GameObject>();
    public List<GameObject> cubeList        = new List<GameObject>();
    public List<GameObject> groundList      = new List<GameObject>();
    public List<GameObject> cylinderList    = new List<GameObject>();
    public List<GameObject> enemyList       = new List<GameObject>();
    public List<GameObject> enemyweaponList = new List<GameObject>();
    public List<GameObject> enemyGunList    = new List<GameObject>();
    public List<GameObject> bulletList      = new List<GameObject>();
    [Header("Finish Settings")]
    public bool isFinish;
    public Finish finish;
    [Header("Prefab Settings")]
    public GameObject prefabBullet;
    #region implement Unity 
    
    private void Start()
    {
        _InitGame();
    }
    #endregion

    #region _InitGame()
    void InitScenePos()
    {
        /* Scene Pos */
        int Scene = PlayerPrefs.GetInt("Scene");
        GameObject obj = cubeObjects;
        /* Cube Position */
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject value = obj.transform.GetChild(i).gameObject;
            value.transform.position = value.GetComponent<Cube>().scenePos[Scene];
            if (value.GetComponent<Cube>().scenePos[Scene] == Vector3.zero)
                value.SetActive(false);
            else
                value.SetActive(true);
        }
        /* Ground Position */
        obj = groundObjects;
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject value = obj.transform.GetChild(i).gameObject;
            value.transform.position = value.GetComponent<Ground>().scenePos[Scene];
            if (value.GetComponent<Ground>().scenePos[Scene] == Vector3.zero)
                value.SetActive(false);
            else
                value.SetActive(true);
        }
        /* Cylinder Position */
        obj = cylinderObjects;
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject value = obj.transform.GetChild(i).gameObject;
            value.transform.position = value.GetComponent<Cylinder>().scenePos[Scene];
            if (value.GetComponent<Cylinder>().scenePos[Scene] == Vector3.zero)
                value.SetActive(false);
            else
                value.SetActive(true);
        }
        /* Enemy Position */
        obj = enemyObjects;
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject value = obj.transform.GetChild(i).gameObject;
            value.transform.position = value.GetComponent<Enemy>().scenePos[Scene];
            if (value.GetComponent<Enemy>().scenePos[Scene] == Vector3.zero)
                value.SetActive(false);
            else
                value.SetActive(true);
        }
        /* EnemyWeapon Position */
        obj = enemyweaponObjects;
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject value = obj.transform.GetChild(i).gameObject;
            value.transform.position = value.GetComponent<Enemy>().scenePos[Scene];
            if (value.GetComponent<Enemy>().scenePos[Scene] == Vector3.zero)
                value.SetActive(false);
            else
                value.SetActive(true);
        }
        /* EnemyGun Position */
        obj = enemyGunObjects;
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject value = obj.transform.GetChild(i).gameObject;
            value.transform.position = value.GetComponent<Enemy>().scenePos[Scene];
            if (value.GetComponent<Enemy>().scenePos[Scene] == Vector3.zero)
                value.SetActive(false);
            else
                value.SetActive(true);
        }
        /* Player Position */
        obj = player.gameObject;
        obj.transform.position = obj.GetComponent<Player>().scenePos[Scene];
        /* Finish Position */
        obj = finish.gameObject;
        obj.transform.position = obj.GetComponent<Finish>().scenePos[Scene];
        if (obj.GetComponent<Finish>().scenePos[Scene] == Vector3.zero)
            obj.SetActive(false);
        /* Adding List Array After Init Position */


        /* Adding Cube List */
        for (int i = 0; i < cubeObjects.transform.childCount; i++)
        {
            GameObject cube = cubeObjects.transform.GetChild(i).gameObject;
            if (cube.active)
                cubeList.Add(cube);
        }

        /* Adding Ground List */
        for (int i = 0; i < groundObjects.transform.childCount; i++)
        {
            GameObject ground = groundObjects.transform.GetChild(i).gameObject;
            if (ground.active)
                groundList.Add(ground);
        }

        /* Adding cylinder List */
        for (int i = 0; i < cylinderObjects.transform.childCount; i++)
        {
            GameObject cylinder = cylinderObjects.transform.GetChild(i).gameObject;
            if (cylinder.active)
                cylinderList.Add(cylinder);
        }

        /* Adding  enemyList */
        for (int i = 0; i < enemyObjects.transform.childCount; i++)
        {
            GameObject enemy = enemyObjects.transform.GetChild(i).gameObject;
            if (enemy.active)
                enemyList.Add(enemy);
        }

        /* Adding enemyWeaponList */
        for (int i = 0; i < enemyweaponObjects.transform.childCount; i++)
        {
            GameObject enemyWeapon = enemyweaponObjects.transform.GetChild(i).gameObject;
            if (enemyWeapon.active)
                enemyweaponList.Add(enemyWeapon);
        }

        /* Adding EnemyGunList */
        for (int i = 0; i < enemyGunObjects.transform.childCount; i++)
        {
            GameObject enemyGun = enemyGunObjects.transform.GetChild(i).gameObject;
            if (enemyGun.active)
            {
                enemyGunList.Add(enemyGun);
            }
        }
        /* 1 bullet each Enemy */
        ObjectPooling(enemyGunList.Count, prefabBullet, bulletObjects.transform);



        /* Adding Bullet List */
        for (int i = 0; i < bulletObjects.transform.childCount; i++)
        {
            GameObject value = bulletObjects.transform.GetChild(i).gameObject;
            bulletList.Add(value);
        }

        /* Adding EnemyBullet 1 each */
        for (int i = 0; i < enemyGunObjects.transform.childCount; i++)
        {
            GameObject enemyGun = enemyGunObjects.transform.GetChild(i).gameObject;
            if (enemyGun.active)
            {
                enemyGun.GetComponent<Enemy>().enemyBullet = bulletList[i].gameObject;
            }
        }
        /* End Adding Bullet List */
    }
    private void _InitGame()
    {
        /* Shop Item Init */
        canvasUI.InitShopItem();
        /* Init Scene Pos */
        InitScenePos();
    }
    #endregion
    
    
    private IEnumerator spawnFinishEffect()
    {
        #region variable
        GameObject row = finish.transform.GetChild(1).gameObject;
        #endregion

        #region action
        finishEffect.SetActive(true);

        finishEffect.transform.parent = row.transform;
        finishEffect.transform.localPosition = Vector3.zero;


        yield return new WaitForSeconds(2f);

        finishEffect.SetActive(false);

        yield return spawnFinishEffect();
        #endregion
    }

    public void _CompleteGame()
    {
        isFinish = true;
        /* Finish Effect Spawning */
        StartCoroutine(C_CompleteGame());

    }

    private IEnumerator C_CompleteGame()
    {
        // camera finish
        cameraFollow._CameraFinish();

        // player animation
        player.PlayerFinish();

        // effect confetti
        StartCoroutine(spawnFinishEffect());

        // show ui
        canvasUI._PanelFinish();
        yield return new WaitForSeconds(1.0f);
    }
    public void _GameOver()
    {
       /* Player Die */
        player._Die();
        /* Show UI */
        canvasUI._PanelGameOver();
    }
    public void _SlowMotion()
    {
        StartCoroutine(C_Slowmotion());
    }
    private IEnumerator C_Slowmotion()
    {
        Time.timeScale = .7f;
        float _time = .0f;
            while(_time < 1f)
            {
                _time += Time.deltaTime / 1.0f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                if (_time > 1f) Time.timeScale = 1f;
                yield return null;
            }
    }
    void ObjectPooling(int childCount, GameObject prefaB, Transform parent)
    {
        for (int i = 0; i < childCount; i++)
        {
            GameObject obj = Instantiate(prefaB, parent);
            obj.SetActive(false);
        }
    }
}
