//place this script in the Editor folder within Assets.

//to be used on the command line:
//$ Unity -quit -batchmode -executeMethod WebGLBuilder.build


using UnityEditor;

class WebGLBuilder {
	static void build() {
		
		AssetDatabase.Refresh();
		
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