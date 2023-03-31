using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeExperiencia : MonoBehaviour
{
    [SerializeField] private int nivelMax;
    [SerializeField] private int expBase;
    [SerializeField] private int valorIncremental;
    
    public int Nivel { get; private set; }

    private float expActual;
    private float expSiguienteNivel;
    // Start is called before the first frame update
    void Start()
    {
        Nivel = 1;
        expSiguienteNivel = expBase;
        ActualizarBarraExperiencia();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            AñadirExperiencia(2f);
        }
    }

    public void AñadirExperiencia(float expObtenida)
    {
        if (expObtenida > 0f)
        {
            float expRestanteNuevoNivel = expSiguienteNivel - expActual;
            if (expObtenida >= expRestanteNuevoNivel)
            {
                expObtenida -= expRestanteNuevoNivel;
                ActualizarNivel();
                AñadirExperiencia(expObtenida);
            }
            else
            {
                expActual += expObtenida;
                if (expActual == expSiguienteNivel)
                {
                    ActualizarNivel();
                }
            }
        }
        
        ActualizarBarraExperiencia();
    }

    private void ActualizarNivel()
    {
        if (Nivel < nivelMax)
        {
            Nivel++;
            expActual = 0f;
            expSiguienteNivel *= valorIncremental;
        }
    }

    private void ActualizarBarraExperiencia()
    {
        UIManager.Instance.ActualizarExpPersonaje(expActual, expSiguienteNivel);
    }
}
