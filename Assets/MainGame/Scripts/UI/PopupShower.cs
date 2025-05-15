using UnityEngine;

namespace SampleGame
{
    public sealed class PopupShower : MonoBehaviour
    {
        [SerializeField]
        private GameObject _winPanel;

        [SerializeField]
        private GameObject _losePanel;

        public void SetActiveWinPanel(bool state)
        {
            _winPanel.SetActive(state);            
        }

        public void SetActiveLosePanel(bool state)
        {
            _losePanel.SetActive(state);
        }

    }
}
