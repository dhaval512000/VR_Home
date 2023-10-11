using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Events
{
    public delegate void OnTextureResetDelegate();

    public static event OnTextureResetDelegate onTextureReset;

    public static void ResetAllTextures()
    {
        onTextureReset?.Invoke();
    }
}
