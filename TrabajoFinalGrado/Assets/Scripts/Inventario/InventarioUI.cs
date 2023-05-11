using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioUI : MonoBehaviour
{
    
    [SerializeField] private InventarioSlot slotPrefab;
    [SerializeField] private Transform contenedor;

    private List<InventarioSlot> slotsDisponibles = new List<InventarioSlot>();

    void Start()
    {
        InicializarInventario();
    }


    private void InicializarInventario()
    {
        for (int i = 0; i < Inventario.Instance.NumeroSlots; i++)
        {
            InventarioSlot nuevoSlot = Instantiate(slotPrefab, contenedor);
            nuevoSlot.Index = i;
            slotsDisponibles.Add(nuevoSlot);
        }
    }


}