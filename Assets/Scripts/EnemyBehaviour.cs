using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform playerPosition;
    public float enemySpeed;
    private Rigidbody myRigid;

    private void Awake()
    {
        myRigid = GetComponent<Rigidbody>();
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        MoveTowardsPlayer();
    }

    private void MoveTowardsPlayer()
    {
        if (playerPosition != null)
        {
            Vector3 moveDirection = (playerPosition.position - this.transform.position).normalized;
            transform.LookAt(playerPosition);
            myRigid.velocity = moveDirection * enemySpeed;
        }
        
    }
}
