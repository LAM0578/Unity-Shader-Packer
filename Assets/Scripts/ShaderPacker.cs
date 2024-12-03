#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace NCat.Tools
{
    public class ShaderPacker : MonoBehaviour
    {
        [LocalizedTooltip(
            "要打包进 AssetBundle 的 Shader",
            "Shaders to pack into AssetBundle"
        )]
        public List<Shader> TargetShaders = new List<Shader>();
        
        [LocalizedTooltip(
            "AssetBundle 的名称",
            "AssetBundle name"
        )]
        public string AssetBundleName = "CustomShaders";
        
        [LocalizedTooltip(
            "AssetBundle 的输出路径",
            "AssetBundle output path"
        )]
        public string BuildOutputPath;

        public void Build()
        {
            if (TargetShaders == null || TargetShaders.Count == 0)
            {
                Debug.LogError("No shaders specified for build.");
                return;
            }

            if (string.IsNullOrEmpty(BuildOutputPath))
            {
                Debug.LogError("Build output path is not specified.");
                return;
            }
            
            Directory.CreateDirectory(BuildOutputPath);
            
            var assets = TargetShaders
                .Where(t => t)
                .Select(AssetDatabase.GetAssetPath).ToArray();
            
            Debug.Log($"Target shaders:\n{string.Join("\n", assets)}");

            var manifest = BuildPipeline.BuildAssetBundles(
                BuildOutputPath,
                new []
                {
                    new AssetBundleBuild()
                    {
                        assetBundleName = AssetBundleName,
                        assetNames = assets
                    }
                },
                BuildAssetBundleOptions.None,
                EditorUserBuildSettings.activeBuildTarget
            );
            
            Debug.Log("Shader build complete.");
        }
    }

    [CustomEditor(typeof(ShaderPacker))]
    public class ShaderPackerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var item = (ShaderPacker)target;
            if (GUILayout.Button("Build"))
            {
                item.Build();
            }
        }
    }
}
#endif
