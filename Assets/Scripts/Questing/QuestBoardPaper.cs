using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestBoardPaper : MonoBehaviour
{
    [SerializeField] private Quest thisQuest;

    [SerializeField] TMP_Text questText;

    public void initPaper(Quest quest) {
        thisQuest = quest;
        setupPaper();
    }

    private void setupPaper(){
        questText.text = thisQuest.getText();
    }

    public Quest pickup() {
        Destroy(gameObject, 0.05f);
        return thisQuest;
    }
}
