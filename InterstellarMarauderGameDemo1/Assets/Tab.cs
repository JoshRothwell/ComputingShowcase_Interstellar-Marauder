using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ControlsPanel; // The prefab of the panel we want to spawn.

    private GameObject spawnedPanel; // The panel instance that we spawn.

    public void SpawnPanel()
    {
        // If there's already a panel spawned, destroy it before spawning a new one.
        if (spawnedPanel != null)
        {
            Destroy(spawnedPanel);
        }

        // Spawn the panel prefab and make it a child of the canvas.
        spawnedPanel = Instantiate(ControlsPanel, transform.parent);
        spawnedPanel.transform.SetAsLastSibling(); // Move the panel to the top of the canvas hierarchy.

        // Set the panel's position and size to fill the canvas.
        RectTransform panelRect = spawnedPanel.GetComponent<RectTransform>();
        panelRect.anchorMin = Vector2.zero;
        panelRect.anchorMax = Vector2.one;
        panelRect.sizeDelta = Vector2.zero;
        panelRect.anchoredPosition = Vector2.zero;
    }
}