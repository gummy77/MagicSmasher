using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryGenerator : MonoBehaviour
{
    // [SerializeField] private TMP_Text text;
    // [SerializeField] private float talkSpeed = 0.05f;

    // private int currentLetter = 0;
    // private float timer = 0;

    // void Update(){
    //     text.text = questText.Substring(0, currentLetter);

    //     timer += Time.deltaTime;
    //     if(currentLetter < questText.Length && timer > talkSpeed){
    //         currentLetter += 1;
    //         timer = 0;
    //     }
    // }

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


    public Quest generateQuest(){
        // currentLetter = 0;
        // timer = 0;

        string questText = "";
        int questType = (int)Random.Range(0,1);
        int enemyType = (int) Random.Range(0, enemies.Length); //which enemy you are fighting
        int enemyCount = (int) Random.Range(1, 10); //amount of enemies for combat trials


        switch(questType){ // Quest Type

            case(0): //Combat Trial
                questText += combatTrial[(int)Random.Range(0, combatTrial.Length)];
                questText += enemyCount + " ";
                questText += enemies[enemyType].name;
                if(enemyCount > 1) questText += "s";

                questText += ".";
            break;

            case(1): //Combat and Collect Trial
                questText += combatTrial[(int)Random.Range(0, combatTrial.Length)];
                questText += enemyCount + " ";
                questText += enemies[enemyType].name;
                if(enemyCount > 1) questText += "s";

                questText += " and then ";
                questText += collectTrial[(int)Random.Range(0, collectTrial.Length)];
                questText += "their thing";
            break;

            case(2): //Collect Trial
                questText += collectTrial[(int)Random.Range(0, collectTrial.Length)];
                questText += "their thing";
            break;
        }
        questText = questText.Substring(0, 1).ToUpper() + questText.Substring(1, questText.Length-1);

        return new Quest(questText, questType, enemyType, enemyCount);
        
    }
}
