using UnityEngine;
using System.Collections;

using VacuumShaders.MeshMaterializer.ColorAdjustment;

public class Runtime_ColorAdjustments : MonoBehaviour
{
    //////////////////////////////////////////////////////////////////////////////
    //                                                                          // 
    //Variables                                                                 //                
    //                                                                          //               
    //////////////////////////////////////////////////////////////////////////////
    public CAData_ColorSpace colorSpace = new CAData_ColorSpace();
    public CAData_Levels levels = new CAData_Levels();
    public CAData_HueSaturationLightness hueSaturation = new CAData_HueSaturationLightness();
    public CAData_BrightnessContrast brightnessAndContrast = new CAData_BrightnessContrast();
    public CAData_ColorOverlay colorOverlay = new CAData_ColorOverlay();
    public CAData_Invert invert = new CAData_Invert();
    public CAData_Alpha alpha = new CAData_Alpha();


    Mesh mesh;
    Color[] originalColors;
    Color[] newColors;

    //////////////////////////////////////////////////////////////////////////////
    //                                                                          // 
    //Unity Functions                                                           //                
    //                                                                          //               
    //////////////////////////////////////////////////////////////////////////////
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        originalColors = GetComponent<MeshFilter>().sharedMesh.colors;

        newColors = new Color[originalColors.Length];
    }

    void Update()
    {
        //Unoptimized solution
        //Better update colors only if any of adjustmet's parameter has changed
        for (int i = 0; i < newColors.Length; i++)
        {
            newColors[i] = originalColors[i];


            newColors[i] = colorSpace.Apply(newColors[i]);
            newColors[i] = levels.Apply(newColors[i]);
            newColors[i] = hueSaturation.Apply(newColors[i]);
            newColors[i] = brightnessAndContrast.Apply(newColors[i]);
            newColors[i] = colorOverlay.Apply(newColors[i]);
            newColors[i] = invert.Apply(newColors[i]);
            newColors[i] = alpha.Apply(newColors[i]);
        }

        mesh.colors = newColors;
    }
}
