﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Foundation.Configuration;
using Foundation.Infrastructure.Notifications;
using Foundation.Web.Extensions;

namespace Foundation.Web
{
    public class WebFlashMessenger : IFlashMessenger
    {
        private readonly ResourceManager resourceManager;
        private readonly Dictionary<FlashMessageType, Queue<string>> messages;

        public WebFlashMessenger(IResourcesLocator resourcesLocator)
        {
            this.resourceManager = resourcesLocator.FlashMessagesResourceManager;
            
            this.messages = new Dictionary<FlashMessageType, Queue<string>>();

            foreach (FlashMessageType flashMessageType in Enum.GetValues(typeof(FlashMessageType)))
            {
                this.messages[flashMessageType] = new Queue<string>();
            }
        }

        public void AddMessageByKey(string resourceKey ,FlashMessageType messageType)
        {
            if (!string.IsNullOrWhiteSpace(resourceKey))
            {
                var message = this.resourceManager.GetString(resourceKey);

                if (!string.IsNullOrWhiteSpace(message))
                {
                    var messageToDisplay = message;
                    this.AddMessage(messageToDisplay, messageType);
                }
            }
        }

        public void AddMessage(string message, FlashMessageType messageType)
        {
            this.messages[messageType].Enqueue(message);
        }

        public string RenderFlashMessages()
        {
            var sb = new StringBuilder();

            if (this.HasMessages())
            {
                foreach (var messageType in Enum.GetValues(typeof (FlashMessageType)))
                {
                    var message = RenderFlashMessagesForType((FlashMessageType) messageType);

                    if (message != null)
                    {
                        sb.Append(message);
                    }
                }
            }

            return sb.ToString();
        }

        public string RenderFlashMessagesForType(FlashMessageType messageType)
        {
                var messagesToRender = GetMessagesForType(messageType);

                if (messagesToRender.Any())
                {
                    var builder = new TagBuilder("div");
                    builder.AddCssClass("alert alert-" + EnumExtensions.GetEnumDescription(messageType).ToLower());
                    builder.InnerHtml = string.Join(".", messagesToRender);
                    return builder.ToString(TagRenderMode.Normal);
                }
            

            return null;
        }

        public IEnumerable<string> GetMessagesForType(FlashMessageType messageType)
        {
            return this.messages[messageType].AsEnumerable();
        }

        public bool HasMessages()
        {
            return this.messages.Any(item => this.messages[item.Key].Any());
        }
    }
}