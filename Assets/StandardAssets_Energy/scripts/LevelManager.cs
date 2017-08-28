using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void LoadNextLevel(int sceneNumber){
		SceneManager.LoadScene(sceneNumber);
	}
}
