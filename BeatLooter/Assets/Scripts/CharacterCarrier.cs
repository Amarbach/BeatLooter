using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCarrier : MonoBehaviour
{
    private Character character = new(new Attributes(100, 100, 10, 10));
    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(transform.gameObject);
    }

    public Character GetCharacter() => character;
}
