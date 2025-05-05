using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggle : MonoBehaviour
{
    public Canvas InventoryCanvas, MapCanvas, MenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            if (MapCanvas.enabled == true)
                MapCanvas.enabled = false;
            else MapCanvas.enabled = true;

        if (Input.GetKeyDown(KeyCode.I))
            if (InventoryCanvas.enabled == true)
                InventoryCanvas.enabled = false;
            else InventoryCanvas.enabled = true;


        if (Input.GetKeyDown(KeyCode.Escape))
            if (MenuCanvas.gameObject.activeSelf == true)
            {
                MenuCanvas.gameObject.transform.GetChild(7).gameObject.SetActive(false);
                MenuCanvas.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
            else
            {
                MenuCanvas.gameObject.SetActive(true);
                Time.timeScale = 0f;
            }
    }
}
