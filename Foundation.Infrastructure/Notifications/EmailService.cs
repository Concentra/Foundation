using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Foundation.Infrastructure.Notifications
{
    public class EmailService : IEmailService
    {
        private readonly IEmailMessageSender emailMessageSender;

        /// <summary>
        /// All keys in the web config that start with Email_XXXXX will be added to dictionary EmailConfigurations as (XXXXX , "Value")
        /// </summary>
        private static readonly Dictionary<string, string> EmailConfigurations =
            ConfigurationManager.AppSettings.AllKeys
                .Where(x => x.StartsWith("Email_"))
                .ToDictionary(x => x.Replace("Email_", string.Empty), x => ConfigurationManager.AppSettings.Get(x));

        public EmailService(IEmailMessageSender emailMessageSender)
        {
            this.emailMessageSender = emailMessageSender;
        }

        public void SendEmail(string to, string cc, string subject, string body)
        {
            emailMessageSender.Send(to, cc, subject, body);
        }

        public void SendEmailWithTemplateString(string to, string cc, string subject, string templateContents, object templateValues)
        {
            var body = ParseText(templateContents, templateValues);
            emailMessageSender.Send(to, cc, subject, body);
        }

        public void SendEmailWithTemplatePath(string to, string cc, string subject, string templatePath, object templateValues)
        {
            string templateContents;
            using (var stream = new StreamReader(templatePath))
            {
                templateContents = stream.ReadToEnd();
                stream.Close();
            }

            SendEmailWithTemplateString(to, cc, subject, templateContents, templateValues);
        }

        /*Utility methods for templates*/
        private static string ParseText(string input, object variables)
        {
            var result = input;

            var variablesIntheTemplate = ExtractVariables(input);
            foreach (var variableName in variablesIntheTemplate)
            {
                var variableValue = GetVariableValue(variableName, variables);

                if (variableValue != null)
                {
                    if (variableValue is DateTime)
                    {
                        var date = (DateTime)variableValue;

                        result = result.Replace(
                            "$(" + variableName + ")",
                            variableName.Contains("Time") ? date.ToShortTimeString() : date.ToShortDateString());
                    }
                    else
                    {
                        result = result.Replace("$(" + variableName + ")", variableValue.ToString());
                    }
                }
                else
                {
                    result = result.Replace("$(" + variableName + ")", string.Empty);
                }
            }

            return result;
        }

        private static IEnumerable<string> ExtractVariables(string text)
        {
            var expression = new Regex(@"\$\((?<VariableName>[a-zA-Z][a-zA-Z\.0-9]*[a-zA-Z0-9])\)");
            var matches = expression.Matches(text).Cast<Match>();

            return matches.Select(x => x.Groups["VariableName"].Value);
        }

        private static object GetVariableValue(string name, object variables)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            var variableNamePath = name.Split('.');

            if (variableNamePath.Length == 1 && EmailConfigurations.ContainsKey(variableNamePath[0]))
            {
                return EmailConfigurations[variableNamePath[0]];
            }

            var rootVariableProperty = variables.GetType().GetProperty(variableNamePath[0]);

            if (rootVariableProperty == null)
            {
                return null;
            }

            var rootVariable = rootVariableProperty.GetValue(variables, null);

            if (variableNamePath.Length == 1)
            {
                return rootVariable;
            }

            return GetVariableValue(string.Join(".", variableNamePath.Skip(1)), rootVariable);
        }
    }
}
