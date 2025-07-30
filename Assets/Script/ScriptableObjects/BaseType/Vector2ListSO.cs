using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Vector2List Reference", menuName = "ScriptableObjects/Types/Vector2List")]

public class Vector2ListSO : ScriptableObject
{
    public List<Vector2> Vector2 = new List<Vector2>();
}
