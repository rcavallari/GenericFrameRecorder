using System.IO;
using UnityEngine;
using UnityEngine.FrameRecorder;

namespace UnityEditor.FrameRecorder
{
    class FRPackagerPaths : ScriptableObject
    {
        public static string GetFrameRecorderRootPath()
        {
            var path = GetFrameRecorderPath();
            path = path.Substring(path.IndexOf("Assets"));
            return path;
        }

        public static string GetFrameRecorderVersionFilePath()
        {
            var dummy = ScriptableObject.CreateInstance<RecorderVersion>();
            var path = Application.dataPath + AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(dummy)).Substring("Assets".Length);
            UnityHelpers.Destroy(dummy);
            return path;
        }

        public static string GetFrameRecorderPath()
        {
            var dummy = ScriptableObject.CreateInstance<FRPackagerPaths>();
            var path = Application.dataPath + AssetDatabase.GetAssetPath(MonoScript.FromScriptableObject(dummy)).Substring("Assets".Length);
            UnityHelpers.Destroy(dummy);

            path= path.Replace("/Packager/Editor/FRPackagerPaths.cs", "");
            return path.Substring(0, path.LastIndexOf("/"));
        }

    }
}