namespace Tumbaga.Messaging
{
    public interface IMessagePublisher
    {
        void RegisterWeak<T>(IMessageReceiver<T> receiver) where T : BaseMessage;

        void Register<T>(IMessageReceiver<T> receiver) where T : BaseMessage;

        void Unregister<T>(IMessageReceiver<T> receiver) where T : BaseMessage;

        void Publish<T>(T messageContent) where T : BaseMessage;
    }
}
