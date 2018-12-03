using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OlfaktometerBackgroundSmell))]
public class OlfaktometerBackgroundSmellEditor : Editor
{
    bool showVentil;
    public override void OnInspectorGUI()
    {
        OlfaktometerBackgroundSmell oc = (OlfaktometerBackgroundSmell)target;
        base.OnInspectorGUI();
        GUIStyle style = EditorStyles.foldout;
        FontStyle previousStyle = style.fontStyle;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = Color.blue;
        style.active.textColor = Color.blue;
        showVentil = EditorGUILayout.Foldout(showVentil, "Ventile", style);
        style.fontStyle = previousStyle;
        if (showVentil)
        {
            for (int i = 0; i < 8; ++i)
            {
                oc.Ventile[i] = EditorGUILayout.Toggle(
                    "Ventil " + (i + 1),
                    oc.Ventile[i]);
            }
        }
    }

}