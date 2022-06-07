using System.Drawing;
using System.Linq;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
struct ExampleVertex
{
    public half4 pos;
}

public class MeshLoading : MonoBehaviour
{
    [SerializeField]
    private MeshFilter input;

    [SerializeField]
    private MeshFilter outputMeshFilter;

    private void Start()
    {
        // input.sharedMesh.isReadable = true;
        var inputVertices = input.sharedMesh.vertices;



        var ret = new Mesh();
        var layout = new[]
        {
            new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float16, 4),
        };
        var vertexCount = 300 * 300 * 300; // Thousands pixels cube

        // 1728 000 000
        //   27 000 000
        //    4 611 329
        // Debug.Log("Size = " + (inputVertices.Length));

        ret.SetVertexBufferParams(vertexCount, layout);
        ret.indexFormat = IndexFormat.UInt32;

        using var verts = new NativeList<ExampleVertex>(vertexCount, Allocator.Temp);
        foreach (var vertex in inputVertices)
        {
            verts.Add(new ExampleVertex()
            {
                pos = new half4(
                    new half(vertex.x),
                    new half(vertex.y),
                    new half(vertex.z),
                    half.zero)
            });
        }
        ret.SetVertexBufferData(verts.ToArray(), 0, 0, inputVertices.Length);
        ret.SetIndices(
            Enumerable.Range(0, vertexCount).ToArray(),
            MeshTopology.Points, 0
        );

        ret.MarkModified();
        outputMeshFilter.gameObject.transform.position = input.gameObject.transform.position;
        outputMeshFilter.gameObject.transform.rotation = input.gameObject.transform.rotation;
        outputMeshFilter.gameObject.transform.localScale = input.gameObject.transform.localScale;
        outputMeshFilter.mesh = ret;
    }
}
