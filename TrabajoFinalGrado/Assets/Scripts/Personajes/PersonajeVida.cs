using System;
using UnityEngine;


public class PersonajeVida : VidaBase
{
    public static Action EventoPersonajeDerrotado;

    public bool Derrotado { get; private set; }
    public bool PuedeSerCurado => Salud < vida_max;

    private BoxCollider2D _boxCollider2D;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

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
            
        if (Input.GetKeyDown(KeyCode.Y))
        {
            RestaurarVida(10);
        } 
    }


    public void RestaurarVida(float cantidad)
    {
        if (Derrotado)
        {
            return;
        }
        if (PuedeSerCurado)
        {
            Salud += cantidad;
            if (Salud > vida_max)
            { 
                Salud = vida_max;
            }
                
            ActualizarVida(Salud, vida_inicial);
        }
    }

    protected override void Morir()
    {
        _boxCollider2D.enabled = false;
        Derrotado = true;
        if (EventoPersonajeDerrotado != null)
        {
            EventoPersonajeDerrotado.Invoke();
        }
    }

    public void RestaurarPersonaje()
    {
        _boxCollider2D.enabled = true;
        Derrotado = false;
        Salud = vida_inicial;
        ActualizarVida(Salud,vida_max);
    }

    protected override void ActualizarVida(float vida_actual, float vida_maxima)
    {
            UIManager.Instance.ActualizarVidaPersonaje(vida_actual,vida_maxima);
    }
}
