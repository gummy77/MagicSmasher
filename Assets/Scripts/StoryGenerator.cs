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
        "end the Lives of",
        "murder",
        "slay",
        "kill",
        "defeat"
    };

    [SerializeField] private string[] collectTrial = {
        "collect",
        "steal"
    };


    public void generateQuest(){
        currentLetter = 0;
        timer = 0;
        questText = "";

        int enemyCount = (int) Random.Range(1, 10); //amount of enemies for combat trials


        switch((int)Random.Range(0,1)){ // Quest Type

            case(0): //Combat Trial
                questText += combatTrial[(int)Random.Range(0, combatTrial.Length)];
                questText += enemyCount + " ";
                questText += enemies[0].name;
                if(enemyCount > 1) questText += "s";

                questText += ".";

                questManager.spawnCombat(enemyCount, enemies[0]);
            break;

            case(1): //Combat and Collect Trial
                questText += combatTrial[(int)Random.Range(0, combatTrial.Length)];
                questText += enemyCount + " ";
                questText += enemies[0].name;
                if(enemyCount > 1) questText += "s";

                questText += " and then ";
                questText += collectTrial[(int)Random.Range(0, collectTrial.Length)];
                questText += "their thing";

                questManager.spawnCombat(enemyCount, enemies[0]);
            break;

            case(2): //Collect Trial
                questText += collectTrial[(int)Random.Range(0, collectTrial.Length)];
                questText += "their thing";

                questManager.spawnCombatants(enemyCount, enemies[0]);
            break;
        }


        questText = questText.Substring(0, 1).ToUpper() + questText.Substring(1, questText.Length-1);
    }
}
