﻿using InnoGotchi.WEB.Utility;

namespace InnoGotchi.WEB.Models
{
    public class ModalWindowModel
    {
        public ModalMsgType ModalMsgType { get; set; }
        public string Color { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public ModalWindowModel(ModalMsgType modalMsgType, string message)
        {
            ModalMsgType = modalMsgType;
            Message = message;
            SetInfo();

        }

        private void SetInfo()
        {
            switch (ModalMsgType)
            {
                case ModalMsgType.ErrorMessage:
                    Color = "#f55549";
                    Title = "Error!";
                    break;
                case ModalMsgType.SuccessMessage:
                    Color = "#15b34c";
                    Title = "Success!";
                    break;
                case ModalMsgType.JustMessage:
                    Color = "#369ab5";
                    Title = "Message";
                    break;
            }
        }
    }
}
