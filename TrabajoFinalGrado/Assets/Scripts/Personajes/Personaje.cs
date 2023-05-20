using System;
using UnityEngine;

public class Personaje:MonoBehaviour
{

        [SerializeField] public PersonajeStats stats;
        
        public PersonajeExperiencia PersonajeExperiencia { get; set; }
        public PersonajeVida PersonajeVida { get; set; }
        public PersonajeAnimaciones PersonajeAnimaciones { get;  set; }
        public PersonajeMana PersonajeMana { get; set; }

        private void Awake()
        {
                PersonajeVida = GetComponent<PersonajeVida>();
                PersonajeAnimaciones = GetComponent<PersonajeAnimaciones>();
                PersonajeMana = GetComponent<PersonajeMana>();
                PersonajeExperiencia = GetComponent<PersonajeExperiencia>();
        }

        public void RestaurarPersonaje()
        {
                PersonajeVida.RestaurarPersonaje();
                PersonajeAnimaciones.RevivirPersonaje();
                PersonajeMana.RestablecerMana();
        }

        private void AtributoRespuesta(TipoAtributo tipo)
        {

                if (stats.PuntosDisponibles <= 0)
                {
                        return;
                }
                switch (tipo)
                {
                        case TipoAtributo.Fuerza:
                                stats.Fuerza++;
                                stats.AñadirBonusPorAtributoFuerza();
                                break;
                        case TipoAtributo.Inteligencia:
                                stats.Inteligencia++;
                                stats.AñadirBonusPorAtributoInteligencia();
                                break;
                        case TipoAtributo.Destreza:
                                stats.Destreza++;
                                stats.AñadirBonusPorAtributoDestreza();
                                break;
                }    
                stats.PuntosDisponibles--;
        }

        private void OnEnable()
        {
                AtributoButton.EventoAgregarAtributo += AtributoRespuesta;
        }
        
        private void OnDisable()
        {
                AtributoButton.EventoAgregarAtributo -= AtributoRespuesta;
        }
}
