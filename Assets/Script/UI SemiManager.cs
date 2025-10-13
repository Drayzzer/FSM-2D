using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UISemiManager : MonoBehaviour
{
        public TMP_Text VolumeText;
        public Slider VolumeSlider;

        public static float Volume;
        
        private void Update()
        {
            Volume = VolumeSlider.value;
            VolumeText.text = VolumeSlider.value.ToString(CultureInfo.InvariantCulture);
        }

        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void Quit()
        {
            Application.Quit();
        }
}

