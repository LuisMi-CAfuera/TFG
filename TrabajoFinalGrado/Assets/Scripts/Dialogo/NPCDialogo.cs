using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteracccionExtra
{
    Quest,
    Tienda,
    Crafting
}

[CreateAssetMenu]
public class NPCDialogo : ScriptableObject
{
    [Header("Info")] public string Nombre;
    public Sprite imagen;
    public bool InteraccionExtra;
    public InteracccionExtra interacccionExtra;


    [Header("Saludo")] 
    [TextArea] public string Saludo;

    [Header("Conversacion")] 
    public DialogoTexto[] Conversacion;
    
    [Header("Despedida")] 
    [TextArea] public string Despedida;
}
[Serializable]
public class DialogoTexto
{
    [TextArea] public string Frases;
}