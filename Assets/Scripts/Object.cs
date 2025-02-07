﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    //Geymir nafn og ID svo inventory-ið veit hvaða hlut spilarinn tók upp
    public string ObjectName;
    public int ObjectID;
    public bool Interactable = true;

    //Values til að vista hluti á milli scene-a
    [Space]
    [HideInInspector]
    public int SceneNumber;
    [HideInInspector]
    public bool Initialized;
}
