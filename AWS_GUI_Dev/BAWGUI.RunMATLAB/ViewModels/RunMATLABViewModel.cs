﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using MathWorks.MATLAB.NET.Arrays;
using MathWorks.MATLAB.NET.Utility;
using System.Threading;
using System.IO;
using BAWGUI.Core;
using BAWGUI.Utilities;

namespace BAWGUI.RunMATLAB.ViewModels
{
    public class RunMATLABViewModel : ViewModelBase
    {
        public RunMATLABViewModel()
        {
            _configFileName = "";
            _initialConfigFilePath = "";
            OpenConfigFile = new RelayCommand(_openConfigFile);
            RunArchiveWalkerNormal = new RelayCommand(_runAWNormal);
            PauseArchiveWalkerNormal = new RelayCommand(_pauseArchiveWalkerNormal);
            ResumeArchiveWalkerNormal = new RelayCommand(_resumeArchiveWalkerNormal);
            StopArchiveWalkerNormal = new RelayCommand(_stopArchiveWalkerNormal);
            //BrowseResultsStorage = new RelayCommand(_browseResultsStorage);
            _engine = MatLabEngine.Instance;
            _engine.MatlabRunning += _onMatlabEngineStatusChange;
            Project = new AWProject();
        }

        private void _onMatlabEngineStatusChange(object sender, bool e)
        {
            OnMatlabEngineStatusChange(e);
        }
        public event EventHandler<bool> MatlabRunning;
        protected virtual void OnMatlabEngineStatusChange(bool e)
        {
            MatlabRunning?.Invoke(this, e);
        }

        private string _configFileName;
        public string ConfigFileName
        {
            get { return _configFileName; }
            set
            {
                _configFileName = value;
                OnPropertyChanged();
            }
        }
        private string _initialConfigFilePath;
        private MatLabEngine _engine;
        public MatLabEngine Engine
        {
            get { return _engine; }
        }

        private AWRunViewModel _run;
        public AWRunViewModel Run
        {
            get { return _run; }
            set
            {
                _run = value;
                Engine.Run = _run;
                OnPropertyChanged();
            }
        }
        public ICommand RunArchiveWalkerNormal { get; set; }
        private void _runAWNormal(object obj)
        {
            if (obj != null)
            {
                Run = (AWRunViewModel)obj;
                if (File.Exists(_run.Model.ConfigFilePath))
                {
                    //var controlPath = RunPath + "ControlRun\\";
                    //var controlPath = _run.ControlRunPath;
                    if (_run.Model.ControlRunPath != null && !Directory.Exists(_run.Model.ControlRunPath))
                    {
                        System.IO.Directory.CreateDirectory(_run.Model.ControlRunPath);
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(_run.Model.RunPath + "\\ControlRun\\");
                    }
                    try
                    {
                        //Engine.RunSelected += Engine_RunSelected;
                        Engine.RuNormalModeByBackgroundWorker(_run);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK);
                    }
                    //if (Run.IsTaskRunning)
                    //{
                    //    OnTaskingIsRunning(Run);
                    //}
                }
                else
                {
                    MessageBox.Show("Config file does not exist. Please specify a valid config file.", "Error!", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("No run is selected!", "Error!", MessageBoxButtons.OK);
            }
        }
        public ICommand OpenConfigFile { get; set; }
        private void _openConfigFile(object obj)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Please select a .xml configure file";
            openFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;
            if (string.IsNullOrEmpty(_initialConfigFilePath))
            {
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;
            }
            else
            {
                openFileDialog.InitialDirectory = _initialConfigFilePath;
            }
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                ConfigFileName = openFileDialog.FileName;
                _initialConfigFilePath = Path.GetDirectoryName(ConfigFileName);
                //_run.ConfigFilePath = ConfigFileName;
                //_run.ControlRunPath = "C:\\Users\\wang690\\Desktop\\projects\\ArchiveWalker\\RerunTest\\Project_RerunTest\\Run_test\\ControlRun";

            }
        }

        public ICommand PauseArchiveWalkerNormal { get; set; }
        public ICommand ResumeArchiveWalkerNormal { get; set; }
        public ICommand StopArchiveWalkerNormal { get; set; }
        private void _stopArchiveWalkerNormal(object obj)
        {
            try
            {
                _engine.StopMatlabNormalRun();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in stop matlab normal run: " + ex.Message, "Error!", MessageBoxButtons.OK);
            }
        }
        private void _pauseArchiveWalkerNormal(object obj)
        {
            _engine.PauseMatlabNormalRun();
        }
        private void _resumeArchiveWalkerNormal(object obj)
        {
            if (obj != null)
            {
                Run = (AWRunViewModel)obj;
                var pauseFlag = _run.Model.ControlRunPath + "PauseFlag.txt";
                try
                {
                    File.Delete(pauseFlag);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error pause normal run, deleting PauseFlag.txt Failed! Original error: " + ex.Message, "Error!", MessageBoxButtons.OK);
                }
                _runAWNormal(obj);
                //_engine.IsNormalRunPaused = false;
            }
            else
            {
                MessageBox.Show("No run is selected!", "Error!", MessageBoxButtons.OK);
            }
        }

        public AWProject Project { get; set; }


    }
}
