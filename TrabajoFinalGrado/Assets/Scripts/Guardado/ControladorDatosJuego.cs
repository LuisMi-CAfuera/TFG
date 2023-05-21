using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Linq;

public class ControladorDatosJuego : Singleton<ControladorDatosJuego>
{
    public GameObject jugador;

    public string archivhoGuardado;
    public DatosJuego datosJuego = new DatosJuego();
    public List<InventarioItem> itemsGuardados = new List<InventarioItem>();

    protected override void Awake()
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
            //nivel
            jugador.GetComponent<Personaje>().stats.Nivel = datosJuego.nivel;
            //expActual
            jugador.GetComponent<Personaje>().stats.ExpActual = datosJuego.expActual;
            //expRequerida
            jugador.GetComponent<Personaje>().stats.ExpRequerida = datosJuego.expRequerida;
            //fuerza
            jugador.GetComponent<Personaje>().stats.Fuerza = datosJuego.fuerza;
            //inteligencia
            jugador.GetComponent<Personaje>().stats.Inteligencia = datosJuego.inteligencia;
            //destreza
            jugador.GetComponent<Personaje>().stats.Destreza = datosJuego.destreza;
            //puntosDisponibles
            jugador.GetComponent<Personaje>().stats.PuntosDisponibles = datosJuego.puntosDisponibles;
            //daño
            jugador.GetComponent<Personaje>().stats.Daño = datosJuego.daño;
            //defensa
            jugador.GetComponent<Personaje>().stats.Defensa = datosJuego.defensa;
            //velocidad
            jugador.GetComponent<Personaje>().stats.Velocidad = datosJuego.velocidad;
            //porcentajeCritico
            jugador.GetComponent<Personaje>().stats.PorcentajeCritico = datosJuego.porcentajeCritico;
            //porcentajeBloqueo
            jugador.GetComponent<Personaje>().stats.PorcentajeBloqueo = datosJuego.porcentajeBloqueo;
            UIManager.Instance.ActualizarPanelStats();
            //inventario
            
            Inventario.Instance.LimpiarInventario(); // Limpia el inventario antes de cargar los datos

            
            if (datosJuego.items != null)
            {
                foreach (InventarioItem item in datosJuego.items)
                {
                    
                    if (item != null)
                    {
                        
                        Debug.Log("Inventario cargado:"+item.Nombre+" Cantidad: "+item.Cantidad);
                        Inventario.Instance.AñadirItem(item, item.Cantidad);
                    }
                }
                
            }

        }
        else
        {
            Debug.Log("No se encontro el archivo");
        }
    }
    
    public void AgregarObjetoGuardado(InventarioItem objeto,int cantidad)
    {
        Debug.Log("Objeto: " + objeto.Nombre + " Cantidad: " + objeto.Cantidad);
        itemsGuardados.Add(objeto);
    }
    
    private void GuardarDatos()
    {
        if (Inventario.Instance == null)
        {
            Debug.LogError("No se ha encontrado la instancia de Inventario.");
            return;
        }

        
        DatosJuego nuevoDatosJuego = new DatosJuego()
        {
            posicion = jugador.transform.position,
            vida = jugador.GetComponent<VidaBase>().Salud,
            mana = jugador.GetComponent<PersonajeMana>().ManaActual,
            experiencia = jugador.GetComponent<PersonajeExperiencia>().expActual,
            //nivel
            nivel = jugador.GetComponent<Personaje>().stats.Nivel,
            expActual = jugador.GetComponent<Personaje>().stats.ExpActual,
            expRequerida = jugador.GetComponent<Personaje>().stats.ExpRequerida,
            //atributos
            fuerza = jugador.GetComponent<Personaje>().stats.Fuerza,
            inteligencia = jugador.GetComponent<Personaje>().stats.Inteligencia,
            destreza = jugador.GetComponent<Personaje>().stats.Destreza,
            //puntos disponibles
            puntosDisponibles = jugador.GetComponent<Personaje>().stats.PuntosDisponibles,
            //stats
            daño = jugador.GetComponent<Personaje>().stats.Daño,
            defensa = jugador.GetComponent<Personaje>().stats.Defensa,
            velocidad = jugador.GetComponent<Personaje>().stats.Velocidad,
            porcentajeCritico = jugador.GetComponent<Personaje>().stats.PorcentajeCritico,
            porcentajeBloqueo = jugador.GetComponent<Personaje>().stats.PorcentajeBloqueo,
            //Inventairio
            items = itemsGuardados.ToArray(),
            
            


        };
        
        
    
        
        string cadenaJson = JsonUtility.ToJson(nuevoDatosJuego);
        
        File.WriteAllText(archivhoGuardado, cadenaJson);
        
        Debug.Log("Datos guardados");
    }
    

}
