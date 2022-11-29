using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPressed : MonoBehaviour
{
    SpriteRenderer sr;
    [SerializeField] private Color changeToDefault;
    [SerializeField] private Color colorPressed;
    // Start is called before the first frame update
    void Start()
    {
       sr = GetComponent<SpriteRenderer>();
       sr.color = changeToDefault;
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.anyKeyDown)
        {
            if (sr.color == changeToDefault)
            {
                sr.color = colorPressed;
            }
            else
            {
                sr.color = changeToDefault;
            }
        }
    }
}
