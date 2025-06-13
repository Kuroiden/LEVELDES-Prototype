using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    public GameManager gameManager;

    public Text task;
    public Text prompt;
    public Text dialogue;
    public Text notes;

    //public int setTaskID = 0;
    public int setObjID = 0;
    bool[] isChecked = new bool[8];
    public bool isOptional = false;
    public int cluesCleared = 0;

    public float dialogueTimer;
    void Update()
    {
        dialogueTimer -= Time.deltaTime;
        if (dialogueTimer < 0)
        {
            dialogue.text = "";
            dialogueTimer = 0;
        }

        if (cluesCleared > 6)
        {
            task.text = "Investigation complete.";
            notes.text = notes.text + "\n\nInvestigation complete\nSuspect: Matt";
        }
    }

    void OnTriggerEnter()
    {
        if (setObjID == 0 && !isChecked[0]) {
            dialogue.text = "No sign of struggle here. Did the victim know the killer?";
            dialogueTimer = 7.0f;

            isChecked[0] = true;
            notes.text = "Door showed no signs of forced entry.";
            //gameManager.taskID++;
        }
        else if (!isChecked[setObjID]) prompt.text = "[F] Inspect";
    }

    void OnTriggerStay()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isChecked[setObjID]) dialogue.text = "I should check elsewhere...";
            else {
                isChecked[setObjID] = true;

                switch (setObjID)
                {
                    case 1:
                        dialogue.text = "Still wet... not from the water. Killer didn\'t even wipe it. Panic?";
                        dialogueTimer = 10.0f;

                        notes.text = notes.text + "\nA bloody knife was found next to the toilet. Must have been dropped in a panic. Bathroom is covered in blood overall. Victim\'s body is in the tub with bloody water.";

                        cluesCleared += 1;
                        break;

                    case 2:
                        dialogue.text = "Who the hell is Matt?";
                        dialogueTimer = 5.0f;

                        notes.text = notes.text + "\nA smartphone was found in the kitchen, still showing a heated chat conversation with \"Matt\".\n\t(Latest message: \"Fine. Come over. But this is the last time.\")";

                        cluesCleared += 1;
                        break;

                    case 3:
                        dialogue.text = "She wasn\'t alone. But only one person walked out.";
                        dialogueTimer = 7.0f;

                        notes.text = notes.text + "\n2 wine glasses are on the coffee table, one nearly empty. Victim wasn\'t alone in the day of the incident.";

                        cluesCleared += 1;
                        break;

                    case 4:
                        notes.text = notes.text + "\nA broken wrist watch was found next to the coffee table. It appears to have stopped at 9:47. Possible time of a struggle.";

                        cluesCleared += 1;
                        break;

                    case 5:
                        notes.text = notes.text + "\nA broken picture frame was found in the bedroom, depicting a couple, Jen and Matt.\n\t(Picture caption: \"Matt + Jen - Summer 2023\")";

                        cluesCleared += 1;
                        break;

                    case 6:
                        notes.text = notes.text + "\nAn open journal was found on a desk in the bedroom.\n\t(Latest entry: \"Matt\'s been different lately...\")";

                        cluesCleared += 1;
                        break;

                    case 7:
                        notes.text = notes.text + "\nThe bedroom window was left opened, with blood on it. Perpetrator must have used it to escape. Floor and walls leading to the room has the same blood.";

                        cluesCleared += 1;
                        break;
                }
            }
        }
    }

    void OnTriggerExit()
    {
        prompt.text = "";
    }
}
