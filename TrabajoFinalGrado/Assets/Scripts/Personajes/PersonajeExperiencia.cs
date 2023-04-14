using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeExperiencia : MonoBehaviour
{


    [Header("Stats")] 
    [SerializeField] private PersonajeStats stats;
    
    [Header("Config")]
    [SerializeField] private int nivelMax;
    [SerializeField] private int expBase;
    [SerializeField] private int valorIncremental;

    private float expActual;
    private float expActualTemp;
    private float expSiguienteNivel;
    // Start is called before the first frame update
    void Start()
    {
        stats.Nivel = 1;
        expSiguienteNivel = expBase;
        stats.ExpRequerida = expSiguienteNivel;
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
                expActual += expObtenida;
                ActualizarNivel();
                AñadirExperiencia(expObtenida);
            }
            else
            {
                expActual += expObtenida;
                expActualTemp += expObtenida;
                if (expActualTemp == expSiguienteNivel)
                {
                    ActualizarNivel();

                }
            }
        }

        stats.ExpActual = expActual;
        ActualizarBarraExperiencia();
    }

    private void ActualizarNivel()
    {
        if (stats.Nivel < nivelMax)
        {
            stats.Nivel++;
            expActualTemp = 0f;
            expActual = 0f;
            expSiguienteNivel *= valorIncremental;
            stats.ExpRequerida = expSiguienteNivel;
        }
    }

    private void ActualizarBarraExperiencia()
    {
        UIManager.Instance.ActualizarExpPersonaje(expActual, expSiguienteNivel);
    }
}
