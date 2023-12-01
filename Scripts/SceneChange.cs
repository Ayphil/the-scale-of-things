using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SceneChange : MonoBehaviour
{
    [SerializeField] List<GameObject> Scenes = new List<GameObject>();
    [SerializeField] GameObject Thought;
    public GameObject Player;
    [SerializeField] Volume FadeVignette;

    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();

    [SerializeField] float FadeLenght = 0.6f;

    private Vignette vg;
    private SmoothCameraFollow cameraScript;
    private int _sceneIndex = 0;
    private int _clipsIndex = 0;
    // Start is called before the first frame update
    private void Start()
    {
        FadeVignette.profile.TryGet(out vg);
        cameraScript = FindObjectOfType<SmoothCameraFollow>();
        ///DebugSceneChanger.onClick.AddListener(OnChangeScene);
    }

    public void OnChangeScene()
    {
        StartCoroutine(Vignette());
    }

    private IEnumerator Vignette()
    {
        StartCoroutine(Fade(FadeLenght, vg));
        if(_sceneIndex == 2)
        {
            Vector3 offset = cameraScript.offset;
            cameraScript.offset = new Vector3(offset.x, 4f, offset.z);
            StartCoroutine(MusicFade(FadeLenght));
        }
        else if(_sceneIndex == 3){
            StartCoroutine(MusicFade(FadeLenght));
        }
        yield return new WaitForSeconds(0.4f);
        FadeVignette.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        Scenes[_sceneIndex].SetActive(false);
        _sceneIndex++;
        Scenes[_sceneIndex].SetActive(true);

        cameraScript.Area = Scenes[_sceneIndex].transform;
        cameraScript.Refresh();
        Player.transform.position = Scenes[_sceneIndex].transform.GetChild(0).transform.position;
        Player.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().DOFade(1, 0.2f);

        if(_sceneIndex == 4 || _sceneIndex == 5){
            Thought.transform.localScale -= new Vector3(0.4f, 0.4f, 0.4f);
            Player.transform.localScale -= new Vector3(0.2f, 0.2f, 0.2f);
            Player.GetComponent<PlayerMovement>().Speed -= 1.5f;
            cameraScript.CameraLock(new Vector3(-0.24f, 0.54f, -1.49f));
        }

        Thought.SetActive(true);
        Color color = Thought.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        Thought.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0);
        Thought.transform.position = Scenes[_sceneIndex].transform.GetChild(1).transform.position;

        yield return new WaitForSeconds(0.2f);
        FadeVignette.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        if (_sceneIndex == 3 || _sceneIndex == 4)
        {
            StartCoroutine(MusicFade(FadeLenght));
        }
        StartCoroutine(Fade(FadeLenght, vg));
    }

    private IEnumerator MusicFade(float duration)
    {
        float time = 0;
        float targetIntensity = 0;
        float currentIntensity = audioSource.volume;
        if (currentIntensity <= 0.5f)
        {
            _clipsIndex++;
            audioSource.clip = audioClips[_clipsIndex];
            audioSource.Play();
            targetIntensity = 0.6f;
        }
        else
        {
            targetIntensity = 0f;
        }
        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(currentIntensity, targetIntensity, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }

    private static IEnumerator Fade(float duration, Vignette vg)
    {
        float time = 0;
        float targetIntensity = 0;
        float currentIntensity = vg.intensity.value;
        if (vg.intensity.value <= 0.5f)
        {
            targetIntensity = 1;
        }
        else
        {
            targetIntensity = 0;
        }
        while (time < duration)
        {
            vg.intensity.value = Mathf.Lerp(currentIntensity, targetIntensity, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
    }
}
