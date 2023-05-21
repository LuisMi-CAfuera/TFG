using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DatosJuego
{
    public Vector3 posicion;

    public float vida;
    public float mana;
    public float experiencia;
    
    //nivel 
    public float nivel;
    public float expActual;
    public float expRequerida;
    //atributos
    public float fuerza;
    public float inteligencia;
    public float destreza;
    //puntos disponibles
    public int puntosDisponibles;
    //stats
    public float daño;
    public float defensa;
    public float velocidad;
    public float porcentajeCritico;
    public float porcentajeBloqueo;
    //inventario
    public InventarioItem[] items;
    
  



}
