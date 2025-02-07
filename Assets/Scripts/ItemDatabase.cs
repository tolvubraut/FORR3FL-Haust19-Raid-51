﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemDatabase : MonoBehaviour
{
    //Klassi fyrir hluti (items), geymir nafn, mynd og prefab fyrir hlutinn
    [System.Serializable]
    public class item
    {
        public string ObjectName;
        public Sprite ObjectIcon;
        public GameObject ObjectPrefab;
        public GameObject HandPrefab;
    }

    public item[] Items; //Hlutirnir
}
