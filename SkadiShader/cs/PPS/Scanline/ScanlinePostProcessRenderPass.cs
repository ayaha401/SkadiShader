using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ScanlinePostProcessRenderPass : ScriptableRenderPass
{
    private const string RenderPassName = nameof(ScanlinePostProcessRenderPass);

    private Material mat = null;
    private new ProfilingSampler profilingSampler;
    private int mainTexPropertyID = Shader.PropertyToID("_MainTex");
    private int distortPowerPropID = Shader.PropertyToID("_DistortPower");
    private int brightnessPropID = Shader.PropertyToID("_Brightness");
    private int lineAmountPropID = Shader.PropertyToID("_LineAmount");
    private int lineMoveSpeedPropID = Shader.PropertyToID("_LineMoveSpeed");
    private int lineNonBlurPropID = Shader.PropertyToID("_LineNonBlur");
    private int useVignettePropID = Shader.PropertyToID("_UseVignette");
    private int useGlitchPropID = Shader.PropertyToID("_UseGlitch");

    private RenderTargetIdentifier source = default;
    private RenderTargetHandle targetHandle;
    private ScanlinePostProcessVolume volume = null;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="shader">適用するシェーダー</param>
    /// <param name="passEvent">描画タイミング</param>
    public ScanlinePostProcessRenderPass(Shader shader, RenderPassEvent passEvent)
    {
        if (shader == null) return;

        // 描画タイミングを設定
        renderPassEvent = passEvent;


        profilingSampler = new ProfilingSampler(RenderPassName);
        targetHandle.Init("_TempRT");

        // マテリアルを作成
        mat = CoreUtils.CreateEngineMaterial(shader);
    }

    public void Setup(RenderTargetIdentifier _source)
    {
        this.source = _source;

        // Volumeコンポーネントを取得
        VolumeStack volumeStack = VolumeManager.instance.stack;
        volume = volumeStack.GetComponent<ScanlinePostProcessVolume>();
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (mat == null) return;                                                        // マテリアルが無かったら何もしない
        if (!renderingData.cameraData.postProcessEnabled) return;                       // PPSが無効なら何もしない
        if (renderingData.cameraData.cameraType == CameraType.SceneView) return;        // シーンビューでは何もしない
        if (!volume.IsActive()) return;                                                 // Volumeを使わないのなら何もしない

        // コマンドバッファを作成
        CommandBuffer cmd = CommandBufferPool.Get(RenderPassName);
        cmd.Clear();

        // RenderTextureを生成する
        RenderTextureDescriptor targetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
        targetDescriptor.depthBufferBits = 0;
        cmd.GetTemporaryRT(targetHandle.id, targetDescriptor);

        using (new ProfilingScope(cmd, profilingSampler))
        {
            // マテリアルにパラメーターを設定
            mat.SetFloat(distortPowerPropID, volume.distortPower.value);
            mat.SetFloat(brightnessPropID, volume.brightness.value);
            mat.SetInt(lineAmountPropID, Mathf.Abs(volume.lineAmount.value));
            mat.SetInt(lineMoveSpeedPropID, volume.lineMoveSpeed.value);
            float lineNonBlurToggle = volume.lineNonBlur.value == true ? 1f : 0f;
            mat.SetFloat(lineNonBlurPropID, lineNonBlurToggle);
            float vignetteToggle = volume.useVignette.value == true ? 1f : 0f;
            mat.SetFloat(useVignettePropID, vignetteToggle);
            float glitchToggle = volume.useGlitch.value == true ? 1f : 0f;
            mat.SetFloat(useGlitchPropID, glitchToggle);

            cmd.SetGlobalTexture(mainTexPropertyID, source);
            Blit(cmd, source, targetHandle.Identifier(), mat);
        }
        Blit(cmd, targetHandle.Identifier(), source);

        // RTを解放
        cmd.ReleaseTemporaryRT(targetHandle.id);

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}
