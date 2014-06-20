using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
//using DimitriVranken;
using System.Xml;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace VrankenBischof.Docxes.BusinessLogic {

    // TODO: Document code of Logging
    // TODO: Implement logging

    /// <summary>
    /// This class only exists for the creation of the class diagram. It will probably be replaced later on.
    /// </summary>
    internal sealed class Logging {

        //private static XmlLogWriter _exceptionLogWriter;

        //private static XmlLogWriter _fallbackExceptionLogWriter;
        //private static XmlLogWriter ExceptionLogWriter {
        //    get {
        //        if (_exceptionLogWriter == null) {
        //            _exceptionLogWriter = new XmlLogWriter(Path.Combine(BusinessLogic.DirectoryManager.GetLoggingDirectory, "ExceptionLog-" + DateAndTime.Now.Date + ".xml")) { DefaultElementType = ElementType.Exception };
        //        }

        //        return _exceptionLogWriter;
        //    }
        //}

        //private static XmlLogWriter FallbackExceptionLogWriter {
        //    get {
        //        if (_fallbackExceptionLogWriter == null) {
        //            string SystemRoot = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
        //            // Most likely "C:\"
        //            _fallbackExceptionLogWriter = new XmlLogWriter(Path.Combine(SystemRoot, "Spitas Synchronisation Logs (Fallback)\\", "ExceptionLog-" + DateAndTime.Now.Date + ".xml")) { DefaultElementType = ElementType.Exception };
        //        }

        //        return _fallbackExceptionLogWriter;
        //    }
        //}

        //private static MailMessage CreateReportingMailMessage(string subject, string body, int number) {
        //    if (ConfigurationReader.GetReportingEMailAddressDistributor == null || ConfigurationReader.GetReportingEMailAddressReceiver(number) == null) {
        //        return null;
        //    }

        //    MailMessage MailMessage = new MailMessage(ConfigurationReader.GetReportingEMailAddressDistributor, ConfigurationReader.GetReportingEMailAddressReceiver(number));
        //    var _with1 = MailMessage;
        //    _with1.BodyEncoding = Encoding.UTF8;
        //    _with1.Priority = MailPriority.Normal;

        //    _with1.Subject = subject;
        //    _with1.Body = body;

        //    return MailMessage;
        //}



        //private static void LogExceptionLocally(Exception exception) {
        //    XmlLogEntry LogEntry = new XmlLogEntry(exception);

        //    try {
        //        ExceptionLogWriter.WriteEntry(LogEntry);
        //    }
        //    catch (Exception ex) {
        //        FallbackExceptionLogWriter.WriteEntry(LogEntry);
        //    }
        //}

        //private static void LogExceptionRemotely(Exception exception) {
        //    const dynamic Subject = "Spitas Synchronisation - Exception Report [auto-generated]";
        //    dynamic Body = "You got this message because an exception occured in the application \"Spitas Synchronisation\"." + Environment.NewLine + "The developers at Kallysoft were informed about this and are working on a solution." + Environment.NewLine + Environment.NewLine + "---------------" + Environment.NewLine + Environment.NewLine + exception.ToRichString();

        //    SmtpClient SmtpClient = new SmtpClient(ConfigurationReader.GetReportingSmtpServer) { EnableSsl = true };
        //    SmtpClient.Credentials = new NetworkCredential(ConfigurationReader.GetReportingEMailAddressDistributor, ConfigurationReader.GetReportingEMailAddressDistributorPassword);

        //    // Send mail messages
        //    for (int MailMessagePosition = 1; MailMessagePosition <= 5; MailMessagePosition++) {
        //        try {
        //            dynamic MailMessage = CreateReportingMailMessage(Subject, Body, MailMessagePosition);

        //            if ((MailMessage != null)) {
        //                SmtpClient.Send(MailMessage);
        //            }
        //        }
        //        catch (Exception ex) {
        //            // Ignore
        //        }
        //    }
        //}


        static internal void Log(Exception exception) {
            //// Locally
            //try {
            //    LogExceptionLocally(exception);
            //}
            //catch (Exception ex) {
            //    // Ignore
            //}

            //// Remote
            //try {
            //    if (ConfigurationReader.GetReportExceptions) {
            //        LogExceptionRemotely(exception);
            //    }
            //}
            //catch (Exception ex) {
            //    // Ignore
            //}
        }

    }

}
