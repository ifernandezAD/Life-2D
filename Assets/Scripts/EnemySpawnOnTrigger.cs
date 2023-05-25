using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnOnTrigger : MonoBehaviour
{
    public GameObject[] enemyArray;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < enemyArray.Length; i++)
            {
                enemyArray[i].SetActive(true);
            }
        }
    }
}
