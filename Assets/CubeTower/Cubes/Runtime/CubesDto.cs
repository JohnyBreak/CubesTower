using System;
using System.Collections.Generic;
using CubeTower.Cubes.Runtime;
using Newtonsoft.Json;

[Serializable]
[JsonObject(MemberSerialization.Fields)]
public class CubesDto
{
    [JsonProperty("sprite_sheet_name")] private string _spriteSheetName;
    [JsonProperty("cubes")] private List<CubeDto> _cubeDtos;

    public string SpriteSheetName => _spriteSheetName;
    public List<CubeDto> Dtos => _cubeDtos;
}
