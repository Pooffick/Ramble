using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(BlockMover))]
[CanEditMultipleObjects]
public class EditorBlocks : Editor {

    SerializedProperty fromProp, toProp, angleProp, speedProp, typeProp;

    void OnEnable()
    {
        fromProp = serializedObject.FindProperty("from");
        toProp = serializedObject.FindProperty("to");
        angleProp = serializedObject.FindProperty("angle");
        speedProp = serializedObject.FindProperty("speed");
        typeProp = serializedObject.FindProperty("type");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        if(typeProp.enumValueIndex == 0)
        {
            fromProp.vector3Value = EditorGUILayout.Vector3Field("From", fromProp.vector3Value);
            toProp.vector3Value = EditorGUILayout.Vector3Field("To", toProp.vector3Value);
        }
        else if (typeProp.enumValueIndex == 1)
        {
            angleProp.vector3Value = EditorGUILayout.Vector3Field("Angle", angleProp.vector3Value);
        }

        speedProp.floatValue = EditorGUILayout.FloatField("Speed", speedProp.floatValue);
        typeProp.enumValueIndex = System.Convert.ToInt32(EditorGUILayout.EnumPopup("Type", (BlockMover.BlockType)typeProp.enumValueIndex));

        serializedObject.ApplyModifiedProperties();
    }
}
