using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class spawnSpot : MonoBehaviour
{
    Collider[] colliders;
    public event EventHandler OnTeleport;
    [SerializeField] private SoundSO soundSO;
    [SerializeField] private spawnSpot nextSpawnSpot;
    [SerializeField] public bool active;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform TeleVisual;
    void Start()
    {
        TeleVisual.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {

        colliders = Physics.OverlapSphere(transform.position, 0f,layerMask);
        if(colliders.Length > 0 && active)
        {
            AudioSource.PlayClipAtPoint(soundSO.teleport,transform.position, 1f );

            TeleVisual.gameObject.SetActive(true);
     
            nextSpawnSpot.gameObject.SetActive(true);
            Invoke("Hide", 2f);
            colliders[0].GetComponent<tank>().spawnToNewPosition(nextSpawnSpot.transform.position,0.4f);
        }

    }
    private void Hide()
    {
        TeleVisual.gameObject.SetActive(false );
        nextSpawnSpot.gameObject.SetActive(false);
    }
}
