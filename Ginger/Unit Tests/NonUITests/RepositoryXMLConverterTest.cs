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

using Amdocs.Ginger.Common;
using Amdocs.Ginger.Repository;
using Ginger.Repository;
using GingerCore;
using GingerCore.Environments;
using GingerCore.Repository;
using GingerTestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
namespace UnitTests.NonUITests
{
    [TestClass]
    [Level1]
    public class RepositoryXMLConverterTest 
    {

        [ClassInitialize]        
        public static void ClassInitialize(TestContext TC)
        {
            Ginger.App.InitClassTypesDictionary();            
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryItemHelper.RepositoryItemFactory = new RepositoryItemFactory();
        }

        [TestMethod]
        public void BusinessFlowDeserializationTest()
        {
            //Arrange            
            string sFileName = TestResources.GetTestResourcesFile(@"Converter\IPDLSAM.Ginger.BusinessFlow.xml");
            string txt = File.ReadAllText(sFileName);

            //Act
            BusinessFlow BF = (BusinessFlow)NewRepositorySerializer.DeserializeFromText(txt);

            //Assert
            //TODO: add more asserts
           Assert.AreEqual(14, BF.Activities.Count, "BF has 14 activities");
        }

        [TestMethod]
        public void AgentDeserializationTest()
        {
            //Arrange
            RepositorySerializer RepositorySerializer = new RepositorySerializer();
            string sFileName = TestResources.GetTestResourcesFile( @"Converter\IB1.Ginger.Agent.xml");

            //Act
            Agent agent = (Agent)RepositorySerializer.DeserializeFromFile(typeof(Agent), sFileName);

            //Assert
            //TODO: add more asserts
           Assert.AreEqual(agent.Name, "IB1");
           Assert.AreEqual(agent.DriverType,Agent.eDriverType.InternalBrowser);
        }
        
        [TestMethod]
        public void EnvironmentDeserializationTest()
        {
            //Arrange
            RepositorySerializer RepositorySerializer = new RepositorySerializer();
            string sFileName = TestResources.GetTestResourcesFile(@"Converter\CMI.Ginger.Environment.xml");

            //Act
            ProjEnvironment env = (ProjEnvironment)RepositorySerializer.DeserializeFromFile(typeof(ProjEnvironment), sFileName);

            //Assert            
           Assert.AreEqual(env.Name, "CMI IIS test server");
           Assert.AreEqual(env.Applications.Count, 2);
            //TODO: add more asserts
    }



    }
}
