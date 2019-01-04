﻿#region License
/*
Copyright © 2014-2018 European Support Limited

Licensed under the Apache License, Version 2.0 (the "License")
you may not use this file except in compliance with the License.
You may obtain a copy of the License at 

http://www.apache.org/licenses/LICENSE-2.0 

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS, 
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
See the License for the specific language governing permissions and 
limitations under the License. 
*/
#endregion

namespace Amdocs.Ginger.Common
{

    // Set the application log mode
    public enum eAppReporterLoggingLevel
    {
        Normal, Debug
    }


    // Each message to be show to
    public enum eAppReportType
    {
       ToLog, ToUser, ToConsole, ToStatusBar 
    }

    // Each message log type
    public enum eLogLevel
    {
        DEBUG, INFO, WARN, ERROR, FATAL
    }

    // When message show to user it is, combine with above?
    public enum eMessageType
    {
        INFO, WARN, ERROR, QUESTION
    }

    public enum eStatusMessageType
    {
        INFO, PROCESS
    }
}
