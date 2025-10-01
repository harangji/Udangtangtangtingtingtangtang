using UnityEngine;

namespace _01.Script.System.SceneManagement
{
    // UI 버튼에 연결하여 씬 전환을 쉽게 할 수 있도록 돕는 스크립트입니다.
    public class SceneLoadButton : MonoBehaviour
    {
        [Tooltip("인스펙터에서 직접 로드할 씬의 이름을 지정할 때 사용합니다.")]
        public string sceneToLoad;

        // 버튼의 OnClick() 이벤트에서 문자열 인자를 직접 받아 씬을 로드하는 함수입니다.
        // 레벨 선택 화면 등에서 여러 버튼이 하나의 함수를 공유할 때 유용합니다.
        public void LoadSceneByName(string sceneName)
        {
            if (SceneLoader.Instance != null)
            {
                SceneLoader.Instance.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError("SceneLoader 인스턴스를 찾을 수 없습니다! 씬에 SceneLoader가 있는지 확인해주세요.");
            }
        }

        // 버튼의 OnClick() 이벤트에서 인스펙터에 지정된 씬(sceneToLoad)을 로드하는 함수입니다.
        // '시작하기', '옵션' 등 단일 기능 버튼에 유용합니다.
        public void LoadSceneFromField()
        {
            if (string.IsNullOrEmpty(sceneToLoad))
            {
                Debug.LogError("로드할 씬 이름이 지정되지 않았습니다!", this);
                return;
            }

            if (SceneLoader.Instance != null)
            {
                SceneLoader.Instance.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogError("SceneLoader 인스턴스를 찾을 수 없습니다! 씬에 SceneLoader가 있는지 확인해주세요.");
            }
        }
    }
}