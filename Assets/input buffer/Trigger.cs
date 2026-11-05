using System;
using UnityEngine;

namespace input_buffer
{
    [Serializable]
    public struct Trigger 
    {
        [SerializeField] private float _endtime;
    
        private bool _hasBeenTriggered;
        private const float Duration = 0.3f;

        public bool Value => Time.time < _endtime && _hasBeenTriggered;
        
        public void launch()
        { 
            _endtime = Time.time + Duration;
            _hasBeenTriggered = true;
        }

        public void stop()
        {
            _endtime = Time.time;
        }
    }
}