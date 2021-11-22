using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class manages the sun's rotation
public class DayCycle : MonoBehaviour
{
    // Referencing a game object, in this case the directional light object which simulates the sun
    public GameObject sun;

    // Referencing the different skybox materials
    public Material skyboxDay;
    public Material skyboxTwilight;

    // One 24h sun cycle in minutes
    public float CycleTimeInMinutes = 6;
    // Cycle speedup as relative float multiplier
    private float cycleSpeedup;
    // Current ingame time in hours, starts at 6 AM
    private float ingameTimeInHours = 6f;
    // Current skybox name (day, twilight)
    private string currentSkyboxName; 
    // Current skybox material (day, twilight)
    private Material currentSkybox;
    // Pervious skybox material (day, twilight)
    private Material previousSkybox;
    // Fade percentage for skybox transitions (1:100 scale)
    private float fadeLerp = 0.01f;
    // Fade duration for skybox transitions in seconds
    private float fadeDuration = 8f;


    // Start is called before the first frame update
    void Start() {
        // Set relative cycleSpeedup by transforming CycleTimeInMinutes
        cycleSpeedup = (24*60)/CycleTimeInMinutes;
        // Set starting skyboxes
        currentSkyboxName = "twilight"; // Twilight
        currentSkybox = Instantiate(skyboxTwilight); // Twilight
        previousSkybox = Instantiate(skyboxDay); // Day

        RenderSettings.skybox = currentSkybox; // Set current skybox


    }


    // Update is called once per frame
    void Update()
    {   
        // Sun object has to make a whole 360 degree rotation during the cycletime
        // Because we don't want to rely on the framerate, we use deltaTime for calculating the sun's rotation
        // Time.deltaTime is the interval in seconds from the last frame to the current one

        // Normalize to degree per second, then multiply it with deltaTime in order to get the rotation in degrees
        float xRotation = 360/(CycleTimeInMinutes*60) * Time.deltaTime;

        // Unity differences between a world and a local coordinate system
        // Use the local coordinate system for the rotation by passing the paramter Space.Self
        // Only rotate around the local x axis, so y and z are 0f
        sun.transform.Rotate(xRotation, 0f, 0f, Space.Self);


        // Increase the ingame time in hours accordingly by the speedup factor
        // Framerate independent because of deltaTime
        ingameTimeInHours += cycleSpeedup*(Time.deltaTime/60/60);

        // Reset ingame time variable at 24h
        if(ingameTimeInHours > 24f) {
            ingameTimeInHours -= 24f;
        }

        // Manage skyboxes according to ingame time

        // Switch skybox to day between 7 and 16
        if(currentSkyboxName != "day" && ingameTimeInHours > 7f && ingameTimeInHours < 16f) {
            currentSkybox = Instantiate(skyboxDay);
            previousSkybox = Instantiate(skyboxTwilight);
            currentSkyboxName = "day";
            fadeLerp = 0f;
            print("Twilight to day");
        // Switch skybox to twilight between 16 and 7
        } else if(currentSkyboxName != "twilight" && (ingameTimeInHours > 16f || ingameTimeInHours < 7f)) {
            currentSkybox = Instantiate(skyboxTwilight);
            previousSkybox = Instantiate(skyboxDay);
            currentSkyboxName = "twilight";
            fadeLerp = 0f;
            print("Day to twilight");
        } 
        // Info: once upon a time there were three different skyboxes, but after seemingly unfixable
        // transition issues between the night and twilight phases we decided to just use the current two

        // Fade skybox transitions
        if(fadeLerp < 0.01f) {
           
            // Lerp is a linear interpolation between two values (in this case 0 and 0.01, 1:100 scale)
            fadeLerp += Time.deltaTime/(fadeDuration*100);
            // Interpolate between the previous and the new current skybox with the fadeLerp value
            RenderSettings.skybox.Lerp(RenderSettings.skybox, currentSkybox, fadeLerp);
        }

    }
}
