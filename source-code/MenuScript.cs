using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



// This class manages the menu
public class MenuScript : MonoBehaviour
{
    // Set menu open state (false at beginning)
    public static bool menuOpened = false;

    // Referencing a game object, in this case the Menu UI
    public GameObject MenuUI;

    public Slider slider;

    void Start() 
    {
        // Menu is closed by default
        Resume();
        // Calibrate sensivity slider with mouse sensivity at start
        sensitivitySlider();
    }

    // Update is called once per frame
    void Update()
    {
        // Checking if ESC-button was pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Check if menu is already opened
            if (menuOpened) {

                // Close menu
                Resume();

            } else {
                // Open menu
                Pause();
            }
        }

    }

    // Resume the scene, close menu
    public void Resume()
    {
        // Deactivate menu UI by accessing Unity API and deactivating the game object
        MenuUI.SetActive(false);

        // Resume the global time by setting it back to default (1 float)
        Time.timeScale = 1f;

        // Sets menuOpened variable to false
        menuOpened = false;

        // Locks the cursor
        Cursor.lockState = CursorLockMode.Locked;


    }

    // Pause scene, open menu
    public void Pause()
    {
        // Activate menu UI by accessing Unity API and activating the game object
        MenuUI.SetActive(true);

        // Freezes time by setting global time scale to 0f
        Time.timeScale = 0f;

        // Sets menuOpened variable to true
        menuOpened = true;

        // Unlocks the cursor
        Cursor.lockState = CursorLockMode.None;
    }

    // Quit application
    public void Quit()
    {
        // Using Unity API's quit function to close the application
        Application.Quit();
    }


    public void sensitivitySlider() 
    {

        MouseLook.mouseSensitivity = slider.value;

    }
}

