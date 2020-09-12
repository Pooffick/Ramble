using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class CreateMergedMesh : ScriptableWizard
{
	[MenuItem ("Prototype/Create Merged Mesh")]
	static void CreateDataMesh ()
	{
		var go = Selection.activeGameObject;
		MeshFilter[] mfs = go.GetComponentsInChildren<MeshFilter> ();

		GameObject newGo = new GameObject ("Merged Mesh");
		var newMf = newGo.AddComponent<MeshFilter> ();
		var newMr = newGo.AddComponent<MeshRenderer> ();

		// Create a mesh in which to add our individual meshes
		Mesh masterMesh = new Mesh ();
		masterMesh.name = "Combined Mesh";
		// Mesh data to combine into full mesh
		List<CombineInstance> combineInstances = new List<CombineInstance> ();
		
		foreach (var m in mfs) {
			CombineInstance c = new CombineInstance ();
			c.mesh = m.sharedMesh;
			c.transform = m.transform.localToWorldMatrix;
			combineInstances.Add (c);
		}

		masterMesh.CombineMeshes (combineInstances.ToArray (), true, true);

		AssetDatabase.CreateAsset (masterMesh, "Assets/masterMesh.asset");

		// Add master mesh to the parent object
		newMf.mesh = masterMesh;
		newMr.material = new Material(Shader.Find("Diffuse"));
	}
}
