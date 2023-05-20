using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDescripcion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titulo;
    [SerializeField] private TextMeshProUGUI descripcion;

    public Quest questCargado { get; set; }
   public virtual void ConfigurarQuestUI(Quest quest)
   {
       titulo.text = quest.Nombre;
      descripcion.text = quest.Descripcion;
   }
}
