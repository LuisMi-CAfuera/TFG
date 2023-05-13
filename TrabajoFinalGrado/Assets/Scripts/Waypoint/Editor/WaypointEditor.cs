using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//Esta clase sirve para poder crear rutas de los NPCS sin tener que estar escribiendo los puntos a mano
[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    Waypoint WaypointTarget => target as Waypoint;

    private void OnSceneGUI()
    {
        Handles.color = Color.red;
        if (WaypointTarget.Puntos == null)
        {
            return;
        }

        for (int i = 0; i < WaypointTarget.Puntos.Length; i++)
        {
            EditorGUI.BeginChangeCheck();
            //Ahora hay que obtener la posicion actual del punto
            Vector3 puntoActual = WaypointTarget.PosActual + WaypointTarget.Puntos[i];
            //Este Handles.FreeMoveHandle sirve para mover el punto y que se guarden los cambios
            Vector3 nuevoPunto = Handles.FreeMoveHandle(puntoActual, Quaternion.identity, 0.7f,new Vector3(0.3f,0.3f,0.3f),Handles.SphereHandleCap);
            
            GUIStyle texto = new GUIStyle();
            texto.fontStyle = FontStyle.Bold;
            texto.fontSize = 16;
            texto.normal.textColor = Color.black;
            //Esto es para Crear y posiconar el texto abajo a la derecha de la esfera
            Vector3 alineamiento = Vector3.down * 0.3f + Vector3.right * 0.3f;
            Handles.Label(WaypointTarget.PosActual + WaypointTarget.Puntos[i] + alineamiento, 
                $"{i + 1}",texto);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target,"Cambio en el punto");
                WaypointTarget.Puntos[i] = nuevoPunto - WaypointTarget.PosActual;
            }
        }
    }
}
