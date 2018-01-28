using UnityEngine;

public class CameraShake : MonoBehaviour {
	// Range from 0-1
	float trauma = 0.0f;
	float shake = 0.0f;
	float traumaDrainRate;
	public float timeFromMaxToMin = 1.0f;
	public float maxAngle = 20.0f;
	// Rate at which trauma decreases per second. 
	public float xTimeScale = 1.0f;
	public float yTimeScale = 1.0f;
	public float zTimeScale = 1.0f;

	Vector3 offset;
	Quaternion baseRotation;
	Quaternion rotation;

	void Awake() {

		traumaDrainRate = 1f / timeFromMaxToMin;

		baseRotation = transform.rotation;
	}

	void Update() {
		if(trauma > 0) {
			shake = trauma * trauma;
	
			// Compute shake angle and offset

			// Perlin noise based off current time. 
			offset.x = Mathf.PerlinNoise(Time.time * xTimeScale, 0.0f);
			offset.y = Mathf.PerlinNoise(0.0f, Time.time * yTimeScale);
			var zstuff = Time.time * zTimeScale;
			offset.z = Mathf.PerlinNoise(zstuff, zstuff);
			
			// Change range of number from (0,1) to (-1, 1) and scales by appropriate values.
			offset.x =  maxAngle * shake * (offset.x - 0.5f) * 2f;
			offset.y =  maxAngle * shake * (offset.y - 0.5f) * 2f;
			offset.z =  maxAngle * shake * (offset.z - 0.5f) * 2f;
	
			rotation = Quaternion.Euler(offset);	
			// Add it to camera for that frame. 
			// transform.localPosition = basePosition + offset;
			transform.rotation = baseRotation * rotation;
	
			// Update the trauma
			trauma -= traumaDrainRate * Time.deltaTime;
			trauma = Mathf.Max(trauma, 0.0f);
		}
	}		

	public void AddTrauma(float amnt) {
		trauma += amnt;
		trauma = Mathf.Min(trauma, 1.0f);
	}

	public void RemoveAllTrauma() {
		trauma = 0.0f;
	}

}