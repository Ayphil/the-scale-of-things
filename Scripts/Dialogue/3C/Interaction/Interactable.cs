using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField] UnityEvent m_OnCloseContact;
    [SerializeField] DialogueChannel dialogue;
    
    [SerializeField] List<Dialogue> dialogues = new List<Dialogue>();

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("2");
        DoInteraction();
    }
    public void DoInteraction()
    {
        dialogue.RaiseRequestDialogue(GetDialogue());
    }
    public Dialogue GetDialogue(){
        Dialogue dialogue = dialogues[0];
        dialogues.Remove(dialogue);
        return dialogue;
    }
}