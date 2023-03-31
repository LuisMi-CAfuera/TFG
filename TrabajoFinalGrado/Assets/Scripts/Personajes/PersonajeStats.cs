using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Stats")]
public class PersonajeStats : ScriptableObject
{
    public float Daño;
    public float Defensa;
    public float Velocidad;
    public float Nivel;
    public float ExpActual;
    public float ExpRequerida;
    [Range(0, 100)] public float PorcentajeCritico;
    [Range(0, 100)] public float PorcentajeBloqueo;

}
