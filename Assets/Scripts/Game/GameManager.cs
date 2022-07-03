using UnityEngine;
using JoyWayTest.Core;

namespace JoyWayTest.Game
{
    //Менеджер для сцены с расширенной функциональностью, наследуется от CoreManager
    public class GameManager : CoreManager
    {
        [SerializeField]
        private GameInterface gameInterface;
        [SerializeField]
        private Scarecrow scarecrow;

        public override void Start()
        {
            base.Start();
            gameInterface.Initialize(scarecrow);
            scarecrow.Initialize(PlayerInput);
        }
    }
}