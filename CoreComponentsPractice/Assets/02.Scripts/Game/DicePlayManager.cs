using DiceGame.UI;
using DiceGame.Character;
using System;
using System.Collections;
using UnityEngine;
using DiceGame.Level;

namespace DiceGame.Game
{
    public class DicePlayManager : MonoBehaviour
    {
        public static DicePlayManager instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GameObject(nameof(DicePlayManager)).AddComponent<DicePlayManager>();

                return _instance;
            }
        }
        private static DicePlayManager _instance;

        public int diceNumber
        {
            get => _diceNumber;
            set
            {
                if (_diceNumber == value)
                    return;

                _diceNumber = value;
                onDiceNumberChanged?.Invoke(_diceNumber);
            }
        }

        private int _diceNumber;
        private bool _isCorouting;
        public event Action<int> onDiceNumberChanged;
        public event Action onRollingDiceStarted;
        public event Action onRollingDiceFinished;


        public void RollADice()
        {
            if (_isCorouting)
                return;

            onRollingDiceStarted?.Invoke();
            int diceValue = UnityEngine.Random.Range(1, 7);
            _isCorouting = true;
            StartCoroutine(C_RollADice(diceValue));
        }

        IEnumerator C_RollADice(int diceValue)
        {
            yield return StartCoroutine(DiceRollingAnimationUI.instance.C_Animation(diceValue));
            BoardGameMap.nodes[PlayerController.instance.nodeIndex].OnDiceRolled(diceValue);
            yield return StartCoroutine(PlayerController.instance.C_Move(diceValue));
            BoardGameMap.nodes[PlayerController.instance.nodeIndex].OnPlayerHere();
            onRollingDiceFinished?.Invoke();
            _isCorouting = false;
        }
    }
}