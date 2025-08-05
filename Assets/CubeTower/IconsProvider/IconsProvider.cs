using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

    public static class IconsProvider
    {
        private static Dictionary<string, Sprite> _spritesMap = new();

        public static Sprite GetSprite(string spriteSheetName, string spriteName)
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

            var loadedSprite = Addressables.LoadAssetAsync<Sprite>(fullName).WaitForCompletion();

            _spritesMap[fullName] = loadedSprite;

            return loadedSprite;
        }
    }

