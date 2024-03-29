using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;
public class DialogoManager : Singleton<DialogoManager>
{
    [SerializeField] private GameObject dialogoPanel;
    [SerializeField] private Image imagenNPC;
    [SerializeField] private TextMeshProUGUI nombreNPC;
    [SerializeField] private TextMeshProUGUI dialogoTexto;

    public NPCInteraccion NPCDisponible { get; set; }


    private Queue<string> dialogosSecuencias;
    private bool dialogoAnimado;
    private bool despediaMostar;

    private void Start()
    {
        dialogosSecuencias = new Queue<string>();
    }

    private void Update()
    {
        if (NPCDisponible == null)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ConfigurarPanel(NPCDisponible.Dialogo);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (despediaMostar)
            {
                AbrirCerrarPanelDialogo(false);
                despediaMostar = false;
                return;
            }
            
            if(NPCDisponible.Dialogo.ContieneInteraccionExtra)
            {
               UIManager.Instance.AbrirPanelInteraccion(NPCDisponible.Dialogo.interaccionExtra);
               AbrirCerrarPanelDialogo(false);
                return;
            }
            if (dialogoAnimado)
            {
                Continuar();
            }
        }
    }

    public void AbrirCerrarPanelDialogo(bool estado)
    {
        dialogoPanel.SetActive(estado);
    }

    private void ConfigurarPanel(NPCDialogo npcDialogo)
    {
        AbrirCerrarPanelDialogo(true);
        CargarDialogoSecuencia(npcDialogo);
        
        
        imagenNPC.sprite = npcDialogo.imagen;
        nombreNPC.text = $"{npcDialogo.Nombre}:";
        
        MostrarTextoAnimado(npcDialogo.Saludo);
    }

    private void CargarDialogoSecuencia(NPCDialogo npcDialogo)
    {
        if(npcDialogo.Conversacion == null || npcDialogo.Conversacion.Length <= 0)
        {
           return;
        }

        for (int i = 0; i < npcDialogo.Conversacion.Length; i++)
        {
            dialogosSecuencias.Enqueue(npcDialogo.Conversacion[i].Frases);
        }
    }

// PAra continuar el dialogo
    private void Continuar()
    {
        if (NPCDisponible == null)
        {
            return;
        }

        if (despediaMostar)
        {
            return;
        }
        
        if(dialogosSecuencias.Count == 0)
        {
            string despedida = NPCDisponible.Dialogo.Despedida;
            MostrarTextoAnimado(despedida);
            despediaMostar = true;
            return;
        }
        
        string sigDialogo = dialogosSecuencias.Dequeue();
        MostrarTextoAnimado(sigDialogo);
    }


    private IEnumerator AnimarTexto(string oracion)
    {
        dialogoAnimado = false;
        dialogoTexto.text = "";
        char[] letras = oracion.ToCharArray();
        for (int i = 0; i < letras.Length; i++)
        {
            dialogoTexto.text += letras[i];
            yield return new WaitForSeconds(0.03f);
        }
        
        dialogoAnimado = true;
    }


    private void MostrarTextoAnimado(string oracion)
    {
        StartCoroutine(AnimarTexto(oracion));
    }
}
