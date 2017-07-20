using UnityEditor;
class WebGLBuilder {
	static void build() {
	
		AssetDatabase.ImportAsset("Assets/models/building1/building1.obj", ImportAssetOptions.Default);
		
		string[] scenes = {"Assets/scenes/S_MainMenu.unity", 
							"Assets/scenes/S_Login.unity",
							"Assets/scenes/S_Help.unity",
							"Assets/scenes/S_1.unity",
							"Assets/scenes/S_Reward.unity",
							"Assets/scenes/S_Credits.unity",
							"Assets/scenes/S_Settings.unity",
							"Assets/scenes/S_SceneSelector.unity"};
		
		string pathToDeploy = "builds/WebGLversion/";		
				
		BuildPipeline.BuildPlayer(scenes, pathToDeploy, BuildTarget.WebGL, BuildOptions.None);		
	}
}