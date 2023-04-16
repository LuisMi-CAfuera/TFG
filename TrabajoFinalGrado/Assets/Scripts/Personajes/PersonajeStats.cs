using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats")]
public class PersonajeStats : ScriptableObject
{
    public float Daño = 5f;
    public float Defensa = 2f;
    public float Velocidad = 5f;
    public float Nivel;
    public float ExpActual;
    public float ExpRequerida;
    [Range(0, 100)] public float PorcentajeCritico;
    [Range(0, 100)] public float PorcentajeBloqueo;


    public void ResetearValores()
    {
        Daño = 5f;
        Defensa = 2f;
        Velocidad = 5f;
        Nivel = 1;
        ExpActual = 0f;
        ExpRequerida = 0f;
        PorcentajeCritico = 0f;
        PorcentajeBloqueo = 0f;
    }

}
