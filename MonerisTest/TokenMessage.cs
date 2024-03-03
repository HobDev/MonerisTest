

using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MonerisTest
{
    public class TokenMessage : ValueChangedMessage<string>
    {
        public TokenMessage(string value) : base(value)
        {
        }
    }
}
