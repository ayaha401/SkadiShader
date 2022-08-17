using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;

public class ScanlinePostProcessRenderFeature : ScriptableRendererFeature
{
    [SerializeField] Shader shader;

    private ScanlinePostProcessRenderPass postProcessPass;


    public override void Create()
    {
        postProcessPass = new ScanlinePostProcessRenderPass(shader, RenderPassEvent.BeforeRenderingPostProcessing);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        postProcessPass.Setup(renderer.cameraColorTarget);
        renderer.EnqueuePass(postProcessPass);
    }
}
