using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PatientController : MonoBehaviour
{
    float curHealth = 0.0f;
    [SerializeField] float maxHealth = 5.0f;
    [SerializeField] ReactionController reaction;
    [SerializeField] Slider healthSlider;
    [SerializeField] ItemDefinition.ItemType cure = ItemDefinition.ItemType.Tomatoe;
    [SerializeField] SpriteRenderer ren;
    [SerializeField] BoxCollider2D coll;
    public UnityEvent<int> Healed = new UnityEvent<int>();
    bool isFading = false;
    float fadeTime = 2f;
    float fadeProg = 0.0f;

    public void SetCure(ItemDefinition.ItemType cureType)
    {
        cure = cureType;
    }

    public ItemDefinition.ItemType Needed { get { return cure; } }

    void Start()
    {
        ScoreController score;
        if (transform.parent.transform.parent.TryGetComponent<ScoreController>(out score))
        {
            Healed.AddListener(score.AddScore);
        }
        Heal(0.0f);
    }

    public void Heal(float amount)
    {
        curHealth += amount;
        
        if (curHealth <= 0.0f)
        {
            healthSlider.gameObject.SetActive(false);
            curHealth = 0f;
        } 
        else if (curHealth < maxHealth)
        {
            healthSlider.gameObject.SetActive(true);
        } 
        else
        {
            healthSlider.gameObject.SetActive(false);
            curHealth = maxHealth;
        }
        healthSlider.value = curHealth / maxHealth;
    }

    public void React(Emotion how)
    {
        reaction.ShowEmotion(how);
    }

    private void Update()
    {
        if(curHealth >= maxHealth && !isFading)
        {
            Healed.Invoke(1);
            coll.enabled = false;
            Destroy(gameObject, fadeTime);
            isFading = true;
        }
        if (isFading)
        {
            ren.color = new Color(ren.color.r, ren.color.g, ren.color.b, (fadeTime-fadeProg)/fadeTime);
            fadeProg += Time.deltaTime;
        }
    }
}
