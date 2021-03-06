﻿using BAWGUI.Core.Models;
using BAWGUI.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BAWGUI.Core.ViewModels
{
    public class InspectionAnalysisParametersViewModel : ViewModelBase
    {
        public InspectionAnalysisParametersViewModel()
        {
            _model = new InspectionAnalysisParameters();
        }
        private InspectionAnalysisParameters _model;
        public InspectionAnalysisParameters Model
        {
            get { return _model; }
        }
        public string AnalysisLengthStr
        {
            get { return _model.AnalysisLengthStr; }
            set
            {
                _model.AnalysisLengthStr = value;
                DataTable dt = new DataTable();
                try
                {
                    var result = dt.Compute(value, "");
                    if (result != DBNull.Value)
                    {
                        AnalysisLength = Convert.ToDouble(result);
                    }
                    else
                    {
                        AnalysisLength = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK);
                }
                OnPropertyChanged();
            }
        }
        public string WindowLengthStr
        {
            get { return _model.WindowLengthStr; }
            set
            {
                _model.WindowLengthStr = value;
                DataTable dt = new DataTable();
                try
                {
                    var result = dt.Compute(value, "");
                    if (result != DBNull.Value)
                    {
                        WindowLength = Convert.ToDouble(result);
                    }
                    else
                    {
                        WindowLength = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK);
                }
                OnPropertyChanged();
            }
        }
        public DetectorWindowType WindowType
        {
            get { return _model.WindowType; }
            set
            {
                _model.WindowType = value;
                OnPropertyChanged();
            }
        }
        public string WindowOverlapStr
        {
            get { return _model.WindowOverlapStr; }
            set
            {
                _model.WindowOverlapStr = value;
                DataTable dt = new DataTable();
                try
                {
                    var result = dt.Compute(value, "");
                    if (result != DBNull.Value)
                    {
                        WindowOverlap = Convert.ToDouble(result);
                    }
                    else
                    {
                        WindowOverlap = 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK);
                }
                OnPropertyChanged();
            }
        }
        public string ZeroPadding
        {
            get
            {
                if (_model.ZeroPadding == null)
                {
                    return "";
                }
                else
                {
                    return _model.ZeroPadding.ToString();
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _model.ZeroPadding = null;
                }
                else
                {
                    int zp = 0;
                    var re = int.TryParse(value, out zp);
                    if (re)
                    {
                        _model.ZeroPadding = zp;
                    }
                    else
                    {
                        _model.ZeroPadding = null;
                    }
                }
                OnPropertyChanged();
            }
        }
        public int Fs
        {
            get { return _model.Fs; }
            set
            {
                _model.Fs = value;
                NumberOfSamplesInAnalysisLength = AnalysisLength * Fs;
                NumberOfSamplesInWindowLength = WindowLength * Fs;
                NumberOfSamplesInWindowOverlap = WindowOverlap * Fs;
                OnPropertyChanged();
            }
        }
        public bool LogScale
        {
            get { return _model.LogScale; }
            set
            {
                _model.LogScale = value;
                OnPropertyChanged();
            }
        }
        public string FreqMin
        {
            get
            {
                if (_model.FreqMin == null)
                {
                    return "";
                }
                else
                {
                    return _model.FreqMin.ToString();
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _model.FreqMin = null;
                }
                else
                {
                    double zp = 0d;
                    var re = double.TryParse(value, out zp);
                    if (re)
                    {
                        _model.FreqMin = zp;
                    }
                    else
                    {
                        _model.FreqMin = null;
                    }
                }
                OnPropertyChanged();
            }
        }
        public string FreqMax
        {
            get
            {
                if (_model.FreqMin == null)
                {
                    return "";
                }
                else
                {
                    return _model.FreqMax.ToString();
                }
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _model.FreqMax = null;
                }
                else
                {
                    double zp = 0d;
                    var re = double.TryParse(value, out zp);
                    if (re)
                    {
                        _model.FreqMax = zp;
                    }
                    else
                    {
                        _model.FreqMax = null;
                    }
                }
                OnPropertyChanged();
            }
        }
        public double NumberOfSamplesInAnalysisLength
        {
            get { return _model.NumberOfSamplesInAnalysisLength; }
            set
            {
                _model.NumberOfSamplesInAnalysisLength = value;
                OnPropertyChanged();
            }
        }
        public double NumberOfSamplesInWindowLength
        {
            get { return _model.NumberOfSamplesInWindowLength; }
            set
            {
                _model.NumberOfSamplesInWindowLength = value;
                OnPropertyChanged();
            }
        }
        public double NumberOfSamplesInWindowOverlap
        {
            get { return _model.NumberOfSamplesInWindowOverlap; }
            set
            {
                _model.NumberOfSamplesInWindowOverlap = value;
                OnPropertyChanged();
            }
        }
        public double AnalysisLength
        {
            get { return _model.AnalysisLength; }
            set
            {
                _model.AnalysisLength = value;
                NumberOfSamplesInAnalysisLength = value * Fs;
                OnPropertyChanged();
            }
        }
        public double WindowLength
        {
            get { return _model.WindowLength; }
            set
            {
                _model.WindowLength = value;
                NumberOfSamplesInWindowLength = value * Fs;
                OnPropertyChanged();
            }
        }
        public double WindowOverlap
        {
            get { return _model.WindowOverlap; }
            set
            {
                _model.WindowOverlap = value;
                NumberOfSamplesInWindowOverlap = value * Fs;
                OnPropertyChanged();
            }
        }
    }
}
