using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientController : MonoBehaviour
{
    float curHealth = 0.0f;
    [SerializeField] float maxHealth = 5.0f;
    [SerializeField] ReactionController reaction;
    [SerializeField] Slider healthSlider;
    [SerializeField] ItemDefinition.ItemType cure = ItemDefinition.ItemType.Tomatoe;

    public void SetCure(ItemDefinition.ItemType cureType)
    {
        cure = cureType;
    }

    public ItemDefinition.ItemType Needed { get { return cure; } }

    void Start()
    {
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
}
