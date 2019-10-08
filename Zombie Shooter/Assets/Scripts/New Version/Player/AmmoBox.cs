﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    private int clipsInBox;

    // Start is called before the first frame update
    void Start()
    {
        clipsInBox = Random.Range(1,6);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GunV2 gun = collision.gameObject.GetComponentInChildren<GunV2>();
            for (int x = 0; x < clipsInBox; x++)
            {
                gun.AddClip(Random.Range(Mathf.Clamp(GunV2.maxClipSize - 5,0, GunV2.maxClipSize), GunV2.maxClipSize));
            }
            Destroy(this.gameObject);
        }
    }
}