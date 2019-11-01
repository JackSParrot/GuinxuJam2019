using UnityEngine;
using System.Collections;

public class Torchelight : MonoBehaviour
{
    public GameObject Player;

	public GameObject TorchLight;
	public GameObject MainFlame;
	public GameObject BaseFlame;
	public GameObject Etincelles;
	public GameObject Fumee;
	public float MaxLightIntensity;
	public float IntensityLight;

    Light l;

	void Start () {
        l = TorchLight.GetComponent<Light>();
        l.intensity=IntensityLight;
	}
	

	void Update () {
        l.gameObject.SetActive((Player.transform.position - transform.position).sqrMagnitude < 25f*25f);
		if (IntensityLight<0) IntensityLight=0;
		if (IntensityLight>MaxLightIntensity) IntensityLight=MaxLightIntensity;		

		l.intensity=IntensityLight/2f+Mathf.Lerp(IntensityLight-0.1f,IntensityLight+0.1f,Mathf.Cos(Time.time*30));

		l.color=new Color(Mathf.Min(IntensityLight/1.5f,1f),Mathf.Min(IntensityLight/2f,1f),0f);	

	}
}
