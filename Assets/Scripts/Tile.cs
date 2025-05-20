using System;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{   
    [SerializeField] private TileSO _tileEvent;
    [SerializeField] private int _tokenID;
    [SerializeField] private bool _isSpawnPoint;
    [SerializeField] private String _type;
    // [SerializeField] private MaterialsSO _tileMaterials;

    void OnEnable()
    {
        _tileEvent = Resources.Load<TileSO>("SOInstance/Core/Tiles");
        _tileEvent.changeMaterial.AddListener(ChangeType);
    }

    void OnDisable()
    {
        _tileEvent.changeMaterial.RemoveListener(ChangeType);
    }
    void Start() {
        _tokenID = _tileEvent.RegisterToken(transform.position); // Pass game obj to the board
    }

    void OnMouseDown()
    {
        _tileEvent.OnTileClick(transform.position, _type);
    }

    void OnMouseEnter()
    {
        _tileEvent.OnTileEnter(transform.position, _type);
    }

    void OnMouseExit()
    {   
        _tileEvent.OnTileExit(transform.position);
    }

    void ChangeType(List<int> tiles, Material material, string type)
    {
        if (tiles.Contains(_tokenID))
        {
            GetComponent<MeshRenderer>().material = material;
            _type = type;
        }
    }
}
