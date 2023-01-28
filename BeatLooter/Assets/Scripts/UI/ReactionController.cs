using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Emotion
{
    HAPPY,
    ANNOYED,
    MIXED,
    GOOD,
    BAD
}

public class ReactionController : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite happy;
    [SerializeField] private Sprite annoyed;
    [SerializeField] private Sprite mixed;
    [SerializeField] private Sprite good;
    [SerializeField] private Sprite bad;
    private float lifetime = 0.5f;
    private float life = 0f;

    void Update()
    {
        life += Time.deltaTime;
        image.color = new Color(image.color.r, image.color.g, image.color.b, (lifetime - life)/lifetime);
        if (life >= lifetime)
        {
            gameObject.SetActive(false);
            life = 0f;
        }
    }

    public void ShowEmotion(Emotion toShow)
    {
        switch (toShow)
        {
            case Emotion.HAPPY:
                image.sprite = happy;
                break;
            case Emotion.ANNOYED:
                image.sprite = annoyed;
                break;
            case Emotion.MIXED:
                image.sprite= mixed;
                break;
            case Emotion.GOOD:
                image.sprite = good;
                break;
            case Emotion.BAD:
                image.sprite = bad;
                break;
            default:
                image.sprite = mixed;
                break;
        }
        image.gameObject.SetActive(true);
        life = 0f;
    }
}
