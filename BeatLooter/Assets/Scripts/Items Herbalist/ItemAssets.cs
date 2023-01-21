using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    public Transform ItemWorld;

    public Sprite TomatoeSeed;
    public Sprite Tomatoe;
    public Sprite PotatoeSeed;
    public Sprite Potatoe;
    public Sprite Beetroot;
    public Sprite BeetrootSeed;
    public Sprite HeadacheMixture;
    public Sprite PotatoeMixture;
}
