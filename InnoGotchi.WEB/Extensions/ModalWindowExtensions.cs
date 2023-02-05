using InnoGotchi.BusinessLogic.Extensions;

namespace InnoGotchi.WEB.Extensions
{
    public enum ModalMsgType
    {
        ErrorMessage = 1,
        SuccessMessage = 2,
        JustMessage = 3,
    }

    public static class ModalWindowExtensions
    {
        public static void SetModalMessage(this HttpContext httpContext, string message, ModalMsgType modalMsgType)
        {
            var type = Enum.GetName(modalMsgType);
            if (type != null)
            {
                httpContext.Session.Set<string>(type, message);
            }
        }

        public static Dictionary<string, string> GetModalMessages(this HttpContext httpContext)
        {
            var list = new Dictionary<string, string>();
            var modalMessages = new[] { ModalMsgType.ErrorMessage, ModalMsgType.SuccessMessage, ModalMsgType.JustMessage };

            foreach (var modalMessage in modalMessages)
            {
                var type = Enum.GetName(modalMessage);
                var message = httpContext.Session.Get<string>(type)?.Replace("\"", "");

                list.Add(type, message);
            }

            httpContext.ClearModalMessages();
            return list;
        }

        public static void ClearModalMessages(this HttpContext httpContext)
        {
            foreach (ModalMsgType type in Enum.GetValues(typeof(ModalMsgType)))
            {
                var key = Enum.GetName(type);
                httpContext.Session.Remove(key);
            }
        }

        //public static void SetModalMessage(this HttpContext httpContext, string message, ModalMsgType modalMsgType)
        //{
        //    httpContext.Session.Set(Enum.GetName(modalMsgType), message);
        //}

        //public static Dictionary<string, string> GetModalMessages(this HttpContext httpContext)
        //{
        //    var list = new Dictionary<string, string>();
        //    list.Add(Enum.GetName(ModalMsgType.ErrorMessage), httpContext.Session.Get<string>(nameof(ModalMsgType.ErrorMessage))?.Replace("\"", ""));
        //    list.Add(Enum.GetName(ModalMsgType.SuccessMessage), httpContext.Session.Get<string>(nameof(ModalMsgType.SuccessMessage))?.Replace("\"", ""));
        //    list.Add(Enum.GetName(ModalMsgType.JustMessage), httpContext.Session.Get<string>(nameof(ModalMsgType.JustMessage))?.Replace("\"", ""));

        //    httpContext.ClearModalMessages();
        //    return list;
        //}

        //public static void ClearModalMessages(this HttpContext httpContext)
        //{
        //    httpContext.Session.Remove(Enum.GetName(ModalMsgType.ErrorMessage));
        //    httpContext.Session.Remove(Enum.GetName(ModalMsgType.SuccessMessage));
        //    httpContext.Session.Remove(Enum.GetName(ModalMsgType.JustMessage));
        //}
    }
}
