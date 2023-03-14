using System;
using UnityEngine;


public class PersonajeVida : VidaBase
{
    public static Action EventoPersonajeDerrotado;
    public bool PuedeSerCurado => Salud < vida_max;

    protected override void Start()
    {
        base.Start();
        ActualizarVida(Salud,vida_max);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            RecibirDaño(10);
        }
            
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestaurarVida(10);
        } 
    }


    public void RestaurarVida(float cantidad)
    {
        if (PuedeSerCurado)
        {
            Salud += cantidad;
            if (Salud > vida_max)
            { 
                Salud = vida_max;
            }
                
            ActualizarVida(Salud, vida_max);
        }
    }

    protected override void Morir()
    {
        if (EventoPersonajeDerrotado != null)
        {
            EventoPersonajeDerrotado.Invoke();
        }
    }

    protected override void ActualizarVida(float vida_actual, float vida_maxima)
    {
            UIManager.Instance.ActualizarVidaPersonaje(vida_actual,vida_maxima);
    }
}
