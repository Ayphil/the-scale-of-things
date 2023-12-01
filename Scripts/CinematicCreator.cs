using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class CinematicCreator : MonoBehaviour
{
    [SerializeField] DialogueChannel dialogueChannel;
    [SerializeField] List<Dialogue> dialogues = new List<Dialogue>();
    [SerializeField] PlayerMovement movement;

    [Space(15)]

    [SerializeField] GameObject Player;
    [SerializeField] GameObject Thought;
    [SerializeField] GameObject Credits;

    [SerializeField] Sprite PlayerBack;
    [SerializeField] Sprite ThoughtBack;

    
    private SceneChange sceneManager = null;

    private int m_dialogueIndex = 0;

    private void OnEnable()
    {
        sceneManager = FindObjectOfType<SceneChange>();
        CinematicManager.OnGameStart += OnGameStart;
    }
    private void Start()
    {
        movement.canMove = false;
        StartCoroutine(coro_OnGameStart());
    }

    private void OnGameStart()
    {
        StartCoroutine(coro_OnGameStart());
    }
    private IEnumerator coro_OnGameStart()
    {
        CinematicManager.OnGameStart -= OnGameStart;
        dialogueChannel.OnDialogueEnd += OnDialogueEnd;
        yield return new WaitForSeconds(2.5f);
        movement.canMove = true;
        dialogueChannel.RaiseRequestDialogue(dialogues[m_dialogueIndex]);
    }

    private void OnDialogueEnd(Dialogue dialogue)
    {
        Debug.Log("rte");
        
        if (dialogue == dialogues[0])
        {
            sceneManager.OnChangeScene();
        }
        else if(dialogue == dialogues[1]){
            StartCoroutine(coro_OnGameEnd());
        }
    }

    private IEnumerator coro_OnGameEnd(){
        movement.canMove = false;
        yield return new WaitForSeconds(0.6f);
        Player.GetComponent<SpriteRenderer>().sprite = PlayerBack;
        Thought.GetComponent<SpriteRenderer>().sprite = ThoughtBack;
        yield return new WaitForSeconds(2f);
        Credits.transform.GetChild(0).GetComponent<TMP_Text>().DOFade(1, 0.8f);
        yield return new WaitForSeconds(0.6f);
        Credits.transform.GetChild(1).GetComponent<TMP_Text>().DOFade(1, 0.8f);
    }

}
