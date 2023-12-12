using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryGenerator : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;

    [SerializeField] private TMP_Text text;
    [SerializeField] private float talkSpeed = 0.05f;
    [SerializeField] private string questText = "";

    private int currentLetter = 0;
    private float timer = 0;

    void Start()
    {
        generateQuest();
    }

    void Update(){
        text.text = questText.Substring(0, currentLetter);

        timer += Time.deltaTime;
        if(currentLetter < questText.Length && timer > talkSpeed){
            currentLetter += 1;
            timer = 0;
        }
    }

    [SerializeField] GameObject[] enemies;

    [SerializeField] private string[] combatTrial = {
        "End the Lives of ",
        "Murder ",
        "Slay ",
        "Kill "
    };


    void generateQuest(){
        switch((int)Random.Range(0,0)){ // Quest Type
            case(0): //Combat Trial
            
            int enemyCount = (int) Random.Range(1, 10);

            questText += combatTrial[(int)Random.Range(0, combatTrial.Length)];
            questText += enemyCount + " ";
            questText += enemies[0].name;
            if(enemyCount > 1) questText += "s";

            questText += ".";

            questManager.spawnCombat(enemyCount, enemies[0]);
            break;
        }
    }
}
