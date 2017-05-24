using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using goedle_sdk;

public class ConfigurationMenu : MonoBehaviour {

	public Simulation simulator;
	public Canvas menu;

	/*public variables where used because the menu
	has specific elements and public variables make
	customizing easier and faster
	*/

	/*////////////////////////
			SLIDERS
	///////////////////////*/
	public Slider windMinSlider;
	public Slider windMaxSlider;
	public Slider reqsMinSlider;
	public Slider reqsMaxSlider;

	/*////////////////////////
			TEXT VALUES
	///////////////////////*/
	public Text windMinText;
	public Text windMaxText;
	public Text reqsMinText;
	public Text reqsMaxText;

	// Use this for initialization
	void Start () {
		hideConfigMenu();
		//set default values to sliders
		SetDefautValueToSlider(windMinSlider,simulator.windMinSpeed);
		SetDefautValueToSlider(windMaxSlider,simulator.windMaxSpeed);
		SetDefautValueToSlider(reqsMinSlider,simulator.powerRequirementsMin);
		SetDefautValueToSlider(reqsMaxSlider,simulator.powerRequirementsMax);
	}
	
	
	void Update () {
		/*check if user tries to give not acceptable values(min > max)
		and block his action */
		if( menu.GetComponent<CanvasGroup>().alpha == 1){
			ValidateSliderValues();	
			SetDefautValueToSlider(windMinSlider,simulator.windMinSpeed);
			SetDefautValueToSlider(windMaxSlider,simulator.windMaxSpeed);
			SetDefautValueToSlider(reqsMinSlider,simulator.powerRequirementsMin);
			SetDefautValueToSlider(reqsMaxSlider,simulator.powerRequirementsMax);
			
			//display values next to sliders
			SetTextValue(windMinText,simulator.windMinSpeed);
			SetTextValue(windMaxText,simulator.windMaxSpeed);
			SetTextValue(reqsMinText,simulator.powerRequirementsMin);
			SetTextValue(reqsMaxText,simulator.powerRequirementsMax);
		}
	}

	void SetDefautValueToSlider(Slider slider,int value){
		slider.value = value;	
	}
	void SetTextValue(Text text,int value){
		text.text = value.ToString();
	}

	public void showConfigMenu(){
		menu.GetComponent<CanvasGroup>().alpha = 1f;
		GoedleAnalytics.track ("configure.open","OpenConfigurationPanel");
	}

	public void hideConfigMenu(){
		menu.GetComponent<CanvasGroup>().alpha = 0f;
		GoedleAnalytics.track ("configure.wind_speed", "max ", simulator.windMaxSpeed.ToString() );
		GoedleAnalytics.track ("configure.wind_speed", "min ", simulator.windMinSpeed.ToString() );
		GoedleAnalytics.track ("configure.power", "max ", simulator.powerRequirementsMax.ToString() );
		GoedleAnalytics.track ("configure.power", "min ", simulator.powerRequirementsMin.ToString() );
		GoedleAnalytics.track ("configure.simulation_speed", null, simulator.simulationSpeed.ToString() );
		GoedleAnalytics.track ("configure.close", "CloseConfigurationPanel");
	}
	
	public void SetwindMax(float max){
		simulator.windMaxSpeed = (int) max;
	}
	public void SetwindMin(float min){
		simulator.windMinSpeed = (int) min;

	}
	public void SetRequirementsMax(float max){
		simulator.powerRequirementsMax = (int) max;	
	}
	public void SetRequirementsMin(float min){
		simulator.powerRequirementsMin = (int) min;	
	}
	public void SetsimulationSpeed(int index){
		if(index ==0) simulator.simulationSpeed = 1;
		else if(index ==1) simulator.simulationSpeed = 3;
		else simulator.simulationSpeed = 4; 
	}

	public void ValidateSliderValues(){
		if(simulator.windMaxSpeed < simulator.windMinSpeed){
			simulator.windMaxSpeed = simulator.windMinSpeed;
		}
		if(simulator.powerRequirementsMax < simulator.powerRequirementsMin){
			simulator.powerRequirementsMax = simulator.powerRequirementsMin;
		}
	}
}
