using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShootingTurret))]
public class ShootingTurretEditor : Editor
{
    ShootingTurret turret;
    SerializedProperty range;    
    SerializedProperty fireRate;    
    SerializedProperty damage;    
    SerializedProperty description;    

    SerializedProperty type;

    SerializedProperty bulletPrefab;
    SerializedProperty animator;

    SerializedProperty lineRenderer;
    SerializedProperty impactEffect;
    SerializedProperty impactLight;
    SerializedProperty laserSpeed;
    SerializedProperty firePoint;
    SerializedProperty enemyTag;

    private void OnEnable()
    {
        turret = target as ShootingTurret;

        range = serializedObject.FindProperty("range");
        fireRate = serializedObject.FindProperty("fireRate");
        damage = serializedObject.FindProperty("damage");
        description = serializedObject.FindProperty("description");

        type = serializedObject.FindProperty("type");
        enemyTag = serializedObject.FindProperty("enemyTag");
        firePoint = serializedObject.FindProperty("firePoint");
        bulletPrefab = serializedObject.FindProperty("bulletPrefab");
        animator = serializedObject.FindProperty("animator");
        lineRenderer = serializedObject.FindProperty("lineRenderer");
        impactEffect = serializedObject.FindProperty("impactEffect");
        impactLight = serializedObject.FindProperty("impactLight");
        laserSpeed = serializedObject.FindProperty("laserSpeed");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUILayout.BeginVertical();

        //[Header("General")]
        EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(range);
        EditorGUILayout.PropertyField(fireRate);
        EditorGUILayout.PropertyField(damage);
        EditorGUILayout.PropertyField(description);

        EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(enemyTag);
        EditorGUILayout.PropertyField(firePoint);

        EditorGUILayout.PropertyField(type);
        if (turret.type == ShootingTurret.TurretType.ShootingBullets)
        {
            EditorGUILayout.LabelField("Use bullets", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(bulletPrefab);
            EditorGUILayout.PropertyField(animator);
        }
        else if (turret.type == ShootingTurret.TurretType.Laser)
        {
            EditorGUILayout.LabelField("Use laser", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(lineRenderer);
            EditorGUILayout.PropertyField(impactEffect);
            EditorGUILayout.PropertyField(impactLight);
            EditorGUILayout.PropertyField(laserSpeed);
        }

        GUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
