using System.Linq;
using UnityEngine;

public class DisappearingTileManager : MonoBehaviour
{
    private DisappearingTile[] m_disappearingTiles;

    void Awake()
    {
        GameObject[] tileObjects = GameObject.FindGameObjectsWithTag("DisappearingTile");
        m_disappearingTiles = tileObjects.Select(obj => obj.GetComponent<DisappearingTile>()).ToArray();
    }

    public void EnableDisappearingTiles()
    {
        // Implementation to enable disappearing tiles
        Debug.Log("Disappearing tiles enabled.");
        foreach (var tile in m_disappearingTiles)
        {
            tile.EnableTile();
        }
    }

    public void DisableDisappearingTiles()
    {
        // Implementation to disable disappearing tiles
        Debug.Log("Disappearing tiles disabled.");
        foreach (var tile in m_disappearingTiles)
        {
            tile.DisableTile();
        }
    }

    public void ToggleDisappearingTiles()
    {
        // Implementation to toggle disappearing tiles
        Debug.Log("Disappearing tiles toggled.");
        foreach (var tile in m_disappearingTiles)
        {
            tile.ToggleTileState();
        }
    }
}
