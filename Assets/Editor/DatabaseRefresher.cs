//place this script in the Editor folder within Assets.

//to be used on the command line:
//$ Unity -quit -batchmode -executeMethod DatabaseRefresher.build


using UnityEditor;

class DatabaseRefresher {
	static void build() {
		
		//AssetDatabase.Refresh();
		
		AssetDatabase.ImportAsset("Assets/models/building2/building2.obj", ImportAssetOptions.Default);
				
	}
}