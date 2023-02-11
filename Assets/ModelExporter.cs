using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Text;

public class ModelExporter
{
    [MenuItem("Export/Export Model as OBJ")]
    public static void ExportModel()
    {
        // Get the selected game object in the hierarchy
        GameObject selectedGameObject = Selection.activeGameObject;

        // Make sure a game object is selected
        if (selectedGameObject == null)
        {
            Debug.LogError("Please select a game object to export as an OBJ file.");
            return;
        }

        // Get the skinned mesh renderer component on the selected game object
        SkinnedMeshRenderer skinnedMeshRenderer = selectedGameObject.GetComponent<SkinnedMeshRenderer>();

        // Make sure the selected game object has a skinned mesh renderer
        if (skinnedMeshRenderer == null)
        {
            Debug.LogError("The selected game object does not have a skinned mesh renderer. Please select a rigged model with a pose to export as an OBJ file.");
            return;
        }

        // Get the mesh from the skinned mesh renderer
        Mesh mesh = new Mesh();
        skinnedMeshRenderer.BakeMesh(mesh);

        // Make sure the mesh is not null
        if (mesh == null)
        {
            Debug.LogError("The selected game object does not have a valid mesh. Please select a rigged model with a pose to export as an OBJ file.");
            return;
        }


        // Create a list to store the vertices of the mesh
        List<Vector3> vertices = new List<Vector3>();
        for (int i = 0; i < mesh.vertexCount; i++)
        {
            vertices.Add(skinnedMeshRenderer.transform.TransformPoint(mesh.vertices[i]));
        }

        // Create a list to store the UVs of the mesh
        List<Vector2> uvs = new List<Vector2>();

        // Add the UVs of the mesh to the list
        for (int i = 0; i < mesh.vertexCount; i++)
        {
            uvs.Add(mesh.uv[i]);
        }

        // Create a list to store the triangles of the mesh
        List<int> triangles = new List<int>();

        // Add the triangles of the mesh to the list
        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            triangles.AddRange(mesh.GetTriangles(i));
        }

        // Create a new string builder to store the OBJ file content
        StringBuilder sb = new StringBuilder();

        // Add the vertices to the OBJ file
        for (int i = 0; i < vertices.Count; i++)
        {
            sb.AppendFormat("v {0} {1} {2}\n", vertices[i].x, vertices[i].y, vertices[i].z);
        }

        // Add the UVs to the OBJ file
        for (int i = 0; i < uvs.Count; i++)
        {
            sb.AppendFormat("vt {0} {1}\n", uvs[i].x, uvs[i].y);
        }

        // Add the triangles to the OBJ file
        for (int i = 0; i < triangles.Count; i += 3)
        {
            sb.AppendFormat("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n", triangles[i] + 1, triangles[i + 1] + 1, triangles[i + 2] + 1);
        }

        // Save the OBJ file to the Assets folder
        File.WriteAllText(Application.dataPath + "/ExportedModel.obj", sb.ToString());

        // Log a message to the console
        Debug.Log("Exported model as OBJ file to the Assets folder.");
    }
}
