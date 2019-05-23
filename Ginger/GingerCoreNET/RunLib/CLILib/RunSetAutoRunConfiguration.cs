﻿using Ginger.Run;
using Ginger.SolutionGeneral;
using GingerUtils;
using System.ComponentModel;
using System.IO;

namespace Amdocs.Ginger.CoreNET.RunLib.CLILib
{
    public class RunSetAutoRunConfiguration: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        Solution mSolution;
        RunsetExecutor mRunsetExecutor;
        CLIHelper mCLIHelper;
        

        public ICLI SelectedCLI;

        public string ConfigFileContent
        {
            get
            {
                return SelectedCLI.CreateContent(mSolution, mRunsetExecutor, mCLIHelper);
            }
        }

        string mConfigFileFolderPath = null;
        public string ConfigFileFolderPath
        {
            get
            {
                if (mConfigFileFolderPath == null)
                {
                    //defualt folder
                    string SolFolder = mSolution.Folder;
                    if (SolFolder.EndsWith(@"\"))
                    {
                        SolFolder = SolFolder.Substring(0, SolFolder.Length - 1);
                    }
                    mConfigFileFolderPath = SolFolder + @"\Documents\RunSetShortCuts\";
                    if (!System.IO.Directory.Exists(mConfigFileFolderPath))
                    {
                        System.IO.Directory.CreateDirectory(SolFolder + @"\Documents\RunSetShortCuts\");
                    }
                }
                return mConfigFileFolderPath;
            }
            set
            {
                mConfigFileFolderPath = value;
                OnPropertyChanged(nameof(ConfigFileFolderPath));
            }
        }


        string mConfigName = null;
        public string ConfigName
        {
            get
            {
                if (mConfigName == null)
                {
                    //defualt name
                    mConfigName = FileUtils.RemoveInvalidChars(mSolution.Name + "-" + mRunsetExecutor.RunSetConfig.Name);
                }
                return mConfigName;
            }
            set
            {
                mConfigName = value;
            }
        }

        public string ConfigFileName
        {
            get
            {
                return FileUtils.RemoveInvalidChars(ConfigName) + ".Ginger.AutoRunConfigs." + SelectedCLI.FileExtension;
            }
        }

        public RunSetAutoRunConfiguration(Solution solution, RunsetExecutor runsetExecutor, CLIHelper cliHelper)
        {
            mSolution = solution;
            mRunsetExecutor = runsetExecutor;
            mCLIHelper = cliHelper;
        }

        public string ConfigFileFullPath
        {
            get
            {
               return Path.Combine(ConfigFileFolderPath, ConfigFileName);
            }
        }

        public void CreateConfigFile()
        {
            if (SelectedCLI.IsFileBasedConfig)
            {
                System.IO.File.WriteAllText(ConfigFileFullPath, ConfigFileContent);
            }
        }

        public string ConfigArgs
        {
            get
            {
                if (SelectedCLI.IsFileBasedConfig)
                {
                    return ConfigFileFullPath;
                }
                else
                {
                    return ConfigFileContent;
                }
            }
        }
    }
}
