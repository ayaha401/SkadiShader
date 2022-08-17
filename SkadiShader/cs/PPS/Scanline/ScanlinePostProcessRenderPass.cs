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
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="shader">�K�p����V�F�[�_�[</param>
    /// <param name="passEvent">�`��^�C�~���O</param>
    public ScanlinePostProcessRenderPass(Shader shader, RenderPassEvent passEvent)
    {
        if (shader == null) return;

        // �`��^�C�~���O��ݒ�
        renderPassEvent = passEvent;


        profilingSampler = new ProfilingSampler(RenderPassName);
        targetHandle.Init("_TempRT");

        // �}�e���A�����쐬
        mat = CoreUtils.CreateEngineMaterial(shader);
    }

    public void Setup(RenderTargetIdentifier _source)
    {
        this.source = _source;

        // Volume�R���|�[�l���g���擾
        VolumeStack volumeStack = VolumeManager.instance.stack;
        volume = volumeStack.GetComponent<ScanlinePostProcessVolume>();
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (mat == null) return;                                                        // �}�e���A�������������牽�����Ȃ�
        if (!renderingData.cameraData.postProcessEnabled) return;                       // PPS�������Ȃ牽�����Ȃ�
        if (renderingData.cameraData.cameraType == CameraType.SceneView) return;        // �V�[���r���[�ł͉������Ȃ�
        if (!volume.IsActive()) return;                                                 // Volume���g��Ȃ��̂Ȃ牽�����Ȃ�

        // �R�}���h�o�b�t�@���쐬
        CommandBuffer cmd = CommandBufferPool.Get(RenderPassName);
        cmd.Clear();

        // RenderTexture�𐶐�����
        RenderTextureDescriptor targetDescriptor = renderingData.cameraData.cameraTargetDescriptor;
        targetDescriptor.depthBufferBits = 0;
        cmd.GetTemporaryRT(targetHandle.id, targetDescriptor);

        using (new ProfilingScope(cmd, profilingSampler))
        {
            // �}�e���A���Ƀp�����[�^�[��ݒ�
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

        // RT�����
        cmd.ReleaseTemporaryRT(targetHandle.id);

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }
}
