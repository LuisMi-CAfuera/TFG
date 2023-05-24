using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ZonaConfinder : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camara;
//Esta clase solo sirve para activar y desactivar la camara de la zona de confinder

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            camara.gameObject.SetActive(true);
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            camara.gameObject.SetActive(false);
        }
    }
}
