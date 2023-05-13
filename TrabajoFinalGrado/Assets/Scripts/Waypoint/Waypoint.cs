using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] private Vector3[] puntos;
    public Vector3[] Puntos => puntos;

    public Vector3 PosActual { get; set; }
    private bool juegoIniciado;

    private void Start()
    {
        juegoIniciado = true;
        PosActual = transform.position;
    }

    //esta funcion es para saber la posicion del punto que se va a mover
    public Vector3 ObtenerPosMovimiento(int index)
    {
        return PosActual + puntos[index]; 
    }

    private void OnDrawGizmos()
    {
        //Sirve para actualizar su posicion actual cuando no estemos en el PlayMode
        if (juegoIniciado == false && transform.hasChanged)
        {
            PosActual = transform.position;
        }
        //Primero Hay que verificar si hay algo que dibujar o si el array no es nulo
        if(puntos == null || puntos.Length <= 0)
        {
            return;
        }

        // Este For sirve para crear las esferas que se van a dibujar tomando el indice y el radio que van a tener
        for (int i = 0; i < puntos.Length; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(puntos[i] + PosActual, 0.5f);

            if (i < puntos.Length - 1)
            {
                Gizmos.color = Color.gray;
                //Este Gizmos.DrawLine sirve para dibujar una linea entre los puntos que se van a dibujar
                Gizmos.DrawLine(puntos[i] + PosActual, puntos[i + 1] + PosActual);
            }
            
        }
    }
}
