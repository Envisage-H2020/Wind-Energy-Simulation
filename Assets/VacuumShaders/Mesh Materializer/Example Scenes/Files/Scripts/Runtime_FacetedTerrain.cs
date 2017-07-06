using UnityEngine;
using System.Collections;

//Mesh Materializer API is here ↓
using VacuumShaders.MeshMaterializer;


[AddComponentMenu("VacuumShaders/Mesh Materializer/Example/Runtime Faceted Terrain")]
public class Runtime_FacetedTerrain : MonoBehaviour 
{
    //////////////////////////////////////////////////////////////////////////////
    //                                                                          // 
    //Variables                                                                 //                
    //                                                                          //               
    //////////////////////////////////////////////////////////////////////////////

    //
    public Terrain targetTerrain;


    //Describes surface type
    public MMData_SurfaceInfo surfaceInfo;

    //Terrain To Mesh options
    public MMData_TerrainToMesh terrainToMesh;
    public MMData_TerrainTexture terrainTexture;


    //This material will be used on final mesh
    public Material vertexColorMaterial;

    //////////////////////////////////////////////////////////////////////////////
    //                                                                          // 
    //Unity Functions                                                           //                
    //                                                                          //               
    //////////////////////////////////////////////////////////////////////////////
	void Start () 
    {
        if (targetTerrain == null)
            return;


      
        //Will contain bake results 
        //Need - array - as materializing terrain returns mesh array depending on chunks count described in 'terrainToMesh'
        Mesh[] newMesh = null;

        //Will contain baking reports, will help if something goes wrong
        MMGenerator.CONVERTION_INFO[] convertionInfo;

        //Same as above but with more detail info
        string[] convertionInfoString;
        


        //Terrain Materializer        
        newMesh = MMGenerator.MaterializeTerrain(targetTerrain, out convertionInfo, out convertionInfoString, surfaceInfo, terrainToMesh, terrainTexture);

        //Check reports
        if (convertionInfoString != null)
            for (int i = 0; i < convertionInfoString.Length; i++)
            {
                Debug.LogWarning(convertionInfoString[i]);
            }


        //Successful conversation
        if (newMesh != null)
        {
            for (int i = 0; i < newMesh.Length; i++)
            {
                //Create new gameobject for each chunk
                GameObject chunk = new GameObject(newMesh[i].name);
                chunk.AddComponent<MeshFilter>().sharedMesh = newMesh[i];
                chunk.AddComponent<MeshRenderer>().sharedMaterial = vertexColorMaterial;

                
                //Move to parent
                chunk.transform.parent = this.gameObject.transform;
                chunk.transform.localPosition = Vector3.zero;
            }
        }
	}
}
