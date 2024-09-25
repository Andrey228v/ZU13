using UnityEngine;

namespace Assets.Scripts.Service
{
    public class UserInputKeybord : MonoBehaviour, IUserInput
    {
        public bool Jump {  get; set; }
        public bool Attack { get; set; }

        private void Update()
        {
            Jump = Input.GetKeyDown(KeyCode.Space);
            Attack = Input.GetMouseButtonDown(0);
        }
    }
}
