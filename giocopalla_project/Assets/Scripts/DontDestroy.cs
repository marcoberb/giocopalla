using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public static GameObject[] objs;

    private void Awake()
    {
        //selects all game objects with "music" tag
        objs = GameObject.FindGameObjectsWithTag("music");
        if (objs.Length > 1)
            //destroys every other object created
            Destroy(this.gameObject);
        else
            //doesn't destroy the first object created (when changing scenes)
            DontDestroyOnLoad(this.gameObject);
    }
}