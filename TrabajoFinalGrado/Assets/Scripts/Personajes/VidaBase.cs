using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaBase : MonoBehaviour
{
    [SerializeField] protected float vida_inicial;
    [SerializeField] public float vida_max;
    
    public float Salud { get;  set; } 
    protected virtual void Start()
    {
        Salud=vida_inicial;
    }

    public void RecibirDaño(float daño)
    {
        if (daño <= 0)
        {
            return;
        }

        if(Salud > 0f)
        {
            Salud -= daño;
            ActualizarVida(Salud, vida_max);
            if (Salud <= 0f)
            {
                ActualizarVida(Salud, vida_max);
                Morir();
            }
        }
    }
    
    protected virtual void ActualizarVida(float vida_actual,float vida_maxima)
    {
        
    }
    
    protected virtual void Morir()
    {
        
    }

}
