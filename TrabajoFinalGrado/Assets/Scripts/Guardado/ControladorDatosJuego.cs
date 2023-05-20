using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ControladorDatosJuego : MonoBehaviour
{
    public GameObject jugador;

    public string archivhoGuardado;
    public DatosJuego datosJuego = new DatosJuego();

    private void Awake()
    {
        archivhoGuardado = Application.dataPath + "/datosJuego.json";
        
        jugador  = GameObject.FindGameObjectWithTag("Player");
    }



    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            CargarDatos();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            GuardarDatos();
        }
    }


    private void CargarDatos()
    {
        if (File.Exists(archivhoGuardado))
        {
            string contenido = File.ReadAllText(archivhoGuardado);
            datosJuego = JsonUtility.FromJson<DatosJuego>(contenido);
            
            Debug.Log("Posicion Jugador: " + datosJuego.posicion);
            
            jugador.transform.position = datosJuego.posicion;
            //vida
            jugador.GetComponent<VidaBase>().Salud = datosJuego.vida;
            UIManager.Instance.ActualizarVidaPersonaje(datosJuego.vida, jugador.GetComponent<VidaBase>().vida_max);
            //mana
            jugador.GetComponent<PersonajeMana>().ManaActual = datosJuego.mana;
            UIManager.Instance.ActualizarManaPersonaje(datosJuego.mana, jugador.GetComponent<PersonajeMana>().manaMaximo);
            //experiencia
            jugador.GetComponent<PersonajeExperiencia>().expActual = datosJuego.experiencia;
            UIManager.Instance.ActualizarExpPersonaje(datosJuego.experiencia, jugador.GetComponent<PersonajeExperiencia>().expSiguienteNivel);
        }
        else
        {
            Debug.Log("No se encontro el archivo");
        }
    }
    
    private void GuardarDatos()
    {
        DatosJuego nuevoDatosJuego = new DatosJuego()
        {
            posicion = jugador.transform.position,
            vida = jugador.GetComponent<VidaBase>().Salud,
            mana = jugador.GetComponent<PersonajeMana>().ManaActual,
            experiencia = jugador.GetComponent<PersonajeExperiencia>().expActual,
        };
        
        string cadenaJson = JsonUtility.ToJson(nuevoDatosJuego);
        
        File.WriteAllText(archivhoGuardado, cadenaJson);
        
        Debug.Log("Datos guardados");
    }
}
