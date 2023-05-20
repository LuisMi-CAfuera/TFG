using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonajeQuestDescripcion : QuestDescripcion
{
    [SerializeField] private TextMeshProUGUI tareaObjetivo;
    [SerializeField] private TextMeshProUGUI recompensaOro;
    [SerializeField] private TextMeshProUGUI recompensaExp;
    
    [Header("Item")]
    [SerializeField] private Image recompensaItemIcono;
    [SerializeField] private TextMeshProUGUI recompensaItemCantidad;

    private void Update()
    {
        if (quesporcompletar.CompletadaCheck)
        {
            return;
        }
        tareaObjetivo.text = $"{quesporcompletar.CantidadActual}/{quesporcompletar.CantidadObjetivo}";
    }

    public override void ConfigurarQuestUI(Quest questPorCargar)
    {
        base.ConfigurarQuestUI(questPorCargar);
        
        recompensaOro.text = questPorCargar.RecompensaOro.ToString();
        recompensaExp.text = questPorCargar.RecompensaExp.ToString();
        tareaObjetivo.text = $"{questPorCargar.CantidadActual}/{questPorCargar.CantidadObjetivo}";
        
        recompensaItemIcono.sprite = questPorCargar.RecompensaItem.item.Icono;
    }

    
    public void QuestCompletadoRespuesta(Quest quest)
    {
        if (quest.id == quesporcompletar.id)
        {
            tareaObjetivo.text = $"{quesporcompletar.CantidadActual}/{quesporcompletar.CantidadObjetivo}";
            gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        if (quesporcompletar.CompletadaCheck)
        {
            gameObject.SetActive(false);
        }
        Quest.EventoQuestCompletada += QuestCompletadoRespuesta;
    }

    private void OnDisable()
    {
        Quest.EventoQuestCompletada -= QuestCompletadoRespuesta;
    }
}
