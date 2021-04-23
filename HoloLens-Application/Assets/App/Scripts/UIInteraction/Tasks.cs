using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tasks", menuName = "UIInteraction", order = 1)]
public class Tasks : ScriptableObject
{
    public ObjectParameters[] tasks;
    public Color[] colors;
    public string[] objectTypeNames;
    public string[] objectQualityNames;
    public string[] colorNames;
    public MeshCollection[] meshes;

    [System.Serializable]
    public class MeshCollection
    {
        [SerializeField]
        public Mesh[] modelMeshes;
    }
}
