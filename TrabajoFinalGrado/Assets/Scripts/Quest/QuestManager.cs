using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Personaje")]
    [SerializeField] private Personaje player;
    
    
    [Header("Quests")]
    [SerializeField] private Quest[] questDisponible;

    
    [Header("Inspector Quest")]
    [SerializeField] private InspectorQuestDescripcion inspectorQuestPrefab;
    [SerializeField] private Transform inspectorQuestContenedor;

    [Header("Personaje Quest")]
    [SerializeField] private PersonajeQuestDescripcion personajeQuestPrefab;
    [SerializeField] private Transform personajeQuestContenedor;
    
    [Header("Quest Window Completado")]
    [SerializeField] private GameObject questWindow;
    [SerializeField] private TextMeshProUGUI questTitulo;
    [SerializeField] private TextMeshProUGUI questRecompensaOro;
    [SerializeField] private TextMeshProUGUI questRecompensaExp;
    [SerializeField] private TextMeshProUGUI questRecompensaCantidad;
    [SerializeField] private Image questRecompensaIcono;


    public Quest Reclamar { get; private set; }
    private void Start()
    {
        CargarQuestInspector();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            AñadirProgreso("mata10",1);
            AñadirProgreso("mata25",1);
            AñadirProgreso("mata50",1);
        }
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

    public void ReclamarRecompensa()
    {
        if(Reclamar == null)
        {
            return;
        }
        
        MonedaManager.Instance.AñadirMonedas(Reclamar.RecompensaOro);
        player.PersonajeExperiencia.AñadirExperiencia(Reclamar.RecompensaExp);
        Inventario.Instance.AñadirItem(Reclamar.RecompensaItem.item, Reclamar.RecompensaItem.cantidad);
        questWindow.SetActive(false);
        Reclamar = null;
    }
    
    public void AñadirProgreso(string questID, int cantidad)
    {
        Quest questporActualizar = QuestExiste(questID);
        questporActualizar.AñadirProgreso(cantidad);
    }
    
    private Quest QuestExiste(string questID)
    {
        for (int i = 0; i < questDisponible.Length; i++)
        {
            if (questDisponible[i].id == questID)
            {
                return questDisponible[i];
            }
        }

        return null;
    }

    private void MostrarQuestComp(Quest questComp)
    {
        questWindow.SetActive(true);
        questTitulo.text = questComp.Nombre;
        questRecompensaOro.text = questComp.RecompensaOro.ToString();
        questRecompensaExp.text = questComp.RecompensaExp.ToString();
        questRecompensaCantidad.text = questComp.RecompensaItem.cantidad.ToString();
        questRecompensaIcono.sprite = questComp.RecompensaItem.item.Icono;
    }
    
    private void QuestCompletadoResp(Quest questComp)
    {
        Reclamar = QuestExiste(questComp.id);
        if(Reclamar != null)
        {
            MostrarQuestComp(Reclamar);
        }
    }

    private void OnEnable()
    {
        Quest.EventoQuestCompletada += QuestCompletadoResp;
    }

    private void OnDisable()
    {
        Quest.EventoQuestCompletada -= QuestCompletadoResp;
    }
}
