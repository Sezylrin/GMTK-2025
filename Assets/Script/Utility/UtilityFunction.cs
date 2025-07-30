using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityFunction
{
    public static Vector3 Vector2ToFlatVector3(Vector2 vec2)
    {
        return new Vector3(vec2.x,0,vec2.y);
    }

    public static Vector2 Vector3ToFlatVector2(Vector3 vec3)
    {
        return new Vector2(vec3.x, vec3.z);
    }
}
