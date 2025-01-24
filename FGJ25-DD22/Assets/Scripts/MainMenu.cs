using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     //Way to load the game after pressing playbutton
   public void PlayGame(){
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
   }

   //Way to quit the game after pressing quit
   public void Quit(){
    Application.Quit();
   }
}
