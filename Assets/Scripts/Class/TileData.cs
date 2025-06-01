public class TileData
{
    private string _terrain; // field
    public string Terrain // property
    {
        get { return _terrain; }
    }

    private int _cost; // field
    public int Cost // property
    {
        get { return _cost; }
    }

    public TileData(string terrain, int cost)
    {
        _terrain = terrain;
        _cost = cost;
    }

}