﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Objectives : MonoBehaviour
{
    public int CurrentObjective;

    [Header("UI")]
    public Text Header;
    public Text Description;

    [Space]
    public int AliensRescued;
    public int MaxAliens;

    [Space]
    public Menus MenuScript;

    [System.Serializable]
    public class objctvs
    {
        [TextArea(1, 1)]
        public string ObjectiveName;
        [TextArea(1, 2)]
        public string ObjectiveDescription;
        [Space]
        public GameObject ObjectiveMarker;
    }

    [Space]
    public objctvs[] AllObjectives; //Hlutirnir


    // Start is called before the first frame update
    void Start()
    {
        AllObjectives[CurrentObjective].ObjectiveMarker.SetActive(true);
        Header.text = AllObjectives[CurrentObjective].ObjectiveName;
        Description.text = AllObjectives[CurrentObjective].ObjectiveDescription;
        print(AllObjectives.Length);
    }

    private void Update()
    {
        if(CurrentObjective == 3)
            Description.text = AllObjectives[CurrentObjective].ObjectiveDescription + " " + AliensRescued + "/" + MaxAliens;
    }

    public void ObjectiveFinished()
    {
        if (CurrentObjective +1 >= AllObjectives.Length)
            MenuScript.GameFinished();
        else
        {
            AllObjectives[CurrentObjective].ObjectiveMarker.SetActive(false);
            CurrentObjective += 1;
            AllObjectives[CurrentObjective].ObjectiveMarker.SetActive(true);
            Header.text = AllObjectives[CurrentObjective].ObjectiveName;
            Description.text = AllObjectives[CurrentObjective].ObjectiveDescription;
        }
    }
}
