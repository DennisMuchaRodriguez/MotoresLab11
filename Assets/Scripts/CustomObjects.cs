using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewCustomMaterial", menuName = "ScriptableObjects/CustomMaterial", order = 1)]
public class CustomObjects : ScriptableObject
{
    public Color color;
    public float shinines;
    public Texture texture;

}
