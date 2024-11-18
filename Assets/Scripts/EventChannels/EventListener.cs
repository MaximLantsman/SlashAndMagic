using UnityEngine;
using UnityEngine.Events;

namespace EventChannels
{
    public abstract class EventListener<T> : MonoBehaviour
    {
        [SerializeField]private EventChannel<T> eventChannel;
        [SerializeField]private UnityEvent<T> unityEvent;

        private protected void Awake()
        {
            eventChannel.Register(this);
        }

        private protected void OnDestroy()
        {
            eventChannel.Unregister(this);
        }

        public void Raise(T value)
        {
            unityEvent?.Invoke(value);
        }
    }

    public class EventListener : EventListener<Empty> { }
}