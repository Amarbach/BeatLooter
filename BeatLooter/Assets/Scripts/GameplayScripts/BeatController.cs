using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using UnityEngine;

public class BeatController : MonoBehaviour
{
    private float beatInterval = 1f;
    private bool isRunning = true;
    private Stopwatch beatEvaluator;
    private float errorMargin = 0.15f;
    public int testBeat = 0;

    public AudioSource audioPlayer;
    public AudioClip beatSound;
    public AudioClip hitSound;

    public ActionController[] enemies = {null, null, null};

    public ActionController playerCharacter;
    // Start is called before the first frame update
    void Start()
    {
        beatEvaluator = new Stopwatch();
        StartCoroutine(ControlBeat());
        playerCharacter.SetCharacter(new Character(new Attributes(200, 200, 15, 15)));
        foreach(ActionController enemy in enemies)
        {
            enemy.SetCharacter(new Character(new Attributes(100, 100, 5, 5)));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            float eval = EvaluateHit();
            //UnityEngine.Debug.Log(eval);
            //audioPlayer.PlayOneShot(hitSound, 1.0f - eval);
            if (eval < 2f * errorMargin) playerCharacter.initiateMove(new Vector3(1, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            float eval = EvaluateHit();
            //UnityEngine.Debug.Log(eval);
            //audioPlayer.PlayOneShot(hitSound, 1.0f - eval);
            if (eval < 2f * errorMargin) playerCharacter.initiateMove(new Vector3(-1, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            float eval = EvaluateHit();
            //UnityEngine.Debug.Log(eval);
            //audioPlayer.PlayOneShot(hitSound, 1.0f - eval);
            if (eval < 2f * errorMargin) playerCharacter.initiateMove(new Vector3(0, 1, 0));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            float eval = EvaluateHit();
            //UnityEngine.Debug.Log(eval);
            //audioPlayer.PlayOneShot(hitSound, 1.0f - eval);
            if (eval < 2f * errorMargin) playerCharacter.initiateMove(new Vector3(0, -1, 0));
        }
    }

    private void OnBeat()
    {
        beatEvaluator.Restart();
        //UnityEngine.Debug.Log(testBeat);
        testBeat += 1;
        audioPlayer.PlayOneShot(beatSound, 0.4f);
        foreach(ActionController enemy in enemies)
        {
            if (Random.Range(0, 2) == 0)
            {
                //enemy.initiateMove(new Vector3(Random.Range(0, 2) * 2 - 1, 0, 0));
            } 
            else
            {
                //enemy.initiateMove(new Vector3(0, Random.Range(0, 2) * 2 - 1, 0));
            }
        }
    }

    private float EvaluateHit()
    {
        float hit = beatEvaluator.ElapsedMilliseconds;
        if (hit > beatInterval/2f * 1000f)
        {
            return (((float)beatInterval * 1000f) - hit) / ((float)beatInterval / 2f * 1000f);
        } else
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
