using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MengerSpongeFractal))]
public class MengerSpongeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MengerSpongeFractal mengerSpongeFractal = (MengerSpongeFractal)target;

        //if (GUILayout.Button("Set Initial Scale"))
        //{
        //    mengerSpongeFractal.InitializeScaleEditor();
        //}

        if (GUILayout.Button("Generate Menger Sponge"))
        {
            mengerSpongeFractal.GenerateMengerSponge();
        }

        //if (GUILayout.Button("Deactivate Components Menger Sponge"))
        //{
        //    mengerSpongeFractal.DeactivateComponents();
        //}
    }
}
