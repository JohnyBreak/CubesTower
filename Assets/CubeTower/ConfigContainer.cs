using Serialization;
using UnityEngine;

public class ConfigContainer : IInitableEntity
{
    private const string _configKey = "CubesConfig";
    
    private readonly AssetProvider _assetProvider;
    private readonly IConfigReader _configReader;
    private CubesDto _cubesDto;
    
    public CubesDto CubesDtos => _cubesDto;
    
    public ConfigContainer(
        AssetProvider assetProvider,
        IConfigReader configReader)
    {
        _assetProvider = assetProvider;
        _configReader = configReader;
    }

    public int GetOrder()
    {
        return -5;
    }

    public void Init()
    {
        ReadConfig();
    }
    
    private void ReadConfig()
    {
        var text = _assetProvider.LoadAssetSync<TextAsset>(_configKey);
        if (text == null)
        {
            Debug.LogError("CubesDto json not found");
            return;
        }

        var result = _configReader.Read<CubesDto>(text.text);

        if (result == null)
        {
            Debug.LogError("CubesDto is not exist");
            return;
        }

        _cubesDto = result;
    }
}
