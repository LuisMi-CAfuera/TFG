using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventario : Singleton<Inventario>
{
    
    [Header("Items")] 
    [SerializeField] public InventarioItem[] itemsInventario;
    [SerializeField] private Personaje personaje;
    [SerializeField] private int numero_slots;
    
    
    public Personaje Personaje => personaje;
    public int NumeroSlots => numero_slots;
    public InventarioItem[] ItemsInventario => itemsInventario;
    



    private void Start()
    {
        itemsInventario = new InventarioItem[numero_slots];
    }

    public void AñadirItem(InventarioItem itemAñadir, int cantidad)
    {
        if (itemAñadir == null)
        {
            return;
        }   
        //Verificacion por si acaso el item ya existe en el inventario
        List<int> indexes = VerificarExistencias(itemAñadir.ID);

        if (itemAñadir.EsAcumulable)
        {
            if (indexes.Count > 0)
            {
                for (int i = 0; i < indexes.Count; i++)
                {
                    if (itemsInventario[indexes[i]].Cantidad < itemAñadir.AcumulacionMax)
                    {
                        itemsInventario[indexes[i]].Cantidad += cantidad;
                        if (itemsInventario[indexes[i]].Cantidad > itemAñadir.AcumulacionMax)
                        {
                            int diferencia = itemsInventario[indexes[i]].Cantidad - itemAñadir.AcumulacionMax;
                            itemsInventario[indexes[i]].Cantidad = itemAñadir.AcumulacionMax;
                            AñadirItem(itemAñadir, diferencia);
                        }
                        
                        InventarioUI.Instance.ItemEnInventario(itemAñadir, itemsInventario[indexes[i]].Cantidad, indexes[i]);
                        return;
                    }
                }
            }
        }
        
        if(cantidad <= 0)
        {
            return;
        }

        if (cantidad > itemAñadir.AcumulacionMax)
        {
            AñadirItemEnSlotDisponible(itemAñadir, itemAñadir.AcumulacionMax);
            cantidad -= itemAñadir.AcumulacionMax;
            AñadirItem(itemAñadir, cantidad);
        }
        else
        {
            AñadirItemEnSlotDisponible(itemAñadir, cantidad);
        }
        
    }

    private List<int> VerificarExistencias(string itemID)
    {
        List<int> indexesDelItem = new List<int>();
        for(int i = 0; i < itemsInventario.Length; i++)
        {
            
            if(itemsInventario[i] != null)
            {
                if (itemsInventario[i].ID == itemID)
                {
                    indexesDelItem.Add(i);
                }
            }
          
        }
        
        return indexesDelItem;
    }

    private void AñadirItemEnSlotDisponible(InventarioItem item, int cantidad)
    {
        for(int i = 0; i < itemsInventario.Length; i++)
        {
            if (itemsInventario[i] == null)
            {
                itemsInventario[i] = item.CopiarItem();
                itemsInventario[i].Cantidad = cantidad;
                InventarioUI.Instance.ItemEnInventario(item, cantidad, i);
                return;
            }
        }
    }


    private void EliminarItem(int index)
    {
        itemsInventario[index].Cantidad--;
        
        if(itemsInventario[index].Cantidad <= 0)
        {
            itemsInventario[index].Cantidad = 0;
            itemsInventario[index] = null;
            InventarioUI.Instance.ItemEnInventario(null, 0, index);
        }
        else
        {
            InventarioUI.Instance.ItemEnInventario(itemsInventario[index], itemsInventario[index].Cantidad, index);
        }
    }

    public void MoverItem(int index, int nuevoIndex)
    {
        if(itemsInventario[index] == null || itemsInventario[nuevoIndex] != null)
        {
            return;
        }
        
        // Copiar el item en el nuevo slot
        InventarioItem itemPorMover = itemsInventario[index].CopiarItem();
        itemsInventario[nuevoIndex] = itemPorMover;
        InventarioUI.Instance.ItemEnInventario(itemPorMover, itemPorMover.Cantidad, nuevoIndex);
        
        // Eliminar el item del slot anterior
        itemsInventario[index] = null;
        InventarioUI.Instance.ItemEnInventario(null, 0, index); 
    }
    
    private void UsarItem(int index)
    {
        if(itemsInventario[index] == null)
        {
            return;
        }

        if (itemsInventario[index].UsarItem())
        {
            EliminarItem(index);
        }

    }

    #region Eventos

    private void SlotInteraccionResp(TipoDeInteraccion tipo, int index)
    {
        switch (tipo)
        {
            case TipoDeInteraccion.Usar:
                UsarItem(index);
                break;
            case TipoDeInteraccion.Equipar:
                break;
            case TipoDeInteraccion.Borrar:
                break;
        }
    }
    
    private void OnEnable()
    {
        InventarioSlot.EventoSlotInteraccion += SlotInteraccionResp;
    }

    private void OnDisable()
    {
        InventarioSlot.EventoSlotInteraccion -= SlotInteraccionResp;
    }

    #endregion
}
