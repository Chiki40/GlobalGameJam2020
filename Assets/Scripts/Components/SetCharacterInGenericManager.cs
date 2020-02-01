using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetCharacterInGenericManager : MonoBehaviour
{
    public RuntimeAnimatorController animatorToPLayInCharacter;
    public Sprite spriteToRenderInCharacter;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        if (target.GetComponent<Animator>() == null)
            target.AddComponent<Animator>();
        target.GetComponent<Animator>().runtimeAnimatorController = animatorToPLayInCharacter;
        target.GetComponent<Image>().sprite = spriteToRenderInCharacter;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
