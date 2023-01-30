using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeatController : MonoBehaviour
{
    private float beatInterval = 0.8f;
    private bool isRunning = true;
    private Stopwatch beatEvaluator;
    private Stopwatch sinceLast;
    private float errorMargin = 0.3f;
    private int comboCount = 0;
    private bool wasHit = false;
    [SerializeField] private Slider rightBar;
    [SerializeField] private Slider leftBar;
    [SerializeField] private TextMeshProUGUI comboText;
    public float ComboMultiplier { get { if (comboCount < 3) return 1f; else if (comboCount < 6) return 1.5f; else return 2f; } }

    public AudioSource audioPlayer;
    public AudioClip beatSound;
    public AudioClip hitSound;

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
            //UnityEngine.Debug.Log(eval);
            if (eval < errorMargin)
            {
                Combo();
                playerCharacter.initiateMove(new Vector3(1, 0, 0), this.ComboMultiplier);
                wasHit = true;
            }
            sinceLast.Restart();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            float eval = EvaluateHit();
            //UnityEngine.Debug.Log(eval);
            if (eval < errorMargin)
            {
                Combo();
                playerCharacter.initiateMove(new Vector3(-1, 0, 0), this.ComboMultiplier);
                wasHit = true;
            }
            sinceLast.Restart();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            float eval = EvaluateHit();
            //UnityEngine.Debug.Log(eval);
            if (eval < errorMargin)
            {
                Combo();
                playerCharacter.initiateMove(new Vector3(0, 1, 0), this.ComboMultiplier);
                wasHit = true;
            }
            sinceLast.Restart();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            float eval = EvaluateHit();
            //UnityEngine.Debug.Log(eval);
            if (eval < errorMargin)
            {
                Combo();
                playerCharacter.initiateMove(new Vector3(0, -1, 0), this.ComboMultiplier);
                wasHit = true;
            }
            sinceLast.Restart();
        }
        //if (beatEvaluator.ElapsedMilliseconds > errorMargin)
        //{
        //    if (!wasHit)
        //    {
        //        MissBeat();
        //    }
        //    wasHit = false;
        //}

    }

    private void OnBeat()
    {
        beatEvaluator.Restart();
        audioPlayer.PlayOneShot(beatSound);
        StartCoroutine(CheckMiss());
    }

    private float EvaluateHit()
    {
        float hit = beatEvaluator.ElapsedMilliseconds;
        float sinceLastClick = sinceLast.ElapsedMilliseconds;
        if (sinceLastClick < beatInterval / 6f) return 1.0f;
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

    private IEnumerator CheckMiss()
    {
        yield return new WaitForSeconds(errorMargin);
        if (!wasHit) MissBeat();
        else wasHit = false;
    }

    private void MissBeat()
    {
        ResetCombo();
    }

    private void Combo()
    {
        comboCount++;
        comboText.text = "Multiplier:" + ComboMultiplier;
        if (comboCount >= 6) comboText.color = Color.red;
        else comboText.color = Color.white;
    }

    private void ResetCombo()
    {
        comboCount = 0;
        comboText.text = "Multiplier:" + ComboMultiplier;
        comboText.color = Color.white;
    }
}