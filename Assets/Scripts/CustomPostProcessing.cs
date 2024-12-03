using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NCat.Tools.Test
{
    [ExecuteAlways, ExecuteInEditMode, ImageEffectAllowedInSceneView]
    public class CustomPostProcessing : MonoBehaviour
    {
        public Material PostProcessingMaterial;
    
        public void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (!PostProcessingMaterial)
            {
                Graphics.Blit(source, destination);
                return;
            }
            Graphics.Blit(source, destination, PostProcessingMaterial);
        }
    }
}
