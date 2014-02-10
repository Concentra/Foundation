using System;

namespace Foundation.Configuration
{
    /// <summary>
    /// Configuration Class for Foundation Business Layer Features
    /// </summary>
    public class BusinessConfigurations
    {
        /// <summary>
        /// the type of Bussiness Invocation Logger. The method "Log" of this class will be called before every method in the business manager.
        ///  This should be a type of A class that implements the interface IBusinessManagerInvocationLogger.
        /// </summary>
        public Type BusinessInvocationLogger { get; set; }
        
        /// <summary>
        /// the type of Email Logger. The method "LogEmail" of this class will be called for every email that been sent through foundation email service.
        ///  This should be a type of A class that implements the interface IEmailLogger.
        /// </summary>
        public Type EmailLogger { get; set; }
    }
}