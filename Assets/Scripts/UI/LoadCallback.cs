using UnityEngine;
/// <summary>
/// функция обратного вызова
/// </summary>
namespace UI.Windows
{ 
    public class LoadCallback : MonoBehaviour
    {
        private bool isFirstUpdate = true;

        private void Update()
        {
            if (isFirstUpdate)
            {
                isFirstUpdate = false;
                LoadLvl.LoadCallback();
            }
        }
    }
}
