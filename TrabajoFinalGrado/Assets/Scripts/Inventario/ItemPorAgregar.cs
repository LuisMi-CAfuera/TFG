using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPorAgregar : MonoBehaviour
{
  [SerializeField] private InventarioItem inventaririoItemRef;
  [SerializeField] private int cantidadPorAgregar;


  private void OnTriggerEnter2D(Collider2D other)
  {
    if(other.CompareTag("Player"))
    {
      Inventario.Instance.AñadirItem(inventaririoItemRef, cantidadPorAgregar);
      Destroy(gameObject);
    }
  }
}
