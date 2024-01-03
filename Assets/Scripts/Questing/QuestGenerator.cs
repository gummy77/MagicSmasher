using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGenerator : MonoBehaviour
{
    private StoryGenerator storyGenerator;

    [SerializeField] private GameObject questPaperPrefab;

    [SerializeField] private Transform questCanvas;
    

    void Start()
    {
        storyGenerator = GetComponent<StoryGenerator>();

        for (int i = 0; i <= 4; i++) {
            Vector3 pos = new Vector3(((1.8f / 4) * i) - 0.9f, Random.Range(-0.3f, 0.3f), -i * 0.01f); 

            GameObject paper = Instantiate (questPaperPrefab, questCanvas.position + pos, Quaternion.identity, questCanvas);
            paper.GetComponent<QuestBoardPaper>().initPaper(storyGenerator.generateQuest());
        }
    }
}
