﻿using amdocs.ginger.GingerCoreNET;
using Amdocs.Ginger.CoreNET.Reports.ReportHelper;
using Amdocs.Ginger.CoreNET.RunLib;
using Amdocs.Ginger.CoreNET.RunLib.CLILib;
using Amdocs.Ginger.Repository;
using Ginger.Run;
using GingerCore.Environments;
using GingerCoreNETUnitTest.RunTestslib;
using GingerTestHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GingerCoreNETUnitTest.ClientAppReport
{
    [TestClass]
    public class EmailWebReport
    {
        #region Data Members
        private string mTempFolder;
        private string mSolutionFolder;
        #endregion

        #region Ctor
        public EmailWebReport()
        {
            mTempFolder = TestResources.GetTempFolder("CLI Tests");
            mSolutionFolder = Path.Combine(TestResources.GetTestResourcesFolder(@"Solutions"), "EmailWebReport");
        }
        #endregion

        #region Events
        [TestInitialize]
        public void TestInitialize()
        {


        }

        [TestCleanup]
        public void TestCleanUp()
        {
        }
        #endregion


        [TestMethod]
        public void RunNewReportWithEmail()
        {
            // Arrange
            CreateWorkspace();
            // Create config file
            CLIHelper cLIHelper = new CLIHelper();
            cLIHelper.RunAnalyzer = true;
            cLIHelper.ShowAutoRunWindow = false;
            cLIHelper.DownloadUpgradeSolutionFromSourceControl = false;

            RunSetAutoRunConfiguration runSetAutoRunConfiguration = new RunSetAutoRunConfiguration(WorkSpace.Instance.Solution, WorkSpace.Instance.RunsetExecutor, cLIHelper);
            runSetAutoRunConfiguration.ConfigFileFolderPath = mTempFolder;
            runSetAutoRunConfiguration.SelectedCLI = new CLIArgs();

            // Act            
            CLIProcessor CLI = new CLIProcessor();
            CLI.ExecuteArgs(new string[] { runSetAutoRunConfiguration.SelectedCLI.Identifier + "=" + runSetAutoRunConfiguration.ConfigFileContent });
            CheckReportFolderCreation();
            CheckJsDataFromFile();
            // Assert            
            //Assert.AreEqual(WorkSpace.Instance.RunsetExecutor.Runners[0].BusinessFlows[0].RunStatus, Amdocs.Ginger.CoreNET.Execution.eRunStatus.Failed, "BF RunStatus=Passed");
        }

        private void CheckJsDataFromFile()
        {
            Console.WriteLine("<<<<<<<<<<CheckJsDataFromFile start>>>>>>>>>>>>");
            string clientAppFilePath = Path.Combine(WorkSpace.Instance.LocalUserApplicationDataFolderPath, "Reports", "Ginger-Web-Client", "assets", "Execution_Data", "executiondata.js");
            Console.WriteLine($"client app data file is :{clientAppFilePath}");
            bool isFileExists = File.Exists(clientAppFilePath);
            string jsDataStr = string.Empty;
            if (isFileExists)
                jsDataStr = File.ReadAllText(clientAppFilePath);
            Console.WriteLine("json report data is : ");
            Console.WriteLine(jsDataStr);
            Assert.IsTrue(isFileExists && jsDataStr.StartsWith("window.runsetData={\"GingerVersion\":\"3.3.6.1\""));
            Console.WriteLine("<<<<<<<<<<CheckJsDataFromFile end>>>>>>>>>>>>");
        }

        private void CheckReportFolderCreation()
        {
            Console.WriteLine("<<<<<<<<<<CheckReportFolderCreation start>>>>>>>>>>>>");
            string clientAppFolderPath = Path.Combine(WorkSpace.Instance.LocalUserApplicationDataFolderPath, "Reports", "Ginger-Web-Client", "assets", "Execution_Data");
            Console.WriteLine($"client app folder is :{clientAppFolderPath}");
            Assert.IsTrue(Directory.Exists(clientAppFolderPath));
            Console.WriteLine("<<<<<<<<<<CheckReportFolderCreation end>>>>>>>>>>>>");
        }

        private void CreateWorkspace()
        {
            WorkSpaceEventHandler WSEH = new WorkSpaceEventHandler();
            WorkSpace.Init(WSEH, "CLITest");
            WorkSpace.Instance.RunningFromUnitTest = true;
            WorkSpace.Instance.InitWorkspace(new GingerUnitTestWorkspaceReporter(), new RepoCoreItem());
            WorkSpace.Instance.OpenSolution(mSolutionFolder);
            WorkSpace.Instance.Solution.LoggerConfigurations.SelectedDataRepositoryMethod = Ginger.Reports.ExecutionLoggerConfiguration.DataRepositoryMethod.LiteDB;
            SolutionRepository SR = WorkSpace.Instance.SolutionRepository;
            RunsetExecutor runsetExecutor = new RunsetExecutor();
            runsetExecutor.RunsetExecutionEnvironment = (from x in SR.GetAllRepositoryItems<ProjEnvironment>() where x.Name == "Default" select x).SingleOrDefault();
            runsetExecutor.RunSetConfig = (from x in SR.GetAllRepositoryItems<RunSetConfig>() where x.Name == "Default Run Set" select x).SingleOrDefault();
            WorkSpace.Instance.RunsetExecutor = runsetExecutor;
            WorkSpace.Instance.RunsetExecutor.InitRunners();
        }

    }
}