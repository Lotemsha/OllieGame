using UnityEngine;

public static class PlayerLocationManager
{
    // המשתנה שזוכר לאן השחקן צריך להשתגר בסצנה הבאה
    public static Vector3 nextSpawnPoint = Vector3.zero;

    // זוכר לאן השחקן צריך להסתכל (למשל X:0, Y:-1 כדי להסתכל למטה)
    public static Vector2 lookDirection = new Vector2(0, -1);

    // האם השחקן בפנים או בחוץ (כדי להדליק/לכבות את בר החיים)
    public static bool isInside;
}
