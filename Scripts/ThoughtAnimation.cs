using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ThoughtAnimation : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] GameObject Thought;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (Thought.GetComponent<SpriteRenderer>().color.a < 1)
        {
            StartCoroutine(coro_EnterAnimation());
        }
    }

    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.transform.position.x > transform.position.x)
        {
            StartCoroutine(coro_ExitAnimation());
        }

    }

    // Update is called once per frame
    private IEnumerator coro_EnterAnimation(){
        gameObject.SetActive(true);
        particle.Play();
        yield return new WaitForSeconds(0.2f);
        Thought.GetComponent<SpriteRenderer>().DOFade(1, 0.5f);
    }

    private IEnumerator coro_ExitAnimation(){
        particle.Play();
        Thought.GetComponent<SpriteRenderer>().DOFade(0, 0.3f);
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
    }
}
