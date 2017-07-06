using UnityEngine;
using System.Collections;

//Mesh Materializer API is here ↓
using VacuumShaders.MeshMaterializer;


[AddComponentMenu("VacuumShaders/Mesh Materializer/Example/Runtime Mesh Combine")]
public class Runtime_MeshCombine : MonoBehaviour 
{
    //////////////////////////////////////////////////////////////////////////////
    //                                                                          // 
    //Variables                                                                 //                
    //                                                                          //               
    //////////////////////////////////////////////////////////////////////////////

    //All meshes inside 'meshfilterCollection' will be combined into one mesh
    public Transform meshfilterCollection;


    //Describes surface type
    public MMData_SurfaceInfo surfaceInfo;

    //Describes which textures and colors will be baked and how
    public MMData_MeshTextureAndColor meshTextureAndColor;



    //This material will be used on final mesh
    public Material vertexColorMaterial;


    //////////////////////////////////////////////////////////////////////////////
    //                                                                          // 
    //Unity Functions                                                           //                
    //                                                                          //               
    //////////////////////////////////////////////////////////////////////////////
	void Start () 
    {
        //First check if meshes inside 'meshfilterCollection' can be combined
        MMGenerator.COMBINE_INFO combineInfo;

        combineInfo =  MMGenerator.CanBeCombined(meshfilterCollection);
        if (combineInfo != MMGenerator.COMBINE_INFO.OK)
        {
            //Houston we have a problem
            Debug.LogError(combineInfo.ToString());

            return;
        }


        
        //Will contain bake results 
        Mesh newMesh = null;

        //Will contain baking reports, will help if something goes wrong
        MMGenerator.CONVERTION_INFO[] convertionInfo;

        //Same as above but with more detail info
        string[] convertionInfoString;
        


        //Mesh Group Materializer        
        newMesh = MMGenerator.MaterializeMeshGroup(meshfilterCollection, out convertionInfo, out convertionInfoString, surfaceInfo, meshTextureAndColor);


        //Check reports
        if (convertionInfoString != null)
            for (int i = 0; i < convertionInfoString.Length; i++)
            {
                if (convertionInfo[i] != MMGenerator.CONVERTION_INFO.Ok)
                    Debug.LogWarning(convertionInfoString[i]);
            }


        //Successful conversation
        if (newMesh != null)
        {
            gameObject.AddComponent<MeshFilter>().sharedMesh = newMesh;
            gameObject.AddComponent<MeshRenderer>().sharedMaterial = vertexColorMaterial;
        }
	}
}
