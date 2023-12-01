using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CinematicManager : MonoBehaviour
{
    public delegate void cinematicManager();
    public static event cinematicManager OnGameStart;

    private bool m_gameHasStarted =false;

    public void FixedUpdate(){
        if(!m_gameHasStarted && Input.anyKeyDown){
            onGameStart();
        }
    }
    // Update is called once per frame
    static void onGameStart()
    {
        if(OnGameStart != null){
            OnGameStart();
        }
    }
}
