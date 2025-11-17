using UnityEngine;
using UnityEditor;
 
public class ResetPlayerPrefs : MonoBehaviour //Window에서 PlayerPrefs 초기화 하게 해주는 스크립트입니다.
{
    #if UNITY_EDITOR || DEVELOPMENT_BUILD
    
        [MenuItem("Window/PlayerPrefs 초기화")]
        private static void ResetPrefs()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("PlayerPrefs has been reset.");
        }
        
    #endif
}