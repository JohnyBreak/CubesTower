using System.Collections.Generic;
using UnityEngine;

    public class IconsProvider
    {
        private readonly AssetProvider _assetProvider;
        private Dictionary<string, Sprite> _spritesMap = new();

        public IconsProvider(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        public Sprite GetSprite(string spriteSheetName, string spriteName)
        {
            if (string.IsNullOrEmpty(spriteSheetName) || string.IsNullOrEmpty(spriteName))
            {
                Debug.LogError("[IconsProvider] spriteSheetName or spriteName is invalid");
                return null;
            }

            var fullName = $"{spriteSheetName}[{spriteName}]";

            if (_spritesMap.TryGetValue(fullName, out var sprite))
            {
                return sprite;
            }

            var loadedSprite = _assetProvider.LoadAssetSync<Sprite>(fullName);

            _spritesMap[fullName] = loadedSprite;

            return loadedSprite;
        }
    }

