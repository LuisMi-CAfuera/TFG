using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Quest : ScriptableObject
{
    [Header("Info")]
    public string Nombre;
    public string id;
    public int CantidadObjetivo;
    
    [Header("Descripcion")]
    [TextArea] public string Descripcion;
    
    
    [Header("Recompensa")]
    public int RecompensaOro;
    public float RecompensaExp;
    public RecompensaItem RecompensaItem;


    [HideInInspector] public int CantidadActual;
    [HideInInspector] public bool Completada;
}

[Serializable]
public class RecompensaItem
{
    public InventarioItem item;
    public int cantidad;
}
