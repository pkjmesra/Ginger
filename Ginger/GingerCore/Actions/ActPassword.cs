#region License
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

using Amdocs.Ginger.Repository;
using System;
using System.Collections.Generic;
using GingerCore.Helpers;
using GingerCoreNET.SolutionRepositoryLib.RepositoryObjectsLib.PlatformsLib;

namespace GingerCore.Actions
{
    //This class is for Text Box actions
    public class ActPassword : Act
    {
        public override string ActionDescription { get { return "Password Action"; } }
        public override string ActionUserDescription { get { return "Perform Password Action"; } }

        public override void ActionUserRecommendedUseCase(TextBlockHelper TBH)
        {
            TBH.AddText("Use this action in case you want to perform any Password actions.");
            TBH.AddLineBreak();
            TBH.AddLineBreak();
            TBH.AddText("To perform a Password action, Select Locate By type, e.g- ByID,ByCSS,ByXPath etc.Then enter the value of property" +
            "that you set in Locate By type then enter the page url in value textbox and run the action.");
        }        

        public override string ActionEditPage { get { return null; } }
        public override bool ObjectLocatorConfigsNeeded { get { return true; } }
        public override bool ValueConfigsNeeded { get { return true; } }

        // return the list of platforms this action is supported on
        public override List<ePlatformType> Platforms
        {
            get
            {
                if (mPlatforms.Count == 0)
                {
                    mPlatforms.Add(ePlatformType.Web);
                    mPlatforms.Add(ePlatformType.Mobile);
                }
                return mPlatforms;
            }
        }

        public enum ePasswordAction
        {
            SetValue = 1,
            SetFocus = 2,
            Clear = 3,
            GetSize=4,
            IsDisabled=5,
            GetWidth = 22,
            GetHeight = 23,
            GetStyle = 24,
        }

        [IsSerializedForLocalRepository]
        public ePasswordAction PasswordAction { get; set; }

        public override String ActionType
        {
            get
            {
                return "TextBox:" + PasswordAction.ToString();
            }
        }
    }
}
