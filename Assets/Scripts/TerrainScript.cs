using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TerrainScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TileBase GreenGrass;
    void Start()
    {
        gameObject.GetComponent<Tilemap>().BoxFill(new Vector3Int(-100, -100, 0), GreenGrass, -100, -100, 100, 100);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
