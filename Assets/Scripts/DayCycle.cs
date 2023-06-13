using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public float nightFogDensity = 0.02f;
    public int rotationScale = 10;

    private void Update()
    {
        transform.Rotate(rotationScale * Time.deltaTime, 0, 0);
        if (transform.rotation.eulerAngles.x > 180 && transform.rotation.eulerAngles.x < 360) 
            RenderSettings.fogDensity = nightFogDensity; 
        else 
            RenderSettings.fogDensity = 0f; 
    }
}