using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData
{
    [JsonProperty] private float positionX;
    [JsonProperty] private float positionY;
    [JsonProperty] private float positionZ;
    [JsonProperty] private float rotationX;
    [JsonProperty] private float rotationY;
    [JsonProperty] private float rotationZ;

    public CharacterData(Vector3 position, Vector3 rotation)
    {
        positionX = position.x;
        positionY = position.y;
        positionZ = position.z;
        rotationX = rotation.x;
        rotationY = rotation.y;
        rotationZ = rotation.z;
    }

    [JsonIgnore]
    public Vector3 Position => new Vector3(positionX, positionY, positionZ);
    [JsonIgnore]
    public Vector3 Rotation => new Vector3(rotationX, rotationY, rotationZ);

}
