using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<GameObject> IslandList = new List<GameObject>();
    public List<RectTransform> IslandMap = new List<RectTransform>();
    public RectTransform playerInMap;
    public RectTransform map2dEnd;
    public Transform map3dParent;
    public Transform map3dEnd;

    private Vector3 normalized, mapped;

    private void Start()
    {
        //Sets island pins on the map
        for(int i = 0; i < IslandList.Count; i++)
        {
            normalized = Divide(
                map3dParent.InverseTransformPoint(IslandList[i].transform.position),
                map3dEnd.position - map3dParent.position
            );
            normalized.y = normalized.z;
            mapped = Multiply(normalized, map2dEnd.localPosition);
            mapped.z = 0;
            IslandMap[i].localPosition = mapped;
        }
    }

    private void Update()
    {
        //Sets scale between the game map and the UI map
        normalized = Divide(
                map3dParent.InverseTransformPoint(this.transform.position),
                map3dEnd.position - map3dParent.position
            );
        normalized.y = normalized.z;
        mapped = Multiply(normalized, map2dEnd.localPosition);
        mapped.z = 0;
        playerInMap.localPosition = mapped;

        //Flips the ship icon according to player's rotation
        if (this.transform.localEulerAngles.y > 180)
            playerInMap.transform.localScale = new Vector3(-0.773672f, 0.773672f, 0.773672f);
        else if (this.transform.localEulerAngles.y > 0 && this.transform.localEulerAngles.y < 180)
            playerInMap.transform.localScale = new Vector3(0.773672f, 0.773672f, 0.773672f);
    }

    private static Vector3 Divide(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }

    private static Vector3 Multiply(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
}
