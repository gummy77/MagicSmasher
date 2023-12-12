using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    [SerializeField] GameObject player;

    bool hasCompleted = false;
    
    [SerializeField] private bool hasToKill = false;
    [SerializeField] private List<GameObject> toKill = new List<GameObject>();

    void Update(){
        if(hasToKill) {
            toKill.RemoveAll(GameObject => GameObject == null);
            if(toKill.Count <= 0){
                hasCompleted = true;
            }
        }

        if(hasCompleted){
            Debug.Log("WINNING");
        }
    }
    
    public void spawnCombat(int count, GameObject enemy){
        for(int i = count; i > 0; i--){
            GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), Quaternion.identity);
            newEnemy.GetComponent<EnemyMovementController>().setTarget(player.transform);
            toKill.Add(newEnemy);
        }
        hasToKill = true;
    }
}
