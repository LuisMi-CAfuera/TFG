using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeMana : MonoBehaviour
{
    
    [SerializeField] private float manaInicial;
    [SerializeField] public float manaMaximo;
    [SerializeField] private float regeneracionPorSegundo;

    public float ManaActual { get;  set; }
    
    public bool PuedeRestauar => ManaActual < manaMaximo;

    private PersonajeVida _personajeVida;
    
    private void Awake()
    {
        _personajeVida = GetComponent<PersonajeVida>();
    }
    
    private void Start()
    {
        ManaActual = manaInicial;
        ActualizarBarraMana();
        
        InvokeRepeating(nameof(RegenerarMana), 1, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            UsarMana(10f);
        }
    }


    public void UsarMana(float cantidad)
    {
        if(ManaActual >= cantidad)
        {
            ManaActual -= cantidad;
            ActualizarBarraMana();
        }
    }

    public void RestauarMana(float cantidad)
    {
        if (ManaActual >= manaMaximo)
        {
            return;
        }
        
        ManaActual += cantidad;
        if(ManaActual > manaMaximo)
        {
            ManaActual = manaMaximo;
        }
        
        UIManager.Instance.ActualizarManaPersonaje(ManaActual,manaMaximo);
    }

    private void RegenerarMana()
    {
        if(_personajeVida.Salud > 0f && ManaActual < manaMaximo)
        {
            ManaActual += regeneracionPorSegundo;
            ActualizarBarraMana();
        }
    }

    public void RestablecerMana()
    {
        ManaActual = manaInicial;
        ActualizarBarraMana();
    }

    private void ActualizarBarraMana()
    {
        UIManager.Instance.ActualizarManaPersonaje(ManaActual,manaMaximo);
    }

    
}
