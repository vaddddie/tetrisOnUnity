using UnityEngine;

public static class DataHolder
{
    private static bool prefabName;

    public static void Set(bool value)
    {
        prefabName = value;
    }

    public static bool Get()
    {
        return prefabName;
    }
}
