using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager Instance;
    public ObjectBehaviourHandler currentSelectedObject;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeSelectedObject()
    {
        
    }
}
