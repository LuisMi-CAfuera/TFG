using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Quests")]
    [SerializeField] private Quest[] questDisponible;

    
    [Header("Inspector Quest")]
    [SerializeField] private InspectorQuestDescripcion inspectorQuestPrefab;
    [SerializeField] private Transform inspectorQuestContenedor;

    [Header("Personaje Quest")]
    [SerializeField] private PersonajeQuestDescripcion personajeQuestPrefab;
    [SerializeField] private Transform personajeQuestContenedor;
    private void Start()
    {
        CargarQuestInspector();
    }

    private void CargarQuestInspector()
    {
        for(int i = 0; i < questDisponible.Length; i++)
        {
           InspectorQuestDescripcion nuevoQuest = Instantiate(inspectorQuestPrefab, inspectorQuestContenedor);
           nuevoQuest.ConfigurarQuestUI(questDisponible[i]);
        }
    }

    private void AñadirQuestPorCompletar(Quest questporCompletar)
    {
        PersonajeQuestDescripcion nuevoQuest = Instantiate(personajeQuestPrefab, personajeQuestContenedor); 
        nuevoQuest.ConfigurarQuestUI(questporCompletar);
    }

    public void AñadirQuest(Quest questporCompletar)
    {
        AñadirQuestPorCompletar(questporCompletar);
    }
}
