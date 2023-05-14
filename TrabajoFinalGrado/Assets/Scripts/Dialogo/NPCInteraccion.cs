using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraccion : MonoBehaviour
{
   [SerializeField] private GameObject NPCBotonInteraccion;
   [SerializeField] private NPCDialogo dialogoNPC;
   
   public NPCDialogo Dialogo => dialogoNPC;


   private void OnTriggerEnter2D(Collider2D other)
   {
      if(other.CompareTag("Player"))
      {
         DialogoManager.Instance.NPCDisponible = this;
         NPCBotonInteraccion.SetActive(true);
      }
   }
   
   private void OnTriggerExit2D(Collider2D other)
   {
      if(other.CompareTag("Player"))
      {
         DialogoManager.Instance.NPCDisponible = null;
         NPCBotonInteraccion.SetActive(false);
      }
   }
}
