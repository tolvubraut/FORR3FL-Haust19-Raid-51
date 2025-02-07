﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    //range til að sjá hluti
    public float Range;

    private Camera cam;
    private Inventory INV;
    private GunController Gun;
    private Interface IF;
    private SwitchSceneManager SSM;

    void Start()
    {
        //Nær í componentana
        cam = GetComponentInChildren<Camera>();
        IF = GameObject.FindGameObjectWithTag("Interface").GetComponent<Interface>();
        INV = IF.gameObject.GetComponentInChildren<Inventory>();
        Gun = IF.gameObject.GetComponentInChildren<GunController>();
        SSM = GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchSceneManager>();
    }

    void Update()
    {
        //Skýtur raycast  í miðju skjás 
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //Ef það er hlutur í raycastinu
        if (Physics.Raycast(ray, out hit, Range))
        {
            //Ef það hittir hlut sem er hægt að taka upp
            if (hit.collider.tag == "Object")
            {
                Object ObjectHit = hit.collider.gameObject.GetComponent<Object>();
                if (ObjectHit.Interactable)
                {
                    IF.Interacting(0); //Sýnir texta
                    if (Input.GetMouseButtonDown(0)) //Ef spilarinn "interactar" við hlutinn (smellir á mús)
                    {
                        if (!INV.Fullinventory) //Ef inventory-ið er ekki fullt
                        {
                            INV.AddToInventory(hit.collider.GetComponent<Object>().ObjectID); //Setur hlutinn í inventory-ið
                            SSM.AllPickups.Remove(hit.collider.gameObject);
                            Destroy(hit.collider.gameObject); //Eyðir hlutinum úr veröldinni
                        }
                    }
                }
            }
            //Ef það hittir girðingu sem er hægt að brjóta
            else if (hit.collider.tag == "Breakable fence")
            {
                if (INV.CurrentItemID() == 2) //Ef hluturinn er vírklippur í höndinni á spilaranum
                {
                    IF.Interacting(4); //Sýnir texta
                    if (Input.GetMouseButtonDown(0)) //Ef spilarinn "interactar" við hlutinn (smellir á mús)
                    {
                        Breakable_fence BF = hit.collider.gameObject.GetComponent<Breakable_fence>(); //Nær í fence scriptina

                        BF.destroyed = true; //Opnar girðinguna
                    }
                }
            }
            //Ef það hittir geimveru sem er hægt að frelsa
            else if (hit.collider.tag == "Alien")
            {
                if (hit.collider.GetComponent<Alien>().Freed == false)
                {
                    IF.Interacting(5); //Sýnir texta
                    if (Input.GetMouseButtonDown(0)) //Ef spilarinn "interactar" við geimeruna (smellir á mús)
                    {
                        hit.collider.GetComponent<Alien>().Run(); //Geymveran hleypur í burtu
                    }
                }
            }
            //Ef það hittir hurð sem er hægt að opna
            else if (hit.collider.tag == "Door")
            {
                if (INV.CurrentItemID() == 4) //Ef spilarinn heldur á keycard
                {
                    IF.Interacting(1);
                    if (Input.GetMouseButtonDown(0)) //Ef spilarinn "interactar" við hurðina (smellir á mús)
                    {
                        hit.collider.GetComponent<Opendoor>().Open(); //hurðinn opnast
                    }
                }
                else
                    IF.Interacting(8);
            }
            //Ef þetta er scenedoor (skiptir um scenes)
            else if (hit.collider.tag == "SwitchSceneDoor")
            {
                SwitchSceneDoor SwitchDoor = hit.collider.gameObject.GetComponent<SwitchSceneDoor>();

                if (SwitchDoor.Locked) //En hún er læst þá er látið mann vita
                    IF.Interacting(9);
                else //Annars
                {
                    IF.Interacting(SwitchDoor.InteractionTextID);

                    if (Input.GetMouseButtonDown(0)) //Ef spilarinn "interactar" við hurðina (smellir á mús)
                    {
                        SwitchDoor.SwitchScene(); //Skiptir um scene
                    }
                }
            }
            //Ef það hittir ammo
            else if (hit.collider.tag == "Ammo")
            {
                IF.Interacting(10); //Sýnir texta
                if (Input.GetMouseButtonDown(0)) //Ef spilarinn "interactar" við ammo-ið (smellir á mús)
                {
                    if (Gun.Ammo != 10) //Ef ammoið er ekki fullt
                    {
                        Gun.Ammo += 10; //Bætir á ammo hjá spilaranum
                        if (Gun.Ammo > 10) Gun.Ammo = 10;
                        Destroy(hit.collider.gameObject); //Eyðir ammokassanum úr veröldinni
                    }
                }
            }
            //Ef það hittir ekki neitt þá er spilarinn ekki að interacta við neitt
            else
                IF.NotInteracting();
        }
        //Ef það hittir ekki neitt þá er spilarinn ekki að interacta við neitt
        else
            IF.NotInteracting();
    }
}
