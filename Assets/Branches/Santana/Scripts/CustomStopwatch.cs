using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Paradoxical.Core
{
    public class CustomStopwatch
    {
        private float startTime = 0f;
        private bool created = false;
        private bool stopped = false;

        public float ElapsedTimeSec()
        {
            if (!created) Restart();
            if (stopped) return 0f;

#if UNITY_EDITOR
            return (Application.isPlaying ? Time.time : (float)EditorApplication.timeSinceStartup) - startTime;
#else
            return Time.time - startTime;
#endif
        }

        public bool LessThan(float seconds) => ElapsedTimeSec() < seconds && created;

        public void Restart()
        {
            created = true;
            stopped = false;
#if UNITY_EDITOR
            startTime = Application.isPlaying ? Time.time : (float)EditorApplication.timeSinceStartup;
#else
            startTime = Time.time;
#endif
        }

        public void Stop() => stopped = true;
    }
}