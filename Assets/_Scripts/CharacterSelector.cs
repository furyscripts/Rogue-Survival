using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public static CharacterSelector instance;
    public CharacterSO characterSO;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("EXTRA:::: " + this + ":::: DELETED");
            Destroy(gameObject);
        }
    }

    public static CharacterSO GetData()
    {
        return instance.characterSO;
    }

    public void SelectCharacter(CharacterSO character)
    {
        characterSO = character;
    }

    public void DestroySingleton()
    {
        instance = null;
        Destroy(gameObject);
    }
}
