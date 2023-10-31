using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;

public class VertexShaderController : MonoBehaviour
{

    #region Constants

    private const string BENDING_FEATURE = "ENABLE_BENDING";

    private const string PLANET_FEATURE = "ENABLE_BENDING_PLANET";
    #endregion

    #region Inspector

    [SerializeField]
    private bool enablePlanet = default;

    [SerializeField]
    [Range(0, 1)]
    public float warpVal = 0.0f;

    #endregion

    #region Fields

     private float _prevAmount;

    #endregion

    // Start is called before the first frame update
    #region MonoBehaviour

  private void Awake ()
  {
    if ( Application.isPlaying )
      Shader.EnableKeyword(BENDING_FEATURE);
    else
      Shader.DisableKeyword(BENDING_FEATURE);

    if ( enablePlanet )
      Shader.EnableKeyword(PLANET_FEATURE);
    else
      Shader.DisableKeyword(PLANET_FEATURE);

    UpdateBendingAmount();
  }

  private void OnEnable ()
  {
    if ( !Application.isPlaying )
      return;
    
    RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
    RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
  }

  private void Update ()
  {
    if (Mathf.Abs(_prevAmount - warpVal) > Mathf.Epsilon )
      UpdateBendingAmount();
  }

  private void OnDisable ()
  {
    RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
    RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
  }

  #endregion


  #region Methods

  private void UpdateBendingAmount ()
  {
    _prevAmount = warpVal;
    Shader.SetGlobalFloat("_warpVal", warpVal);
  }

  private static void OnBeginCameraRendering (ScriptableRenderContext ctx,
                                              Camera cam)
  {
    cam.cullingMatrix = Matrix4x4.Ortho(-499, 499, -499, 499, 0.001f, 499) *
                        cam.worldToCameraMatrix;
  }

  private static void OnEndCameraRendering (ScriptableRenderContext ctx,
                                            Camera cam)
  {
    cam.ResetCullingMatrix();
  }

  #endregion
}
