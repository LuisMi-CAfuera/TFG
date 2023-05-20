using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class InspectorQuestDescripcion : QuestDescripcion
{

   [SerializeField] private TextMeshProUGUI questRecompensa;
   public override void ConfigurarQuestUI(Quest quest)
   {
      base.ConfigurarQuestUI(quest);
      questCargado = quest;
      questRecompensa.text = $"-Oro {quest.RecompensaOro}" +
                             $"\n-Exp {quest.RecompensaExp}" +
                             $"\n-{quest.RecompensaItem.item.Nombre} x {quest.RecompensaItem.cantidad} ";
   }

   public void AceptarQuest()
   {
       if (questCargado == null)
       {
           return;
       }
       QuestManager.Instance.AñadirQuest(questCargado);
       gameObject.SetActive(false);
   }
}
