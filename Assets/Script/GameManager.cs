using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
   public GameObject Pause;
   public GameObject Item;
   public GameObject OptionsPanel;
   public bool Paused;
   public bool ItemMenuOpen = false;
   public TMP_Text VolumeText;
   public Slider VolumeSlider;

   public static float Volume;

   public void LoadScene(string sceneName)
   {
      Scene sceneToLoad = SceneManager.GetSceneByName(sceneName);
      LoadScene(sceneToLoad.buildIndex);
   }
   public void LoadScene(int sceneIndex)
   {
      SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single );
   }
   
   public void ChangeScene(string sceneName)
   {
      SceneManager.LoadScene(sceneName);
   }
   public void SetVolume(float volume)
   {
      AudioListener.volume = volume;
      Volume = VolumeSlider.value;
      VolumeText.text = VolumeSlider.value.ToString(CultureInfo.InvariantCulture);
   }

   public void LeaveGame()
   {
      Application.Quit();
   }

   public void OpenPauseMenu()
   {
      Time.timeScale = 0;
      Paused = true;
      Pause.SetActive(true);
   }

   public void ClosePauseMenu()
   {
      Time.timeScale = 1;
      Paused = false;
      Pause.SetActive(false);
   }
   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         if (Paused)
         {
            ClosePauseMenu();
         }
         else
         {
            OpenPauseMenu();
         }
      }
   }

   public void OnPlayButtonClicked()
   {
      ClosePauseMenu();
   }

   public void OnOptionsButtonClicked()
   {
      OpenOptionsMenu();
   }

   public void OpenOptionsMenu()
   {
      Pause.SetActive(false);
      OptionsPanel.SetActive(true);
   }

   public void CloseOptionsMenu()
   {
      OptionsPanel.SetActive(false);
      Pause.SetActive(false);
   }
   public void OnQuitButtonClicked()
   {
      LeaveGame();
   }
}