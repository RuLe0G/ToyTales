using UnityEngine;
/// <summary>
/// ������� ��������� ������
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
