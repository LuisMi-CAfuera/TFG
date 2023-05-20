using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu]
public class Quest : ScriptableObject
{
    public static Action<Quest> EventoQuestCompletada;

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
    [HideInInspector] public bool CompletadaCheck;
    
    public  void AñadirProgreso(int cantidad)
    {
        CantidadActual += cantidad;
        VerificarQuestComp();

    }

    private void VerificarQuestComp()
    {
        if (CantidadActual >= CantidadObjetivo)
        {
            CantidadActual = CantidadObjetivo;
            QuestCompletada();
        }
    }
    
    private void QuestCompletada()
    {
        if (CompletadaCheck)
        {
            return;
        }
        
        CompletadaCheck = true;
        EventoQuestCompletada?.Invoke(this);
    }

    private void OnEnable()
    {
        CompletadaCheck = false;
        CantidadActual = 0;
    }
}

[Serializable]
public class RecompensaItem
{
    public InventarioItem item;
    public int cantidad;
}
