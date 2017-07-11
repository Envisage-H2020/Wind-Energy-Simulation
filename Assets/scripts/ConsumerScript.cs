using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerScript : MonoBehaviour {


	public float MeanPowerConsume = 0.5f;
	public float VarPowerConsume = 0.1f;
	public float MinPowerConsume = 0f;
	public float MaxPowerConsume = 1f;
	public float CurrPowerConsume = 0.5f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		CurrPowerConsume = NextGaussianFloat () * Mathf.Sqrt (VarPowerConsume) + MeanPowerConsume;

		if (CurrPowerConsume > MaxPowerConsume)
			CurrPowerConsume = MaxPowerConsume;
		else if (CurrPowerConsume < MinPowerConsume)
			CurrPowerConsume = MinPowerConsume;
	}


	public static float NextGaussianFloat()
	{
		float U, u, v, S;

		do
		{
			u = 2.0f * Random.value - 1.0f;
			v = 2.0f * Random.value - 1.0f;
			S = u * u + v * v;
		}
		while (S >= 1.0f);


		float fac = Mathf.Sqrt(-2.0f *  Mathf.Log((float) S) / (float)S);

		return u * fac;
	}
}
