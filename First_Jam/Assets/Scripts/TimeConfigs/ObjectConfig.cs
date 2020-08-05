using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewObjectConfig", menuName ="Configs/Object COnfig")]
public class ObjectConfig : ScriptableObject
{
    public Vector3[] positions, rotations;
    
}
