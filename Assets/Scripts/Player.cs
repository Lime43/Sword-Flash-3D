using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Ragdoll Settings")]
    [SerializeField] GameObject ragdollObject;
    [SerializeField] GameObject meshObject, playerObject;
    [Header("Player Settings")]
    public GameObject weaponLeft;
    public GameObject weaponRight;
    [Header("Move Settings")]
    [SerializeField] float moveSpeed;
    [SerializeField] Vector2 startPos, movePos;
    [SerializeField] Vector3 startRotate, endRotate;
    [SerializeField] bool isMoving;
    [Header("Scene Settings")]
    public List<Vector3> scenePos = new List<Vector3>();
    #region implement Unity
    private void Update()
    {
        if (!GameManager.instance.isFinish)
            _Movement();
    }
    private void LateUpdate()
    {
        if (!GameManager.instance.isFinish)
            _LookRotation();
    }
    
    private void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.tag == "ground" )
        {
            _FreezePositionPlayer();
        }
    }
    #endregion

    #region Player script
    public void PlayerFinish()
    {
        _LookRotationFinish();
        _PlayerMoveFinish();
    }
    public void _Die()
    {
        StartCoroutine(C_Die());
    }
    private IEnumerator C_Die()
    {
        weaponLeft.active = weaponRight.active = meshObject.active = playerObject.active = false;
        ragdollObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    private void _FreezePositionPlayer()
    {
        bool isFinish = GameManager.instance.isFinish;
        if (!isFinish)
        {
            Rigidbody rg = GetComponent<Rigidbody>();
            //RigidbodyConstraints.FreezePositionY |
            rg.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
    private void _Movement()
    {
        Rigidbody rg = GetComponent<Rigidbody>();
        if(Input.GetMouseButtonDown(0))
        {
            isMoving = true;
            startPos = movePos = Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            isMoving = true;
            movePos = Input.mousePosition;
            Vector3 direction = (movePos - startPos).normalized;
            direction = new Vector3(direction.x, 0, direction.y);
            rg.velocity = direction * moveSpeed * Time.deltaTime;
        }
        if(Input.GetMouseButtonUp(0))
        {
            isMoving = false;
            rg.velocity = Vector3.zero;
        }
    }
    private void _LookRotation()
    {
        if(isMoving)
        {
            startRotate = transform.position;
            Vector3 direc = startRotate- endRotate;
            if(direc != Vector3.zero)
            {
                Quaternion targetRotate = Quaternion.LookRotation(direc);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotate, 8f * Time.deltaTime);
            }
            endRotate = transform.position;
        }
    }
    private void _PlayerMoveFinish()
    {
        StartCoroutine(C_PlayerMoveFinish());
    }

    private IEnumerator C_PlayerMoveFinish()
    {
        bool isLoop = true;
        while (isLoop)
        {
            Transform targetPos = GameManager.instance.finish.GetComponent<Finish>().middlePosition.transform;
            Vector3 direc = targetPos.transform.position - transform.position;

            if (direc != Vector3.zero)
            {
                /* Move to Position Middle  */
                Rigidbody rg = GetComponent<Rigidbody>();
                rg.velocity = direc * moveSpeed * Time.deltaTime;
            }

            if (direc == Vector3.zero) isLoop = false;

            yield return null;
        }
    }

    private void _LookRotationFinish()
    {
        StartCoroutine(C__LookRotationFinish());
    }

    private IEnumerator C__LookRotationFinish()
    {
        /* Look rotation to Middle Position */
        Transform targetPos = GameManager.instance.finish.GetComponent<Finish>().middlePosition.transform;
        Vector3 direc = targetPos.transform.position - transform.position;
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotate = Quaternion.LookRotation(direc);
        float _time = 0.0f;
        bool isLoop = true;
        while(isLoop)
        {
            float _distance = Vector3.Distance(transform.position,targetPos.position);
            if (_distance > .5f)
            {
                transform.rotation = Quaternion.Lerp(currentRotation, targetRotate, 8f * Time.deltaTime);
                yield return null;
            }
            else isLoop = false;
        }
        _time = 0.0f;
        currentRotation = transform.rotation;
        _DanceAnimation();

        while (_time < 1.0f)
        {
            _time += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(currentRotation, Quaternion.Euler(0, -180f, 0), _time);
            yield return null;
        }
    }

    private void _DanceAnimation()
    {
        Animator anim = GetComponent<Animator>();
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Dance"))
        {
            anim.SetTrigger("isFinish");
            /* Disable Weapon */
                weaponLeft.active = weaponRight.active = false;
        }
    }

    #endregion
}
