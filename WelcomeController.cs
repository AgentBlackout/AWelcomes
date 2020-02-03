using Mirror;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AWelcomes
{
    public class WelcomeController : MonoBehaviour
    {
        public IEnumerable<Message> Messages { get; internal set; }

        private Stack<Message> _messageStack;
        private float _nextCycle = 0f;
        private Message _curMessage;

        private Broadcast _broadcast;
        private NetworkConnection _connection;

        private void Start()
        {
            WelcomesMod.Instance.Info($"InitWC");

            _nextCycle = Time.time;
            _messageStack = new Stack<Message>(Messages.Reverse());

            WelcomesMod.Instance.Info($"{_messageStack.Count}");

            _broadcast = GetComponent<Broadcast>();
            _connection = ReferenceHub.GetHub(gameObject).playerStats.connectionToClient;
        }

        private void Update()
        {
            if (Time.time < _nextCycle) return; //Not time to update. 

            if (_messageStack.Count == 0) Destroy(this); //Stack empty

            _curMessage = _messageStack.Pop();

            WelcomesMod.Instance.Info($"{_nextCycle} - {_curMessage.Content} - {_curMessage.Period}");

            _nextCycle = Time.time + _curMessage.Period;

            _broadcast.TargetAddElement(_connection, _curMessage.Content, (uint)_curMessage.Period, _curMessage.Monospaced);
        }


    }
}
