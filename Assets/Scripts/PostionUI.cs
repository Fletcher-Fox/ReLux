using UnityEngine;
using TMPro;


public class PostionUI : MonoBehaviour
{   
    [SerializeField] private TileSO _tileEvent;
    [SerializeField] private TextMeshProUGUI posField;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _tileEvent.tileClick.AddListener(onTileClick);
    }

    void OnEnable()
    {
        _tileEvent.tileClick.AddListener(onTileClick);
    }

    void OnDisable()
    {
        _tileEvent.tileClick.AddListener(onTileClick);
    }

    void onTileClick(Vector3 tilePosition)
    {
        posField.text = string.Format("Pos: [{0},{1}]", tilePosition.x.ToString(), tilePosition.z.ToString());
    }
}
