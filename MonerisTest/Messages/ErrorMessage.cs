

namespace MonerisTest.Messages
{
    public class ErrorMessage : ValueChangedMessage<string>
    {

        public ErrorMessage(string value) : base(value)
        {
        }

    }
}
