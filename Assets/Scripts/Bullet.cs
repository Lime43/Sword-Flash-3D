using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    private void OnCollisionEnter(Collision c)
    {
        if (tag == "Bullet")
        {
            if(c.gameObject.tag == "weapon")
            {
                
            }
            if(c.gameObject.tag == "Player")
            {
                GameManager.instance._GameOver();
            }
        }
    }
    private void OnEnable()
    {
        bulletFollowPlayer();
    }
    void bulletFollowPlayer()
    {
        StartCoroutine(C_bulletFollowPlayer());
    }
    IEnumerator C_bulletFollowPlayer()
    {
            /* Const */
        Rigidbody rg = GetComponent<Rigidbody>();
        rg.velocity = Vector3.zero;
            /* ----- */
        GameObject player = GameManager.instance.player.gameObject;
        bool Loop = true;
        while (Loop)
        {
            if(!player.GetComponent<Player>())
            {
                Loop = false;
            }
            else
            {
                Transform currentPos = transform;
                Transform playerPos = player.transform;
                Vector3 direc = playerPos.position - currentPos.position;
                rg.velocity = bulletSpeed * direc * Time.deltaTime;
                yield return null;
            }
        }
    }
}
