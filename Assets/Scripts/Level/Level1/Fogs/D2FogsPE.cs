//#define DEBUG_RENDER

using UnityEngine;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;

namespace UB
{
    
    [ExecuteInEditMode]
    public class D2FogsPE : EffectBase
    {
        public Color Color = new Color(1f, 1f, 1f, 1f);//颜色为白
        public float Size = 1f;
        public float HorizontalSpeed = 0.2f;//水平速度
        public float VerticalSpeed = 0f;//垂直速度
        [Range(0.0f, 5)]
        public float Density = 2f;//浓度


       
        void Update()
        {
            /*
            if (Input.GetKey(KeyCode.A))
                Density = 0.3f;
            if (Input.GetKey(KeyCode.D))
                Density = 1.3f;
                */
           
        }



        public Shader Shader;
        private Material _material;

        void OnRenderImage(RenderTexture source, RenderTexture destination)//渲染图片
        {
            if (Shader == null)
            {
                Shader = Shader.Find("UB/PostEffects/D2Fogs");
            }

            if (_material)
            {
                DestroyImmediate(_material);
                _material = null;
            }
            //设置shader的属性
            if (Shader)
            {
                _material = new Material(Shader);
                _material.hideFlags = HideFlags.HideAndDontSave;//？？The GameObject is not shown in the Hierarchy, not saved to to Scenes, and not unloaded by Resources.UnloadUnusedAssets.

                if (_material.HasProperty("_Color"))
                {
                    _material.SetColor("_Color", Color);
                }
                if (_material.HasProperty("_Size"))
                {
                    _material.SetFloat("_Size", Size);
                }
                if (_material.HasProperty("_Speed"))
                {
                    _material.SetFloat("_Speed", HorizontalSpeed);
                }
                if (_material.HasProperty("_VSpeed"))
                {
                    _material.SetFloat("_VSpeed", VerticalSpeed);
                }
                if (_material.HasProperty("_Density"))
                {
                    _material.SetFloat("_Density", Density);
                }
            }

            if (Shader != null && _material != null)
            {
                Graphics.Blit(source, destination, _material);
                {

                    //source	Source texture.
                    //dest The destination RenderTexture. Set this to null to blit directly to screen.See description for more information.
                    //mat Material to use.Material's shader could do some post-processing effect, for example.

                    /*
                     * 描述

                       使用着色器将源纹理复制到目标渲染纹理。

                       这主要用于实现后处理效果。

                       Blit将dest设置为渲染目标，在材质上设置source _MainTex属性，并绘制全屏四边形。

                       如果dest为null，则屏幕后台缓冲区用作blit目标，除非主摄像机当前设置为渲染到RenderTexture（即Camera.main具有非null的targetTexture属性）。在这种情况下，blit使用主摄像机的渲染目标作为目标。为了确保实际对屏幕后台缓冲区执行了blit，请确保在调用Blit之前将/Camera.main.targetTexture/设置为null。

                       请注意，如果要使用作为源（渲染）纹理一部分的深度或模板缓冲区，则必须手动执行等效的Blit功能 - 即具有目标颜色缓冲区和源深度缓冲区的Graphics.SetRenderTarget，设置正交投影（GL.LoadOrtho），设置材质传递（Material.SetPass）并绘制四边形（GL.Begin）。

                       请注意，在线性颜色空间中，设置正确的sRGB < - >线性颜色转换状态非常重要。根据先前渲染的内容，当前状态可能不是您期望的状态。在进行Blit或任何其他手动渲染之前，您应该考虑根据需要设置GL.sRGBWrite。

                       请注意，调用Blit并将source和dest设置为相同的RenderTexture可能会导致未定义的行为。更好的方法是使用具有双缓冲的自定义渲染纹理，或使用两个RenderTextures并在它们之间交替以手动实现双缓冲。

                       另请参见：Graphics.BlitMultiTap，后处理效果。
                     */
                }
            }
        }
    }
}