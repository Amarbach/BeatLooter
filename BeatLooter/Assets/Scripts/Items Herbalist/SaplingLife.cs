using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class SaplingLife : MonoBehaviour
{
    [SerializeField]
    Sprite stage0;
    [SerializeField]
    Sprite stage1;
    [SerializeField]
    Sprite stage2;
    [SerializeField]
    Sprite stage3;
    [SerializeField]
    Sprite stage4;
    SpriteRenderer childImg;
    [SerializeField]
    ItemDefinition.ItemType type;
    void Start()
    {
        childImg = transform.Find("Img").GetComponent<SpriteRenderer>();
        StartCoroutine(LifeCycle());
    }

    IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(4f);
        childImg.sprite = stage0;
        yield return new WaitForSeconds(4f);
        childImg.sprite = stage1;
        yield return new WaitForSeconds(4f);
        childImg.sprite = stage2;
        yield return new WaitForSeconds(4f);
        childImg.sprite = stage3;
        yield return new WaitForSeconds(4f);
        childImg.sprite = stage4;
        yield return new WaitForSeconds(4f);
        ItemWorld.SpawnItemWorld(childImg.transform.position, new ItemDefinition { itemType = type, amount = 1 });
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
