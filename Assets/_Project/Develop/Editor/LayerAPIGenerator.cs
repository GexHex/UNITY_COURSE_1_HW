using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class LayerAPIGenerator
{
    private static string OutputPath => Path.Combine(Application.dataPath, "_Project/Develop/Runtime/_TestGenerated/UnityLayersAPI.cs");

    [MenuItem("Tools/LayerAPIGenerator")]
    private static void Generate()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("using UnityEngine;");
        sb.AppendLine();

        sb.AppendLine("public static class UnityLayers");
        sb.AppendLine("{");

        foreach (string layer in GetLayers())
        {
            string layerName = GetValidIdentifier(layer);

            sb.AppendLine($"\tpublic static readonly int Layer{layerName} = LayerMask.NameToLayer(\"{layer}\");");
        }

        sb.AppendLine();

        foreach (string layer in GetLayers())
        {
            string layerName = GetValidIdentifier(layer);

            sb.AppendLine($"\tpublic static readonly int LayerMask{layerName} = 1 << Layer{layerName};");
        }

        sb.AppendLine("}");

        File.WriteAllText(OutputPath, sb.ToString());

        AssetDatabase.Refresh();
        AssetDatabase.SaveAssets();
    }

    private static string[] GetLayers()
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);

        SerializedProperty layers = tagManager.FindProperty("layers");

        return Enumerable
            .Range(0, layers.arraySize)
            .Select(i => layers.GetArrayElementAtIndex(i).stringValue)
            .Where(layer => string.IsNullOrWhiteSpace(layer) == false)
            .ToArray();
    }

    private static string GetValidIdentifier(string layerName)
    {
        StringBuilder sb = new StringBuilder();

        foreach (char c in layerName)
        {
            if (char.IsLetterOrDigit(c))
                sb.Append(c);
        }

        return sb.ToString();
    }
}
