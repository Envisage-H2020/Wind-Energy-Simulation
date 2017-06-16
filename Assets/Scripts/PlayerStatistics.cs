using UnityEngine;
using UnityEngine.SceneManagement;
using goedle_sdk;
using System.Security.Cryptography;
using System.Text;

public class PlayerStatistics : MonoBehaviour {

	private Simulation simulator;
	public static int underPowerSec;
    public static int correctPowerSec;
    public static int overPowerSec;
    public bool endSimulation = false;
    private int sceneCount;

    // Use this for initialization
    void Start () {
		sceneCount = SceneManager.sceneCountInBuildSettings;
		endSimulation = false;
		if(SceneManager.GetActiveScene().buildIndex != sceneCount - 1){ // if not the last scene
			simulator = GameObject.FindGameObjectWithTag("Simulator").GetComponent<Simulation>();
			InitializeCountValues();
			InvokeRepeating("CalculatePowerUsageStatistics",60.0f,1.0f);
		}
	}

	void Update(){
		EndSimulation();
	}

	void InitializeCountValues(){
		underPowerSec = 0;
		correctPowerSec = 0;
		overPowerSec = 0;
	}


	/*
	Stops the simulation and loads the end scene in the game. This can be achieved either by clicking on the exit button,
	or after 24 minutes have passed.
	*/
	public void EndSimulation(){
		if(SceneManager.GetActiveScene().buildIndex != sceneCount - 1){  // if not the last scene
			if(simulator.minutesCount >= 24 || endSimulation == true){
				SceneManager.LoadScene(sceneCount - 1 ); // loads last scene
				Resources.UnloadUnusedAssets(); //removes unused assets to free memory
			}
		}
	}

	public void ExitButtonPressed(){
		GoedleAnalytics.track ("end.simulation");
		endSimulation = true;
	}
	

	/*
	It holds to static variables the seconds that the player has spent in each power output scenario respectively.
	These values are later used in the end scene to calculate and display the usage of the wind farm, concerning the time spent in each scenario. 
	*/
 	void CalculatePowerUsageStatistics(){
		if(SceneManager.GetActiveScene().buildIndex != sceneCount - 1){
			if(string.Equals(simulator.powerUsage,"-Under power")){
				underPowerSec++;
			}
			else if(string.Equals(simulator.powerUsage,"-Correct power")){
				correctPowerSec++;
			}
			else {
				overPowerSec++;
			}
		}
	}

	void GetPlayerInfo(){
		
	}

	static string GetMd5Hash(MD5 md5Hash, string input)
	{

		// Convert the input string to a byte array and compute the hash.
		byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

		// Create a new Stringbuilder to collect the bytes
		// and create a string.
		StringBuilder sBuilder = new StringBuilder();

		// Loop through each byte of the hashed data 
		// and format each one as a hexadecimal string.
		for (int i = 0; i < data.Length; i++)
		{
			sBuilder.Append(data[i].ToString("x2"));
		}

		// Return the hexadecimal string.
		return sBuilder.ToString();
	}

}
