using UnityEngine;
using TMPro;


public class PostionUI : MonoBehaviour
{   
    [SerializeField] private TileSO tile_event;
    [SerializeField] private TextMeshProUGUI posField;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tile_event.TileClick += onTileClick;
    }

    void OnEnable()
    {
        tile_event.TileClick += onTileClick;
    }

    void OnDisable()
    {
        tile_event.TileClick -= onTileClick;
    }

    void onTileClick(GameObject tile)
    {
        posField.text = string.Format("Pos: [{0},{1}]", tile.transform.position.x.ToString(), tile.transform.position.z.ToString());
    }
}
