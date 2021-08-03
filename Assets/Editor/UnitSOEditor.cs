using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UnitSO))]
public class UnitSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        UnitSO uso = (UnitSO)target;

        uso.type = (UnitType)EditorGUILayout.EnumPopup("Select type", uso.type);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Speed");
        uso.speed = EditorGUILayout.FloatField(uso.speed);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Health");
        uso.defaultHealth = EditorGUILayout.IntField(uso.defaultHealth);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Damage");
        uso.defaultDamage = EditorGUILayout.IntField(uso.defaultDamage);
        EditorGUILayout.EndHorizontal();

        if (uso.type == UnitType.Player)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Player num");
            uso.playerNum = EditorGUILayout.IntSlider(uso.playerNum, 1, 2);
            EditorGUILayout.EndHorizontal();
        }
        else if(uso.type == UnitType.Enemy)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Spawn bonus");
            uso.spawnBonus = EditorGUILayout.Toggle(uso.spawnBonus);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Score reward");
            uso.scoreReward = EditorGUILayout.IntField(uso.scoreReward);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Move time");
            uso.moveTime = EditorGUILayout.FloatField(uso.moveTime);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Shoot interval");
            uso.shootInterval = EditorGUILayout.FloatField(uso.shootInterval);
            EditorGUILayout.EndHorizontal();
        }

        EditorUtility.SetDirty(target);
    }
}
