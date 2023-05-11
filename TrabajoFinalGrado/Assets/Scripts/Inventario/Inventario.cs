using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : Singleton<Inventario>
{
    [SerializeField] private int numero_slots;
    public int NumeroSlots => numero_slots;

    [Header("Items")] [SerializeField] private InventarioItem[] itemsInventario;


    private void Start()
    {
        itemsInventario = new InventarioItem[numero_slots];
    }
}
