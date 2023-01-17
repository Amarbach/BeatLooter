using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class BeatController : MonoBehaviour
{
    private float beatInterval = 1f;
    private bool isRunning = true;
    private Stopwatch beatEvaluator;
    private Stopwatch sinceLast;
    private float errorMargin = 0.3f;
    public int testBeat = 0;
    [SerializeField] private Slider rightBar;
    [SerializeField] private Slider leftBar;

    public AudioSource audioPlayer;
    public AudioClip beatSound;
    public AudioClip hitSound;

    public ActionController[] enemies = { null, null, null };

    public ActionController playerCharacter;
    // Start is called before the first frame update
    void Start()
    {
        beatEvaluator = new Stopwatch();
        sinceLast = new Stopwatch();
        StartCoroutine(ControlBeat());
    }

    // Update is called once per frame
    void Update()
    {
        leftBar.value = 1f - ((beatEvaluator.ElapsedMilliseconds) / (beatInterval * 1000f));
        rightBar.value = 1f - ((beatEvaluator.ElapsedMilliseconds) / (beatInterval * 1000f));
        if (Input.GetKeyDown(KeyCode.D))
        {
            float eval = EvaluateHit();
            float sinceLastClick = sinceLast.ElapsedMilliseconds;
            UnityEngine.Debug.Log(eval);
            if (eval < errorMargin && sinceLastClick > 300) playerCharacter.initiateMove(new Vector3(1, 0, 0));
            sinceLast.Restart();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            float eval = EvaluateHit();
            float sinceLastClick = sinceLast.ElapsedMilliseconds;
            UnityEngine.Debug.Log(eval);
            if (eval < errorMargin && sinceLastClick > 300) playerCharacter.initiateMove(new Vector3(-1, 0, 0));
            sinceLast.Restart();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            float eval = EvaluateHit();
            float sinceLastClick = sinceLast.ElapsedMilliseconds;
            UnityEngine.Debug.Log(eval);
            if (eval < errorMargin && sinceLastClick > 300) playerCharacter.initiateMove(new Vector3(0, 1, 0));
            sinceLast.Restart();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            float eval = EvaluateHit();
            float sinceLastClick = sinceLast.ElapsedMilliseconds;
            UnityEngine.Debug.Log(eval);
            if (eval < errorMargin && sinceLastClick > 300) playerCharacter.initiateMove(new Vector3(0, -1, 0));
            sinceLast.Restart();
        }
    }

    private void OnBeat()
    {
        beatEvaluator.Restart();
        testBeat += 1;
    }

    private float EvaluateHit()
    {
        float hit = beatEvaluator.ElapsedMilliseconds;
        if (hit > beatInterval / 2f * 1000f)
        {
            return (((float)beatInterval * 1000f) - hit) / ((float)beatInterval / 2f * 1000f);
        }
        else
        {
            return hit / ((float)beatInterval / 2f * 1000f);
        }
    }

    private IEnumerator ControlBeat()
    {
        beatEvaluator.Start();
        while (isRunning)
        {
            yield return new WaitForSeconds(beatInterval);
            OnBeat();
        }
    }
}