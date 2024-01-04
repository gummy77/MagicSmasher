using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private StoryGenerator storyGenerator;

    [SerializeField] private TMP_Text timerText;

    [SerializeField] private int questCount = 0;


    bool hasCompleted = false;
    bool hasFailed = false;
    private float startTimer = 5;
    
    private bool hasToKill = false;
    private List<GameObject> entities = new List<GameObject>();
    private List<GameObject> toKill = new List<GameObject>();

    void Start(){
        StartNewQuest();
    }

    void Update(){
        if(hasCompleted || hasFailed){
            timerText.text = (int)(startTimer+1)+"";
            if(startTimer <= 0) {
                StartNewQuest();
                startTimer = 5f;
            }
            startTimer -= Time.deltaTime;
            return;
        }

        if(player.GetComponent<PlayerHealthController>().isDead()) {
            hasFailed = true;
        }

        if(hasToKill) {
            toKill.RemoveAll(GameObject => GameObject == null);
            if(toKill.Count <= 0){
                hasCompleted = true;
            }
        }
    }
    
    public void StartNewQuest() {
        hasCompleted = false;
        hasFailed = false;
        questCount += 1;
        timerText.text = "";

        foreach(GameObject entity in entities){
            Destroy(entity);
        }
        player.GetComponent<PlayerHealthController>().revive();
        player.transform.position = new Vector3(0, 0, -20);
        player.transform.rotation = Quaternion.identity;
        storyGenerator.generateQuest();
    }

    public void spawnCombat(int count, GameObject enemy){
        for(int i = count; i > 0; i--){
            GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), Quaternion.identity);
            newEnemy.GetComponent<EnemyController>().setTarget(player.transform);
            toKill.Add(newEnemy);
            entities.Add(newEnemy);
        }
        hasToKill = true;
    }

    public void spawnCombatants(int count, GameObject enemy){
        for(int i = count; i > 0; i--){
            GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)), Quaternion.identity);
            newEnemy.GetComponent<EnemyController>().setTarget(player.transform);
            entities.Add(newEnemy);
        }
    }
}
