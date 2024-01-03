using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class EffectVolume : MonoBehaviour
{
    public Material material;
    public int colorCount;
    public Color[] colors;

    public void Awake(){
        colors = new Color[colorCount];

        for(int i = 0; i < colorCount/4; i++){ //Greys
            float color = (1.0f / ((float)colorCount/4f)) * i;
            colors[i] = new Color(color, color, color, 1);
            colors[i + (colorCount/4)] = new Color(color, 0, 0, 1);
            colors[i + (colorCount/2)] = new Color(0, color, 0, 1);
            colors[i + (colorCount/4 * 3)] = new Color(0, 0, color, 1);
        }
    }

    public void OnRenderImage(RenderTexture src, RenderTexture dest) {
        if (material && colors.Length > 0) {
            material.SetInt("_ColorCount", colors.Length);
            material.SetColorArray("_Colors", colors);

            Graphics.Blit(src, dest, material);
        } else {
            Graphics.Blit(src, dest);
        }
    }
}
