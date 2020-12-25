using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] BoxCollider boxCollider;
    [Header("Enemy Melee Settings")]
    [SerializeField] float Distance,speedMoving;
    [Header("Enemy Gun Settings")]
    [SerializeField] bool isGun;
    [SerializeField] public GameObject enemyBullet;
    [SerializeField] Transform gunPos;
    [Header("Scene Settings")]
    public List<Vector3> scenePos = new List<Vector3>();
    private void Start()
    {
        if (!isGun)
            _EnemyMove();
        else
            _EnemyGun();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            /* Player Die On Anim Enemy Punch */
            GameManager.instance._GameOver();
        }
    }
    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "weapon" || c.gameObject.tag == "Player")
        {
            /* Enemy Die */
            _die();
        }
    }
    void _EnemyGun()
    {
        StartCoroutine(C_EnemyGunLook());
        StartCoroutine(C_ParticleEffectGun());
    }
    public IEnumerator C_ParticleEffectGun()
    {
        ParticleSystem particleEffect = transform.GetChild(4).GetComponent<ParticleSystem>();
        bool Loop = true;
        float Distance;
        while(Loop)
        {
            Transform playerPos = GameManager.instance.player.transform;
            Distance = Vector3.Distance(transform.position, playerPos.position);
            if(Distance < 5f)
            {
                if (enemyBullet)
                {
                    if (!enemyBullet.active)
                    {
                        yield return new WaitForSeconds(2f);
                        particleEffect.Play();
                        enemyBullet.transform.position = gunPos.position;
                        enemyBullet.SetActive(true);
                    }
                }
            }
            yield return null;
        }
        
    }
    IEnumerator C_EnemyGunLook()
    {
        GameObject target = GameManager.instance.player.gameObject;
        bool isLoop = true;
        while (isLoop)
        {
            // This condition checking if Enemy is Dead ?
            if (!GetComponent<Rigidbody>())
            {
                isLoop = false;
            }
            else
            {
                /* LookRotate to Player */
                Rigidbody rg = GetComponent<Rigidbody>();
                Vector3 direc = target.transform.position - gameObject.transform.position;
                direc.y = 0f;
                Quaternion targetRotate = Quaternion.LookRotation(direc);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotate, 8f * Time.deltaTime);
                rg.velocity = new Vector3(0f, rg.velocity.y, 0f);
                yield return null;
            }
        }
    }
    private void _EnemyMove()
    {
        StartCoroutine(C_EnemyMove());
    }
    private IEnumerator C_EnemyMove()
    {
        
            Animator anim = GetComponent<Animator>();
            Rigidbody rg = GetComponent<Rigidbody>();
            GameObject target = GameManager.instance.player.gameObject;
            bool isLoop = true;
            while (isLoop)
            {
                if (gameObject.GetComponent<Rigidbody>() != null)
                {
                    Vector3 direc = target.transform.position - gameObject.transform.position;
                    Distance = Vector3.Distance(gameObject.transform.position, target.transform.position);
                    if (Distance < 3f && direc != Vector3.zero)
                    {
                        if (Distance < .45f)
                        {
                            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
                            {
                                rg.velocity = Vector3.zero;
                                anim.SetTrigger("isPunch");
                            }
                            yield return null;
                        }
                        else
                        {
                            Quaternion targetRotate = Quaternion.LookRotation(direc);
                            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotate, 8f * Time.deltaTime);
                            /* Animate Run */
                            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
                                anim.SetTrigger("isRun");
                            /* Run Forward */
                            rg.velocity = direc * speedMoving * Time.deltaTime;
                            rg.constraints = RigidbodyConstraints.FreezeRotation;
                            yield return null;
                        }
                    }
                    if (Distance > 3f)
                    {
                        rg.velocity = Vector3.zero;
                        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                            anim.SetTrigger("isIdle");
                        yield return null;
                    }
                }
                else isLoop = false;
            }
    }
    private void _die()
    {
        StartCoroutine(C_die());
    }
    private IEnumerator C_die()
    {
        /* Activate Slow-motion */
        GameManager.instance._SlowMotion();
        Animator anim = GetComponent<Animator>();
        /* Rg-Disable */
        Rigidbody rg = GetComponent<Rigidbody>();
        Destroy(rg);
        boxCollider.enabled = anim.enabled = false; 
        /* Enable ragdoll */
        GameObject ragdoll = gameObject.transform.GetChild(2).gameObject;
        GameObject mesh = gameObject.transform.GetChild(0).gameObject;
        GameObject modelEnemy = gameObject.transform.GetChild(1).gameObject;
            /* Disable Object that not Ragdoll */
            modelEnemy.active = mesh.active = false;
            ragdoll.SetActive(true);
        /* Add force Rigibody Ragdoll */
        Rigidbody rgRagdoll = ragdoll.transform.GetChild(0).gameObject.GetComponent<Rigidbody>();
        float RandomX = Random.Range(-250f, 250f);
        float RandomZ = Random.Range(-250f, 250f);
        rgRagdoll.AddForce(RandomX, 500f, RandomZ);
        /* Add Text */
        GameObject text = gameObject.transform.GetChild(3).gameObject;
        int r = UnityEngine.Random.Range(0, 5);
            switch(r)
            {
                case 1:
                    text.GetComponent<TextMesh>().text = "Ah!";
                    break;
                case 0:
                    text.GetComponent<TextMesh>().text = "Ow!";
                    break;
                case 2:
                    text.GetComponent<TextMesh>().text = "Uh!";
                    break;
                case 3:
                    text.GetComponent<TextMesh>().text = "T_T~";
                    break;
                case 4:
                    text.GetComponent<TextMesh>().text = "X_X~";
                    break;
            }
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        text.SetActive(true);
        /* Sound Effect */
        AudioSource adiSrc = gameObject.AddComponent<AudioSource>();
        AudioClip kill = SoundManager.instance.kill;
        adiSrc.clip = kill;
        adiSrc.volume = .35f;
        adiSrc.Play();
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}