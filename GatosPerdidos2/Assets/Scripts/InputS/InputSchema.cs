using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "playerx",menuName = "InputSchema")]
public class InputSchema : ScriptableObject
{
    public string axis;

    public KeyCode[] jump;
    public KeyCode[] meow;

    public bool IsJumping()
    {
        foreach (KeyCode button in jump)
        {
            if(Input.GetKeyDown(button))
            {
                return true;
            }
        }
        return false;
    }

    public bool IsMeowning()
    {
        foreach (KeyCode button in meow)
        {
            if (Input.GetKeyDown(button))
            {
                return true;
            }
        }
        return false;
    }
}
