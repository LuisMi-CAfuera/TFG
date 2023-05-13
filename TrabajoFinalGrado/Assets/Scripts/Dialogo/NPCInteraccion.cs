using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraccion : MonoBehaviour
{
   [SerializeField] private GameObject NPCBotonInteraccion;


   private void OnTriggerEnter2D(Collider2D other)
   {
      if(other.CompareTag("Player"))
      {
         NPCBotonInteraccion.SetActive(true);
      }
   }
   
   private void OnTriggerExit2D(Collider2D other)
   {
      if(other.CompareTag("Player"))
      {
         NPCBotonInteraccion.SetActive(false);
      }
   }
}
