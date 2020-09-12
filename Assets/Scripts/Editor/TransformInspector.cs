using UnityEngine;
using UnityEditor;
using System.Collections;
[CustomEditor(typeof(Transform))]
public class TransformInspector:Editor
{
	public override void OnInspectorGUI()
	{
		Transform t = (Transform)target;
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("P")) {
			Undo.RecordObject (t, "Reset Position " + t.name);
			t.transform.localPosition = Vector3.zero;
		}
		if (GUILayout.Button ("R")) {
			Undo.RecordObject(t, "Reset Rotation " + t.name);
			t.transform.localRotation = Quaternion.identity;
		}
		if (GUILayout.Button ("S")) {
			Undo.RecordObject(t, "Reset Scales " + t.name);
			t.transform.localScale = Vector3.one;
		}
		GUILayout.EndHorizontal ();
		// Replicate the standard transform inspector gui
		EditorGUIUtility.LookLikeControls ();
		EditorGUI.indentLevel = 0;
		Vector3 position = EditorGUILayout.Vector3Field ("Position", t.localPosition);
		Vector3 eulerAngles = EditorGUILayout.Vector3Field ("Rotation", t.localEulerAngles);
		Vector3 scale = EditorGUILayout.Vector3Field ("Scale", t.localScale);
		//EditorGUIUtility.LookLikeInspector ();
		if (GUI.changed) {
			Undo.RecordObject(t, "Transform Change");
			t.localPosition = FixIfNaN (position);
			t.localEulerAngles = FixIfNaN (eulerAngles);
			t.localScale = FixIfNaN (scale);
		}
	}

	private Vector3 FixIfNaN(Vector3 v)
	{
		if(float.IsNaN(v.x))
		{
			v.x =0;
		}
		if(float.IsNaN(v.y))
		{
			v.y =0;
		}
		if(float.IsNaN(v.z))
		{
			v.z =0;
		}
		return v;
	}
}