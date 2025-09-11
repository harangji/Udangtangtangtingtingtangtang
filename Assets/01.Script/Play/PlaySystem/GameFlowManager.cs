using System;
using TMPro;
using UnityEngine;

namespace _01.Script.Play.PlaySystem
{
    public class GameFlowManager : MonoBehaviour
    {
        public TMP_Text GameFlowText;
        
        private void Start()
        {
            InGameEventHandler.Instance.CheakGameEndHandler += GetIsGameOver;
        }
        
        public void GetIsGameOver(EColliderCamp camp)
        {
            Time.timeScale = 0f;

            GameFlowText.text = camp switch
            {
                EColliderCamp.AllyCamp => "Lose",
                EColliderCamp.EnemyCamp => "Win",
                EColliderCamp.None => "Draw",
                _ => GameFlowText.text
            };

            GameFlowText.gameObject.SetActive(true);
        }
    }
}