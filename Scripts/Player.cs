using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Collider2D _collider;
    private SceneChange sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider2D>();
        sceneManager = FindFirstObjectByType<SceneChange>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject colObjet = collision.collider.gameObject;

        if(colObjet.tag == "Exit")
        {
            sceneManager.OnChangeScene();
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
