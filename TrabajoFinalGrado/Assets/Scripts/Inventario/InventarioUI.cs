using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventarioUI : Singleton<InventarioUI>
{
    [Header("Panel Inventario Descripcion")]
    [SerializeField] private GameObject panelInvDescripcion;
    [SerializeField] private Image itemIcono;
    [SerializeField] private TextMeshProUGUI itemNombre;
    [SerializeField] private TextMeshProUGUI itemDescripcion;
    
    [SerializeField] private InventarioSlot slotPrefab;
    [SerializeField] private Transform contenedor;

    public int IndexSlotInicialPorMover { get; private set; }
    public InventarioSlot SlotSeleccionado { get; private set; }
    private List<InventarioSlot> slotsDisponibles = new List<InventarioSlot>();

    void Start()
    {
        InicializarInventario();
        IndexSlotInicialPorMover = -1;
    }

    private void Update()
    {
        ActualizarSlotSeleccionado();
        if (Input.GetKeyDown(KeyCode.M))
        {
            if(SlotSeleccionado != null)
            {
                IndexSlotInicialPorMover = SlotSeleccionado.Index;
            }
        }
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


    private void ActualizarSlotSeleccionado()
    {
        GameObject goSeleccionado = EventSystem.current.currentSelectedGameObject;
        if (goSeleccionado == null)
        {
            return;
        }
        
        InventarioSlot slot = goSeleccionado.GetComponent<InventarioSlot>();
        if (slot != null)
        {
            SlotSeleccionado = slot;
        }
    }

    public void ItemEnInventario(InventarioItem itemAñadir, int cantidad, int index)
    {
        InventarioSlot slot = slotsDisponibles[index];
        if (itemAñadir != null)
        {
            slot.ActivarSlotUI(true);
            slot.ActualizarSlot(itemAñadir, cantidad);
        }
        else
        {
            slot.ActivarSlotUI(false);
        }
    }

    private void ActualizarDescripcion(int index)
    {
        if(Inventario.Instance.ItemsInventario[index] != null)
        {
            itemIcono.sprite = Inventario.Instance.ItemsInventario[index].Icono;
            itemNombre.text = Inventario.Instance.ItemsInventario[index].Nombre;
            itemDescripcion.text = Inventario.Instance.ItemsInventario[index].Descripcion;
            panelInvDescripcion.SetActive(true);
        }
        else
        {
            panelInvDescripcion.SetActive(false);
        }
       
    }

    public void UsarItem()
    {
        if(SlotSeleccionado != null)
        {
            SlotSeleccionado.SlotUsarItem();
            SlotSeleccionado.SeleccionarSlot();
        }
    }
    
    #region Evento

        private void SlotInteraccionResp(TipoDeInteraccion Tipo, int index)
        {
            if (Tipo == TipoDeInteraccion.Click)
            {
                ActualizarDescripcion(index);
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
