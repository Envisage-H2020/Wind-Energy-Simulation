using UnityEngine;
using System.Collections;

//Mesh Materializer API is here ↓
using VacuumShaders.MeshMaterializer;


[AddComponentMenu("VacuumShaders/Mesh Materializer/Example/Runtime")]
public class Runtime : MonoBehaviour 
{
    //////////////////////////////////////////////////////////////////////////////
    //                                                                          // 
    //Variables                                                                 //                
    //                                                                          //               
    //////////////////////////////////////////////////////////////////////////////

    //Describes surface type
    public MMData_SurfaceInfo surfaceInfo;

    //Describes which textures and colors will be baked and how
    public MMData_MeshTextureAndColor meshTextureAndColor;

    //Ambient occlusion options
    public MMData_AmbientOcclusion ambientOcclusion;

    //Indirect Lighting options
    public MMData_IndirectLighting indirectLighting;

    //Displace options
    public MMData_MeshDisplace displace;



    //This material will be used on final mesh
    public Material vertexColorMaterial;


    //////////////////////////////////////////////////////////////////////////////
    //                                                                          // 
    //Unity Functions                                                           //                
    //                                                                          //               
    //////////////////////////////////////////////////////////////////////////////
	void Start () 
    {
        Mesh origianlMesh = null;

        //Get original mesh from MeshFilter or SkinnedMeshRenderer
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        if (meshFilter != null)
            origianlMesh = meshFilter.sharedMesh;

        if (origianlMesh == null && skinnedMeshRenderer != null)
            origianlMesh = skinnedMeshRenderer.sharedMesh;


        //Oops, no mesh found
        if (origianlMesh == null)
            return;



        //Will contain bake results 
        Mesh newMesh = null;

        //Will contain baking reports, will help if something goes wrong
        MMGenerator.CONVERTION_INFO[] convertionInfo;

        //Same as above but with more detail info
        string[] convertionInfoString;
        


        //Mesh Materializer        
        newMesh = MMGenerator.MaterializeMesh(GetComponent<Renderer>(), out convertionInfo, out convertionInfoString, surfaceInfo, meshTextureAndColor, ambientOcclusion, indirectLighting, displace);


        //Check reports
        if (convertionInfoString != null)
            for (int i = 0; i < convertionInfoString.Length; i++)
            {
                Debug.Log(convertionInfoString[i]);
            }


        //Successful conversation
        if (newMesh != null)
        {
            //Replace old mesh with new one
            if (meshFilter != null)
                meshFilter.sharedMesh = newMesh;
            else if (skinnedMeshRenderer != null)
                skinnedMeshRenderer.sharedMesh = newMesh;


            //Replace material to make baked data visible
            GetComponent<Renderer>().sharedMaterials = new Material[] { vertexColorMaterial };
        }
	}
}
