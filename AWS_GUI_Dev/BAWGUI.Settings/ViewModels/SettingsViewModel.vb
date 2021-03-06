﻿Imports System.Collections.ObjectModel
Imports System.Windows.Forms
Imports System.IO
Imports System.Windows.Input
Imports System.Windows
Imports BAWGUI.RunMATLAB.ViewModels
Imports BAWGUI.Core
Imports BAWGUI.Utilities
Imports BAWGUI.SignalManagement.ViewModels
Imports BAWGUI.ReadConfigXml
Imports BAWGUI.Core.Models
Imports ModeMeter.ViewModels
Imports ModeMeter.Models
Imports DissipationEnergyFlow.ViewModels
Imports BAWGUI.CoordinateMapping.ViewModels

'Public Shared HighlightColor = Brushes.Cornsilk
'Imports BAWGUI.DataConfig
Namespace ViewModels
    Partial Public Class SettingsViewModel
        Inherits ViewModelBase

        Public Sub New()
            _configFileName = ""
            '_sampleFile = ""

            _dataConfigure = New DataConfig
            _processConfigure = New ProcessConfig
            _postProcessConfigure = New PostProcessCustomizationConfig
            _detectorConfigure = New DetectorConfig
            _logs = New ObservableCollection(Of String)
            _run = New AWRunViewModel()
            _project = New AWProject()
            _isMatlabEngineRunning = False

            '_openConfigFile = New DelegateCommand(AddressOf openConfigXMLFile, AddressOf CanExecute)
            _browseInputFileDir = New DelegateCommand(AddressOf _browseInputFileFolder, AddressOf CanExecute)
            _fileTypeChanged = New DelegateCommand(AddressOf _cleanInputFileInfo, AddressOf CanExecute)
            _dqfilterSelected = New DelegateCommand(AddressOf _dqfilterSelection, AddressOf CanExecute)
            _customizationSelected = New DelegateCommand(AddressOf _customizationStepSelection, AddressOf CanExecute)
            _selectedSignalChanged = New DelegateCommand(AddressOf _signalSelected, AddressOf CanExecute)
            _dataConfigStepSelected = New DelegateCommand(AddressOf _stepSelectedToEdit, AddressOf CanExecute)
            _dataConfigStepDeSelected = New DelegateCommand(AddressOf DeSelectAllDataConfigSteps, AddressOf CanExecute)
            _setCurrentFocusedTextbox = New DelegateCommand(AddressOf _currentFocusedTextBoxChanged, AddressOf CanExecute)
            _setCurrentFocusedTextboxUnarySteps = New DelegateCommand(AddressOf _currentFocusedTextBoxForUnaryStepsChanged, AddressOf CanExecute)
            '_selectedOutputSignalChanged = New DelegateCommand(AddressOf _outputSignalSelectionChanged, AddressOf CanExecute)
            _textboxesLostFocus = New DelegateCommand(AddressOf _recoverCheckStatusOfCurrentStep, AddressOf CanExecute)
            _deleteDataConfigStep = New DelegateCommand(AddressOf _deleteADataConfigStep, AddressOf CanExecute)
            _powerPhasorTextBoxGotFocus = New DelegateCommand(AddressOf _powerPhasorCurrentFocusedTextbox, AddressOf CanExecute)
            _choosePhasorForPowerCalculation = New DelegateCommand(AddressOf _powerCalculationPhasorOption, AddressOf CanExecute)
            _chooseMagAngForPowerCalculation = New DelegateCommand(AddressOf _powerCalculationMagAngOption, AddressOf CanExecute)
            _addFileSource = New DelegateCommand(AddressOf _addAFileSource, AddressOf CanExecute)
            _deleteThisFileSource = New DelegateCommand(AddressOf _deleteAFileSource, AddressOf CanExecute)
            '_saveConfigFile = New DelegateCommand(AddressOf _saveConfigureFile, AddressOf CanExecute)
            '_saveConfigFileAs = New DelegateCommand(AddressOf _saveConfigureFileAs, AddressOf CanExecute)
            '_walkerStageChanged = New DelegateCommand(AddressOf _changeSignalSelectionDropDownChoice, AddressOf CanExecute)
            _addUnwrap = New DelegateCommand(AddressOf _addAUnwrap, AddressOf CanExecute)
            _deleteUnwrapStep = New DelegateCommand(AddressOf _deleteAUnwrap, AddressOf CanExecute)
            _addInterpolate = New DelegateCommand(AddressOf _addAnInterpolate, AddressOf CanExecute)
            _deleteInterpolateStep = New DelegateCommand(AddressOf _deleteAnInterpolate, AddressOf CanExecute)
            _addWrap = New DelegateCommand(AddressOf _addAWrap, AddressOf CanExecute)
            _deleteWrap = New DelegateCommand(AddressOf _deleteAWrap, AddressOf CanExecute)
            _addTunableFilterOrMultirate = New DelegateCommand(AddressOf _addATunableFilterOrMultirate, AddressOf CanExecute)
            _deleteTunableFilterOrMultirate = New DelegateCommand(AddressOf _deleteATunableFilterOrMultirate, AddressOf CanExecute)
            _multirateParameterChoice = New DelegateCommand(AddressOf _chooseParameterForMultirate, AddressOf CanExecute)
            _processConfigStepSelected = New DelegateCommand(AddressOf _processStepSelectedToEdit, AddressOf CanExecute)
            _processConfigStepDeSelected = New DelegateCommand(AddressOf DeSelectAllProcessConfigSteps, AddressOf CanExecute)
            _deleteNameTypeUnit = New DelegateCommand(AddressOf _deleteANameTypeUnit, AddressOf CanExecute)
            _addNameTypeUnit = New DelegateCommand(AddressOf _addANameTypeUnit, AddressOf CanExecute)
            _postProcessConfigStepSelected = New DelegateCommand(AddressOf _postProcessConfigureStepSelected, AddressOf CanExecute)
            _postProcessConfigStepDeSelected = New DelegateCommand(AddressOf DeSelectAllPostProcessConfigSteps, AddressOf CanExecute)
            _deletePostProcessStep = New DelegateCommand(AddressOf _deleteAPostProcessStep, AddressOf CanExecute)
            _detectorSelectedToAdd = New DelegateCommand(AddressOf _addSelectedDetector, AddressOf CanExecute)
            _detectorConfigStepDeSelected = New DelegateCommand(AddressOf DeSelectAllDetectors, AddressOf CanExecute)
            _detectorSelected = New DelegateCommand(AddressOf _selectedADetector, AddressOf CanExecute)
            _deleteDetector = New DelegateCommand(AddressOf _deleteADetector, AddressOf CanExecute)
            _alarmingDetectorSelectedToAdd = New DelegateCommand(AddressOf _addSelectedAlarmingDetector, AddressOf CanExecute)
            _specifyInitializationPath = New DelegateCommand(AddressOf _openInitializationPathFolder, AddressOf CanExecute)
            _specifyEventPath = New DelegateCommand(AddressOf _openEventPathFolder, AddressOf CanExecute)
            _setCurrentPhasorCreationFocusedTextBox = New DelegateCommand(AddressOf _phasrCreationCurrentFocusedTextBoxChanged, AddressOf CanExecute)
            _readExampleFile = New DelegateCommand(AddressOf _parseExampleFile, AddressOf CanExecute)
            _updateExampleFile = New DelegateCommand(AddressOf _writeExampleFileAddressToConfig, AddressOf CanExecute)
            _setCurrentPointOnWavePowerCalFilterInputFocusedTexBox = New DelegateCommand(AddressOf _setCurrentPointOnWavePowerCalFilterInput, AddressOf CanExecute)
            '_detectorStepDeSelected = New DelegateCommand(AddressOf _aDetectorStepDeSelected, AddressOf CanExecute)
            '_postProcessingSelected = New DelegateCommand(AddressOf _selectPostProcessing, AddressOf CanExecute)
            _addDataWriterDetector = New DelegateCommand(AddressOf _addADataWriterDetector, AddressOf CanExecute)
            _deleteDataWriterDetector = New DelegateCommand(AddressOf _deleteADataWriterDetector, AddressOf CanExecute)

            '_inputFileDirTree = New ObservableCollection(Of Folder)
            _signalMgr = SignalManager.Instance
            _signalMgr.cleanUp()

            '_allPMUs = New ObservableCollection(Of String)

            _timezoneList = TimeZoneInfo.GetSystemTimeZones
            _signalSelectionTreeViewVisibility = "Visible"
            _selectSignalMethods = {"All Initial Input Channels by Signal Type",
                                "All Initial Input Channels by PMU",
                                "Input Channels by Step",
                                "Output Channels by Step"}.ToList
            _selectedSelectionMethod = "All Initial Input Channels by Signal Type"
            '_powerTypeDictionary = New Dictionary(Of String, String) From {{"CP", "Complex"}, {"S", "Apparent"}, {"P", "Active"}, {"Q", "Reactive"}}
            _powerTypeDictionary = New Dictionary(Of String, String) From {{"Complex", "CP"}, {"Apparent", "S"}, {"Active", "P"}, {"Reactive", "Q"}}
            _nameTypeUnitStatusFlag = 0

            _dummySignature = New SignalSignatureViewModel("", "", "")
            _dummySignature.Unit = ""
            _dummySignature.IsValid = False
            _dummySignature.SamplingRate = -1
            _dummySignature.IsCustomSignal = True
            _dummySignature.OldSignalName = _dummySignature.SignalName
            _dummySignature.OldTypeAbbreviation = _dummySignature.TypeAbbreviation
            _dummySignature.OldUnit = _dummySignature.Unit
        End Sub

        'Private _pmuSignalDictionary As Dictionary(Of String, List(Of SignalSignatures))
        'Public Property PMUSignalDictionary As Dictionary(Of String, List(Of SignalSignatures))
        '    Get
        '        Return _pmuSignalDictionary
        '    End Get
        '    Set(ByVal value As Dictionary(Of String, List(Of SignalSignatures)))
        '        _pmuSignalDictionary = value
        '        OnPropertyChanged()
        '    End Set
        'End Property
        'Private _groupedRawSignalsByType As ObservableCollection(Of SignalTypeHierachy)
        'Public Property GroupedRawSignalsByType As ObservableCollection(Of SignalTypeHierachy)
        '    Get
        '        Return _groupedRawSignalsByType
        '    End Get
        '    Set(ByVal value As ObservableCollection(Of SignalTypeHierachy))
        '        _groupedRawSignalsByType = value
        '        OnPropertyChanged()
        '    End Set
        'End Property
        'Private _reGroupedRawSignalsByType As ObservableCollection(Of SignalTypeHierachy)
        'Public Property ReGroupedRawSignalsByType As ObservableCollection(Of SignalTypeHierachy)
        '    Get
        '        Return _reGroupedRawSignalsByType
        '    End Get
        '    Set(ByVal value As ObservableCollection(Of SignalTypeHierachy))
        '        _reGroupedRawSignalsByType = value
        '        OnPropertyChanged()
        '    End Set
        'End Property
        'Private _groupedRawSignalsByPMU As ObservableCollection(Of SignalTypeHierachy)
        'Public Property GroupedRawSignalsByPMU As ObservableCollection(Of SignalTypeHierachy)
        '    Get
        '        Return _groupedRawSignalsByPMU
        '    End Get
        '    Set(ByVal value As ObservableCollection(Of SignalTypeHierachy))
        '        _groupedRawSignalsByPMU = value
        '        OnPropertyChanged()
        '    End Set
        'End Property
        'Private _groupedSignalByDataConfigStepsInput As ObservableCollection(Of SignalTypeHierachy)
        'Public Property GroupedSignalByDataConfigStepsInput As ObservableCollection(Of SignalTypeHierachy)
        '    Get
        '        Return _groupedSignalByDataConfigStepsInput
        '    End Get
        '    Set(ByVal value As ObservableCollection(Of SignalTypeHierachy))
        '        _groupedSignalByDataConfigStepsInput = value
        '        OnPropertyChanged()
        '    End Set
        'End Property
        'Private _groupedSignalByDataConfigStepsOutput As ObservableCollection(Of SignalTypeHierachy)
        'Public Property GroupedSignalByDataConfigStepsOutput As ObservableCollection(Of SignalTypeHierachy)
        '    Get
        '        Return _groupedSignalByDataConfigStepsOutput
        '    End Get
        '    Set(ByVal value As ObservableCollection(Of SignalTypeHierachy))
        '        _groupedSignalByDataConfigStepsOutput = value
        '        OnPropertyChanged()
        '    End Set
        'End Property
        Private Function _getAllDataConfigOutput() As ObservableCollection(Of SignalSignatureViewModel)
            Dim allOutputSignals = New ObservableCollection(Of SignalSignatureViewModel)
            For Each stp In DataConfigure.CollectionOfSteps
                For Each signal In stp.OutputChannels
                    If Not allOutputSignals.Contains(signal) AndAlso signal.IsSignalInformationComplete Then
                        allOutputSignals.Add(signal)
                    End If
                Next
            Next
            Return allOutputSignals
        End Function
        'Private _allDataConfigOutputGroupedByType As ObservableCollection(Of SignalTypeHierachy)
        'Public Property AllDataConfigOutputGroupedByType As ObservableCollection(Of SignalTypeHierachy)
        '    Get
        '        Return _allDataConfigOutputGroupedByType
        '    End Get
        '    Set(value As ObservableCollection(Of SignalTypeHierachy))
        '        _allDataConfigOutputGroupedByType = value
        '        OnPropertyChanged()
        '    End Set
        'End Property
        ''Private Function _getAllDataConfigOutputGroupedByPMU() As ObservableCollection(Of SignalTypeHierachy)
        ''    Dim allOutputSignals = New ObservableCollection(Of SignalSignatures)
        ''    For Each stp In DataConfigure.CollectionOfSteps
        ''        For Each signal In stp.OutputChannels
        ''            If Not allOutputSignals.Contains(signal) AndAlso signal.IsSignalInformationComplete Then
        ''                allOutputSignals.Add(signal)
        ''            End If
        ''        Next
        ''    Next
        ''    Return SortSignalByPMU(allOutputSignals)
        ''End Function
        'Private _allDataConfigOutputGroupedByPMU As ObservableCollection(Of SignalTypeHierachy)
        'Public Property AllDataConfigOutputGroupedByPMU As ObservableCollection(Of SignalTypeHierachy)
        '    Get
        '        Return _allDataConfigOutputGroupedByPMU
        '    End Get
        '    Set(value As ObservableCollection(Of SignalTypeHierachy))
        '        _allDataConfigOutputGroupedByPMU = value
        '        OnPropertyChanged()
        '    End Set
        'End Property
        Private _run As AWRunViewModel
        Public Property Run As AWRunViewModel
            Get
                Return _run
            End Get
            Set(ByVal value As AWRunViewModel)
                _run = value
                'If File.Exists(_run.Model.ConfigFilePath) Then
                '    ConfigFileName = _run.Model.ConfigFilePath
                'End If
                OnPropertyChanged()
            End Set
        End Property
        Private _project As AWProject
        Public Property Project As AWProject
            Get
                Return _project
            End Get
            Set(ByVal value As AWProject)
                _project = value
                OnPropertyChanged()
            End Set
        End Property
        'Private Sub _tagSignals(fileInfo As InputFileInfo, signalList As List(Of String))
        '    Dim newSignalList As New ObservableCollection(Of SignalSignatureViewModel)
        '    For Each name In signalList
        '        Dim signal As New SignalSignatureViewModel
        '        'signal.SignalName = name
        '        Dim nameParts = name.Split(".")
        '        signal.PMUName = nameParts(0)
        '        signal.SamplingRate = fileInfo.SamplingRate
        '        If nameParts.Length = 3 Then
        '            Select Case nameParts(2)
        '                Case "F"
        '                    signal.TypeAbbreviation = "F"
        '                    signal.SignalName = nameParts(0) & ".frq"
        '                    signal.Unit = "Hz"
        '                Case "R"
        '                    signal.TypeAbbreviation = "RCF"
        '                    signal.SignalName = nameParts(0) & ".rocof"
        '                    signal.Unit = "mHz/sec"
        '                Case "A"
        '                    signal.SignalName = nameParts(0) & "." & nameParts(1) & ".ANG"
        '                    Dim channel = nameParts(1).Substring(nameParts(1).Length - 2).ToArray
        '                    If channel(0) = "I" OrElse channel(0) = "V" Then
        '                        signal.TypeAbbreviation = channel(0) & "A" & channel(1)
        '                        signal.Unit = "DEG"
        '                    Else
        '                        signal.TypeAbbreviation = "OTHER"
        '                        signal.Unit = "OTHER"
        '                        _addLog("Signal name " & signal.SignalName & " does not comply naming convention. Setting signal type to OTHER.")
        '                    End If
        '                Case "M"
        '                    signal.SignalName = nameParts(0) & "." & nameParts(1) & ".MAG"
        '                    Dim channel = nameParts(1).Substring(nameParts(1).Length - 2).ToArray
        '                    If channel(0) = "I" Then
        '                        signal.TypeAbbreviation = channel(0) & "M" & channel(1)
        '                        signal.Unit = "A"
        '                    ElseIf channel(0) = "V" Then
        '                        signal.TypeAbbreviation = channel(0) & "M" & channel(1)
        '                        signal.Unit = "V"
        '                    Else
        '                        signal.TypeAbbreviation = "OTHER"
        '                        signal.Unit = "OTHER"
        '                        _addLog("Signal name " & signal.SignalName & " does not comply naming convention. Setting signal type to OTHER.")
        '                    End If
        '                Case Else
        '                    Throw New Exception("Error! Invalid signal name " & name & " found!")
        '            End Select
        '        ElseIf nameParts.Length = 2 Then
        '            If nameParts(1).Substring(0, 1) = "D" Then
        '                signal.TypeAbbreviation = "D"
        '                signal.SignalName = nameParts(0) & ".dig" & nameParts(1).Substring(1)
        '                signal.Unit = "D"
        '            Else
        '                Dim lastLetter = nameParts(1).Last
        '                Select Case lastLetter
        '                    Case "V"
        '                        signal.TypeAbbreviation = "Q"
        '                        signal.SignalName = name
        '                        signal.Unit = "MVAR"
        '                    Case "W"
        '                        signal.TypeAbbreviation = "P"
        '                        signal.SignalName = name
        '                        signal.Unit = "MW"
        '                        'Case "D"
        '                        '    signal.TypeAbbreviation = "D"
        '                        '    signal.SignalName = nameParts(0) & "dig"
        '                    Case Else
        '                        Throw New Exception("Error! Invalid signal name " & name & " found!")
        '                End Select
        '            End If
        '        Else
        '            Throw New Exception("Error! Invalid signal name " & name & " found!")
        '        End If
        '        signal.OldSignalName = signal.SignalName
        '        signal.OldTypeAbbreviation = signal.TypeAbbreviation
        '        signal.OldUnit = signal.Unit
        '        newSignalList.Add(signal)
        '    Next
        '    fileInfo.TaggedSignals = newSignalList
        '    fileInfo.GroupedSignalsByPMU = SortSignalByPMU(newSignalList)
        '    'For Each group In fileInfo.GroupedSignalsByPMU
        '    '    If Not _allPMUs.Contains(group.SignalSignature.PMUName) Then
        '    '        _allPMUs.Add(group.SignalSignature.PMUName)
        '    '    End If
        '    'Next
        '    'Dim a = New SignalTypeHierachy(New SignalSignatures(fileInfo.FileDirectory & ", Sampling Rate: " & fileInfo.SamplingRate & "/Second"))
        '    Dim a = New SignalTypeHierachy(New SignalSignatureViewModel(fileInfo.FileDirectory))
        '    a.SignalList = fileInfo.GroupedSignalsByPMU
        '    _signalMgr.GroupedRawSignalsByPMU.Add(a)
        '    'GroupedRawSignalsByPMU = New ObservableCollection(Of SignalTypeHierachy)(GroupedRawSignalsByPMU.Concat(fileInfo.GroupedSignalsByPMU))
        '    fileInfo.GroupedSignalsByType = SortSignalByType(newSignalList)
        '    'Dim b = New SignalTypeHierachy(New SignalSignatures(fileInfo.FileDirectory & ", Sampling Rate: " & fileInfo.SamplingRate & "/Second"))
        '    Dim b = New SignalTypeHierachy(New SignalSignatureViewModel(fileInfo.FileDirectory))
        '    b.SignalList = fileInfo.GroupedSignalsByType
        '    _signalMgr.GroupedRawSignalsByType.Add(b)
        '    _signalMgr.ReGroupedRawSignalsByType = _signalMgr.GroupedRawSignalsByType
        'End Sub
        'Private Function SortSignalByType(signalList As ObservableCollection(Of SignalSignatureViewModel)) As ObservableCollection(Of SignalTypeHierachy)
        '    Dim signalTypeTreeGroupedBySamplingRate As New ObservableCollection(Of SignalTypeHierachy)
        '    Dim signalTypeGroupBySamplingRate = signalList.GroupBy(Function(x) x.SamplingRate)
        '    For Each rateGroup In signalTypeGroupBySamplingRate
        '        Dim rate = rateGroup.Key
        '        Dim subSignalGroup = rateGroup.ToList
        '        Dim signalTypeTree As New ObservableCollection(Of SignalTypeHierachy)
        '        Dim signalTypeDictionary = subSignalGroup.GroupBy(Function(x) x.TypeAbbreviation.ToArray(0).ToString).ToDictionary(Function(x) x.Key, Function(x) New ObservableCollection(Of SignalSignatureViewModel)(x.ToList))
        '        For Each signalType In signalTypeDictionary
        '            Select Case signalType.Key
        '                Case "S"
        '                    Dim groups = signalType.Value.GroupBy(Function(x) x.TypeAbbreviation)
        '                    For Each group In groups
        '                        Select Case group.Key
        '                            Case "S"
        '                                Dim newHierachy = New SignalTypeHierachy(New SignalSignatureViewModel("Apparent"))
        '                                newHierachy.SignalSignature.TypeAbbreviation = "S"
        '                                For Each signal In group
        '                                    newHierachy.SignalList.Add(New SignalTypeHierachy(signal))
        '                                Next
        '                                signalTypeTree.Add(newHierachy)
        '                            Case "SC"
        '                                Dim newHierachy = New SignalTypeHierachy(New SignalSignatureViewModel("Scalar"))
        '                                newHierachy.SignalSignature.TypeAbbreviation = "SC"
        '                                For Each signal In group
        '                                    newHierachy.SignalList.Add(New SignalTypeHierachy(signal))
        '                                Next
        '                                signalTypeTree.Add(newHierachy)
        '                            Case Else
        '                                _addLog("Unknown signal type: " & group.Key & "found!")
        '                        End Select
        '                    Next
        '                Case "O"
        '                    Dim newHierachy = New SignalTypeHierachy(New SignalSignatureViewModel("Other"))
        '                    newHierachy.SignalSignature.TypeAbbreviation = "OTHER"
        '                    For Each signal In signalType.Value
        '                        newHierachy.SignalList.Add(New SignalTypeHierachy(signal))
        '                    Next
        '                    signalTypeTree.Add(newHierachy)
        '                Case "C"
        '                    'Dim groups = signalType.Value.GroupBy(Function(x) x.TypeAbbreviation).ToDictionary(Function(x) x.Key, Function(x) New ObservableCollection(Of SignalSignatures)(x.ToList))
        '                    Dim groups = signalType.Value.GroupBy(Function(x) x.TypeAbbreviation)
        '                    For Each group In groups
        '                        Select Case group.Key
        '                            Case "C"
        '                                Dim newHierachy = New SignalTypeHierachy(New SignalSignatureViewModel("CustomizedSignal"))
        '                                newHierachy.SignalSignature.TypeAbbreviation = "C"
        '                                For Each signal In group
        '                                    newHierachy.SignalList.Add(New SignalTypeHierachy(signal))
        '                                Next
        '                                signalTypeTree.Add(newHierachy)
        '                            Case "CP"
        '                                Dim newHierachy = New SignalTypeHierachy(New SignalSignatureViewModel("Complex"))
        '                                newHierachy.SignalSignature.TypeAbbreviation = "CP"
        '                                For Each signal In group
        '                                    newHierachy.SignalList.Add(New SignalTypeHierachy(signal))
        '                                Next
        '                                signalTypeTree.Add(newHierachy)
        '                            Case Else
        '                                _addLog("Unknown signal type: " & group.Key & "found!")
        '                        End Select
        '                    Next
        '                Case "D"
        '                    Dim newHierachy = New SignalTypeHierachy(New SignalSignatureViewModel("Digital"))
        '                    newHierachy.SignalSignature.TypeAbbreviation = "D"
        '                    For Each signal In signalType.Value
        '                        newHierachy.SignalList.Add(New SignalTypeHierachy(signal))
        '                    Next
        '                    signalTypeTree.Add(newHierachy)
        '                Case "F"
        '                    Dim newHierachy = New SignalTypeHierachy(New SignalSignatureViewModel("Frequency"))
        '                    newHierachy.SignalSignature.TypeAbbreviation = "F"
        '                    For Each signal In signalType.Value
        '                        newHierachy.SignalList.Add(New SignalTypeHierachy(signal))
        '                    Next
        '                    signalTypeTree.Add(newHierachy)
        '                Case "R"
        '                    Dim newHierachy = New SignalTypeHierachy(New SignalSignatureViewModel("Rate of Change of Frequency"))
        '                    newHierachy.SignalSignature.TypeAbbreviation = "R"
        '                    For Each signal In signalType.Value
        '                        newHierachy.SignalList.Add(New SignalTypeHierachy(signal))
        '                    Next
        '                    signalTypeTree.Add(newHierachy)
        '                Case "Q"
        '                    Dim newHierachy = New SignalTypeHierachy(New SignalSignatureViewModel("Reactive Power"))
        '                    newHierachy.SignalSignature.TypeAbbreviation = "Q"
        '                    For Each signal In signalType.Value
        '                        newHierachy.SignalList.Add(New SignalTypeHierachy(signal))
        '                    Next
        '                    signalTypeTree.Add(newHierachy)
        '                Case "P"
        '                    Dim newHierachy = New SignalTypeHierachy(New SignalSignatureViewModel("Active Power"))
        '                    newHierachy.SignalSignature.TypeAbbreviation = "P"
        '                    For Each signal In signalType.Value
        '                        newHierachy.SignalList.Add(New SignalTypeHierachy(signal))
        '                    Next
        '                    signalTypeTree.Add(newHierachy)
        '                Case "V"
        '                    Dim newHierachy = New SignalTypeHierachy(New SignalSignatureViewModel("Voltage"))
        '                    newHierachy.SignalSignature.TypeAbbreviation = "V"
        '                    Dim voltageHierachy = signalType.Value.GroupBy(Function(y) y.TypeAbbreviation.ToArray(1).ToString)
        '                    For Each group In voltageHierachy
        '                        Select Case group.Key
        '                            Case "M"
        '                                Dim mGroup = New SignalTypeHierachy(New SignalSignatureViewModel("Magnitude"))
        '                                mGroup.SignalSignature.TypeAbbreviation = "VM"
        '                                Dim mGroupHierachky = group.GroupBy(Function(z) z.TypeAbbreviation.ToArray(2).ToString)
        '                                For Each phase In mGroupHierachky
        '                                    Select Case phase.Key
        '                                        Case "P"
        '                                            Dim positiveGroup = New SignalTypeHierachy(New SignalSignatureViewModel("Positive Sequence"))
        '                                            positiveGroup.SignalSignature.TypeAbbreviation = "VMP"
        '                                            For Each signal In phase
        '                                                positiveGroup.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            mGroup.SignalList.Add(positiveGroup)
        '                                        Case "A"
        '                                            Dim AGroup = New SignalTypeHierachy(New SignalSignatureViewModel("Phase A"))
        '                                            AGroup.SignalSignature.TypeAbbreviation = "VMA"
        '                                            For Each signal In phase
        '                                                AGroup.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            mGroup.SignalList.Add(AGroup)
        '                                        Case "B"
        '                                            Dim BGroup = New SignalTypeHierachy(New SignalSignatureViewModel("Phase B"))
        '                                            BGroup.SignalSignature.TypeAbbreviation = "VMB"
        '                                            For Each signal In phase
        '                                                BGroup.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            mGroup.SignalList.Add(BGroup)
        '                                        Case "C"
        '                                            Dim CGroup = New SignalTypeHierachy(New SignalSignatureViewModel("Phase C"))
        '                                            CGroup.SignalSignature.TypeAbbreviation = "VMC"
        '                                            For Each signal In phase
        '                                                CGroup.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            mGroup.SignalList.Add(CGroup)
        '                                        Case Else
        '                                            Throw New Exception("Error! Invalid signal phase: " & phase.Key & " found in Voltage magnitude!")
        '                                    End Select
        '                                Next
        '                                newHierachy.SignalList.Add(mGroup)
        '                            Case "A"
        '                                Dim aGroup = New SignalTypeHierachy(New SignalSignatureViewModel("Angle"))
        '                                aGroup.SignalSignature.TypeAbbreviation = "VA"
        '                                Dim aGroupHierachy = group.GroupBy(Function(z) z.TypeAbbreviation.ToArray(2).ToString)
        '                                For Each phase In aGroupHierachy
        '                                    Select Case phase.Key
        '                                        Case "P"
        '                                            Dim positiveGroup = New SignalTypeHierachy(New SignalSignatureViewModel("Positive Sequence"))
        '                                            positiveGroup.SignalSignature.TypeAbbreviation = "VAP"
        '                                            For Each signal In phase
        '                                                positiveGroup.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            aGroup.SignalList.Add(positiveGroup)
        '                                        Case "A"
        '                                            Dim GroupA = New SignalTypeHierachy(New SignalSignatureViewModel("Phase A"))
        '                                            GroupA.SignalSignature.TypeAbbreviation = "VAA"
        '                                            For Each signal In phase
        '                                                GroupA.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            aGroup.SignalList.Add(GroupA)
        '                                        Case "B"
        '                                            Dim GroupB = New SignalTypeHierachy(New SignalSignatureViewModel("Phase B"))
        '                                            GroupB.SignalSignature.TypeAbbreviation = "VAB"
        '                                            For Each signal In phase
        '                                                GroupB.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            aGroup.SignalList.Add(GroupB)
        '                                        Case "C"
        '                                            Dim GroupC = New SignalTypeHierachy(New SignalSignatureViewModel("Phase C"))
        '                                            GroupC.SignalSignature.TypeAbbreviation = "VAC"
        '                                            For Each signal In phase
        '                                                GroupC.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            aGroup.SignalList.Add(GroupC)
        '                                        Case Else
        '                                            Throw New Exception("Error! Invalid signal phase: " & phase.Key & " found in Voltage Angle!")
        '                                    End Select
        '                                Next
        '                                newHierachy.SignalList.Add(aGroup)
        '                            Case "P"
        '                                Dim aGroup = New SignalTypeHierachy(New SignalSignatureViewModel("Phasor"))
        '                                aGroup.SignalSignature.TypeAbbreviation = "VP"
        '                                Dim aGroupHierachy = group.GroupBy(Function(z) z.TypeAbbreviation.ToArray(2).ToString)
        '                                For Each phase In aGroupHierachy
        '                                    Select Case phase.Key
        '                                        Case "P"
        '                                            Dim positiveGroup = New SignalTypeHierachy(New SignalSignatureViewModel("Positive Sequence"))
        '                                            positiveGroup.SignalSignature.TypeAbbreviation = "VPP"
        '                                            For Each signal In phase
        '                                                positiveGroup.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            aGroup.SignalList.Add(positiveGroup)
        '                                        Case "A"
        '                                            Dim GroupA = New SignalTypeHierachy(New SignalSignatureViewModel("Phase A"))
        '                                            GroupA.SignalSignature.TypeAbbreviation = "VPA"
        '                                            For Each signal In phase
        '                                                GroupA.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            aGroup.SignalList.Add(GroupA)
        '                                        Case "B"
        '                                            Dim GroupB = New SignalTypeHierachy(New SignalSignatureViewModel("Phase B"))
        '                                            GroupB.SignalSignature.TypeAbbreviation = "VPB"
        '                                            For Each signal In phase
        '                                                GroupB.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            aGroup.SignalList.Add(GroupB)
        '                                        Case "C"
        '                                            Dim GroupC = New SignalTypeHierachy(New SignalSignatureViewModel("Phase C"))
        '                                            GroupC.SignalSignature.TypeAbbreviation = "VPC"
        '                                            For Each signal In phase
        '                                                GroupC.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            aGroup.SignalList.Add(GroupC)
        '                                        Case Else
        '                                            Throw New Exception("Error! Invalid signal phase: " & phase.Key & " found in Voltage Angle!")
        '                                    End Select
        '                                Next
        '                                newHierachy.SignalList.Add(aGroup)
        '                            Case Else
        '                                Throw New Exception("Error! Invalid voltage signal type found: " & group.Key)
        '                        End Select
        '                    Next
        '                    signalTypeTree.Add(newHierachy)
        '                Case "I"
        '                    Dim newHierachy = New SignalTypeHierachy(New SignalSignatureViewModel("Current"))
        '                    newHierachy.SignalSignature.TypeAbbreviation = "I"
        '                    Dim currentHierachy = signalType.Value.GroupBy(Function(y) y.TypeAbbreviation.ToArray(1).ToString)
        '                    For Each group In currentHierachy
        '                        Select Case group.Key
        '                            Case "M"
        '                                Dim mGroup = New SignalTypeHierachy(New SignalSignatureViewModel("Magnitude"))
        '                                mGroup.SignalSignature.TypeAbbreviation = "IM"
        '                                Dim mGroupHierachky = group.GroupBy(Function(z) z.TypeAbbreviation.ToArray(2).ToString)
        '                                For Each phase In mGroupHierachky
        '                                    Select Case phase.Key
        '                                        Case "P"
        '                                            Dim positiveGroup = New SignalTypeHierachy(New SignalSignatureViewModel("Positive Sequence"))
        '                                            positiveGroup.SignalSignature.TypeAbbreviation = "IMP"
        '                                            For Each signal In phase
        '                                                positiveGroup.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            mGroup.SignalList.Add(positiveGroup)
        '                                        Case "A"
        '                                            Dim AGroup = New SignalTypeHierachy(New SignalSignatureViewModel("Phase A"))
        '                                            AGroup.SignalSignature.TypeAbbreviation = "IMA"
        '                                            For Each signal In phase
        '                                                AGroup.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            mGroup.SignalList.Add(AGroup)
        '                                        Case "B"
        '                                            Dim BGroup = New SignalTypeHierachy(New SignalSignatureViewModel("Phase B"))
        '                                            BGroup.SignalSignature.TypeAbbreviation = "IMB"
        '                                            For Each signal In phase
        '                                                BGroup.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            mGroup.SignalList.Add(BGroup)
        '                                        Case "C"
        '                                            Dim CGroup = New SignalTypeHierachy(New SignalSignatureViewModel("Phase C"))
        '                                            CGroup.SignalSignature.TypeAbbreviation = "IMC"
        '                                            For Each signal In phase
        '                                                CGroup.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            mGroup.SignalList.Add(CGroup)
        '                                        Case Else
        '                                            Throw New Exception("Error! Invalid signal phase: " & phase.Key & " found in Voltage magnitude!")
        '                                    End Select
        '                                Next
        '                                newHierachy.SignalList.Add(mGroup)
        '                            Case "A"
        '                                Dim aGroup = New SignalTypeHierachy(New SignalSignatureViewModel("Angle"))
        '                                aGroup.SignalSignature.TypeAbbreviation = "IA"
        '                                Dim aGroupHierachy = group.GroupBy(Function(z) z.TypeAbbreviation.ToArray(2).ToString)
        '                                For Each phase In aGroupHierachy
        '                                    Select Case phase.Key
        '                                        Case "P"
        '                                            Dim positiveGroup = New SignalTypeHierachy(New SignalSignatureViewModel("Positive Sequence"))
        '                                            positiveGroup.SignalSignature.TypeAbbreviation = "IAP"
        '                                            For Each signal In phase
        '                                                positiveGroup.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            aGroup.SignalList.Add(positiveGroup)
        '                                        Case "A"
        '                                            Dim GroupA = New SignalTypeHierachy(New SignalSignatureViewModel("Phase A"))
        '                                            GroupA.SignalSignature.TypeAbbreviation = "IAA"
        '                                            For Each signal In phase
        '                                                GroupA.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            aGroup.SignalList.Add(GroupA)
        '                                        Case "B"
        '                                            Dim GroupB = New SignalTypeHierachy(New SignalSignatureViewModel("Phase B"))
        '                                            GroupB.SignalSignature.TypeAbbreviation = "IAB"
        '                                            For Each signal In phase
        '                                                GroupB.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            aGroup.SignalList.Add(GroupB)
        '                                        Case "C"
        '                                            Dim GroupC = New SignalTypeHierachy(New SignalSignatureViewModel("Phase C"))
        '                                            GroupC.SignalSignature.TypeAbbreviation = "IAC"
        '                                            For Each signal In phase
        '                                                GroupC.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            aGroup.SignalList.Add(GroupC)
        '                                        Case Else
        '                                            Throw New Exception("Error! Invalid signal phase: " & phase.Key & " found in Voltage Angle!")
        '                                    End Select
        '                                Next
        '                                newHierachy.SignalList.Add(aGroup)
        '                            Case "P"
        '                                Dim aGroup = New SignalTypeHierachy(New SignalSignatureViewModel("Phasor"))
        '                                aGroup.SignalSignature.TypeAbbreviation = "IP"
        '                                Dim aGroupHierachy = group.GroupBy(Function(z) z.TypeAbbreviation.ToArray(2).ToString)
        '                                For Each phase In aGroupHierachy
        '                                    Select Case phase.Key
        '                                        Case "P"
        '                                            Dim positiveGroup = New SignalTypeHierachy(New SignalSignatureViewModel("Positive Sequence"))
        '                                            positiveGroup.SignalSignature.TypeAbbreviation = "IPP"
        '                                            For Each signal In phase
        '                                                positiveGroup.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            aGroup.SignalList.Add(positiveGroup)
        '                                        Case "A"
        '                                            Dim GroupA = New SignalTypeHierachy(New SignalSignatureViewModel("Phase A"))
        '                                            GroupA.SignalSignature.TypeAbbreviation = "IPA"
        '                                            For Each signal In phase
        '                                                GroupA.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            aGroup.SignalList.Add(GroupA)
        '                                        Case "B"
        '                                            Dim GroupB = New SignalTypeHierachy(New SignalSignatureViewModel("Phase B"))
        '                                            GroupB.SignalSignature.TypeAbbreviation = "IPB"
        '                                            For Each signal In phase
        '                                                GroupB.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            aGroup.SignalList.Add(GroupB)
        '                                        Case "C"
        '                                            Dim GroupC = New SignalTypeHierachy(New SignalSignatureViewModel("Phase C"))
        '                                            GroupC.SignalSignature.TypeAbbreviation = "IPC"
        '                                            For Each signal In phase
        '                                                GroupC.SignalList.Add(New SignalTypeHierachy(signal))
        '                                            Next
        '                                            aGroup.SignalList.Add(GroupC)
        '                                        Case Else
        '                                            Throw New Exception("Error! Invalid signal phase: " & phase.Key & " found in Voltage Angle!")
        '                                    End Select
        '                                Next
        '                                newHierachy.SignalList.Add(aGroup)
        '                            Case Else
        '                                Throw New Exception("Error! Invalid voltage signal type found: " & group.Key)
        '                        End Select
        '                    Next
        '                    signalTypeTree.Add(newHierachy)
        '                Case Else
        '                    Throw New Exception("Error! Invalid signal type found: " & signalType.Key)
        '            End Select
        '        Next
        '        Dim newSig = New SignalSignatureViewModel("Sampling Rate: " & rate.ToString & "/Second")
        '        newSig.SamplingRate = rate
        '        Dim a = New SignalTypeHierachy(newSig)
        '        a.SignalList = signalTypeTree
        '        signalTypeTreeGroupedBySamplingRate.Add(a)
        '    Next
        '    Return signalTypeTreeGroupedBySamplingRate
        'End Function
        'Private Function SortSignalByPMU(signalList As ObservableCollection(Of SignalSignatureViewModel)) As ObservableCollection(Of SignalTypeHierachy)
        '    Dim groupBySamplingRate = signalList.GroupBy(Function(x) x.SamplingRate)
        '    Dim pmuSignalTreeGroupedBySamplingRate = New ObservableCollection(Of SignalTypeHierachy)
        '    For Each group In groupBySamplingRate
        '        Dim rate = group.Key
        '        Dim subSignalList = group.ToList
        '        Dim PMUSignalDictionary = subSignalList.GroupBy(Function(x) x.PMUName).ToDictionary(Function(x) x.Key, Function(x) x.ToList)
        '        Dim pmuSignalTree = New ObservableCollection(Of SignalTypeHierachy)
        '        For Each subgroup In PMUSignalDictionary
        '            Dim newPMUSignature = New SignalSignatureViewModel(subgroup.Key, subgroup.Key)
        '            Dim newGroup = New SignalTypeHierachy(newPMUSignature)
        '            For Each signal In subgroup.Value
        '                newGroup.SignalList.Add(New SignalTypeHierachy(signal))
        '            Next
        '            newGroup.SignalSignature.SamplingRate = subgroup.Value.FirstOrDefault.SamplingRate
        '            pmuSignalTree.Add(newGroup)
        '        Next
        '        Dim newSig = New SignalSignatureViewModel("Sampling Rate: " & rate.ToString & "/Second")
        '        newSig.SamplingRate = rate
        '        Dim a = New SignalTypeHierachy(newSig)
        '        a.SignalList = pmuSignalTree
        '        pmuSignalTreeGroupedBySamplingRate.Add(a)
        '    Next
        '    'Dim PMUSignalDictionary = subSignalList.GroupBy(Function(x) x.PMUName).ToDictionary(Function(x) x.Key, Function(x) x.ToList)
        '    'Dim pmuSignalTree = New ObservableCollection(Of SignalTypeHierachy)
        '    'For Each group In PMUSignalDictionary
        '    '    Dim newPMUSignature = New SignalSignatures(group.Key, group.Key)
        '    '    Dim newGroup = New SignalTypeHierachy(newPMUSignature)
        '    '    For Each signal In group.Value
        '    '        newGroup.SignalList.Add(New SignalTypeHierachy(signal))
        '    '    Next
        '    '    newGroup.SignalSignature.SamplingRate = group.Value.FirstOrDefault.SamplingRate
        '    '    pmuSignalTree.Add(newGroup)
        '    'Next
        '    Return pmuSignalTreeGroupedBySamplingRate
        'End Function

        Private _powerTypeDictionary As Dictionary(Of String, String)
        Private _lastCustPMUname As String

        Private _lastInputFolderLocation As String
        Private _browseInputFileDir As ICommand
        Public Property BrowseInputFileDir As ICommand
            Get
                Return _browseInputFileDir
            End Get
            Set(ByVal value As ICommand)
                _browseInputFileDir = value
            End Set
        End Property
        Private Sub _browseInputFileFolder(obj As InputFileInfoViewModel)
            ''Dim previousDir = New InputFileInfo(obj)
            'Dim openDirectoryDialog As New FolderBrowserDialog()
            'openDirectoryDialog.Description = "Select the directory that data files (.pdat or .csv) are located "
            'If _lastInputFolderLocation Is Nothing Then
            '    openDirectoryDialog.SelectedPath = Environment.CurrentDirectory
            'Else
            '    openDirectoryDialog.SelectedPath = _lastInputFolderLocation
            'End If
            'openDirectoryDialog.ShowNewFolderButton = False
            'If (openDirectoryDialog.ShowDialog = DialogResult.OK) Then
            '    ' When a new directory is selected, we need to clean out everything that display contents of that directory
            '    'obj = _parseExampleFile(obj, openDirectoryDialog)
            '    '_buildInputFileFolderTree(obj)
            '    'If _configData IsNot Nothing Then
            '    '    _readDataConfigStages(_configData)
            '    '    _readProcessConfig(_configData)
            '    '    _readPostProcessConfig(_configData)
            '    '    _readDetectorConfig(_configData)
            '    'End If
            '    _lastInputFolderLocation = openDirectoryDialog.SelectedPath
            'End If
            Dim openFileDialog As New Microsoft.Win32.OpenFileDialog()
            openFileDialog.RestoreDirectory = True
            openFileDialog.FileName = ""
            openFileDialog.DefaultExt = ".pdat"
            openFileDialog.Filter = "pdat files (*.pdat)|*.pdat|JSIS_CSV files (*.csv)|*.csv|HQ Point on Wave (*.mat)|*.mat|PI Reader Preset (*.xml)|*.xml|openHistorian Preset (*.xml)|*.xml|All files (*.*)|*.*"
            If obj.FileType = DataFileType.csv Then
                openFileDialog.DefaultExt = ".csv"
                openFileDialog.Filter = "JSIS_CSV files (*.csv)|*.csv|HQ Point on Wave (*.mat)|*.mat|PI Reader Preset (*.xml)|*.xml|pdat files (*.pdat)|*.pdat|openHistorian Preset (*.xml)|*.xml|All files (*.*)|*.*"
            ElseIf obj.FileType = DataFileType.PI Then
                openFileDialog.DefaultExt = ".xml"
                openFileDialog.Filter = "PI Reader Preset (*.xml)|*.xml|pdat files (*.pdat)|*.pdat|JSIS_CSV files (*.csv)|*.csv|HQ Point on Wave (*.mat)|*.mat|openHistorian Preset (*.xml)|*.xml|All files (*.*)|*.*"
            ElseIf obj.FileType = DataFileType.powHQ Then
                openFileDialog.DefaultExt = ".mat"
                openFileDialog.Filter = "HQ Point on Wave (*.mat)|*.mat|pdat files (*.pdat)|*.pdat|JSIS_CSV files (*.csv)|*.csv|PI Reader Preset (*.xml)|*.xml|openHistorian Preset (*.xml)|*.xml|All files (*.*)|*.*"
            ElseIf obj.FileType = DataFileType.OpenHistorian Then
                openFileDialog.DefaultExt = ".xml"
                openFileDialog.Filter = "openHistorian Preset (*.xml)|*.xml|HQ Point on Wave (*.mat)|*.mat|pdat files (*.pdat)|*.pdat|JSIS_CSV files (*.csv)|*.csv|PI Reader Preset (*.xml)|*.xml|All files (*.*)|*.*"
            ElseIf obj.FileType = DataFileType.OpenPDC Then
                openFileDialog.DefaultExt = ".xml"
                openFileDialog.Filter = "openPDC Preset (*.xml)|*.xml|HQ Point on Wave (*.mat)|*.mat|pdat files (*.pdat)|*.pdat|JSIS_CSV files (*.csv)|*.csv|PI Reader Preset (*.xml)|*.xml|All files (*.*)|*.*"
            End If
            If _lastInputFolderLocation Is Nothing Then
                openFileDialog.InitialDirectory = Environment.CurrentDirectory
            Else
                openFileDialog.InitialDirectory = _lastInputFolderLocation
            End If
            Dim DialogResult? As Boolean = openFileDialog.ShowDialog
            If DialogResult Then
                obj.ExampleFile = openFileDialog.FileName
                _lastInputFolderLocation = Path.GetDirectoryName(openFileDialog.FileName)
                'ConfigFileName = openFileDialog.FileName
                '_addLog("Open file: " & ConfigFileName & " successfully!")
                'GroupedRawSignalsByType = New ObservableCollection(Of SignalTypeHierachy)
                'GroupedRawSignalsByPMU = New ObservableCollection(Of SignalTypeHierachy)
                'NameTypeUnitStatusFlag = 0
                'Try
                '    _configData = XDocument.Load(_configFileName)
                '    _addLog("Reading " & ConfigFileName)
                '    _readConfigFile(_configData)
                '    _addLog("Done reading " & ConfigFileName & " .")
                'Catch ex As Exception
                '    _addLog("Error reading config file!" & vbCrLf & ex.Message)
                '    Forms.MessageBox.Show("Error reading config file!" & vbCrLf & ex.Message & vbCrLf & "Please see logs below!", "Error!", MessageBoxButtons.OK)
                'End Try
            End If

        End Sub
        Private _isMatlabEngineRunning As Boolean
        Public Property IsMatlabEngineRunning As Boolean
            Get
                Return _isMatlabEngineRunning
            End Get
            Set(ByVal value As Boolean)
                _isMatlabEngineRunning = value
                OnPropertyChanged()
            End Set
        End Property
        Private _readExampleFile As ICommand
        Public Property ReadExampleFile As ICommand
            Get
                Return _readExampleFile
            End Get
            Set(ByVal value As ICommand)
                _readExampleFile = value
            End Set
        End Property
        Private Sub _parseExampleFile(obj As InputFileInfoViewModel)
            If (obj.Model.CheckDataFileMatch()) Then
                Dim dirs = New List(Of String)
                Dim dirMnDict = New Dictionary(Of String, List(Of String))
                For Each info In DataConfigure.ReaderProperty.InputFileInfos
                    If Not dirs.Contains(info.FileDirectory) Then
                        dirs.Add(info.FileDirectory)
                    End If
                    If Not dirMnDict.ContainsKey(info.FileDirectory) Then
                        dirMnDict(info.FileDirectory) = New List(Of String)
                    End If
                    If Not dirMnDict(info.FileDirectory).Contains(info.Mnemonic) Then
                        dirMnDict(info.FileDirectory).Add(info.Mnemonic)
                    Else
                        Forms.MessageBox.Show("Duplicate file source not allowed:" + Environment.NewLine + info.FileDirectory + Environment.NewLine + info.Mnemonic, "Warning!", MessageBoxButtons.OK)
                        Exit Sub
                    End If
                Next
                For Each group In _signalMgr.GroupedRawSignalsByType
                    Dim frags = group.SignalSignature.SignalName.Split(",")
                    Dim dir = frags(0).Trim
                    Dim mnic = frags(1).Trim
                    If Not dirs.Contains(dir) Then
                        _signalMgr.GroupedRawSignalsByType.Remove(group)
                        Exit For
                    Else
                        If Not dirMnDict(dir).Contains(mnic) Then
                            _signalMgr.GroupedRawSignalsByType.Remove(group)
                            Exit For
                        End If
                    End If
                Next
                For Each group In _signalMgr.ReGroupedRawSignalsByType
                    Dim frags = group.SignalSignature.SignalName.Split(",")
                    Dim dir = frags(0).Trim
                    Dim mnic = frags(1).Trim
                    If Not dirs.Contains(dir) Then
                        _signalMgr.ReGroupedRawSignalsByType.Remove(group)
                        Exit For
                    Else
                        If Not dirMnDict(dir).Contains(mnic) Then
                            _signalMgr.ReGroupedRawSignalsByType.Remove(group)
                            Exit For
                        End If
                    End If
                Next
                For Each group In _signalMgr.GroupedRawSignalsByPMU
                    Dim frags = group.SignalSignature.SignalName.Split(",")
                    Dim dir = frags(0).Trim
                    Dim mnic = frags(1).Trim
                    If Not dirs.Contains(dir) Then
                        _signalMgr.GroupedRawSignalsByPMU.Remove(group)
                        Exit For
                    Else
                        If Not dirMnDict(dir).Contains(mnic) Then
                            _signalMgr.GroupedRawSignalsByPMU.Remove(group)
                            Exit For
                        End If
                    End If
                Next
                ' this for each loop is trying to avoid read the example file and load everything again when user accidentally clicked the read file button
                ' it also avoid load the same multiple times
                ' however, since the database preset.xml file do not change and the preset could be changed
                ' even we read the same file, we want a different preset, so we reload everything, till I find a way to compare to previous mnemonic
                For Each group In _signalMgr.GroupedRawSignalsByType
                    Dim frags = group.SignalSignature.SignalName.Split(",")
                    Dim dir = frags(0).Trim
                    Dim mnic = frags(1).Trim
                    If obj.FileDirectory = dir And obj.Mnemonic = mnic Then
                        Exit Sub
                    End If
                Next
                Dim exampleFile = obj.ExampleFile
                If File.Exists(exampleFile) Then
                    If obj.FileType IsNot Nothing And DataConfigure.ReaderProperty IsNot Nothing Then
                        Try
                            _signalMgr.AddRawSignalsFromADir(obj, DataConfigure.ReaderProperty.ExampleTime)
                        Catch ex As Exception
                            Forms.MessageBox.Show("Error reading selected data file. Original message: " & ex.Message, "Error!", MessageBoxButtons.OK)
                            Exit Sub
                        End Try
                    End If
                    Try
                        _writeExampleFileAddressToConfig(exampleFile)
                        Dim config = New ConfigFileReader(Run.Model.ConfigFilePath)
                        _signalMgr.CleanUpSettingsSignals()
                        DataConfigure = New DataConfig(config.DataConfigure, _signalMgr)
                        ProcessConfigure = New ProcessConfig(config.ProcessConfigure, _signalMgr)
                        PostProcessConfigure = New PostProcessCustomizationConfig(config.PostProcessConfigure, _signalMgr)
                        DetectorConfigure = New DetectorConfig(config.DetectorConfigure, _signalMgr)

                        Dim modeMeters = New ModeMeterReader(Run.Model.ConfigFilePath).GetDetectors
                        If modeMeters.Count > 0 Then
                            For Each mm In modeMeters
                                DetectorConfigure.DetectorList.Add(New SmallSignalStabilityToolViewModel(mm, _signalMgr))
                            Next
                            ModeMeterXmlWriter.CheckMMDirsStatus(Run.Model, modeMeters)
                        End If
                        _signalMgr.DistinctMappingSignal()
                        'SiteMappingVM.SignalCoordsMappingVM = New SignalCoordsMappingViewModel(CoordsTableVM.SiteCoords, _signalMgr, config.SignalSiteMappingConfig);
                        RaiseEvent DEFAreasChanged()
                    Catch ex As Exception
                        Forms.MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK)
                    End Try
                    Run.Model.DataFileDirectories = New List(Of String)
                    For Each info In SignalMgr.FileInfo
                        Run.Model.DataFileDirectories.Add(info.FileDirectory)
                    Next
                Else
                    Forms.MessageBox.Show("Specified example file does not exits.", "Error!", MessageBoxButtons.OK)
                End If
            Else
                Forms.MessageBox.Show("Selected example file type does not match selected data input file type.", "Error!", MessageBoxButtons.OK)
            End If
        End Sub

        'Private Function _checkDataFileMatch(obj As InputFileInfoViewModel) As Boolean
        '    Dim tp = ""
        '    Try
        '        tp = Path.GetExtension(obj.ExampleFile).Substring(1).ToLower()
        '    Catch

        '    End Try
        '    If obj.FileType.ToString.ToLower = tp Then
        '        Return True
        '    ElseIf obj.FileType = DataFileType.powHQ AndAlso tp = "mat" Then
        '        Return True
        '    ElseIf obj.FileType = DataFileType.PI AndAlso tp = "xml" Then
        '        Return True
        '    Else
        '        Return False
        '    End If
        'End Function

        Private _updateExampleFile As ICommand
        Public Property UpdateExampleFile As ICommand
            Get
                Return _updateExampleFile
            End Get
            Set(ByVal value As ICommand)
                _updateExampleFile = value
            End Set
        End Property
        Event SaveNewTask(ByRef svm As SettingsViewModel)
        Private Sub _writeExampleFileAddressToConfig(exampleFilePath As String)
            If Not File.Exists(Run.Model.ConfigFilePath) Then
                RaiseEvent SaveNewTask(Me)
            End If
            If File.Exists(Run.Model.ConfigFilePath) Then
                Dim writer = New ConfigFileWriter(Me, Run.Model)
                writer.WriteReaderProperties(Run.Model.ConfigFilePath, DataConfigure.ReaderProperty)
                'writer.UpdateExampleFileAddress(exampleFilePath)
                'writer.WriteXmlConfigFile(Run.Model.ConfigFilePath)
            Else
                Forms.MessageBox.Show("Task does not exist yet.", "Error!", MessageBoxButtons.OK)
            End If
        End Sub

        'Private Sub _buildInputFileFolderTree(fileInfo As InputFileInfoViewModel)
        '    For Each group In _signalMgr.GroupedRawSignalsByType
        '        If group.SignalSignature.SignalName.Split(",")(0) = fileInfo.FileDirectory Then
        '            _signalMgr.GroupedRawSignalsByType.Remove(group)
        '            Exit For
        '        End If
        '    Next
        '    For Each group In _signalMgr.GroupedRawSignalsByPMU
        '        If group.SignalSignature.SignalName.Split(",")(0) = fileInfo.FileDirectory Then
        '            _signalMgr.GroupedRawSignalsByPMU.Remove(group)
        '            Exit For
        '        End If
        '    Next
        '    If Directory.Exists(fileInfo.FileDirectory) Then
        '        _signalMgr.AddRawSignalsFromADir(fileInfo)
        '    End If
        '    'Dim _sampleFile = ""
        '    'Try
        '    '    fileInfo.InputFileTree = New ObservableCollection(Of Folder)
        '    '    fileInfo.InputFileTree.Add(New Folder(fileInfo.FileDirectory, fileInfo.FileType.ToString, _sampleFile))
        '    'Catch ex As Exception
        '    '    _addLog("Error reading input data directory! " & ex.Message)
        '    'End Try
        '    'If String.IsNullOrEmpty(_sampleFile) Then
        '    '    _addLog("No file of type: " & fileInfo.FileType.ToString & " is found in: " & fileInfo.FileDirectory)
        '    'Else
        '    '    Try
        '    '        _readFirstDataFile(_sampleFile, fileInfo)
        '    '        If fileInfo.FileType.ToString = "pdat" Then
        '    '            _signalMgr.TagSignals(fileInfo, fileInfo.SignalList)
        '    '        End If
        '    '    Catch ex As Exception
        '    '        _addLog("Error sampling input data file! " & ex.Message)
        '    '    End Try
        '    'End If
        'End Sub
        'Private Sub _readFirstDataFile(sampleFile As String, fileInfo As InputFileInfo)
        '    If System.IO.Path.GetExtension(sampleFile).Substring(1) = "csv" Then
        '        'Dim CSVSampleFile As New JSIS_CSV_Reader.JSISCSV_Reader
        '        'Dim signals = CSVSampleFile.OpenCSV4row(_sampleFile)
        '        Dim fr As FileIO.TextFieldParser = New FileIO.TextFieldParser(sampleFile)
        '        fr.TextFieldType = FileIO.FieldType.Delimited
        '        fr.Delimiters = New String() {","}
        '        fr.HasFieldsEnclosedInQuotes = True
        '        'fileInfo.SamplingRate = System.IO.File.ReadAllLines(sampleFile).Length
        '        Dim pmuName = sampleFile.Split("\").Last.Split("_")(0)
        '        'Dim pmuName = sampleFile.Split("\").Last.Split(".")(0)
        '        'If Not _allPMUs.Contains(pmuName) Then
        '        '    _allPMUs.Add(pmuName)
        '        'End If
        '        Dim signalNames = fr.ReadFields.Skip(1).ToList
        '        Dim signalTypes = fr.ReadFields.Skip(1).ToList
        '        Dim signalUnits = fr.ReadFields.Skip(1).ToList
        '        fr.ReadLine()
        '        fr.ReadLine()
        '        Dim time1 = fr.ReadFields(0)
        '        Dim time2 = fr.ReadFields(0)
        '        Try
        '            Dim t1 = Convert.ToDouble(time1)
        '            Dim t2 = Convert.ToDouble(time2)
        '            fileInfo.SamplingRate = Math.Round((1 / (t2 - t1)) / 10) * 10.ToString
        '        Catch ex As Exception
        '            Dim t1 = DateTime.Parse(time1, CultureInfo.InvariantCulture)
        '            Dim t2 = DateTime.Parse(time2, CultureInfo.InvariantCulture)
        '            Dim dif = t2.Subtract(t1).TotalSeconds
        '            fileInfo.SamplingRate = Math.Round((1 / dif) / 10) * 10.ToString
        '        End Try
        '        Dim type = ""
        '        Dim signalName = ""
        '        Dim signalList = New List(Of String)
        '        Dim signalSignatureList = New ObservableCollection(Of SignalSignatureViewModel)
        '        For index = 0 To signalNames.Count - 1
        '            Dim newSignal = New SignalSignatureViewModel
        '            newSignal.PMUName = pmuName
        '            newSignal.Unit = signalUnits(index)
        '            newSignal.SignalName = signalNames(index)
        '            newSignal.SamplingRate = fileInfo.SamplingRate
        '            signalList.Add(signalNames(index))
        '            Select Case signalTypes(index)
        '                Case "VPM"
        '                    'signalName = signalNames(index).Split(".")(0) & ".VMP"
        '                    'signalName = signalNames(index)
        '                    newSignal.TypeAbbreviation = "VMP"
        '                Case "VPA"
        '                    'signalName = signalNames(index).Split(".")(0) & ".VAP"
        '                    'signalName = signalNames(index)
        '                    newSignal.TypeAbbreviation = "VAP"
        '                Case "IPM"
        '                    'signalName = signalNames(index).Split(".")(0) & ".IMP"
        '                    'signalName = signalNames(index)
        '                    newSignal.TypeAbbreviation = "IMP"
        '                Case "IPA"
        '                    'signalName = signalNames(index).Split(".")(0) & ".IAP"
        '                    'signalName = signalNames(index)
        '                    newSignal.TypeAbbreviation = "IAP"
        '                Case "F"
        '                    'signalName = signalNames(index)
        '                    newSignal.TypeAbbreviation = "F"
        '                Case "P"
        '                    'signalName = signalNames(index)
        '                    newSignal.TypeAbbreviation = "P"
        '                Case "Q"
        '                    'signalName = signalNames(index)
        '                    newSignal.TypeAbbreviation = "Q"
        '                Case Else
        '                    Throw New Exception("Error! Invalid signal type " & signalTypes(index) & " found in file: " & sampleFile & " !")
        '            End Select
        '            newSignal.OldSignalName = newSignal.SignalName
        '            newSignal.OldTypeAbbreviation = newSignal.TypeAbbreviation
        '            newSignal.OldUnit = newSignal.Unit
        '            signalSignatureList.Add(newSignal)
        '        Next
        '        fileInfo.SignalList = signalList
        '        fileInfo.TaggedSignals = signalSignatureList
        '        fileInfo.GroupedSignalsByPMU = _signalMgr.SortSignalByPMU(signalSignatureList)
        '        Dim newSig = New SignalSignatureViewModel(fileInfo.FileDirectory & ", Sampling Rate: " & fileInfo.SamplingRate & "/Second")
        '        newSig.SamplingRate = fileInfo.SamplingRate
        '        Dim a = New SignalTypeHierachy(newSig)
        '        a.SignalList = fileInfo.GroupedSignalsByPMU
        '        _signalMgr.GroupedRawSignalsByPMU.Add(a)
        '        fileInfo.GroupedSignalsByType = _signalMgr.SortSignalByType(signalSignatureList)
        '        newSig = New SignalSignatureViewModel(fileInfo.FileDirectory & ", Sampling Rate: " & fileInfo.SamplingRate & "/Second")
        '        newSig.SamplingRate = fileInfo.SamplingRate
        '        Dim b = New SignalTypeHierachy(newSig)
        '        b.SignalList = fileInfo.GroupedSignalsByType
        '        _signalMgr.GroupedRawSignalsByType.Add(b)
        '        _signalMgr.ReGroupedRawSignalsByType = _signalMgr.GroupedRawSignalsByType
        '    Else
        '        Dim PDATSampleFile As New PDATReader
        '        Try
        '            fileInfo.SignalList = PDATSampleFile.GetPDATSignalNameList(sampleFile)
        '            fileInfo.SamplingRate = PDATSampleFile.GetSamplingRate()
        '        Catch ex As Exception
        '            Forms.MessageBox.Show("PDAT Reading error! " & ex.Message)
        '        End Try
        '    End If
        'End Sub

        Private _fileTypeChanged As ICommand
        Public Property FileTypeChanged As ICommand
            Get
                Return _fileTypeChanged
            End Get
            Set(ByVal value As ICommand)
                _fileTypeChanged = value
            End Set
        End Property
        Private Sub _cleanInputFileInfo(obj As InputFileInfoViewModel)
            obj.ExampleFile = ""
            obj.FileDirectory = ""
            obj.Mnemonic = ""
            DataConfigure.ReaderProperty.CheckHasDBDataSource()
            'If obj.FileType = DataFileType.PI Then
            '    DataConfigure.ReaderProperty.ModeName = ModeType.Archive
            '    DataConfigure.ReaderProperty.CanChooseMode = False
            'Else
            'DataConfigure.ReaderProperty.CanChooseMode = True
            '    For Each info In DataConfigure.ReaderProperty.InputFileInfos
            '        If info.FileType = DataFileType.PI Then
            '            DataConfigure.ReaderProperty.CanChooseMode = False
            '            Exit For
            '        End If
            '    Next
            'End If
        End Sub
        'Private _canChooseMode As Boolean
        'Public Property CanChooseMode As Boolean
        '    Get
        '        Return _canChooseMode
        '    End Get
        '    Set(ByVal value As Boolean)
        '        _canChooseMode = value
        '        OnPropertyChanged()
        '    End Set
        'End Property
        Private _configFileName As String
        Public Property ConfigFileName As String
            Get
                Return _configFileName
            End Get
            Set(ByVal value As String)
                _configFileName = value
                _addLog("Open file: " & _configFileName & " successfully!")
                _signalMgr.GroupedRawSignalsByType = New ObservableCollection(Of SignalTypeHierachy)
                _signalMgr.GroupedRawSignalsByPMU = New ObservableCollection(Of SignalTypeHierachy)
                NameTypeUnitStatusFlag = 0
                Try
                    Dim x = New ReadConfigXml.ConfigFileReader(value)

                    '_configData = XDocument.Load(_configFileName)
                    '_addLog("Reading " & _configFileName)
                    '_readConfigFile(_configData)
                    '_addLog("Done reading " & _configFileName & " .")
                Catch ex As Exception
                    _addLog("Error reading config file!" & vbCrLf & ex.Message)
                    Forms.MessageBox.Show("Error reading config file!" & vbCrLf & ex.Message & vbCrLf & "Please see logs below!", "Error!", MessageBoxButtons.OK)
                End Try

                OnPropertyChanged("ConfigFileName")
            End Set
        End Property

        Private _configData As XDocument

        Private _signalMgr As SignalManager
        Public Property SignalMgr As SignalManager
            Get
                Return _signalMgr
            End Get
            Set(ByVal value As SignalManager)
                _signalMgr = value
                OnPropertyChanged()
            End Set
        End Property

        Private _dataConfigure As DataConfig
        Public Property DataConfigure As DataConfig
            Get
                Return _dataConfigure
            End Get
            Set(ByVal value As DataConfig)
                _dataConfigure = value
                OnPropertyChanged()
            End Set
        End Property

        Private _processConfigure As ProcessConfig
        Public Property ProcessConfigure As ProcessConfig
            Get
                Return _processConfigure
            End Get
            Set(ByVal value As ProcessConfig)
                _processConfigure = value
                OnPropertyChanged()
            End Set
        End Property

        Private _postProcessConfigure As PostProcessCustomizationConfig
        Public Property PostProcessConfigure As PostProcessCustomizationConfig
            Get
                Return _postProcessConfigure
            End Get
            Set(ByVal value As PostProcessCustomizationConfig)
                _postProcessConfigure = value
                OnPropertyChanged()
            End Set
        End Property

        Private _detectorConfigure As DetectorConfig
        Public Property DetectorConfigure As DetectorConfig
            Get
                Return _detectorConfigure
            End Get
            Set(ByVal value As DetectorConfig)
                _detectorConfigure = value
                OnPropertyChanged()
            End Set
        End Property

        Private ReadOnly _dummySignature As SignalSignatureViewModel
        Public ReadOnly Property DummySignature As SignalSignatureViewModel
            Get
                Return _dummySignature
            End Get
        End Property

        'Private _openConfigFile As ICommand
        'Public Property OpenConfigFile As ICommand
        '    Get
        '        Return _openConfigFile
        '    End Get
        '    Set(ByVal value As ICommand)
        '        _openConfigFile = value
        '    End Set
        'End Property
        'Private Sub openConfigXMLFile()
        '    Dim openFileDialog As New Microsoft.Win32.OpenFileDialog()
        '    openFileDialog.RestoreDirectory = True
        '    openFileDialog.FileName = ""
        '    openFileDialog.DefaultExt = ".xml"
        '    openFileDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*"
        '    openFileDialog.InitialDirectory = CurDir() + "\ConfigFiles"

        '    Dim DialogResult? As Boolean = openFileDialog.ShowDialog
        '    If DialogResult Then
        '        ConfigFileName = openFileDialog.FileName
        '        _addLog("Open file: " & ConfigFileName & " successfully!")
        '        GroupedRawSignalsByType = New ObservableCollection(Of SignalTypeHierachy)
        '        GroupedRawSignalsByPMU = New ObservableCollection(Of SignalTypeHierachy)
        '        NameTypeUnitStatusFlag = 0
        '        Try
        '            _configData = XDocument.Load(_configFileName)
        '            _addLog("Reading " & ConfigFileName)
        '            _readConfigFile(_configData)
        '            _addLog("Done reading " & ConfigFileName & " .")
        '        Catch ex As Exception
        '            _addLog("Error reading config file!" & vbCrLf & ex.Message)
        '            Forms.MessageBox.Show("Error reading config file!" & vbCrLf & ex.Message & vbCrLf & "Please see logs below!", "Error!", MessageBoxButtons.OK)
        '        End Try
        '    End If
        'End Sub

        Private _specifyInitializationPath As ICommand
        Public Property SpecifyInitializationPath As ICommand
            Get
                Return _specifyInitializationPath
            End Get
            Set(ByVal value As ICommand)
                _specifyInitializationPath = value
            End Set
        End Property
        Private Sub _openInitializationPathFolder(obj As Object)
            Dim openDirectoryDialog As New FolderBrowserDialog()
            openDirectoryDialog.Description = "Select the initialization path"
            If Directory.Exists(ProcessConfigure.InitializationPath) Then
                openDirectoryDialog.SelectedPath = ProcessConfigure.InitializationPath
            Else
                openDirectoryDialog.SelectedPath = Environment.CurrentDirectory + "\Initialization"
            End If
            openDirectoryDialog.ShowNewFolderButton = True
            If (openDirectoryDialog.ShowDialog = DialogResult.OK) Then
                ProcessConfigure.InitializationPath = openDirectoryDialog.SelectedPath
            End If
        End Sub
        Private _specifyEventPath As ICommand
        Public Property SpecifyEventPath As ICommand
            Get
                Return _specifyEventPath
            End Get
            Set(ByVal value As ICommand)
                _specifyEventPath = value
            End Set
        End Property
        Private Sub _openEventPathFolder(obj As Object)
            Dim openDirectoryDialog As New FolderBrowserDialog()
            openDirectoryDialog.Description = "Select the initialization path"
            If Directory.Exists(DetectorConfigure.EventPath) Then
                openDirectoryDialog.SelectedPath = DetectorConfigure.EventPath
            Else
                openDirectoryDialog.SelectedPath = Environment.CurrentDirectory
            End If
            openDirectoryDialog.ShowNewFolderButton = True
            If (openDirectoryDialog.ShowDialog = DialogResult.OK) Then
                DetectorConfigure.EventPath = openDirectoryDialog.SelectedPath
            End If
        End Sub
#Region "Change signal selection"
        Private _selectedSignalChanged As ICommand
        Public Property SelectedSignalChanged As ICommand
            Get
                Return _selectedSignalChanged
            End Get
            Set(ByVal value As ICommand)
                _selectedSignalChanged = value
            End Set
        End Property
        Private Sub _signalSelected(obj As SignalTypeHierachy)
            If _currentSelectedStep IsNot Nothing Then
                If obj.SignalList.Count < 1 And (obj.SignalSignature.PMUName Is Nothing Or obj.SignalSignature.TypeAbbreviation Is Nothing) Then
                    _keepOriginalSelection(obj)
                    Forms.MessageBox.Show("Clicked item is not a valid signal, or contains no valid signal!", "Error!", MessageBoxButtons.OK)
                Else
                    If TypeOf _currentSelectedStep Is DQFilter OrElse TypeOf _currentSelectedStep Is Wrap OrElse TypeOf _currentSelectedStep Is Interpolate OrElse TypeOf _currentSelectedStep Is Unwrap OrElse TypeOf _currentSelectedStep Is NameTypeUnitPMU Then
                        Try
                            _changeSignalSelection(obj)
                            '_signalMgr.DetermineFileDirCheckableStatus()
                            _determineSamplingRateCheckableStatus()
                        Catch ex As Exception
                            _keepOriginalSelection(obj)
                            Forms.MessageBox.Show("Error selecting signal(s) for step " & _currentSelectedStep.StepCounter.ToString & " - " & _currentSelectedStep.Name & " ." & vbCrLf & ex.Message, "Error!", MessageBoxButtons.OK)
                            _addLog("Error selecting signal(s) for step " & _currentSelectedStep.StepCounter.ToString & " - " & _currentSelectedStep.Name & " ." & ex.Message)
                        End Try
                    ElseIf TypeOf _currentSelectedStep Is Multirate Then
                        If CurrentSelectedStep.FilterChoice <> 0 Then
                            Try
                                _changeSignalSelection(obj)
                                '_signalMgr.DetermineFileDirCheckableStatus()
                                _determineSamplingRateCheckableStatus()
                            Catch ex As Exception
                                _keepOriginalSelection(obj)
                                Forms.MessageBox.Show("Error selecting signal(s) for Multirate!" & vbCrLf & ex.Message, "Error!", MessageBoxButtons.OK)
                                _addLog("Error selecting signal(s) for Multirate!" & ex.Message)
                            End Try
                        Else
                            _keepOriginalSelection(obj)
                            Forms.MessageBox.Show("Please choose a way to specify sampling rate for Multirate!", "Error!", MessageBoxButtons.OK)
                            _addLog("Error selecting signal(s) for Multirate! No sampling rate specified!")
                        End If
                    ElseIf TypeOf _currentSelectedStep Is SmallSignalStabilityToolViewModel Then
                        Try
                            _currentSelectedStep.ChangeSignalSelection(obj)
                        Catch ex As Exception
                            Forms.MessageBox.Show("Error changing mode meter signal. Original message: " & ex.Message, "Error!", MessageBoxButtons.OK)
                        End Try
                    ElseIf TypeOf _currentSelectedStep Is DEFDetectorViewModel Then
                        Try
                            _currentSelectedStep.ChangeSignalSelection(obj)
                        Catch ex As Exception
                            Forms.MessageBox.Show("Error changing dissipation energy flow detector signal. Original message: " & ex.Message, "Error!", MessageBoxButtons.OK)
                        End Try
                    ElseIf TypeOf _currentSelectedStep Is DetectorBase Then
                        Try
                            _changeSignalSelection(obj)
                            '_signalMgr.DetermineFileDirCheckableStatus()
                            _determineSamplingRateCheckableStatus()
                        Catch ex As Exception
                            _keepOriginalSelection(obj)
                            Forms.MessageBox.Show("Error selecting signal(s) for detector " & _currentSelectedStep.Name & " ." & vbCrLf & ex.Message, "Error!", MessageBoxButtons.OK)
                            _addLog("Error selecting signal(s) for detector " & _currentSelectedStep.Name & " ." & ex.Message)
                        End Try
                    ElseIf TypeOf _currentSelectedStep Is TunableFilter Then
                        If DirectCast(_currentSelectedStep, TunableFilter).Type = TunableFilterType.PointOnWavePower Then
                            Try
                                _changePointOnWavePowCalFltrInputSignal(obj)
                                _checkPointOnWavePowCalFltrOutputSamplingRate(obj)
                                _checkPointOnWavePowCalFltrOutputUnit()
                                _signalMgr.ProcessConfigDetermineAllParentNodeStatus()
                                _determinePointOnWavePowCalFltrSamplingRateCheckableStatus()
                            Catch ex As Exception
                                _keepOriginalSelection(obj)
                                Forms.MessageBox.Show("Error selecting signal(s) for TunableFilter step! " & ex.Message, "Error!", MessageBoxButtons.OK)
                            End Try
                        Else
                            Try
                                _changeSignalSelectionUnarySteps(obj)
                                _signalMgr.ProcessConfigDetermineAllParentNodeStatus()
                                _determineSamplingRateCheckableStatus()
                            Catch ex As Exception
                                _keepOriginalSelection(obj)
                                Forms.MessageBox.Show("Error selecting signal(s) for TunableFilter step! " & ex.Message, "Error!", MessageBoxButtons.OK)
                            End Try
                        End If
                    Else
                        Try
                            Select Case _currentSelectedStep.Name
                                Case "Scalar Repetition"
                                    Throw New Exception("Please do NOT select signals for Scalar Repetition!")
                                Case "Addition"
                                    _changeSignalSelection(obj)
                                    _checkAdditionCustomizationOutputTypeAndSamplingRate()
                                Case "Multiplication"
                                    _changeSignalSelection(obj)
                                    _checkMultiplicationCustomizationOutputTypeAndSamplingRate()
                                Case "Subtraction"
                                    _setFocusedTextboxSubtraction(obj)
                                    _checkSubtractionCustomizationOutputTypeAndSamplingRate()
                                Case "Division"
                                    _setFocusedTextboxDivision(obj)
                                    If _currentSelectedStep.Dividend.IsValid AndAlso _currentSelectedStep.Divisor.IsValid Then
                                        _checkDivisionCustomizationOutputTypeAndSamplingRate()
                                    End If
                                Case "Exponential"
                                    _changeSignalSelectionUnarySteps(obj)
                                    _checkRaiseExpCustomizationOutputType()
                                Case "Sign Reversal", "Absolute Value", "Real Component", "Imaginary Component", "Complex Conjugate", "Angle Conversion", "Metric Prefix", "Duplicate Signals"
                                    _changeSignalSelectionUnarySteps(obj)
                                ' For these 7 unary customization steps, the signal type and units of the input signal are applied to the output signal
                                ' So, the type are applied while signals are added in the _changeSignalSelectionUnarySteps() subroutine
                                ' Thus we don't need another subroutine to check output signal type
                                ' However, if we encounter bugs due to type for these steps, we might want to add a subroutine: _checkUnaryCustomizationOutputType()
                                Case "Angle Calculation"
                                    _changeSignalSelectionUnarySteps(obj)
                                    _checkAngleForComplexSignalCustomizationOutputType()
                                Case "Phasor Creation"
                                    _changeSignalSelectionPhasorCreation(obj)
                                ' Type for output signal was set when signal selected, no need to check
                                Case "Power Calculation"
                                    If _currentSelectedStep.OutputInputMappingPair(0).Value.Count = 2 Then
                                        _changePhasorSignalForPowerCalculationCustomization(obj)
                                    Else
                                        _changeMagAngSignalForPowerCalculationCustomization(obj)
                                    End If
                                ' Here, type of output signal is not related to input signal, no need to check
                                ' It only relates to user choice and would only affect MATLAB calculation afterwards
                                Case "Signal Type/Unit"
                                    _specifySignalTypeUnitSignalSelectionChanged(obj)
                                Case "PCA", "Graph Eigenvalue"
                                    _changeSignalSelection(obj)
                                    _checkEVPCACustomizationOutputTypeAndSamplingRate()
                                Case Else
                                    Throw New Exception("Customization step not supported!")
                            End Select
                            _recoverCheckStatusOfCurrentStep(_currentSelectedStep)
                            '_signalMgr.DetermineFileDirCheckableStatus()
                            _determineSamplingRateCheckableStatus()
                        Catch ex As Exception
                            _keepOriginalSelection(obj)
                            If _currentSelectedStep.Name = "Addition" Or _currentSelectedStep.Name = "Multiplication" Or _currentSelectedStep.Name = "Graph Eigenvalue" Or _currentSelectedStep.Name = "PCA" Then
                                _addOrDeleteInputSignal(obj, obj.SignalSignature.IsChecked)
                            End If
                            If _currentSelectedStep.Name = "Angle Calculation" Then
                                _removeMatchingInputOutputSignalsUnary(obj)
                            End If
                            Forms.MessageBox.Show("Error selecting signal(s) for customization step! " & ex.Message, "Error!", MessageBoxButtons.OK)
                        End Try
                    End If
                End If
            Else
                _keepOriginalSelection(obj)
                Forms.MessageBox.Show("Please select a step first!", "Error!", MessageBoxButtons.OK)
            End If
        End Sub

        Private Sub _checkPointOnWavePowCalFltrOutputSamplingRate(obj As SignalTypeHierachy)
            Dim freq = -1
            For Each signal In CurrentSelectedStep.InputChannels
                If signal.SamplingRate <> -1 Then
                    freq = signal.SamplingRate
                    Exit For
                End If
            Next
            For Each signal In CurrentSelectedStep.OutputChannels
                signal.SamplingRate = freq
            Next
        End Sub
        Private Sub _checkPointOnWavePowCalFltrOutputUnit()
            Dim PhAVUnit = CurrentSelectedStep.POWInputSignals.PhaseAVoltage.Unit
            Dim PhAIUnit = CurrentSelectedStep.POWInputSignals.PhaseACurrent.Unit
            Dim output1 = CurrentSelectedStep.OutputChannels(0)
            Dim output2 = CurrentSelectedStep.OutputChannels(1)
            If PhAVUnit = "V" And PhAIUnit = "A" Then
                output1.Unit = "W"
                output2.Unit = "VAR"
                output1.OldUnit = output1.Unit
                output2.OldUnit = output2.Unit
            ElseIf PhAVUnit = "kV" And PhAIUnit = "A" Then
                output1.Unit = "kW"
                output2.Unit = "kVAR"
                output1.OldUnit = output1.Unit
                output2.OldUnit = output2.Unit
            ElseIf PhAVUnit = "V" And PhAIUnit = "kA" Then
                output1.Unit = "kW"
                output2.Unit = "kVAR"
                output1.OldUnit = output1.Unit
                output2.OldUnit = output2.Unit
            ElseIf PhAVUnit = "kV" And PhAIUnit = "kA" Then
                output1.Unit = "MW"
                output2.Unit = "MVAR"
                output1.OldUnit = output1.Unit
                output2.OldUnit = output2.Unit
            End If
        End Sub
        Private Sub _determinePointOnWavePowCalFltrSamplingRateCheckableStatus()
            Dim freq = -1
            For Each signal In CurrentSelectedStep.InputChannels
                If signal.SamplingRate <> -1 Then
                    freq = signal.SamplingRate
                    Exit For
                End If
            Next
            _signalMgr.DetermineSamplingRateCheckableStatus(_currentSelectedStep, _currentTabIndex, freq)
        End Sub
        Private Sub _changePointOnWavePowCalFltrInputSignal(obj As SignalTypeHierachy)
            If obj.SignalList.Count > 0 Then
                Throw New Exception("Please only select a signal valid signal instead of a group of signals!")
            Else
                If _pointOnWavePowCalFltrInputSignalNeedToBeChanged = "PhaseAVoltage" Then
                    If obj.SignalSignature.TypeAbbreviation.Substring(0, 2) <> "VW" Then
                        Throw New Exception("Input must be a point-on-wave voltage")
                    End If
                    CurrentSelectedStep.InputChannels.Remove(CurrentSelectedStep.POWInputSignals.PhaseAVoltage)
                    CurrentSelectedStep.POWInputSignals.PhaseAVoltage.IsChecked = False
                    CurrentSelectedStep.POWInputSignals.PhaseAVoltage = DummySignature
                    If obj.SignalSignature.IsChecked Then
                        CurrentSelectedStep.POWInputSignals.PhaseAVoltage = obj.SignalSignature
                        CurrentSelectedStep.InputChannels.Add(obj.SignalSignature)
                    End If
                End If
                If _pointOnWavePowCalFltrInputSignalNeedToBeChanged = "PhaseBVoltage" Then
                    If obj.SignalSignature.TypeAbbreviation.Substring(0, 2) <> "VW" Then
                        Throw New Exception("Input must be a point-on-wave voltage")
                    End If
                    CurrentSelectedStep.InputChannels.Remove(CurrentSelectedStep.POWInputSignals.PhaseBVoltage)
                    CurrentSelectedStep.POWInputSignals.PhaseBVoltage.IsChecked = False
                    CurrentSelectedStep.POWInputSignals.PhaseBVoltage = DummySignature
                    If obj.SignalSignature.IsChecked Then
                        CurrentSelectedStep.POWInputSignals.PhaseBVoltage = obj.SignalSignature
                        CurrentSelectedStep.InputChannels.Add(obj.SignalSignature)
                    End If
                End If
                If _pointOnWavePowCalFltrInputSignalNeedToBeChanged = "PhaseCVoltage" Then
                    If obj.SignalSignature.TypeAbbreviation.Substring(0, 2) <> "VW" Then
                        Throw New Exception("Input must be a point-on-wave voltage")
                    End If
                    CurrentSelectedStep.InputChannels.Remove(CurrentSelectedStep.POWInputSignals.PhaseCVoltage)
                    CurrentSelectedStep.POWInputSignals.PhaseCVoltage.IsChecked = False
                    CurrentSelectedStep.POWInputSignals.PhaseCVoltage = DummySignature
                    If obj.SignalSignature.IsChecked Then
                        CurrentSelectedStep.POWInputSignals.PhaseCVoltage = obj.SignalSignature
                        CurrentSelectedStep.InputChannels.Add(obj.SignalSignature)
                    End If
                End If
                If _pointOnWavePowCalFltrInputSignalNeedToBeChanged = "PhaseACurrent" Then
                    If obj.SignalSignature.TypeAbbreviation.Substring(0, 2) <> "IW" Then
                        Throw New Exception("Input must be a point-on-wave current")
                    End If
                    CurrentSelectedStep.InputChannels.Remove(CurrentSelectedStep.POWInputSignals.PhaseACurrent)
                    CurrentSelectedStep.POWInputSignals.PhaseACurrent.IsChecked = False
                    CurrentSelectedStep.POWInputSignals.PhaseACurrent = DummySignature
                    If obj.SignalSignature.IsChecked Then
                        CurrentSelectedStep.POWInputSignals.PhaseACurrent = obj.SignalSignature
                        CurrentSelectedStep.InputChannels.Add(obj.SignalSignature)
                    End If
                End If
                If _pointOnWavePowCalFltrInputSignalNeedToBeChanged = "PhaseBCurrent" Then
                    If obj.SignalSignature.TypeAbbreviation.Substring(0, 2) <> "IW" Then
                        Throw New Exception("Input must be a point-on-wave current")
                    End If
                    CurrentSelectedStep.InputChannels.Remove(CurrentSelectedStep.POWInputSignals.PhaseBCurrent)
                    CurrentSelectedStep.POWInputSignals.PhaseBCurrent.IsChecked = False
                    CurrentSelectedStep.POWInputSignals.PhaseBCurrent = DummySignature
                    If obj.SignalSignature.IsChecked Then
                        CurrentSelectedStep.POWInputSignals.PhaseBCurrent = obj.SignalSignature
                        CurrentSelectedStep.InputChannels.Add(obj.SignalSignature)
                    End If
                End If
                If _pointOnWavePowCalFltrInputSignalNeedToBeChanged = "PhaseCCurrent" Then
                    If obj.SignalSignature.TypeAbbreviation.Substring(0, 2) <> "IW" Then
                        Throw New Exception("Input must be a point-on-wave current")
                    End If
                    CurrentSelectedStep.InputChannels.Remove(CurrentSelectedStep.POWInputSignals.PhaseCCurrent)
                    CurrentSelectedStep.POWInputSignals.PhaseCCurrent.IsChecked = False
                    CurrentSelectedStep.POWInputSignals.PhaseCCurrent = DummySignature
                    If obj.SignalSignature.IsChecked Then
                        CurrentSelectedStep.POWInputSignals.PhaseCCurrent = obj.SignalSignature
                        CurrentSelectedStep.InputChannels.Add(obj.SignalSignature)
                    End If
                End If
            End If
        End Sub

        Private Sub _checkAngleForComplexSignalCustomizationOutputType()
            For Each inputOutputPair In CurrentSelectedStep.OutputInputMappingPair
                If inputOutputPair.Value.Count > 0 Then
                    Dim input = inputOutputPair.Value(0)
                    inputOutputPair.Key.TypeAbbreviation = "OTHER"
                    inputOutputPair.Key.Unit = "O"
                    If input.IsValid Then
                        'If input.TypeAbbreviation = "CP" Then
                        '    inputOutputPair.Key.Unit = "RAD"
                        If input.TypeAbbreviation.Length = 3 Then
                                Dim letter2 = input.TypeAbbreviation.ToString.ToArray(1)
                                If letter2 = "P" Then
                                    inputOutputPair.Key.TypeAbbreviation = input.TypeAbbreviation.Substring(0, 1) & "A" & input.TypeAbbreviation.Substring(2, 1)
                                    inputOutputPair.Key.Unit = "RAD"
                                End If
                            End If
                        End If
                End If
            Next
        End Sub

        Private Sub _checkRaiseExpCustomizationOutputType()
            For Each inputOutputPair In CurrentSelectedStep.OutputInputMappingPair
                If inputOutputPair.Value.Count > 0 Then
                    If CurrentSelectedStep.Exponent = 1 OrElse inputOutputPair.Value(0).TypeAbbreviation = "SC" Then
                        inputOutputPair.Key.TypeAbbreviation = inputOutputPair.Value(0).TypeAbbreviation
                        inputOutputPair.Key.Unit = inputOutputPair.Value(0).Unit
                    Else
                        inputOutputPair.Key.TypeAbbreviation = "OTHER"
                        inputOutputPair.Key.Unit = "O"
                    End If
                End If
            Next
        End Sub

        Private Sub _checkMultiplicationCustomizationOutputTypeAndSamplingRate()
            Dim type = ""
            Dim unit = ""
            Dim countNonScalarType = 0
            Dim rate = -1
            For Each signal In CurrentSelectedStep.InputChannels
                If signal.TypeAbbreviation <> "SC" Then
                    countNonScalarType += 1
                    If String.IsNullOrEmpty(type) Then
                        type = signal.TypeAbbreviation
                    End If
                    If String.IsNullOrEmpty(unit) Then
                        unit = signal.Unit
                    End If
                End If
                If rate = -1 Then
                    rate = signal.SamplingRate
                ElseIf rate <> signal.SamplingRate Then
                    _addLog("Sampling rate of all factors in multiplication customization have to be the same. Different sampling rate found in multiplication customization step: " & CurrentSelectedStep.stepCounter & ", with sampling rate: " & rate & " and " & signal.SamplingRate & ".")
                    CurrentSelectedStep.OutputChannels(0).SamplingRate = -1
                    'Throw New Exception("Sampling rate of all terms in multiplication customization have to be the same. Different sampling rate found in multiplication customization step: " & CurrentSelectedStep.stepCounter & ", with sampling rate: " & rate & " and " & signal.SamplingRate & ".")
                    Exit Sub
                End If
            Next
            If countNonScalarType = 0 Then
                CurrentSelectedStep.OutputChannels(0).TypeAbbreviation = "SC"
                CurrentSelectedStep.OutputChannels(0).Unit = "SC"
            ElseIf countNonScalarType = 1 Then
                CurrentSelectedStep.OutputChannels(0).TypeAbbreviation = type
                CurrentSelectedStep.OutputChannels(0).Unit = unit
            Else
                CurrentSelectedStep.OutputChannels(0).TypeAbbreviation = "OTHER"
                CurrentSelectedStep.OutputChannels(0).Unit = "O"
            End If
            If rate <> -1 Then
                CurrentSelectedStep.OutputChannels(0).SamplingRate = rate
            End If
        End Sub

        Private Sub _checkDivisionCustomizationOutputTypeAndSamplingRate()
            'If CurrentSelectedStep.Divisor.TypeAbbreviation IsNot Nothing AndAlso CurrentSelectedStep.Dividend.TypeAbbreviation IsNot Nothing Then
            '    If CurrentSelectedStep.Divisor.TypeAbbreviation <> "SC" AndAlso CurrentSelectedStep.Divisor.TypeAbbreviation <> "OTHER" Then
            '        If CurrentSelectedStep.Dividend.TypeAbbreviation <> "SC" AndAlso CurrentSelectedStep.Dividend.TypeAbbreviation <> "OTHER" Then
            '            If CurrentSelectedStep.Divisor.TypeAbbreviation <> CurrentSelectedStep.Dividend.TypeAbbreviation Then
            '                _addLog("Type of Divisor and Dividend should match! Different signal type found in Division customization step: " & CurrentSelectedStep.stepCounter & ", with types: " & CurrentSelectedStep.Divisor.TypeAbbreviation & " and " & CurrentSelectedStep.Dividend.TypeAbbreviation & ".")
            '                Throw New Exception("Type of Dividend and Divisor should match! Different signal type found in Division customization step: " & CurrentSelectedStep.stepCounter & ", with types: " & CurrentSelectedStep.Divisor.TypeAbbreviation & " and " & CurrentSelectedStep.Dividend.TypeAbbreviation & ".")
            '            End If
            '        End If
            '    End If
            'End If
            If CurrentSelectedStep.Divisor.IsValid AndAlso CurrentSelectedStep.Dividend.IsValid AndAlso CurrentSelectedStep.Divisor.Unit IsNot Nothing AndAlso CurrentSelectedStep.Dividend.Unit IsNot Nothing AndAlso CurrentSelectedStep.Divisor.SamplingRate = CurrentSelectedStep.Dividend.SamplingRate Then
                CurrentSelectedStep.OutputChannels(0).SamplingRate = CurrentSelectedStep.Divisor.SamplingRate
                If CurrentSelectedStep.Divisor.TypeAbbreviation = "SC" Then
                    CurrentSelectedStep.OutputChannels(0).TypeAbbreviation = CurrentSelectedStep.Dividend.TypeAbbreviation
                    CurrentSelectedStep.OutputChannels(0).Unit = CurrentSelectedStep.Dividend.Unit
                ElseIf CurrentSelectedStep.Divisor.Unit = CurrentSelectedStep.Dividend.Unit Then
                    CurrentSelectedStep.OutputChannels(0).TypeAbbreviation = "SC"
                    CurrentSelectedStep.OutputChannels(0).Unit = "SC"
                Else
                    CurrentSelectedStep.OutputChannels(0).TypeAbbreviation = "OTHER"
                    CurrentSelectedStep.OutputChannels(0).Unit = "O"
                End If
            Else
                CurrentSelectedStep.OutputChannels(0).SamplingRate = -1
                _addLog("Dividend and Divisor have to have units and their sampling rate should match! Different Sampling rate found in Division customization step: " & CurrentSelectedStep.stepCounter & ", with sampling rate: " & CurrentSelectedStep.Divisor.SamplingRate & " and " & CurrentSelectedStep.Dividend.SamplingRate & ".")
                'Throw New Exception("Sampling rate of Dividend and Divisor should match! Different Sampling rate found in Division customization step: " & CurrentSelectedStep.stepCounter & ", with sampling rate: " & CurrentSelectedStep.Divisor.SamplingRate & " and " & CurrentSelectedStep.Dividend.SamplingRate & ".")
            End If
        End Sub

        Private Sub _checkSubtractionCustomizationOutputTypeAndSamplingRate()
            CurrentSelectedStep.OutputChannels(0).TypeAbbreviation = "OTHER"
            CurrentSelectedStep.OutputChannels(0).Unit = "O"
            If CurrentSelectedStep.Subtrahend.TypeAbbreviation IsNot Nothing AndAlso CurrentSelectedStep.Minuend.TypeAbbreviation IsNot Nothing Then
                If CurrentSelectedStep.Subtrahend.TypeAbbreviation <> CurrentSelectedStep.Minuend.TypeAbbreviation Then
                    '_addLog("Type of subtrahend and minuend should match! Different signal type found in subtraction customization step: " & CurrentSelectedStep.stepCounter & ", with types: " & CurrentSelectedStep.Subtrahend.TypeAbbreviation & " and " & CurrentSelectedStep.Minuend.TypeAbbreviation & ".")
                    'Throw New Exception("Type of subtrahend and minuend should match! Different signal type found in subtraction customization step: " & CurrentSelectedStep.stepCounter & ", with types: " & CurrentSelectedStep.Subtrahend.TypeAbbreviation & " and " & CurrentSelectedStep.Minuend.TypeAbbreviation & ".")
                Else
                    CurrentSelectedStep.OutputChannels(0).TypeAbbreviation = CurrentSelectedStep.Subtrahend.TypeAbbreviation
                End If
                If CurrentSelectedStep.Subtrahend.Unit <> CurrentSelectedStep.Minuend.Unit Then
                Else
                    CurrentSelectedStep.OutputChannels(0).Unit = CurrentSelectedStep.Subtrahend.Unit
                End If
            End If
            If CurrentSelectedStep.Subtrahend.IsValid AndAlso CurrentSelectedStep.Minuend.IsValid Then
                If CurrentSelectedStep.Subtrahend.SamplingRate = CurrentSelectedStep.Minuend.SamplingRate Then
                    CurrentSelectedStep.OutputChannels(0).SamplingRate = CurrentSelectedStep.Subtrahend.SamplingRate
                Else
                    CurrentSelectedStep.OutputChannels(0).SamplingRate = -1
                    '_addLog("Sampling rate of subtrahend and minuend should match! Different Sampling rate found in subtraction customization step: " & CurrentSelectedStep.stepCounter & ", with sampling rate: " & CurrentSelectedStep.Subtrahend.SamplingRate & " and " & CurrentSelectedStep.Minuend.SamplingRate & ".")
                    'Throw New Exception("Sampling rate of subtrahend and minuend should match! Different Sampling rate found in subtraction customization step: " & CurrentSelectedStep.stepCounter & ", with sampling rate: " & CurrentSelectedStep.Subtrahend.SamplingRate & " and " & CurrentSelectedStep.Minuend.SamplingRate & ".")
                End If
            End If
            'If CurrentSelectedStep.SubtrahendOrDivisor.IsValid AndAlso CurrentSelectedStep.MinuendOrDividend.IsValid AndAlso CurrentSelectedStep.SubtrahendOrDivisor.SamplingRate = CurrentSelectedStep.MinuendOrDividend.SamplingRate Then
            '    CurrentSelectedStep.OutputChannels(0).SamplingRate = CurrentSelectedStep.SubtrahendOrDivisor.SamplingRate
            'Else
            '    CurrentSelectedStep.OutputChannels(0).SamplingRate = -1
            '    _addLog("Sampling rate of subtrahend and minuend should match! Different Sampling rate found in subtraction customization step: " & CurrentSelectedStep.stepCounter & ", with sampling rate: " & CurrentSelectedStep.SubtrahendOrDivisor.SamplingRate & " and " & CurrentSelectedStep.MinuendOrDividend.SamplingRate & ".")
            '    Throw New Exception("Sampling rate of subtrahend and minuend should match! Different Sampling rate found in subtraction customization step: " & CurrentSelectedStep.stepCounter & ", with sampling rate: " & CurrentSelectedStep.SubtrahendOrDivisor.SamplingRate & " and " & CurrentSelectedStep.MinuendOrDividend.SamplingRate & ".")
            'End If
        End Sub

        Private Sub _checkAdditionCustomizationOutputTypeAndSamplingRate()
            Dim type = ""
            Dim rate = -1
            Dim unit = ""
            For Each signal In CurrentSelectedStep.InputChannels
                If String.IsNullOrEmpty(type) Then
                    type = signal.TypeAbbreviation
                ElseIf type <> signal.TypeAbbreviation Then
                    _addLog("All terms of addition customization have to be the same signal type! Different signal type found in addition customization step: " & CurrentSelectedStep.stepCounter & ", with types: " & type & " and " & signal.TypeAbbreviation & ".")
                    CurrentSelectedStep.OutputChannels(0).TypeAbbreviation = "OTHER"
                    CurrentSelectedStep.OutputChannels(0).Unit = "O"
                    'Throw New Exception("All terms of addition customization have to be the same signal type! Different signal type found in addition customization step: " & CurrentSelectedStep.stepCounter & ", with types: " & type & " and " & signal.TypeAbbreviation & ".")
                    Exit Sub
                End If
                If String.IsNullOrEmpty(unit) Then
                    unit = signal.TypeAbbreviation
                ElseIf unit <> signal.TypeAbbreviation Then
                    CurrentSelectedStep.OutputChannels(0).Unit = "O"
                    CurrentSelectedStep.OutputChannels(0).TypeAbbreviation = "OTHER"
                    'Throw New Exception("All terms of addition customization have to be the same signal type! Different signal type found in addition customization step: " & CurrentSelectedStep.stepCounter & ", with types: " & type & " and " & signal.TypeAbbreviation & ".")
                    Exit Sub
                End If
                If rate = -1 Then
                    rate = signal.SamplingRate
                ElseIf rate <> signal.SamplingRate Then
                    _addLog("Sampling rate of all terms in addition customization have to be the same. Different sampling rate found in addition customization step: " & CurrentSelectedStep.stepCounter & ", with sampling rate: " & rate & " and " & signal.SamplingRate & ".")
                    CurrentSelectedStep.OutputChannels(0).SamplingRate = -1
                    'Throw New Exception("Sampling rate of all terms in addition customization have to be the same. Different sampling rate found in addition customization step: " & CurrentSelectedStep.stepCounter & ", with sampling rate: " & rate & " and " & signal.SamplingRate & ".")
                    Exit Sub
                End If
            Next
            If Not String.IsNullOrEmpty(type) Then
                CurrentSelectedStep.OutputChannels(0).TypeAbbreviation = type
            End If
            If rate <> -1 Then
                CurrentSelectedStep.OutputChannels(0).SamplingRate = rate
            End If
        End Sub

        Private Sub _checkEVPCACustomizationOutputTypeAndSamplingRate()
            Dim rate = -1
            For Each signal In CurrentSelectedStep.InputChannels
                If rate = -1 Then
                    rate = signal.SamplingRate
                ElseIf rate <> signal.SamplingRate Then
                    _addLog("Sampling rate of all terms in PCA/EigenValue Graph customization have to be the same. Different sampling rate found in addition customization step: " & CurrentSelectedStep.stepCounter & ", with sampling rate: " & rate & " and " & signal.SamplingRate & ".")
                    CurrentSelectedStep.OutputChannels(0).SamplingRate = -1
                    Exit Sub
                End If
            Next
            If rate <> -1 Then
                For Each output In CurrentSelectedStep.OutputChannels
                    output.SamplingRate = rate
                Next
            End If
        End Sub

        Private Sub _keepOriginalSelection(obj As SignalTypeHierachy)
            If obj.SignalList.Count = 0 AndAlso Not String.IsNullOrEmpty(obj.SignalSignature.PMUName) AndAlso Not String.IsNullOrEmpty(obj.SignalSignature.TypeAbbreviation) Then
                If obj.SignalSignature.IsChecked Then
                    obj.SignalSignature.IsChecked = False
                Else
                    obj.SignalSignature.IsChecked = True
                End If
            Else
                If obj.SignalSignature.IsChecked Then
                    _signalMgr.CheckAllChildren(obj, False)
                Else
                    _signalMgr.CheckAllChildren(obj, True)
                End If
                '_dataConfigDetermineAllParentNodeStatus()
                '_processConfigDetermineAllParentNodeStatus()
                '_postProcessDetermineAllParentNodeStatus()
                '_detectorConfigDetermineAllParentNodeStatus()
            End If
            _signalMgr.DetermineAllParentNodeStatus()
        End Sub
        Private _textboxesLostFocus As ICommand
        Public Property TextboxesLostFocus As ICommand
            Get
                Return _textboxesLostFocus
            End Get
            Set(ByVal value As ICommand)
                _textboxesLostFocus = value
            End Set
        End Property
        Private Sub _recoverCheckStatusOfCurrentStep(obj As Object)
            'why am I doing this? they should be checked already! Need to come back and think.........
            For Each signal In obj.InputChannels
                signal.IsChecked = True
            Next
            '_dataConfigDetermineAllParentNodeStatus()
            _signalMgr.DetermineDataConfigPostProcessConfigAllParentNodeStatus()
            If _currentSelectedStep IsNot Nothing AndAlso TypeOf _currentSelectedStep Is Customization Then
                _currentSelectedStep.CurrentCursor = ""
            End If
            _currentInputOutputPair = Nothing
            _currentFocusedPhasorSignalForPowerCalculation = Nothing
        End Sub

        'Private Sub _setFocusedTextboxSubtraction(obj As SignalTypeHierachy)
        '    If obj.SignalList.Count > 0 OrElse obj.SignalSignature.PMUName Is Nothing OrElse obj.SignalSignature.TypeAbbreviation Is Nothing Then    'if selected a group of signal
        '        Throw New Exception("Error! Please select valid signal for this textbox! We need a single signal, cannot be group of signals!")
        '    Else
        '        If _currentSelectedStep.CurrentCursor = "" Then ' if no textbox selected, textbox lost it focus right after a click any where else, so only click immediate follow a textbox selection would work
        '            Throw New Exception("Error! Please select a valid text box for this input signal!")
        '        ElseIf _currentSelectedStep.CurrentCursor = "Minuend" Then
        '            If _currentSelectedStep.Subtrahend IsNot Nothing AndAlso obj.SignalSignature = _currentSelectedStep.Subtrahend Then
        '                Throw New Exception("Minuend cannot be the same as the subtrahend!")
        '            End If
        '            If obj.SignalSignature.IsChecked Then       ' check box checked
        '                If _currentSelectedStep.Minuend IsNot Nothing And _currentSelectedStep.Minuend IsNot _currentSelectedStep.Subtrahend Then  ' if the current text box has content and not equal to the divisor
        '                    _currentSelectedStep.Minuend.IsChecked = False
        '                    _currentSelectedStep.InputChannels.Remove(_currentSelectedStep.Minuend)
        '                End If
        '                _currentSelectedStep.Minuend = obj.SignalSignature
        '                If Not _currentSelectedStep.InputChannels.Contains(obj.SignalSignature) Then
        '                    _currentSelectedStep.InputChannels.Add(obj.SignalSignature)
        '                End If
        '            Else                                        ' check box unchecked
        '                If _currentSelectedStep.Minuend Is obj.SignalSignature Then   ' if the content of the text box is the same as the clicked item and the checkbox is unchecked, means user wants to delete the content in the textbox
        '                    If _currentSelectedStep.Subtrahend Is obj.SignalSignature Then     ' however, if the textbox has the same contect as the divisor or subtrahend, we cannot uncheck the clicked item
        '                        obj.SignalSignature.IsChecked = True
        '                    Else
        '                        _currentSelectedStep.InputChannels.Remove(obj.SignalSignature)
        '                    End If
        '                    Dim dummy = New SignalSignatureViewModel("", "")
        '                    dummy.IsValid = False
        '                    _currentSelectedStep.Minuend = dummy
        '                End If
        '            End If
        '            _currentSelectedStep.CurrentCursor = ""
        '            _currentSelectedStep.ThisStepInputsAsSignalHerachyByType.SignalList = _signalMgr.SortSignalByType(_currentSelectedStep.InputChannels)
        '            _signalMgr.DetermineDataConfigPostProcessConfigAllParentNodeStatus()
        '        ElseIf _currentSelectedStep.CurrentCursor = "Subtrahend" Then
        '            If _currentSelectedStep.Minuend IsNot Nothing AndAlso obj.SignalSignature = _currentSelectedStep.Minuend Then
        '                Throw New Exception("Subtrahend cannot be the same as the minuend!")
        '            End If
        '            If obj.SignalSignature.IsChecked Then
        '                If _currentSelectedStep.Subtrahend IsNot Nothing And _currentSelectedStep.Subtrahend IsNot _currentSelectedStep.Minuend Then
        '                    _currentSelectedStep.Subtrahend.IsChecked = False
        '                    _currentSelectedStep.InputChannels.Remove(_currentSelectedStep.Subtrahend)
        '                End If
        '                _currentSelectedStep.Subtrahend = obj.SignalSignature
        '                If Not _currentSelectedStep.InputChannels.Contains(obj.SignalSignature) Then
        '                    _currentSelectedStep.InputChannels.Add(obj.SignalSignature)
        '                End If
        '            Else
        '                If _currentSelectedStep.Subtrahend Is obj.SignalSignature Then
        '                    If _currentSelectedStep.Minuend Is obj.SignalSignature Then
        '                        obj.SignalSignature.IsChecked = True
        '                    Else
        '                        _currentSelectedStep.InputChannels.Remove(obj.SignalSignature)
        '                    End If
        '                    Dim dummy = New SignalSignatureViewModel("", "")
        '                    dummy.IsValid = False
        '                    _currentSelectedStep.Subtrahend = dummy
        '                End If
        '            End If
        '            _currentSelectedStep.CurrentCursor = ""
        '            _currentSelectedStep.ThisStepInputsAsSignalHerachyByType.SignalList = _signalMgr.SortSignalByType(_currentSelectedStep.InputChannels)
        '            _signalMgr.DetermineDataConfigPostProcessConfigAllParentNodeStatus()
        '        End If
        '    End If
        'End Sub
        'Private Sub _setFocusedTextboxDivision(obj As SignalTypeHierachy)
        '    If obj.SignalList.Count > 0 OrElse obj.SignalSignature.PMUName Is Nothing OrElse obj.SignalSignature.TypeAbbreviation Is Nothing Then    'if selected a group of signal
        '        Throw New Exception("Error! Please select valid signal for this textbox! We need a single signal, cannot be group of signals!")
        '    Else
        '        If _currentSelectedStep.CurrentCursor = "" Then ' if no textbox selected, textbox lost it focus right after a click any where else, so only click immediate follow a textbox selection would work
        '            Throw New Exception("Error! Please select a valid text box (Dividend or Divisor) for this input signal!")
        '        ElseIf _currentSelectedStep.CurrentCursor = "Dividend" Then
        '            If _currentSelectedStep.Divisor IsNot Nothing AndAlso obj.SignalSignature = _currentSelectedStep.Divisor Then
        '                Throw New Exception("Dividend cannot be the same as the divisor!")
        '            End If
        '            If obj.SignalSignature.IsChecked Then       ' check box checked
        '                If _currentSelectedStep.Dividend IsNot Nothing And _currentSelectedStep.Dividend IsNot _currentSelectedStep.Divisor Then  ' if the current text box has content and not equal to the divisor
        '                    _currentSelectedStep.Dividend.IsChecked = False
        '                    _currentSelectedStep.InputChannels.Remove(_currentSelectedStep.Dividend)
        '                End If
        '                _currentSelectedStep.Dividend = obj.SignalSignature
        '                If Not _currentSelectedStep.InputChannels.Contains(obj.SignalSignature) Then
        '                    _currentSelectedStep.InputChannels.Add(obj.SignalSignature)
        '                End If
        '            Else                                        ' check box unchecked
        '                If _currentSelectedStep.Dividend Is obj.SignalSignature Then   ' if the content of the text box is the same as the clicked item and the checkbox is unchecked, means user wants to delete the content in the textbox
        '                    If _currentSelectedStep.Divisor Is obj.SignalSignature Then     ' however, if the textbox has the same contect as the divisor or subtrahend, we cannot uncheck the clicked item
        '                        obj.SignalSignature.IsChecked = True
        '                    Else
        '                        _currentSelectedStep.InputChannels.Remove(obj.SignalSignature)
        '                    End If
        '                    Dim dummy = New SignalSignatureViewModel("", "")
        '                    dummy.IsValid = False
        '                    _currentSelectedStep.Dividend = dummy
        '                End If
        '            End If
        '            _currentSelectedStep.CurrentCursor = ""
        '            _currentSelectedStep.ThisStepInputsAsSignalHerachyByType.SignalList = _signalMgr.SortSignalByType(_currentSelectedStep.InputChannels)
        '            _signalMgr.DetermineDataConfigPostProcessConfigAllParentNodeStatus()
        '        ElseIf _currentSelectedStep.CurrentCursor = "Divisor" Then
        '            If _currentSelectedStep.Dividend IsNot Nothing AndAlso obj.SignalSignature = _currentSelectedStep.Dividend Then
        '                Throw New Exception("Divisor cannot be the same as thedivident!")
        '            End If
        '            If obj.SignalSignature.IsChecked Then
        '                If _currentSelectedStep.Divisor IsNot Nothing And _currentSelectedStep.Divisor IsNot _currentSelectedStep.Dividend Then
        '                    _currentSelectedStep.Divisor.IsChecked = False
        '                    _currentSelectedStep.InputChannels.Remove(_currentSelectedStep.Divisor)
        '                End If
        '                _currentSelectedStep.Divisor = obj.SignalSignature
        '                If Not _currentSelectedStep.InputChannels.Contains(obj.SignalSignature) Then
        '                    _currentSelectedStep.InputChannels.Add(obj.SignalSignature)
        '                End If
        '            Else
        '                If _currentSelectedStep.Divisor Is obj.SignalSignature Then
        '                    If _currentSelectedStep.Dividend Is obj.SignalSignature Then
        '                        obj.SignalSignature.IsChecked = True
        '                    Else
        '                        _currentSelectedStep.InputChannels.Remove(obj.SignalSignature)
        '                    End If
        '                    Dim dummy = New SignalSignatureViewModel("", "")
        '                    dummy.IsValid = False
        '                    _currentSelectedStep.Divisor = dummy
        '                End If
        '            End If
        '            _currentSelectedStep.CurrentCursor = ""
        '            _currentSelectedStep.ThisStepInputsAsSignalHerachyByType.SignalList = _signalMgr.SortSignalByType(_currentSelectedStep.InputChannels)
        '            '_dataConfigDetermineAllParentNodeStatus()
        '            _signalMgr.DetermineDataConfigPostProcessConfigAllParentNodeStatus()
        '        End If



        '    End If
        '    '_signalMgr.DetermineFileDirCheckableStatus()
        'End Sub
        ''' <summary>
        ''' This method is for the subtraction or division cutomization steps
        ''' </summary>
        ''' <param name="obj"></param>
        Private Sub _setFocusedTextboxSubtraction(obj As SignalTypeHierachy)
            Dim sgnl = New SignalSignatureViewModel
            Dim signalCount = _determineSignalCountInTree(obj)
            If signalCount = 1 Then
                sgnl = _findTheBottomSignal(obj)
                sgnl.IsChecked = obj.SignalSignature.IsChecked
            Else 'if selected a group of signal
                _keepOriginalSelection(obj)
                Throw New Exception("Error! Please select ONLY ONE valid signal for this textbox! No group of signals!")
            End If
            If sgnl.PMUName Is Nothing OrElse sgnl.TypeAbbreviation Is Nothing Then
                _keepOriginalSelection(obj)
                Throw New Exception("Error! Signal selected is not valid.")
            Else
                If _currentSelectedStep.CurrentCursor = "" Then ' if no textbox selected, textbox lost it focus right after a click any where else, so only click immediate follow a textbox selection would work
                    Throw New Exception("Error! Please select a valid text box for this input signal!")
                ElseIf _currentSelectedStep.CurrentCursor = "Minuend" Then
                    If _currentSelectedStep.Subtrahend IsNot Nothing AndAlso sgnl = _currentSelectedStep.Subtrahend Then
                        Throw New Exception("Minuend cannot be the same as the subtrahend!")
                    End If
                    If obj.SignalSignature.IsChecked Then       ' check box checked
                        If _currentSelectedStep.Minuend IsNot Nothing And _currentSelectedStep.Minuend IsNot _currentSelectedStep.Subtrahend Then  ' if the current text box has content and not equal to the divisor
                            _currentSelectedStep.Minuend.IsChecked = False
                            _currentSelectedStep.InputChannels.Remove(_currentSelectedStep.Minuend)
                        End If
                        _currentSelectedStep.Minuend = sgnl
                        If Not _currentSelectedStep.InputChannels.Contains(sgnl) Then
                            _currentSelectedStep.InputChannels.Add(sgnl)
                        End If
                    Else                                        ' check box unchecked
                        If _currentSelectedStep.Minuend Is sgnl Then   ' if the content of the text box is the same as the clicked item and the checkbox is unchecked, means user wants to delete the content in the textbox
                            If _currentSelectedStep.Subtrahend Is sgnl Then     ' however, if the textbox has the same contect as the divisor or subtrahend, we cannot uncheck the clicked item
                                sgnl.IsChecked = True
                            Else
                                _currentSelectedStep.InputChannels.Remove(sgnl)
                            End If
                            Dim dummy = New SignalSignatureViewModel("", "")
                            dummy.IsValid = False
                            _currentSelectedStep.Minuend = dummy
                        End If
                    End If
                    _currentSelectedStep.CurrentCursor = ""
                    _currentSelectedStep.ThisStepInputsAsSignalHerachyByType.SignalList = _signalMgr.SortSignalByType(_currentSelectedStep.InputChannels)
                    _signalMgr.DetermineDataConfigPostProcessConfigAllParentNodeStatus()
                ElseIf _currentSelectedStep.CurrentCursor = "Subtrahend" Then
                    If _currentSelectedStep.Minuend IsNot Nothing AndAlso sgnl = _currentSelectedStep.Minuend Then
                        Throw New Exception("Subtrahend cannot be the same as the minuend!")
                    End If
                    If obj.SignalSignature.IsChecked Then
                        If _currentSelectedStep.Subtrahend IsNot Nothing And _currentSelectedStep.Subtrahend IsNot _currentSelectedStep.Minuend Then
                            _currentSelectedStep.Subtrahend.IsChecked = False
                            _currentSelectedStep.InputChannels.Remove(_currentSelectedStep.Subtrahend)
                        End If
                        _currentSelectedStep.Subtrahend = sgnl
                        If Not _currentSelectedStep.InputChannels.Contains(sgnl) Then
                            _currentSelectedStep.InputChannels.Add(sgnl)
                        End If
                    Else
                        If _currentSelectedStep.Subtrahend Is sgnl Then
                            If _currentSelectedStep.Minuend Is sgnl Then
                                sgnl.IsChecked = True
                            Else
                                _currentSelectedStep.InputChannels.Remove(sgnl)
                            End If
                            Dim dummy = New SignalSignatureViewModel("", "")
                            dummy.IsValid = False
                            _currentSelectedStep.Subtrahend = dummy
                        End If
                    End If
                    _currentSelectedStep.CurrentCursor = ""
                    _currentSelectedStep.ThisStepInputsAsSignalHerachyByType.SignalList = _signalMgr.SortSignalByType(_currentSelectedStep.InputChannels)
                    _signalMgr.DetermineDataConfigPostProcessConfigAllParentNodeStatus()
                End If
            End If
        End Sub

        Private Sub _setFocusedTextboxDivision(obj As SignalTypeHierachy)
            Dim sgnl = New SignalSignatureViewModel
            Dim signalCount = _determineSignalCountInTree(obj)
            If signalCount = 1 Then
                sgnl = _findTheBottomSignal(obj)
                sgnl.IsChecked = obj.SignalSignature.IsChecked
            Else 'if selected a group of signal
                _keepOriginalSelection(obj)
                Throw New Exception("Error! Please select ONLY ONE valid signal for this textbox! No group of signals!")
            End If
            If sgnl.PMUName Is Nothing OrElse sgnl.TypeAbbreviation Is Nothing Then
                _keepOriginalSelection(obj)
                Throw New Exception("Error! Signal selected is not valid.")
            Else
                If _currentSelectedStep.CurrentCursor = "" Then 'If no textbox selected, textbox lost it focus right after a click any where else, so only click immediate follow a textbox selection would work
                    Throw New Exception("Error! Please select a valid text box (Dividend or Divisor) for this input signal!")

                ElseIf _currentSelectedStep.CurrentCursor = "Dividend" Then
                    If sgnl.IsChecked Then ' If a Signal box is selected
                        If _currentSelectedStep.Dividend IsNot Nothing Then ' If the current Dividend box has PMU and signal names
                            _currentSelectedStep.InputChannels.Remove(_currentSelectedStep.Dividend)
                        End If
                        If _currentSelectedStep.Divisor IsNot Nothing AndAlso sgnl = _currentSelectedStep.Divisor Then
                            ' If Dividend and Divisor are the same
                            _currentSelectedStep.InputChannels.Remove(sgnl)
                            Dim dummy = New SignalSignatureViewModel("", "")
                            dummy.IsValid = False
                            _currentSelectedStep.Dividend = dummy
                            Throw New Exception("Error! Dividend cannot be the same as the Divisor!")

                        End If
                        _currentSelectedStep.Dividend = sgnl ' Assign the selected signal to Dividend
                        If Not _currentSelectedStep.InputChannels.Contains(_currentSelectedStep.Dividend) Then
                            _currentSelectedStep.InputChannels.Add(_currentSelectedStep.Dividend)
                        End If
                    Else ' If a Signal box is unselected
                        If _currentSelectedStep.Dividend Is sgnl Then ' If the current Dividend box has PMU and signal names
                            _currentSelectedStep.InputChannels.Remove(_currentSelectedStep.Dividend)
                            Dim dummy = New SignalSignatureViewModel("", "")
                            dummy.IsValid = False
                            _currentSelectedStep.Dividend = dummy ' Delete the PMU and signal names in current Dividend box
                        End If
                    End If
                    _currentSelectedStep.CurrentCursor = ""
                    _currentSelectedStep.ThisStepInputsAsSignalHerachyByType.SignalList = _signalMgr.SortSignalByType(_currentSelectedStep.InputChannels)
                    _signalMgr.DetermineDataConfigPostProcessConfigAllParentNodeStatus()

                ElseIf _currentSelectedStep.CurrentCursor = "Divisor" Then
                    If sgnl.IsChecked Then ' If a Signal box is selected
                        If _currentSelectedStep.Divisor IsNot Nothing Then ' If the current Divisor box has PMU and signal names
                            _currentSelectedStep.InputChannels.Remove(_currentSelectedStep.Divisor)
                        End If
                        If _currentSelectedStep.Dividend IsNot Nothing AndAlso sgnl = _currentSelectedStep.Dividend Then
                            ' If Dividend and Divisor are the same
                            _currentSelectedStep.InputChannels.Remove(obj.SignalSignature)
                            Dim dummy = New SignalSignatureViewModel("", "")
                            dummy.IsValid = False
                            _currentSelectedStep.Divisor = dummy
                            Throw New Exception("Error! Divisor cannot be the same as the Dividend!")
                        End If
                        _currentSelectedStep.Divisor = sgnl ' Assign the selected signal to Divisor
                        If Not _currentSelectedStep.InputChannels.Contains(_currentSelectedStep.Divisor) Then
                            _currentSelectedStep.InputChannels.Add(_currentSelectedStep.Divisor)
                        End If
                    Else ' If a Signal box is unselected
                        If _currentSelectedStep.Divisor Is sgnl Then ' If the current Divisor box has PMU and signal names
                            _currentSelectedStep.InputChannels.Remove(_currentSelectedStep.Divisor)
                            Dim dummy = New SignalSignatureViewModel("", "")
                            dummy.IsValid = False
                            _currentSelectedStep.Divisor = dummy ' Delete the PMU and signal names in current Divisor box
                        End If
                    End If
                    _currentSelectedStep.CurrentCursor = ""
                    _currentSelectedStep.ThisStepInputsAsSignalHerachyByType.SignalList = _signalMgr.SortSignalByType(_currentSelectedStep.InputChannels)
                    _signalMgr.DetermineDataConfigPostProcessConfigAllParentNodeStatus()

                End If
            End If
            '_signalMgr.DetermineFileDirCheckableStatus()
        End Sub

        Private Sub _changeSignalSelectionUnarySteps(obj As SignalTypeHierachy)
            If Not _currentInputOutputPair.HasValue Then
                If obj.SignalSignature.IsChecked Then
                    '_checkAllChildren(obj, obj.SignalSignature.IsChecked)
                    _addOrDeleteInputSignal(obj, obj.SignalSignature.IsChecked)
                    _addOuputSignalsForUnaryCustomizationStep(obj)
                Else
                    _removeMatchingInputOutputSignalsUnary(obj)
                End If
            Else
                If obj.SignalList.Count > 0 Or String.IsNullOrEmpty(obj.SignalSignature.PMUName) Or String.IsNullOrEmpty(obj.SignalSignature.TypeAbbreviation) Then
                    _keepOriginalSelection(obj)
                    Throw New Exception("Please select a valid signal!")
                ElseIf obj.SignalSignature.IsChecked AndAlso _currentSelectedStep.InputChannels.Contains(obj.SignalSignature) Then
                    Throw New Exception("Selected signal already in this step!")
                Else
                    'Dim targetPairs = (From x In DirectCast(_currentSelectedStep, Customization).OutputInputMappingPair Where x.Key = _currentInputOutputPair.Value.Key Select x).ToList
                    Dim targetPairs As List(Of KeyValuePair(Of SignalSignatureViewModel, ObservableCollection(Of SignalSignatureViewModel))) = Nothing
                    If TypeOf _currentSelectedStep Is TunableFilter Then
                        targetPairs = (From x In DirectCast(_currentSelectedStep, TunableFilter).OutputInputMappingPair Where x.Key = _currentInputOutputPair.Value.Key Select x).ToList
                    Else
                        targetPairs = (From x In DirectCast(_currentSelectedStep, Customization).OutputInputMappingPair Where x.Key = _currentInputOutputPair.Value.Key Select x).ToList
                    End If

                    If targetPairs.Count = 1 Then
                        Dim oldInput = targetPairs.FirstOrDefault.Value.FirstOrDefault
                        If Not String.IsNullOrEmpty(oldInput.PMUName) AndAlso Not String.IsNullOrEmpty(oldInput.SignalName) AndAlso Not String.IsNullOrEmpty(oldInput.TypeAbbreviation) Then
                            _currentSelectedStep.InputChannels.Remove(oldInput)
                            oldInput.IsChecked = False
                        End If
                        targetPairs.FirstOrDefault.Value.Clear()
                        If obj.SignalSignature.IsChecked Then
                            targetPairs.FirstOrDefault.Value.Add(obj.SignalSignature)
                            _currentSelectedStep.InputChannels.Add(obj.SignalSignature)
                            targetPairs.FirstOrDefault.Key.SignalName = obj.SignalSignature.SignalName
                            targetPairs.FirstOrDefault.Key.TypeAbbreviation = obj.SignalSignature.TypeAbbreviation
                            targetPairs.FirstOrDefault.Key.SamplingRate = obj.SignalSignature.SamplingRate
                            targetPairs.FirstOrDefault.Key.Unit = obj.SignalSignature.Unit
                            targetPairs.FirstOrDefault.Key.IsCustomSignal = True
                        Else
                            _currentSelectedStep.OutputChannels.Remove(targetPairs.FirstOrDefault.Key)
                            _currentSelectedStep.OutputInputMappingPair.Remove(targetPairs.FirstOrDefault)
                        End If
                        _currentInputOutputPair = Nothing
                    Else
                        Throw New Exception("Error adding/deleting selected item to the step!")
                    End If
                End If
            End If

            '_currentSelectedStep.ThisStepInputsAsSignalHerachyByType.SignalList = _signalMgr.SortSignalByType(_currentSelectedStep.InputChannels)
            'If TypeOf (_currentSelectedStep) Is Customization Then
            '    _currentSelectedStep.ThisStepOutputsAsSignalHierachyByPMU.SignalList = _signalMgr.SortSignalByPMU(_currentSelectedStep.OutputChannels)
            'End If
            '_dataConfigDetermineAllParentNodeStatus()
            _signalMgr.DetermineDataConfigPostProcessConfigAllParentNodeStatus()
            '_signalMgr.DetermineFileDirCheckableStatus()
        End Sub

        Private Sub _changeSignalSelection(obj As SignalTypeHierachy)
            _signalMgr.CheckAllChildren(obj, obj.SignalSignature.IsChecked)
            _addOrDeleteInputSignal(obj, obj.SignalSignature.IsChecked)
            If TypeOf _currentSelectedStep Is DQFilter Then
                _currentSelectedStep.ThisStepOutputsAsSignalHierachyByPMU.SignalList = _signalMgr.SortSignalByPMU(_currentSelectedStep.OutputChannels)
            ElseIf TypeOf _currentSelectedStep Is TunableFilter OrElse TypeOf _currentSelectedStep Is Wrap OrElse TypeOf _currentSelectedStep Is Interpolate OrElse TypeOf _currentSelectedStep Is Unwrap OrElse TypeOf _currentSelectedStep Is NameTypeUnitPMU Then
                '_currentSelectedStep.ThisStepOutputsAsSignalHierachyByPMU.SignalList = SortSignalByPMU(_currentSelectedStep.OutputChannels)
            ElseIf TypeOf _currentSelectedStep Is Multirate Then
                '_currentSelectedStep.ThisStepOutputsAsSignalHierachyByPMU.SignalList = SortSignalByPMU(_currentSelectedStep.OutputChannels)
                '_currentSelectedStep.ThisStepInputsAsSignalHerachyByType.SignalList = SortSignalByType(_currentSelectedStep.InputChannels)
            Else
                _currentSelectedStep.ThisStepInputsAsSignalHerachyByType.SignalList = _signalMgr.SortSignalByType(_currentSelectedStep.InputChannels)
            End If
            '_dataConfigDetermineAllParentNodeStatus()
            '_processConfigDetermineAllParentNodeStatus()
            '_postProcessDetermineAllParentNodeStatus()
            '_detectorConfigDetermineAllParentNodeStatus()
            _signalMgr.DetermineAllParentNodeStatus()
            '_signalMgr.DetermineFileDirCheckableStatus()
        End Sub
        Private Sub _changeSignalSelectionPhasorCreation(obj As SignalTypeHierachy)
            If Not _currentInputOutputPair.HasValue Then
                If obj.SignalSignature.IsChecked Then
                    _addInputOutputPairsPhasor(obj)
                Else
                    _removeMatchingInputOutputSignalsPhasor(obj)
                End If
            Else
                If obj.SignalList.Count > 0 Or String.IsNullOrEmpty(obj.SignalSignature.PMUName) Or Len(obj.SignalSignature.TypeAbbreviation) <> 3 Then
                    '_keepOriginalSelection(obj)
                    Throw New Exception("Please select a valid signal!")
                ElseIf obj.SignalSignature.IsChecked AndAlso _currentSelectedStep.InputChannels.Contains(obj.SignalSignature) Then
                    Throw New Exception("Selected signal already in this step!")
                Else
                    'Dim targetPairs = (From x In DirectCast(_currentSelectedStep, Customization).OutputInputMappingPair Where x.Key = _currentInputOutputPair.Value.Key Select x).ToList

                    'If targetPairs.Count = 1 Then
                    '    Dim oldInputMag = targetPairs.FirstOrDefault.Value.FirstOrDefault
                    '    Dim oldInputAng = targetPairs.FirstOrDefault.Value(1)
                    '    If obj.SignalSignature.IsChecked AndAlso obj.SignalSignature.TypeAbbreviation.Substring(1, 1) = "M" Then
                    '        oldInputMag.IsChecked = False
                    '        _currentSelectedStep.InputChannels.Remove(oldInputMag)
                    '        targetPairs.FirstOrDefault.Value(0) = obj.SignalSignature
                    '        _currentSelectedStep.InputChannels.Add(obj.SignalSignature)
                    '        If oldInputAng.IsValid AndAlso oldInputAng.SamplingRate = obj.SignalSignature.SamplingRate Then
                    '            targetPairs.FirstOrDefault.Key.SamplingRate = obj.SignalSignature.SamplingRate
                    '            targetPairs.FirstOrDefault.Key.Unit = obj.SignalSignature.Unit
                    '        End If
                    '    ElseIf obj.SignalSignature.IsChecked AndAlso obj.SignalSignature.TypeAbbreviation.Substring(1, 1) = "A" Then
                    '        oldInputAng.IsChecked = False
                    '        _currentSelectedStep.InputChannels.Remove(oldInputAng)
                    '        targetPairs.FirstOrDefault.Value(1) = obj.SignalSignature
                    '        _currentSelectedStep.InputChannels.Add(obj.SignalSignature)
                    '        If oldInputMag.IsValid AndAlso oldInputMag.SamplingRate = obj.SignalSignature.SamplingRate Then
                    '            targetPairs.FirstOrDefault.Key.SamplingRate = obj.SignalSignature.SamplingRate
                    '            targetPairs.FirstOrDefault.Key.Unit = oldInputMag.Unit
                    '        End If
                    '    ElseIf Not obj.SignalSignature.IsChecked AndAlso obj.SignalSignature.TypeAbbreviation.Substring(1, 1) = "M" Then
                    '        _currentSelectedStep.InputChannels.Remove(oldInputMag)
                    '        targetPairs.FirstOrDefault.Value(0) = DummySignature
                    '        targetPairs.FirstOrDefault.Key.SamplingRate = -1
                    '        'this step might be extra, test it
                    '        oldInputMag.IsChecked = False

                    '    ElseIf Not obj.SignalSignature.IsChecked AndAlso obj.SignalSignature.TypeAbbreviation.Substring(1, 1) = "A" Then
                    '        _currentSelectedStep.InputChannels.Remove(oldInputAng)
                    '        targetPairs.FirstOrDefault.Value(1) = DummySignature
                    '        targetPairs.FirstOrDefault.Key.SamplingRate = -1

                    '        'this step might be extra, test it
                    '        oldInputAng.IsChecked = False
                    '    Else
                    '        Throw New Exception("Error changing signal selection of step: " & _currentSelectedStep.StepCounter)
                    '    End If
                    '    If Not targetPairs.FirstOrDefault.Value(0).IsValid AndAlso Not targetPairs.FirstOrDefault.Value(1).IsValid Then
                    '        _currentSelectedStep.OutputChannels.Remove(targetPairs.FirstOrDefault.Key)
                    '        _currentSelectedStep.OutputInputMappingPair.Remove(targetPairs.FirstOrDefault)
                    '    End If




                    Dim oldInputMag = _currentInputOutputPair.Value.Value(0)
                    Dim oldInputAng = _currentInputOutputPair.Value.Value(1)
                    If obj.SignalSignature.IsChecked AndAlso obj.SignalSignature.TypeAbbreviation.Substring(1, 1) = "M" Then
                        oldInputMag.IsChecked = False
                        _currentSelectedStep.InputChannels.Remove(oldInputMag)
                        _currentInputOutputPair.Value.Value(0) = obj.SignalSignature
                        _currentSelectedStep.InputChannels.Add(obj.SignalSignature)
                        If oldInputAng.IsValid AndAlso oldInputAng.SamplingRate = obj.SignalSignature.SamplingRate Then
                            _currentInputOutputPair.Value.Key.SamplingRate = obj.SignalSignature.SamplingRate
                            _currentInputOutputPair.Value.Key.Unit = obj.SignalSignature.Unit
                        End If
                    ElseIf obj.SignalSignature.IsChecked AndAlso obj.SignalSignature.TypeAbbreviation.Substring(1, 1) = "A" Then
                        oldInputAng.IsChecked = False
                        _currentSelectedStep.InputChannels.Remove(oldInputAng)
                        _currentInputOutputPair.Value.Value(1) = obj.SignalSignature
                        _currentSelectedStep.InputChannels.Add(obj.SignalSignature)
                        If oldInputMag.IsValid AndAlso oldInputMag.SamplingRate = obj.SignalSignature.SamplingRate Then
                            _currentInputOutputPair.Value.Key.SamplingRate = obj.SignalSignature.SamplingRate
                            _currentInputOutputPair.Value.Key.Unit = oldInputMag.Unit
                        End If
                    ElseIf Not obj.SignalSignature.IsChecked AndAlso obj.SignalSignature.TypeAbbreviation.Substring(1, 1) = "M" Then
                        _currentSelectedStep.InputChannels.Remove(oldInputMag)
                        _currentInputOutputPair.Value.Value(0) = DummySignature
                        _currentInputOutputPair.Value.Key.SamplingRate = -1
                        'this step might be extra, test it
                        oldInputMag.IsChecked = False

                    ElseIf Not obj.SignalSignature.IsChecked AndAlso obj.SignalSignature.TypeAbbreviation.Substring(1, 1) = "A" Then
                        _currentSelectedStep.InputChannels.Remove(oldInputAng)
                        _currentInputOutputPair.Value.Value(1) = DummySignature
                        _currentInputOutputPair.Value.Key.SamplingRate = -1

                        'this step might be extra, test it
                        oldInputAng.IsChecked = False
                    Else
                        Throw New Exception("Error changing signal selection of step: " & _currentSelectedStep.StepCounter)
                    End If
                    If Not _currentInputOutputPair.Value.Value(0).IsValid AndAlso Not _currentInputOutputPair.Value.Value(1).IsValid Then
                        _currentSelectedStep.OutputChannels.Remove(_currentInputOutputPair.Value.Key)
                        _currentSelectedStep.OutputInputMappingPair.Remove(_currentInputOutputPair)
                    End If

                    'If Not String.IsNullOrEmpty(oldInputMag.PMUName) AndAlso Not String.IsNullOrEmpty(oldInputMag.SignalName) AndAlso Not String.IsNullOrEmpty(oldInputMag.TypeAbbreviation) Then
                    '    _currentSelectedStep.InputChannels.Remove(oldInputMag)
                    '    oldInputMag.IsChecked = False
                    'End If
                    'If Not String.IsNullOrEmpty(oldInputAng.PMUName) AndAlso Not String.IsNullOrEmpty(oldInputAng.SignalName) AndAlso Not String.IsNullOrEmpty(oldInputAng.TypeAbbreviation) Then
                    '    _currentSelectedStep.InputChannels.Remove(oldInputAng)
                    '    oldInputAng.IsChecked = False
                    'End If
                    'targetPairs.FirstOrDefault.Value.Clear()
                    'If obj.SignalSignature.IsChecked Then
                    'Dim ang = _findMatchingAng(obj.SignalSignature)
                    'If ang IsNot Nothing Then
                    '    ang.IsChecked = True
                    '    targetPairs.FirstOrDefault.Value.Add(obj.SignalSignature)
                    '    _currentSelectedStep.InputChannels.Add(obj.SignalSignature)
                    '    targetPairs.FirstOrDefault.Value.Add(ang)
                    '    _currentSelectedStep.InputChannels.Add(ang)
                    '    targetPairs.FirstOrDefault.Key.TypeAbbreviation = ang.TypeAbbreviation.Substring(0, 1) & "P" & ang.TypeAbbreviation.Substring(2, 1)
                    '    'targetPairs.FirstOrDefault.Key.TypeAbbreviation = ang.SamplingRate
                    '    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    '    ' How about unit of the new phasor signal? What should it be? 'It should be the same as the magnitude'
                    '    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Else
                    '    _currentInputOutputPair = Nothing
                    '    Throw New Exception("Cannot find matching angle signal for selected magnitude signal: " & obj.SignalSignature.SignalName)
                    'End If
                    'Else
                    '    _currentSelectedStep.OutputChannels.Remove(targetPairs.FirstOrDefault.Key)
                    '    _currentSelectedStep.OutputInputMappingPair.Remove(targetPairs.FirstOrDefault)
                    'End If
                    _currentInputOutputPair = Nothing
                    'Else
                    '    _currentInputOutputPair = Nothing
                    '    Throw New Exception("Error adding/deleting selected item to the step!")
                    'End If
                End If
            End If

            _currentSelectedStep.ThisStepInputsAsSignalHerachyByType.SignalList = _signalMgr.SortSignalByType(_currentSelectedStep.InputChannels)
            If TypeOf (_currentSelectedStep) Is Customization Then
                _currentSelectedStep.ThisStepOutputsAsSignalHierachyByPMU.SignalList = _signalMgr.SortSignalByPMU(_currentSelectedStep.OutputChannels)
            End If
            '_dataConfigDetermineAllParentNodeStatus()
            _signalMgr.DetermineDataConfigPostProcessConfigAllParentNodeStatus()
            '_signalMgr.DetermineFileDirCheckableStatus()
        End Sub
        ''' <summary>
        ''' find matching Ang signal given a Mag signal
        ''' </summary>
        ''' <param name="mag"></param>
        Private Function _findMatchingAng(mag As SignalSignatureViewModel) As SignalSignatureViewModel
            Dim type = mag.TypeAbbreviation
            Dim signalName = mag.SignalName
            Dim parts = signalName.Split(".")
            If parts.Length <> 3 Then
                signalName = parts(0)
            Else
                signalName = parts(1)
            End If
            Dim pmu = mag.PMUName
            If mag.IsCustomSignal Then
                For Each group In _signalMgr.GroupedSignalByDataConfigStepsOutput
                    For Each subgroupBySamplingRate In group.SignalList
                        For Each subgroup In subgroupBySamplingRate.SignalList
                            If subgroup.SignalSignature.PMUName = pmu Then
                                For Each signal In subgroup.SignalList
                                    Dim target = signal.SignalSignature.SignalName.Split(".")
                                    Dim foundSignalName = ""
                                    If target.Length <> 3 Then
                                        foundSignalName = target(0)
                                    Else
                                        foundSignalName = target(1)
                                    End If
                                    If foundSignalName = signalName Then
                                        Return signal.SignalSignature
                                    End If
                                Next
                            End If
                        Next
                    Next
                Next
            Else
                For Each group In _signalMgr.GroupedRawSignalsByType
                    If group.SignalSignature.IsEnabled Then
                        For Each subgroupBySamplingRate In group.SignalList
                            For Each subgroup In subgroupBySamplingRate.SignalList
                                If subgroup.SignalSignature.TypeAbbreviation = type.Substring(0, 1) Then
                                    For Each subsubgroup In subgroup.SignalList
                                        If subsubgroup.SignalSignature.TypeAbbreviation.Substring(1, 1) = "A" Then
                                            For Each subsubsubgroup In subsubgroup.SignalList
                                                If subsubsubgroup.SignalSignature.TypeAbbreviation.Substring(2, 1) = type.Substring(2, 1) Then
                                                    For Each signal In subsubsubgroup.SignalList
                                                        If signal.SignalSignature.PMUName = pmu Then
                                                            Dim target = signal.SignalSignature.SignalName.Split(".")
                                                            Dim foundSignalName = ""
                                                            If target.Length <> 3 Then
                                                                foundSignalName = target(0)
                                                            Else
                                                                foundSignalName = target(1)
                                                            End If
                                                            If foundSignalName = signalName Then
                                                                Return signal.SignalSignature
                                                            End If
                                                        End If
                                                    Next
                                                End If
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        Next
                    End If
                Next
            End If
            Return Nothing
        End Function

        Private Sub _changePhasorSignalForPowerCalculationCustomization(obj As SignalTypeHierachy)

            Dim sgnl = New SignalSignatureViewModel
            Dim signalCount = _determineSignalCountInTree(obj)
            If signalCount = 1 Then
                sgnl = _findTheBottomSignal(obj)
                sgnl.IsChecked = obj.SignalSignature.IsChecked
            Else 'if selected a group of signal
                _keepOriginalSelection(obj)
                Throw New Exception("Error! Please select ONLY ONE valid signal for this textbox! No group of signals!")
            End If
            If sgnl.PMUName Is Nothing OrElse sgnl.TypeAbbreviation Is Nothing Then
                _keepOriginalSelection(obj)
                Throw New Exception("Error! Signal selected is not valid.")
            Else

                If sgnl.TypeAbbreviation.Length <> 3 OrElse sgnl.TypeAbbreviation.Substring(1, 1) <> "P" Then
                    _addLog("Selected signal: " & sgnl.SignalName & " is not a phasor signal.")
                    Throw New Exception("Signal selection is not Valid! Please select a signal of type phasor.")
                ElseIf _currentFocusedPhasorSignalForPowerCalculation Is Nothing Then
                    Throw New Exception("No textbox selected!")
                ElseIf _currentSelectedStep.OutputInputMappingPair(0).Value(0) Is _currentFocusedPhasorSignalForPowerCalculation Then
                    If sgnl.TypeAbbreviation.Substring(0, 1) = "V" Then
                        Dim oldPhasor = _currentSelectedStep.OutputInputMappingPair(0).Value(0)
                        If _currentSelectedStep.InputChannels.Contains(oldPhasor) Then
                            oldPhasor.IsChecked = False
                            _currentSelectedStep.InputChannels.Remove(oldPhasor)
                        End If
                        _currentSelectedStep.OutputInputMappingPair(0).Value.Remove(oldPhasor)
                        If sgnl.IsChecked Then
                            _currentSelectedStep.OutputInputMappingPair(0).Value.Insert(0, sgnl)
                            _currentSelectedStep.InputChannels.Add(sgnl)
                            '_currentSelectedStep.OutputInputMappingPair(0).Key.SamplingRate = obj.SignalSignature.SamplingRate
                        Else
                            'Dim dummy = New SignalSignatures("PleaseAddVoltagePhasor", "PleaseAddVoltagePhasor")
                            'dummy.IsValid = False
                            _currentSelectedStep.OutputInputMappingPair(0).Value.Insert(0, DummySignature)
                            _currentSelectedStep.OutputInputMappingPair(0).Key.SamplingRate = -1
                        End If
                    Else
                        _addLog("Selected signal: " & sgnl.SignalName & " is not of signal type Voltage phasor ")
                        Throw New Exception("Signal selection is not Valid! Please select a signal of voltage phasor")
                    End If
                ElseIf _currentSelectedStep.OutputInputMappingPair(0).Value(1) Is _currentFocusedPhasorSignalForPowerCalculation Then
                    If sgnl.TypeAbbreviation.Substring(0, 1) = "I" Then
                        Dim oldPhasor = _currentSelectedStep.OutputInputMappingPair(0).Value(1)
                        If _currentSelectedStep.InputChannels.Contains(oldPhasor) Then
                            oldPhasor.IsChecked = False
                            _currentSelectedStep.InputChannels.Remove(oldPhasor)
                        End If
                        _currentSelectedStep.OutputInputMappingPair(0).Value.Remove(oldPhasor)
                        If sgnl.IsChecked Then
                            _currentSelectedStep.OutputInputMappingPair(0).Value.Insert(1, sgnl)
                            _currentSelectedStep.InputChannels.Add(sgnl)
                            '_currentSelectedStep.OutputInputMappingPair(0).Key.SamplingRate = obj.SignalSignature.SamplingRate
                        Else
                            'Dim dummy = New SignalSignatures("PleaseAddCurrentPhasor", "PleaseAddVoltagePhasor")
                            'dummy.IsValid = False
                            _currentSelectedStep.OutputInputMappingPair(0).Value.Insert(1, DummySignature)
                            _currentSelectedStep.OutputInputMappingPair(0).Key.SamplingRate = -1
                        End If
                    Else
                        _addLog("Selected signal: " & sgnl.SignalName & " is not of signal type current phasor.")
                        Throw New Exception("Signal selection is not Valid! Please select a signal of current phasor.")
                    End If
                Else
                    Throw New Exception("Error changing signal for this power calculation step!")
                End If

                If _currentSelectedStep.OutputInputMappingPair(0).Value(0).IsValid AndAlso _currentSelectedStep.OutputInputMappingPair(0).Value(1).IsValid Then
                    _currentSelectedStep.OutputInputMappingPair(0).Key.SamplingRate = _currentSelectedStep.OutputInputMappingPair(0).Value(0).SamplingRate
                Else
                    _currentSelectedStep.OutputInputMappingPair(0).Key.SamplingRate = -1
                End If


            End If
            _currentFocusedPhasorSignalForPowerCalculation = Nothing
            _currentSelectedStep.ThisStepInputsAsSignalHerachyByType.SignalList = _signalMgr.SortSignalByType(_currentSelectedStep.InputChannels)
            _currentSelectedStep.ThisStepOutputsAsSignalHierachyByPMU.SignalList = _signalMgr.SortSignalByPMU(_currentSelectedStep.OutputChannels)
            '_dataConfigDetermineAllParentNodeStatus()
            _signalMgr.DetermineDataConfigPostProcessConfigAllParentNodeStatus()
            '_signalMgr.DetermineFileDirCheckableStatus()
        End Sub
        Private Sub _changeMagAngSignalForPowerCalculationCustomization(obj As SignalTypeHierachy)

            Dim sgnl = New SignalSignatureViewModel
            Dim signalCount = _determineSignalCountInTree(obj)
            If signalCount = 1 Then
                sgnl = _findTheBottomSignal(obj)
                sgnl.IsChecked = obj.SignalSignature.IsChecked
            Else 'if selected a group of signal
                _keepOriginalSelection(obj)
                Throw New Exception("Error! Please select ONLY ONE valid signal for this textbox! No group of signals!")
            End If
            If sgnl.TypeAbbreviation.Length <> 3 OrElse (sgnl.TypeAbbreviation.Substring(1, 1) <> "M" AndAlso sgnl.TypeAbbreviation.Substring(1, 1) <> "A") Then
                _keepOriginalSelection(obj)
                _addLog("Selected signal: " & sgnl.SignalName & " is not a magnitude or angle signal.")
                Throw New Exception("Signal selection is not Valid! Please select a signal of VM, VA, IM or IA type.")
            Else
                'If obj.SignalSignature.IsChecked Then       'add signal
                If _currentFocusedPhasorSignalForPowerCalculation Is Nothing Then
                    Throw New Exception("No textbox selected!")
                ElseIf _currentSelectedStep.OutputInputMappingPair(0).Value(0) Is _currentFocusedPhasorSignalForPowerCalculation Then
                    If sgnl.TypeAbbreviation.Substring(0, 2) = "VM" Then
                        Dim oldVM = _currentSelectedStep.OutputInputMappingPair(0).Value(0)
                        'Dim oldVA = _currentSelectedStep.OutputInputMappingPair(0).Value(1)
                        If _currentSelectedStep.InputChannels.Contains(oldVM) Then
                            oldVM.IsChecked = False
                            _currentSelectedStep.InputChannels.Remove(oldVM)
                        End If
                        'If _currentSelectedStep.InputChannels.Contains(oldVA) Then
                        '    oldVA.IsChecked = False
                        '    _currentSelectedStep.InputChannels.Remove(oldVA)
                        'End If
                        _currentSelectedStep.OutputInputMappingPair(0).Value.Remove(oldVM)
                        '_currentSelectedStep.OutputInputMappingPair(0).Value.Remove(oldVA)
                        If sgnl.IsChecked Then
                            _currentSelectedStep.OutputInputMappingPair(0).Value.Insert(0, sgnl)
                            _currentSelectedStep.InputChannels.Add(sgnl)
                            'Dim newVA = _findMatchingAng(obj.SignalSignature)
                            'If newVA Is Nothing Then
                            '    newVA = New SignalSignatures("NoMatchingAnglefound")
                            '    newVA.IsValid = False
                            'Else
                            '    newVA.IsChecked = True
                            'End If
                            '_currentSelectedStep.OutputInputMappingPair(0).Value.Insert(1, newVA)
                            '_currentSelectedStep.InputChannels.Add(newVA)
                        Else
                            'Dim dummyVM = New SignalSignatures("PleaseAddVoltageMag")
                            'dummyVM.IsValid = False
                            _currentSelectedStep.OutputInputMappingPair(0).Value.Insert(0, DummySignature)
                            '_currentSelectedStep.OutputInputMappingPair(0).Key.SamplingRate = -1
                            '    Dim dummyVA = New SignalSignatures("PleaseAddVoltageAng")
                            '    dummyVA.IsValid = False
                            '    _currentSelectedStep.OutputInputMappingPair(0).Value.Insert(1, dummyVA)
                        End If
                    Else
                        _addLog("Selected signal: " & sgnl.SignalName & " is not of signal type Voltage Magnitude.")
                        Throw New Exception("Signal selection is not Valid! Please select a signal of voltage Magnitude.")
                    End If
                ElseIf _currentSelectedStep.OutputInputMappingPair(0).Value(1) Is _currentFocusedPhasorSignalForPowerCalculation Then
                    If sgnl.TypeAbbreviation.Substring(0, 2) = "VA" Then
                        'Dim oldVM = _currentSelectedStep.OutputInputMappingPair(0).Value(0)
                        Dim oldVA = _currentSelectedStep.OutputInputMappingPair(0).Value(1)
                        'If _currentSelectedStep.InputChannels.Contains(oldVM) Then
                        '    oldVM.IsChecked = False
                        '    _currentSelectedStep.InputChannels.Remove(oldVM)
                        'End If
                        If _currentSelectedStep.InputChannels.Contains(oldVA) Then
                            oldVA.IsChecked = False
                            _currentSelectedStep.InputChannels.Remove(oldVA)
                        End If
                        '_currentSelectedStep.OutputInputMappingPair(0).Value.Remove(oldVM)
                        _currentSelectedStep.OutputInputMappingPair(0).Value.Remove(oldVA)
                        If sgnl.IsChecked Then
                            _currentSelectedStep.OutputInputMappingPair(0).Value.Insert(1, sgnl)
                            _currentSelectedStep.InputChannels.Add(sgnl)
                        Else
                            'Dim dummyVM = New SignalSignatures("PleaseAddVoltageMag")
                            'dummyVM.IsValid = False
                            '_currentSelectedStep.OutputInputMappingPair(0).Value.Insert(0, dummyVM)
                            'Dim dummyVA = New SignalSignatures("PleaseAddVoltageAng")
                            'dummyVA.IsValid = False
                            _currentSelectedStep.OutputInputMappingPair(0).Value.Insert(1, DummySignature)
                            '_currentSelectedStep.OutputInputMappingPair(0).Key.SamplingRate = -1
                        End If
                    Else
                        _addLog("Selected signal: " & sgnl.SignalName & " is not of signal type Voltage Angle.")
                        Throw New Exception("Signal selection is not Valid! Please select a signal of voltage Angle.")
                    End If
                ElseIf _currentSelectedStep.OutputInputMappingPair(0).Value(2) Is _currentFocusedPhasorSignalForPowerCalculation Then
                    If sgnl.TypeAbbreviation.Substring(0, 2) = "IM" Then
                        Dim oldIM = _currentSelectedStep.OutputInputMappingPair(0).Value(2)
                        'Dim oldIA = _currentSelectedStep.OutputInputMappingPair(0).Value(3)
                        If _currentSelectedStep.InputChannels.Contains(oldIM) Then
                            oldIM.IsChecked = False
                            _currentSelectedStep.InputChannels.Remove(oldIM)
                        End If
                        'If _currentSelectedStep.InputChannels.Contains(oldIA) Then
                        '    oldIA.IsChecked = False
                        '    _currentSelectedStep.InputChannels.Remove(oldIA)
                        'End If
                        _currentSelectedStep.OutputInputMappingPair(0).Value.Remove(oldIM)
                        '_currentSelectedStep.OutputInputMappingPair(0).Value.Remove(oldIA)
                        If sgnl.IsChecked Then
                            _currentSelectedStep.OutputInputMappingPair(0).Value.Insert(2, sgnl)
                            _currentSelectedStep.InputChannels.Add(sgnl)
                            'Dim newIA = _findMatchingAng(obj.SignalSignature)
                            'If newIA Is Nothing Then
                            '    newIA = New SignalSignatures("NoMatchingAnglefound")
                            '    newIA.IsValid = False
                            'Else
                            '    newIA.IsChecked = True
                            'End If
                            '_currentSelectedStep.OutputInputMappingPair(0).Value.Insert(3, newIA)
                            '_currentSelectedStep.InputChannels.Add(newIA)
                        Else
                            'Dim dummyIM = New SignalSignatures("PleaseAddCurrentMag")
                            'dummyIM.IsValid = False
                            _currentSelectedStep.OutputInputMappingPair(0).Value.Insert(2, DummySignature)
                            '_currentSelectedStep.OutputInputMappingPair(0).Key.SamplingRate = -1
                            'Dim dummyIA = New SignalSignatures("PleaseAddCurrentAng")
                            'dummyIA.IsValid = False
                            '_currentSelectedStep.OutputInputMappingPair(0).Value.Insert(3, dummyIA)
                        End If
                    Else
                        _addLog("Selected signal: " & sgnl.SignalName & " is not of signal type current magnitude.")
                        Throw New Exception("Signal selection is not Valid! Please select a signal of current magnitude.")
                    End If
                ElseIf _currentSelectedStep.OutputInputMappingPair(0).Value(3) Is _currentFocusedPhasorSignalForPowerCalculation Then
                    If sgnl.TypeAbbreviation.Substring(0, 2) = "IA" Then
                        'Dim oldIM = _currentSelectedStep.OutputInputMappingPair(0).Value(2)
                        Dim oldIA = _currentSelectedStep.OutputInputMappingPair(0).Value(3)
                        'If _currentSelectedStep.InputChannels.Contains(oldIM) Then
                        '    oldIM.IsChecked = False
                        '    _currentSelectedStep.InputChannels.Remove(oldIM)
                        'End If
                        If _currentSelectedStep.InputChannels.Contains(oldIA) Then
                            oldIA.IsChecked = False
                            _currentSelectedStep.InputChannels.Remove(oldIA)
                        End If
                        '_currentSelectedStep.OutputInputMappingPair(0).Value.Remove(oldIM)
                        _currentSelectedStep.OutputInputMappingPair(0).Value.Remove(oldIA)
                        If sgnl.IsChecked Then
                            _currentSelectedStep.OutputInputMappingPair(0).Value.Insert(3, sgnl)
                            _currentSelectedStep.InputChannels.Add(sgnl)
                        Else
                            'Dim dummyIM = New SignalSignatures("PleaseAddCurrentMag")
                            'dummyIM.IsValid = False
                            '_currentSelectedStep.OutputInputMappingPair(0).Value.Insert(2, dummyIM)
                            'Dim dummyIA = New SignalSignatures("PleaseAddCurrentAng")
                            'dummyIA.IsValid = False
                            _currentSelectedStep.OutputInputMappingPair(0).Value.Insert(3, DummySignature)
                            '_currentSelectedStep.OutputInputMappingPair(0).Key.SamplingRate = -1
                        End If
                    Else
                        _addLog("Selected signal: " & sgnl.SignalName & " is not of signal type current angle.")
                        Throw New Exception("Signal selection is not Valid! Please select a signal of current angle.")
                    End If
                Else
                    Throw New Exception("Error changing signal for this power calculation step!")
                End If
                'Else        ' remove signal

                'End If
                If _currentSelectedStep.OutputInputMappingPair(0).Value(0).IsValid AndAlso _currentSelectedStep.OutputInputMappingPair(0).Value(1).IsValid AndAlso _currentSelectedStep.OutputInputMappingPair(0).Value(2).IsValid AndAlso _currentSelectedStep.OutputInputMappingPair(0).Value(3).IsValid Then
                    _currentSelectedStep.OutputInputMappingPair(0).Key.SamplingRate = _currentSelectedStep.OutputInputMappingPair(0).Value(0).SamplingRate
                Else
                    _currentSelectedStep.OutputInputMappingPair(0).Key.SamplingRate = -1
                End If

                _currentFocusedPhasorSignalForPowerCalculation = Nothing
                _currentSelectedStep.ThisStepInputsAsSignalHerachyByType.SignalList = _signalMgr.SortSignalByType(_currentSelectedStep.InputChannels)
                _currentSelectedStep.ThisStepOutputsAsSignalHierachyByPMU.SignalList = _signalMgr.SortSignalByPMU(_currentSelectedStep.OutputChannels)
                '_dataConfigDetermineAllParentNodeStatus()
                _signalMgr.DetermineDataConfigPostProcessConfigAllParentNodeStatus()
                '_signalMgr.DetermineFileDirCheckableStatus()
            End If
        End Sub
        Private Sub _specifySignalTypeUnitSignalSelectionChanged(obj As SignalTypeHierachy)
            Dim sgnl = New SignalSignatureViewModel
            Dim signalCount = _determineSignalCountInTree(obj)
            If signalCount = 1 Then
                sgnl = _findTheBottomSignal(obj)
                sgnl.IsChecked = obj.SignalSignature.IsChecked
            Else 'if selected a group of signal
                Throw New Exception("Error! Please select ONLY ONE valid signal for this textbox! No group of signals!")
            End If
            If sgnl.PMUName Is Nothing OrElse sgnl.TypeAbbreviation Is Nothing Then
                _keepOriginalSelection(obj)
                Throw New Exception("Signal selection is not Valid! Please select a single valid signal.")
            Else
                If _currentSelectedStep.InputChannels.Count > 0 Then
                    _currentSelectedStep.InputChannels(0).IsChecked = False
                    _currentSelectedStep.InputChannels.Clear
                End If
                If sgnl.IsChecked Then
                    _currentSelectedStep.InputChannels.Add(sgnl)
                    If String.IsNullOrEmpty(_currentSelectedStep.OutputChannels(0).SignalName) Then
                        _currentSelectedStep.OutputChannels(0).SignalName = sgnl.SignalName
                    End If
                    _currentSelectedStep.OutputChannels(0).SamplingRate = sgnl.SamplingRate
                End If
                _currentSelectedStep.ThisStepInputsAsSignalHerachyByType.SignalList = _signalMgr.SortSignalByType(_currentSelectedStep.InputChannels)
                '_dataConfigDetermineAllParentNodeStatus()
                _signalMgr.DetermineDataConfigPostProcessConfigAllParentNodeStatus()
                '_signalMgr.DetermineFileDirCheckableStatus()
            End If
        End Sub

        Private Sub _removeMatchingInputOutputSignalsUnary(obj As SignalTypeHierachy)
            If obj.SignalList.Count > 0 Then
                For Each child In obj.SignalList
                    _removeMatchingInputOutputSignalsUnary(child)
                Next
            Else
                Dim targetToRemove As List(Of KeyValuePair(Of SignalSignatureViewModel, ObservableCollection(Of SignalSignatureViewModel))) = Nothing
                If TypeOf _currentSelectedStep Is TunableFilter Then
                    targetToRemove = (From x In DirectCast(_currentSelectedStep, TunableFilter).OutputInputMappingPair Where x.Value(0).SignalName = obj.SignalSignature.SignalName Select x).ToList
                    If Not _currentSelectedStep.UseCustomPMU Then
                        obj.SignalSignature.PassedThroughProcessor -= 1
                    End If
                Else
                    targetToRemove = (From x In DirectCast(_currentSelectedStep, Customization).OutputInputMappingPair Where x.Value(0).SignalName = obj.SignalSignature.SignalName Select x).ToList
                End If
                For Each target In targetToRemove
                    _currentSelectedStep.OutputChannels.Remove(target.Key)
                    _currentSelectedStep.InputChannels.Remove(obj.SignalSignature)
                    _currentSelectedStep.OutputInputMappingPair.Remove(target)
                    obj.SignalSignature.IsChecked = False
                Next
            End If
        End Sub
        Private Sub _removeMatchingInputOutputSignalsPhasor(obj As SignalTypeHierachy)
            If obj.SignalList.Count > 0 Then
                For Each signal In obj.SignalList
                    _removeMatchingInputOutputSignalsPhasor(signal)
                Next
            Else
                If obj.SignalSignature.TypeAbbreviation.Length = 3 AndAlso obj.SignalSignature.TypeAbbreviation.Substring(1, 1) = "M" Then
                    Dim targetToRemove = (From x In DirectCast(_currentSelectedStep, Customization).OutputInputMappingPair Where x.Value(0).SignalName = obj.SignalSignature.SignalName Select x).ToList
                    For Each target In targetToRemove
                        _currentSelectedStep.OutputChannels.Remove(target.Key)
                        _currentSelectedStep.InputChannels.Remove(obj.SignalSignature)
                        _currentSelectedStep.InputChannels.Remove(target.Value(1))
                        _currentSelectedStep.OutputInputMappingPair.Remove(target)
                        obj.SignalSignature.IsChecked = False
                        target.Value(1).IsChecked = False
                    Next
                ElseIf obj.SignalSignature.TypeAbbreviation.Length = 3 AndAlso obj.SignalSignature.TypeAbbreviation.Substring(1, 1) = "A" Then
                    Dim targetToRemove = (From x In DirectCast(_currentSelectedStep, Customization).OutputInputMappingPair Where x.Value(1).SignalName = obj.SignalSignature.SignalName Select x).ToList
                    For Each target In targetToRemove
                        _currentSelectedStep.OutputChannels.Remove(target.Key)
                        _currentSelectedStep.InputChannels.Remove(obj.SignalSignature)
                        _currentSelectedStep.InputChannels.Remove(target.Value(0))
                        _currentSelectedStep.OutputInputMappingPair.Remove(target)
                        obj.SignalSignature.IsChecked = False
                        target.Value(0).IsChecked = False
                    Next
                End If
            End If
        End Sub
        ''' <summary>
        ''' This subroutine adds output signal for each selected input signal for unary customization steps
        ''' It also pairs the output to the input
        ''' </summary>
        ''' <param name="obj"></param>
        Private Sub _addOuputSignalsForUnaryCustomizationStep(obj As SignalTypeHierachy)
            If _currentSelectedStep IsNot Nothing Then
                If obj.SignalList.Count > 0 Then
                    For Each child In obj.SignalList
                        If child.SignalSignature.IsEnabled Then
                            _addOuputSignalsForUnaryCustomizationStep(child)
                        End If
                    Next
                Else
                    If TypeOf _currentSelectedStep Is TunableFilter Then
                        Dim newOutput = obj.SignalSignature
                        If _currentSelectedStep.UseCustomPMU Then
                            newOutput = New SignalSignatureViewModel(obj.SignalSignature.SignalName, _currentSelectedStep.CustPMUname, obj.SignalSignature.TypeAbbreviation)
                            newOutput.Unit = obj.SignalSignature.Unit
                            'newOutput.PMUName = _currentSelectedStep.CustPMUname
                            newOutput.SamplingRate = obj.SignalSignature.SamplingRate
                        Else
                            newOutput.PassedThroughProcessor += 1
                        End If
                        newOutput.IsCustomSignal = True
                        If DirectCast(_currentSelectedStep, TunableFilter).Type = TunableFilterType.FrequencyDerivation Then
                            newOutput.TypeAbbreviation = "F"
                            newOutput.Unit = "Hz"
                        End If
                        newOutput.OldUnit = newOutput.Unit
                        newOutput.OldSignalName = newOutput.SignalName
                        newOutput.OldTypeAbbreviation = newOutput.TypeAbbreviation
                        _currentSelectedStep.outputChannels.Add(newOutput)
                        Dim targetkey = (From kvp In DirectCast(_currentSelectedStep, TunableFilter).OutputInputMappingPair Where kvp.Key = newOutput Select kvp Distinct).ToList()
                        If targetkey.Count = 0 Then
                            Dim kvp = New KeyValuePair(Of SignalSignatureViewModel, ObservableCollection(Of SignalSignatureViewModel))(newOutput, New ObservableCollection(Of SignalSignatureViewModel))
                            kvp.Value.Add(obj.SignalSignature)
                            _currentSelectedStep.OutputInputMappingPair.Add(kvp)
                        End If
                    ElseIf TypeOf _currentSelectedStep Is MetricPrefixCust Then
                        Dim newOutput = obj.SignalSignature
                        'If _currentSelectedStep.UseCustomPMU Then
                        newOutput = New SignalSignatureViewModel(obj.SignalSignature.SignalName, obj.SignalSignature.PMUName, obj.SignalSignature.TypeAbbreviation)
                        newOutput.PMUName = _currentSelectedStep.CustPMUname
                        newOutput.SamplingRate = obj.SignalSignature.SamplingRate
                        'End If
                        newOutput.IsCustomSignal = True
                        Dim units = New List(Of String)(PostProcessConfigure.TypeUnitDictionary(obj.SignalSignature.TypeAbbreviation))
                        units.Remove(obj.SignalSignature.Unit)
                        newOutput.OldUnit = newOutput.Unit
                        newOutput.Unit = units.FirstOrDefault()
                        newOutput.OldSignalName = newOutput.SignalName
                        newOutput.OldTypeAbbreviation = newOutput.TypeAbbreviation
                        _currentSelectedStep.outputChannels.Add(newOutput)
                        Dim targetkey = (From kvp In DirectCast(_currentSelectedStep, Customization).OutputInputMappingPair Where kvp.Key = newOutput Select kvp Distinct).ToList()
                        If targetkey.Count = 0 Then
                            Dim kvp = New KeyValuePair(Of SignalSignatureViewModel, ObservableCollection(Of SignalSignatureViewModel))(newOutput, New ObservableCollection(Of SignalSignatureViewModel))
                            kvp.Value.Add(obj.SignalSignature)
                            _currentSelectedStep.OutputInputMappingPair.Add(kvp)
                        End If
                    Else
                        Dim newOutput = New SignalSignatureViewModel(obj.SignalSignature.SignalName, _currentSelectedStep.CustPMUname, obj.SignalSignature.TypeAbbreviation)
                        newOutput.IsCustomSignal = True
                        newOutput.SamplingRate = obj.SignalSignature.SamplingRate
                        newOutput.Unit = obj.SignalSignature.Unit
                        newOutput.OldSignalName = newOutput.SignalName
                        newOutput.OldTypeAbbreviation = newOutput.TypeAbbreviation
                        newOutput.OldUnit = newOutput.Unit
                        _currentSelectedStep.outputChannels.Add(newOutput)
                        Dim targetkey = (From kvp In DirectCast(_currentSelectedStep, Customization).OutputInputMappingPair Where kvp.Key = newOutput Select kvp Distinct).ToList()
                        If targetkey.Count = 0 Then
                            Dim kvp = New KeyValuePair(Of SignalSignatureViewModel, ObservableCollection(Of SignalSignatureViewModel))(newOutput, New ObservableCollection(Of SignalSignatureViewModel))
                            kvp.Value.Add(obj.SignalSignature)
                            _currentSelectedStep.OutputInputMappingPair.Add(kvp)
                        End If
                    End If
                End If
            End If
        End Sub
        ''' <summary>
        ''' add or remove selected signals to the inputChannels of customization steps except power calculation and phasor creation
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <param name="isChecked"></param>
        Private Sub _addOrDeleteInputSignal(obj As SignalTypeHierachy, isChecked As Boolean)
            If _currentSelectedStep IsNot Nothing Then
                If obj.SignalList.Count > 0 Then
                    For Each signal In obj.SignalList
                        If signal.SignalSignature.IsEnabled Then
                            _addOrDeleteInputSignal(signal, isChecked)
                            'Else
                            '    Forms.MessageBox.Show("disabled item.", "warning", MessageBoxButtons.OK)
                        End If
                    Next
                Else
                    ' This happens when user check a input grouped by steps, but that step has no input signals thus has no child
                    If (obj.SignalSignature.PMUName Is Nothing Or obj.SignalSignature.TypeAbbreviation Is Nothing) Then
                        Throw New Exception("Item is not a valid signal, or contains no valid signal, nothing to be added or removed!")
                    Else
                        If isChecked Then
                            If _currentSelectedStep.InputChannels.Contains(obj.SignalSignature) Then
                                Throw New Exception("Selected item " & obj.SignalSignature.SignalName & " already exist in this step!")
                            End If
                            If TypeOf _currentSelectedStep Is DQFilter Then
                                obj.SignalSignature.PassedThroughDQFilter = obj.SignalSignature.PassedThroughDQFilter + 1
                                _currentSelectedStep.OutputChannels.Add(obj.SignalSignature)
                            End If
                            If TypeOf _currentSelectedStep Is Wrap OrElse TypeOf _currentSelectedStep Is Interpolate OrElse TypeOf _currentSelectedStep Is Unwrap Then
                                obj.SignalSignature.PassedThroughProcessor = obj.SignalSignature.PassedThroughProcessor + 1
                                _currentSelectedStep.OutputChannels.Add(obj.SignalSignature)
                            End If
                            If TypeOf _currentSelectedStep Is Multirate Then
                                Dim newOutput = New SignalSignatureViewModel(obj.SignalSignature.SignalName)
                                If String.IsNullOrEmpty(_currentSelectedStep.MultiRatePMU) Then
                                    'Throw New Exception("Please enter a PMU name for this multirate step.")
                                Else
                                    newOutput.PMUName = _currentSelectedStep.MultiRatePMU
                                End If
                                newOutput.TypeAbbreviation = obj.SignalSignature.TypeAbbreviation
                                newOutput.IsCustomSignal = True
                                newOutput.Unit = obj.SignalSignature.Unit
                                If _currentSelectedStep.FilterChoice = 1 Then
                                    newOutput.SamplingRate = _currentSelectedStep.NewRate
                                ElseIf _currentSelectedStep.FilterChoice = 2 Then
                                    Dim p = 0
                                    Integer.TryParse(_currentSelectedStep.PElement, p)
                                    Dim q = 0
                                    Integer.TryParse(_currentSelectedStep.QElement, q)
                                    If q <> 0 Then
                                        newOutput.SamplingRate = obj.SignalSignature.SamplingRate * p / q
                                    End If
                                End If
                                newOutput.OldSignalName = newOutput.SignalName
                                newOutput.OldTypeAbbreviation = newOutput.TypeAbbreviation
                                newOutput.OldUnit = newOutput.Unit
                                _currentSelectedStep.OutputChannels.Add(newOutput)
                            End If
                            If TypeOf _currentSelectedStep Is NameTypeUnitPMU Then
                                obj.SignalSignature.PassedThroughProcessor = obj.SignalSignature.PassedThroughProcessor + 1
                                obj.SignalSignature.IsNameTypeUnitChanged = True
                                If CurrentSelectedStep.OutputChannels.Count = 0 Then
                                    If Not String.IsNullOrEmpty(CurrentSelectedStep.NewChannel) Then
                                        obj.SignalSignature.OldSignalName = obj.SignalSignature.SignalName
                                        obj.SignalSignature.SignalName = CurrentSelectedStep.NewChannel
                                    End If
                                ElseIf CurrentSelectedStep.OutputChannels.Count = 1 Then
                                    Dim existingSignal = CurrentSelectedStep.OutputChannels(0)
                                    If Not String.IsNullOrEmpty(CurrentSelectedStep.NewChannel) AndAlso Not String.IsNullOrEmpty(existingSignal.OldSignalName) Then
                                        existingSignal.SignalName = existingSignal.OldSignalName
                                        'existingSignal.OldSignalName = ""
                                    End If
                                    CurrentSelectedStep.NewChannel = ""
                                Else
                                End If
                                If Not String.IsNullOrEmpty(CurrentSelectedStep.NewType) Then
                                    obj.SignalSignature.OldTypeAbbreviation = obj.SignalSignature.TypeAbbreviation
                                    obj.SignalSignature.TypeAbbreviation = CurrentSelectedStep.NewType
                                End If
                                If Not String.IsNullOrEmpty(CurrentSelectedStep.NewUnit) Then
                                    obj.SignalSignature.OldUnit = obj.SignalSignature.Unit
                                    obj.SignalSignature.Unit = CurrentSelectedStep.NewUnit
                                End If
                                _currentSelectedStep.OutputChannels.Add(obj.SignalSignature)
                            End If
                            'If TypeOf _currentSelectedStep Is SignalReplicationCust Then
                            '    Dim newOutput = New SignalSignatureViewModel(obj.SignalSignature.SignalName)
                            '    If String.IsNullOrEmpty(_currentSelectedStep.CustPMUName) Then
                            '        'Throw New Exception("Please enter a PMU name for this multirate step.")
                            '    Else
                            '        newOutput.PMUName = _currentSelectedStep.CustPMUName
                            '    End If
                            '    newOutput.TypeAbbreviation = obj.SignalSignature.TypeAbbreviation
                            '    newOutput.IsCustomSignal = True
                            '    newOutput.Unit = obj.SignalSignature.Unit
                            '    newOutput.SamplingRate = obj.SignalSignature.SamplingRate
                            '    newOutput.OldSignalName = newOutput.SignalName
                            '    newOutput.OldTypeAbbreviation = newOutput.TypeAbbreviation
                            '    newOutput.OldUnit = newOutput.Unit
                            '    _currentSelectedStep.OutputChannels.Add(newOutput)
                            'End If
                            If TypeOf _currentSelectedStep Is PeriodogramDetector Or TypeOf _currentSelectedStep Is SpectralCoherenceDetector Or TypeOf _currentSelectedStep Is OutOfRangeFrequencyDetector Or TypeOf _currentSelectedStep Is RingdownDetector Or TypeOf _currentSelectedStep Is WindRampDetector Then
                                'If Not _signalMgr.MappingSignals.Contains(obj.SignalSignature) Then
                                _signalMgr.MappingSignals.Add(obj.SignalSignature)
                                'End If
                            End If
                            _currentSelectedStep.InputChannels.Add(obj.SignalSignature)
                        Else
                            If TypeOf _currentSelectedStep Is DQFilter Then
                                obj.SignalSignature.PassedThroughDQFilter = obj.SignalSignature.PassedThroughDQFilter - 1
                                _currentSelectedStep.OutputChannels.Remove(obj.SignalSignature)
                            End If
                            If TypeOf _currentSelectedStep Is Wrap OrElse TypeOf _currentSelectedStep Is Interpolate OrElse TypeOf _currentSelectedStep Is Unwrap Then
                                obj.SignalSignature.PassedThroughProcessor = obj.SignalSignature.PassedThroughProcessor - 1
                                _currentSelectedStep.OutputChannels.Remove(obj.SignalSignature)
                            End If
                            If TypeOf _currentSelectedStep Is Multirate Then
                                For Each output In _currentSelectedStep.OutputChannels
                                    If output.SignalName = obj.SignalSignature.SignalName AndAlso output.TypeAbbreviation = obj.SignalSignature.TypeAbbreviation Then
                                        _currentSelectedStep.OutputChannels.Remove(output)
                                        Exit For
                                    End If
                                Next
                            End If
                            If TypeOf _currentSelectedStep Is NameTypeUnitPMU Then
                                For Each output In _currentSelectedStep.OutputChannels
                                    If output.SignalName = obj.SignalSignature.SignalName AndAlso output.PMUName = obj.SignalSignature.PMUName Then
                                        If _currentSelectedStep.OutputChannels.Count = 1 AndAlso Not String.IsNullOrEmpty(output.OldSignalName) Then
                                            output.SignalName = output.OldSignalName
                                            'output.OldSignalName = ""
                                        End If
                                        If Not String.IsNullOrEmpty(output.OldUnit) Then
                                            output.Unit = output.OldUnit
                                            'output.OldUnit = ""
                                        End If
                                        If Not String.IsNullOrEmpty(output.OldTypeAbbreviation) Then
                                            output.TypeAbbreviation = output.OldTypeAbbreviation
                                            'output.OldTypeAbbreviation = ""
                                        End If
                                        output.IsNameTypeUnitChanged = False
                                        output.PassedThroughProcessor = output.PassedThroughProcessor - 1
                                        _currentSelectedStep.OutputChannels.Remove(output)
                                        Exit For
                                    End If
                                Next
                                'If CurrentSelectedStep.OutputChannels.Count = 1 Then
                                '    Dim onlySignal = CurrentSelectedStep.OutputChannels(0)
                                '    CurrentSelectedStep.OutputChannels.Clear
                                '    onlySignal.SignalName = onlySignal.OldSignalName
                                '    onlySignal.OldSignalName = ""
                                '    onlySignal.Unit = onlySignal.OldUnit
                                '    onlySignal.OldUnit = ""
                                '    onlySignal.TypeAbbreviation = onlySignal.OldTypeAbbreviation
                                '    onlySignal.OldTypeAbbreviation = ""
                                '    onlySignal.IsNameTypeUnitChanged = False
                                '    onlySignal.PassedThroughProcessor = onlySignal.PassedThroughProcessor - 1
                                'Else

                                'End If
                            End If
                            If TypeOf _currentSelectedStep Is PeriodogramDetector Or TypeOf _currentSelectedStep Is SpectralCoherenceDetector Or TypeOf _currentSelectedStep Is OutOfRangeFrequencyDetector Or TypeOf _currentSelectedStep Is RingdownDetector Then
                                If _signalMgr.MappingSignals.Contains(obj.SignalSignature) Then
                                    _signalMgr.MappingSignals.Remove(obj.SignalSignature)
                                End If
                            End If
                            _currentSelectedStep.InputChannels.Remove(obj.SignalSignature)
                        End If
                        obj.SignalSignature.IsChecked = isChecked
                    End If
                End If
            End If
        End Sub
        Private Sub _addInputOutputPairsPhasor(obj As SignalTypeHierachy)
            If obj.SignalList.Count > 0 Then
                For Each signal In obj.SignalList
                    If signal.SignalSignature.IsEnabled Then
                        _addInputOutputPairsPhasor(signal)
                    End If
                Next
            Else
                If (obj.SignalSignature.PMUName Is Nothing Or obj.SignalSignature.TypeAbbreviation Is Nothing) Then
                    Throw New Exception("Item is not a valid signal, or contains no valid signal, nothing to be added or removed!")
                ElseIf _currentSelectedStep.InputChannels.Contains(obj.SignalSignature) Then
                    Throw New Exception("Selected signal: " & obj.SignalSignature.SignalName & " already exists in this step, duplication not allowed!")
                ElseIf obj.SignalSignature.TypeAbbreviation.Length = 3 AndAlso obj.SignalSignature.TypeAbbreviation.Substring(1, 1) = "M" Then
                    Dim pmu = _currentSelectedStep.CustPMUname
                    If pmu Is Nothing Then
                        pmu = _lastCustPMUname
                    End If
                    Dim type = obj.SignalSignature.TypeAbbreviation.Substring(0, 1) & "P" & obj.SignalSignature.TypeAbbreviation.Substring(2, 1)
                    Dim newOutput = New SignalSignatureViewModel("", pmu, type)
                    newOutput.Unit = obj.SignalSignature.Unit
                    newOutput.IsCustomSignal = True
                    Dim newPair = New KeyValuePair(Of SignalSignatureViewModel, ObservableCollection(Of SignalSignatureViewModel))(newOutput, New ObservableCollection(Of SignalSignatureViewModel))
                    newPair.Value.Add(obj.SignalSignature)
                    'Dim dummy = New SignalSignatures()
                    'dummy.IsValid = False
                    newPair.Value.Add(DummySignature)
                    _currentSelectedStep.OutputInputMappingPair.Add(newPair)
                    _currentSelectedStep.InputChannels.Add(obj.SignalSignature)
                    newOutput.OldSignalName = newOutput.SignalName
                    newOutput.OldTypeAbbreviation = newOutput.TypeAbbreviation
                    newOutput.OldUnit = newOutput.Unit
                    _currentSelectedStep.OutputChannels.Add(newOutput)
                ElseIf obj.SignalSignature.TypeAbbreviation.Length = 3 AndAlso obj.SignalSignature.TypeAbbreviation.Substring(1, 1) = "A" Then
                    Dim pmu = _currentSelectedStep.CustPMUname
                    If pmu Is Nothing Then
                        pmu = _lastCustPMUname
                    End If
                    Dim type = obj.SignalSignature.TypeAbbreviation.Substring(0, 1) & "P" & obj.SignalSignature.TypeAbbreviation.Substring(2, 1)
                    Dim newOutput = New SignalSignatureViewModel("", pmu, type)
                    'newOutput.Unit = ""
                    newOutput.IsCustomSignal = True
                    Dim newPair = New KeyValuePair(Of SignalSignatureViewModel, ObservableCollection(Of SignalSignatureViewModel))(newOutput, New ObservableCollection(Of SignalSignatureViewModel))
                    'Dim dummy = New SignalSignatures()
                    'dummy.IsValid = False
                    newPair.Value.Add(DummySignature)
                    newPair.Value.Add(obj.SignalSignature)
                    _currentSelectedStep.OutputInputMappingPair.Add(newPair)
                    _currentSelectedStep.InputChannels.Add(obj.SignalSignature)
                    newOutput.OldSignalName = newOutput.SignalName
                    newOutput.OldTypeAbbreviation = newOutput.TypeAbbreviation
                    newOutput.OldUnit = newOutput.Unit
                    _currentSelectedStep.OutputChannels.Add(newOutput)
                    'Dim ang = _findMatchingAng(obj.SignalSignature)
                    'If ang IsNot Nothing Then
                    '    ang.IsChecked = True
                    '    Dim pmu = _currentSelectedStep.CustPMUname
                    '    If pmu Is Nothing Then
                    '        pmu = _lastCustPMUname
                    '    End If
                    '    Dim name = ang.SignalName
                    '    Dim nameParts = name.Split(".")
                    '    If nameParts.Length <> 3 Then
                    '        name = nameParts(0)
                    '    Else
                    '        name = nameParts(0) & nameParts(1)
                    '    End If
                    '    Dim type = ang.TypeAbbreviation.Substring(0, 1) & "P" & ang.TypeAbbreviation.Substring(2, 1)
                    '    Dim newOutput = New SignalSignatures(name, pmu, type)
                    '    newOutput.IsCustomSignal = True
                    '    Dim newPair = New KeyValuePair(Of SignalSignatures, ObservableCollection(Of SignalSignatures))(newOutput, New ObservableCollection(Of SignalSignatures))
                    '    newPair.Value.Add(obj.SignalSignature)
                    '    newPair.Value.Add(ang)
                    '    _currentSelectedStep.OutputInputMappingPair.Add(newPair)
                    '    _currentSelectedStep.InputChannels.Add(obj.SignalSignature)
                    '    _currentSelectedStep.InputChannels.Add(ang)
                    '    _currentSelectedStep.OutputChannels.Add(newOutput)
                    'Else
                    '    Throw New Exception("Cannot find matching angle signal for selected magnitude signal: " & obj.SignalSignature.SignalName)
                    'End If
                End If
            End If
        End Sub
        ''' <summary>
        ''' This sub checks/unchecks of all children of a node in the signal grouped by type parent tree
        ''' </summary>
        ''' <param name="node"></param>
        ''' <param name="isChecked"></param>
        'Private Sub _checkAllChildren(ByRef node As SignalTypeHierachy, ByVal isChecked As Boolean)
        '    If node.SignalList.Count > 0 Then
        '        ' if not a leaf node, call itself recursively to check/uncheck all children
        '        For Each child In node.SignalList
        '            If child.SignalSignature.IsEnabled Then
        '                child.SignalSignature.IsChecked = isChecked
        '                _checkAllChildren(child, isChecked)
        '            End If
        '        Next
        '    End If
        'End Sub


        Private _setCurrentFocusedTextbox As ICommand
        Public Property SetCurrentFocusedTextbox As ICommand
            Get
                Return _setCurrentFocusedTextbox
            End Get
            Set(ByVal value As ICommand)
                _setCurrentFocusedTextbox = value
            End Set
        End Property
        ''' <summary>
        ''' This method is called when a textbox is clicked in subtraction, division, exponent, unary.... customization
        ''' where we want signal to be put in individual textboxes.
        ''' </summary>
        ''' <param name="obj"></param>
        Private Sub _currentFocusedTextBoxChanged(obj As Object)
            If _currentSelectedStep IsNot Nothing Then
                For Each signal In _currentSelectedStep.InputChannels
                    signal.IsChecked = False
                Next
                If obj IsNot Nothing AndAlso Not String.IsNullOrEmpty(obj.TypeAbbreviation) AndAlso Not String.IsNullOrEmpty(obj.PMUName) Then
                    obj.IsChecked = True
                End If
                '_dataConfigDetermineAllParentNodeStatus()
                _signalMgr.DetermineDataConfigPostProcessConfigAllParentNodeStatus()
                'If _currentSelectedStep.Name = "Phasor Creation" Then
                '    _currentInputOutputPair = obj
                'End If
            End If
        End Sub

        ''' <summary>
        ''' This points to the current selected textbox of a Unary operation step which is a pair of input output.
        ''' </summary>
        Private _currentInputOutputPair As KeyValuePair(Of SignalSignatureViewModel, ObservableCollection(Of SignalSignatureViewModel))? = Nothing
        'Private _currentMultipleInputOutputPair As KeyValuePair(Of SignalSignatures, ObservableCollection(Of SignalSignatures)) ? = Nothing
        Private _setCurrentFocusedTextboxUnarySteps As ICommand
        Public Property SetCurrentFocusedTextboxUnarySteps As ICommand
            Get
                Return _setCurrentFocusedTextboxUnarySteps
            End Get
            Set(ByVal value As ICommand)
                _setCurrentFocusedTextboxUnarySteps = value
            End Set
        End Property
        Private Sub _currentFocusedTextBoxForUnaryStepsChanged(obj As Object)
            _currentInputOutputPair = obj
            _currentFocusedTextBoxChanged(obj.Value(0))
        End Sub
        Private _powerPhasorTextBoxGotFocus As ICommand
        Public Property PowerPhasorTextBoxGotFocus As ICommand
            Get
                Return _powerPhasorTextBoxGotFocus
            End Get
            Set(ByVal value As ICommand)
                _powerPhasorTextBoxGotFocus = value
            End Set
        End Property
        Private Sub _powerPhasorCurrentFocusedTextbox(obj As Object)
            _currentFocusedTextBoxChanged(obj)
            _currentFocusedPhasorSignalForPowerCalculation = obj
        End Sub
        Private _currentFocusedPhasorSignalForPowerCalculation As SignalSignatureViewModel
        Private _setCurrentPhasorCreationFocusedTextBox As ICommand
        Public Property SetCurrentPhasorCreationFocusedTextBox As ICommand
            Get
                Return _setCurrentPhasorCreationFocusedTextBox
            End Get
            Set(ByVal value As ICommand)
                _setCurrentPhasorCreationFocusedTextBox = value
            End Set
        End Property
        Private Sub _phasrCreationCurrentFocusedTextBoxChanged(obj As Object)
            If _currentSelectedStep IsNot Nothing Then
                For Each signal In _currentSelectedStep.InputChannels
                    signal.IsChecked = False
                Next
                If obj Is Nothing Then
                    _currentInputOutputPair = Nothing
                Else
                    If obj(1) IsNot Nothing AndAlso Not String.IsNullOrEmpty(obj(1).TypeAbbreviation) AndAlso Not String.IsNullOrEmpty(obj(1).PMUName) Then
                        obj(1).IsChecked = True
                    End If
                    _currentInputOutputPair = obj(0)
                End If
                _signalMgr.DetermineDataConfigPostProcessConfigAllParentNodeStatus()
            End If
        End Sub
        Private _setCurrentPointOnWavePowerCalFilterInputFocusedTexBox As ICommand
        Public Property SeCurrentPointOnWavePowerCalFilterInputFocusedTexBox As ICommand
            Get
                Return _setCurrentPointOnWavePowerCalFilterInputFocusedTexBox
            End Get
            Set(ByVal value As ICommand)
                _setCurrentPointOnWavePowerCalFilterInputFocusedTexBox = value
            End Set
        End Property
        Private Sub _setCurrentPointOnWavePowerCalFilterInput(obj As Object)
            If _currentSelectedStep IsNot Nothing Then
                For Each signal In _currentSelectedStep.InputChannels
                    signal.IsChecked = False
                Next
                If obj(0) IsNot Nothing AndAlso Not String.IsNullOrEmpty(obj(0).TypeAbbreviation) AndAlso Not String.IsNullOrEmpty(obj(0).PMUName) Then
                    obj(0).IsChecked = True
                End If
                _signalMgr.ProcessConfigDetermineAllParentNodeStatus()
                _pointOnWavePowCalFltrInputSignalNeedToBeChanged = obj(1)
            End If
        End Sub
        Private _pointOnWavePowCalFltrInputSignalNeedToBeChanged As String
#End Region
        Private Function _determineSignalCountInTree(obj As SignalTypeHierachy) As Integer
            If obj.SignalList.Count > 0 Then
                Dim totalcount = 0
                For Each tree In obj.SignalList
                    totalcount += _determineSignalCountInTree(tree)
                Next
                Return totalcount
            Else
                Return 1
            End If
        End Function
        Private Function _findTheBottomSignal(obj As SignalTypeHierachy) As SignalSignatureViewModel
            While obj.SignalList.Count > 0
                Return _findTheBottomSignal(obj.SignalList.FirstOrDefault)
            End While
            Return obj.SignalSignature
        End Function
        'Private Sub _dataConfigDetermineAllParentNodeStatus()
        '    _determineParentGroupedByTypeNodeStatus(_signalMgr.GroupedRawSignalsByType)
        '    _determineParentGroupedByTypeNodeStatus(_signalMgr.GroupedRawSignalsByPMU)
        '    For Each stepInput In _signalMgr.GroupedSignalByDataConfigStepsInput
        '        If stepInput.SignalList.Count > 0 Then
        '            _determineParentGroupedByTypeNodeStatus(stepInput.SignalList)
        '            _determineParentCheckStatus(stepInput)
        '        Else
        '            stepInput.SignalSignature.IsChecked = False
        '        End If
        '    Next
        '    For Each stepOutput In _signalMgr.GroupedSignalByDataConfigStepsOutput
        '        If stepOutput.SignalList.Count > 0 Then
        '            _determineParentGroupedByTypeNodeStatus(stepOutput.SignalList)
        '            _determineParentCheckStatus(stepOutput)
        '        Else
        '            stepOutput.SignalSignature.IsChecked = False
        '        End If
        '    Next
        'End Sub
        'Private Sub _determineDataConfigPostProcessConfigAllParentNodeStatus()
        '    _determineParentGroupedByTypeNodeStatus(_signalMgr.GroupedRawSignalsByType)
        '    _determineParentGroupedByTypeNodeStatus(_signalMgr.GroupedRawSignalsByPMU)
        '    _determineParentGroupedByTypeNodeStatus(_signalMgr.AllDataConfigOutputGroupedByType)
        '    _determineParentGroupedByTypeNodeStatus(_signalMgr.AllDataConfigOutputGroupedByPMU)
        '    _determineParentGroupedByTypeNodeStatus(_signalMgr.AllProcessConfigOutputGroupedByType)
        '    _determineParentGroupedByTypeNodeStatus(_signalMgr.AllProcessConfigOutputGroupedByPMU)
        '    For Each stepInput In _signalMgr.GroupedSignalByDataConfigStepsInput
        '        If stepInput.SignalList.Count > 0 Then
        '            _determineParentGroupedByTypeNodeStatus(stepInput.SignalList)
        '            _determineParentCheckStatus(stepInput)
        '        Else
        '            stepInput.SignalSignature.IsChecked = False
        '        End If
        '    Next
        '    For Each stepOutput In _signalMgr.GroupedSignalByDataConfigStepsOutput
        '        If stepOutput.SignalList.Count > 0 Then
        '            _determineParentGroupedByTypeNodeStatus(stepOutput.SignalList)
        '            _determineParentCheckStatus(stepOutput)
        '        Else
        '            stepOutput.SignalSignature.IsChecked = False
        '        End If
        '    Next
        '    For Each stepInput In _signalMgr.GroupedSignalByPostProcessConfigStepsInput
        '        If stepInput.SignalList.Count > 0 Then
        '            _determineParentGroupedByTypeNodeStatus(stepInput.SignalList)
        '            _determineParentCheckStatus(stepInput)
        '        Else
        '            stepInput.SignalSignature.IsChecked = False
        '        End If
        '    Next
        '    For Each stepOutput In _signalMgr.GroupedSignalByPostProcessConfigStepsOutput
        '        If stepOutput.SignalList.Count > 0 Then
        '            _determineParentGroupedByTypeNodeStatus(stepOutput.SignalList)
        '            _determineParentCheckStatus(stepOutput)
        '        Else
        '            stepOutput.SignalSignature.IsChecked = False
        '        End If
        '    Next
        'End Sub
        'Private Sub _determineAllParentNodeStatus()
        '    _determineParentGroupedByTypeNodeStatus(_signalMgr.GroupedRawSignalsByType)
        '    _determineParentGroupedByTypeNodeStatus(_signalMgr.GroupedRawSignalsByPMU)
        '    _determineParentGroupedByTypeNodeStatus(_signalMgr.ReGroupedRawSignalsByType)
        '    _determineParentGroupedByTypeNodeStatus(_signalMgr.AllDataConfigOutputGroupedByType)
        '    _determineParentGroupedByTypeNodeStatus(_signalMgr.AllDataConfigOutputGroupedByPMU)
        '    _determineParentGroupedByTypeNodeStatus(_signalMgr.AllProcessConfigOutputGroupedByType)
        '    _determineParentGroupedByTypeNodeStatus(_signalMgr.AllProcessConfigOutputGroupedByPMU)
        '    _determineParentGroupedByTypeNodeStatus(_signalMgr.AllPostProcessOutputGroupedByType)
        '    _determineParentGroupedByTypeNodeStatus(_signalMgr.AllPostProcessOutputGroupedByPMU)
        '    For Each stepInput In _signalMgr.GroupedSignalByDataConfigStepsInput
        '        If stepInput.SignalList.Count > 0 Then
        '            _determineParentGroupedByTypeNodeStatus(stepInput.SignalList)
        '            _determineParentCheckStatus(stepInput)
        '        Else
        '            stepInput.SignalSignature.IsChecked = False
        '        End If
        '    Next
        '    For Each stepOutput In _signalMgr.GroupedSignalByDataConfigStepsOutput
        '        If stepOutput.SignalList.Count > 0 Then
        '            _determineParentGroupedByTypeNodeStatus(stepOutput.SignalList)
        '            _determineParentCheckStatus(stepOutput)
        '        Else
        '            stepOutput.SignalSignature.IsChecked = False
        '        End If
        '    Next
        '    For Each stepInput In _signalMgr.GroupedSignalByProcessConfigStepsInput
        '        If stepInput.SignalList.Count > 0 Then
        '            _determineParentGroupedByTypeNodeStatus(stepInput.SignalList)
        '            _determineParentCheckStatus(stepInput)
        '        Else
        '            stepInput.SignalSignature.IsChecked = False
        '        End If
        '    Next
        '    For Each stepOutput In _signalMgr.GroupedSignalByProcessConfigStepsOutput
        '        If stepOutput.SignalList.Count > 0 Then
        '            _determineParentGroupedByTypeNodeStatus(stepOutput.SignalList)
        '            _determineParentCheckStatus(stepOutput)
        '        Else
        '            stepOutput.SignalSignature.IsChecked = False
        '        End If
        '    Next
        '    For Each stepInput In _signalMgr.GroupedSignalByPostProcessConfigStepsInput
        '        If stepInput.SignalList.Count > 0 Then
        '            _determineParentGroupedByTypeNodeStatus(stepInput.SignalList)
        '            _determineParentCheckStatus(stepInput)
        '        Else
        '            stepInput.SignalSignature.IsChecked = False
        '        End If
        '    Next
        '    For Each stepOutput In _signalMgr.GroupedSignalByPostProcessConfigStepsOutput
        '        If stepOutput.SignalList.Count > 0 Then
        '            _determineParentGroupedByTypeNodeStatus(stepOutput.SignalList)
        '            _determineParentCheckStatus(stepOutput)
        '        Else
        '            stepOutput.SignalSignature.IsChecked = False
        '        End If
        '    Next
        '    For Each stepInput In _signalMgr.GroupedSignalByDetectorInput
        '        If stepInput.SignalList.Count > 0 Then
        '            _determineParentGroupedByTypeNodeStatus(stepInput.SignalList)
        '            _determineParentCheckStatus(stepInput)
        '        Else
        '            stepInput.SignalSignature.IsChecked = False
        '        End If
        '    Next
        'End Sub

        ''' <summary>
        ''' Go down a tree to determine nodes checking status
        ''' </summary>
        ''' <param name="groups"></param>
        'Private Sub _determineParentGroupedByTypeNodeStatus(groups As ObservableCollection(Of SignalTypeHierachy))
        '    If groups.Count > 0 Then
        '        For Each group In groups
        '            ' if has children, then its status depends on children status
        '            If group.SignalList.Count > 0 Then
        '                For Each subgroup In group.SignalList
        '                    If subgroup.SignalList.Count > 0 Then
        '                        For Each subsubgroup In subgroup.SignalList
        '                            If subsubgroup.SignalList.Count > 0 Then
        '                                For Each subsubsubgroup In subsubgroup.SignalList
        '                                    If subsubsubgroup.SignalList.Count > 0 Then
        '                                        For Each subsubsubsubgroup In subsubsubgroup.SignalList
        '                                            If subsubsubsubgroup.SignalList.Count > 0 Then
        '                                                _determineParentCheckStatus(subsubsubsubgroup)
        '                                            End If
        '                                        Next
        '                                        _determineParentCheckStatus(subsubsubgroup)
        '                                    End If
        '                                Next
        '                                _determineParentCheckStatus(subsubgroup)
        '                            End If
        '                        Next
        '                        _determineParentCheckStatus(subgroup)
        '                    End If
        '                Next
        '                _determineParentCheckStatus(group)
        '            Else
        '                ' else, no children, status must be false, this only applies top level nodes, since leaf node won't have children at all
        '                group.SignalSignature.IsChecked = False
        '            End If
        '        Next
        '    End If
        'End Sub
        ''' <summary>
        ''' This sub loop through all children of a hierachy node to determine the node's status of checked/unchecked/indeterminate
        ''' </summary>
        ''' <param name="group"></param>
        'Private Sub _determineParentCheckStatus(group As SignalTypeHierachy)
        '    If group.SignalList.Count > 0 Then
        '        Dim hasCheckedItem = False
        '        Dim hasUnCheckedItem = False
        '        For Each subgroup In group.SignalList
        '            If subgroup.SignalSignature.IsChecked Is Nothing Then
        '                hasCheckedItem = True
        '                hasUnCheckedItem = True
        '                Exit For
        '            End If
        '            If subgroup.SignalSignature.IsChecked And Not hasCheckedItem Then
        '                hasCheckedItem = True
        '                Continue For
        '            End If
        '            If subgroup.SignalSignature.IsChecked = False And Not hasUnCheckedItem Then
        '                hasUnCheckedItem = True
        '            End If
        '            If hasCheckedItem And hasUnCheckedItem Then
        '                Exit For
        '            End If
        '        Next
        '        If hasCheckedItem And hasUnCheckedItem Then
        '            group.SignalSignature.IsChecked = Nothing
        '        Else
        '            group.SignalSignature.IsChecked = hasCheckedItem
        '        End If
        '    End If
        'End Sub

        ''' <summary>
        ''' Check and decide if a file directory and its sub grouped signal is checkable or not depends on other file directory check status
        ''' </summary>
        'Private Sub _determineFileDirCheckableStatus()
        '    Dim disableOthers = False
        '    For Each group In _signalMgr.GroupedRawSignalsByType
        '        If group.SignalSignature.IsChecked Or group.SignalSignature.IsChecked Is Nothing Then
        '            disableOthers = True
        '            Exit For
        '        End If
        '    Next
        '    If disableOthers Then
        '        For Each group In _signalMgr.GroupedRawSignalsByType
        '            If Not group.SignalSignature.IsChecked Then
        '                group.SignalSignature.IsEnabled = False
        '            Else
        '                group.SignalSignature.IsEnabled = True
        '            End If
        '        Next
        '        For Each group In _signalMgr.GroupedRawSignalsByPMU
        '            If Not group.SignalSignature.IsChecked Then
        '                group.SignalSignature.IsEnabled = False
        '            Else
        '                group.SignalSignature.IsEnabled = True
        '            End If
        '        Next
        '    Else
        '        For Each group In _signalMgr.GroupedRawSignalsByType
        '            group.SignalSignature.IsEnabled = True
        '        Next
        '        For Each group In _signalMgr.GroupedRawSignalsByPMU
        '            group.SignalSignature.IsEnabled = True
        '        Next
        '    End If
        'End Sub
        Private Sub _determineSamplingRateCheckableStatus()
            Dim freq = -1
            If _currentSelectedStep IsNot Nothing AndAlso _currentSelectedStep.InputChannels.Count > 0 AndAlso _currentSelectedStep.InputChannels(0).SamplingRate <> -1 Then
                freq = _currentSelectedStep.InputChannels(0).SamplingRate
                '    If _currentTabIndex = 1 Then
                '        For Each group In _signalMgr.GroupedSignalByDataConfigStepsInput
                '            For Each subgroup In group.SignalList
                '                If subgroup.SignalSignature.SamplingRate <> freq Then
                '                    subgroup.SignalSignature.IsEnabled = False
                '                Else
                '                    subgroup.SignalSignature.IsEnabled = True
                '                End If
                '            Next
                '        Next
                '        For Each group In _signalMgr.GroupedSignalByDataConfigStepsOutput
                '            For Each subgroup In group.SignalList
                '                If subgroup.SignalSignature.SamplingRate <> freq Then
                '                    subgroup.SignalSignature.IsEnabled = False
                '                Else
                '                    subgroup.SignalSignature.IsEnabled = True
                '                End If
                '            Next
                '        Next
                '    ElseIf _currentTabIndex = 2 Then
                '        For Each group In _signalMgr.GroupedSignalByProcessConfigStepsInput
                '            For Each subgroup In group.SignalList
                '                If subgroup.SignalSignature.SamplingRate <> freq Then
                '                    subgroup.SignalSignature.IsEnabled = False
                '                Else
                '                    subgroup.SignalSignature.IsEnabled = True
                '                End If
                '            Next
                '        Next
                '        For Each group In _signalMgr.GroupedSignalByProcessConfigStepsOutput
                '            For Each subgroup In group.SignalList
                '                If subgroup.SignalSignature.SamplingRate <> freq Then
                '                    subgroup.SignalSignature.IsEnabled = False
                '                Else
                '                    subgroup.SignalSignature.IsEnabled = True
                '                End If
                '            Next
                '        Next
                '        For Each group In _signalMgr.AllDataConfigOutputGroupedByPMU
                '            If group.SignalSignature.SamplingRate <> freq Then
                '                group.SignalSignature.IsEnabled = False
                '            Else
                '                group.SignalSignature.IsEnabled = True
                '            End If
                '        Next
                '        For Each group In _signalMgr.AllDataConfigOutputGroupedByType
                '            If group.SignalSignature.SamplingRate <> freq Then
                '                group.SignalSignature.IsEnabled = False
                '            Else
                '                group.SignalSignature.IsEnabled = True
                '            End If
                '        Next
                '    ElseIf _currentTabIndex = 3 Then
                '        For Each group In _signalMgr.GroupedSignalByPostProcessConfigStepsInput
                '            For Each subgroup In group.SignalList
                '                If subgroup.SignalSignature.SamplingRate <> freq Then
                '                    subgroup.SignalSignature.IsEnabled = False
                '                Else
                '                    subgroup.SignalSignature.IsEnabled = True
                '                End If
                '            Next
                '        Next
                '        For Each group In _signalMgr.GroupedSignalByPostProcessConfigStepsOutput
                '            For Each subgroup In group.SignalList
                '                If subgroup.SignalSignature.SamplingRate <> freq Then
                '                    subgroup.SignalSignature.IsEnabled = False
                '                Else
                '                    subgroup.SignalSignature.IsEnabled = True
                '                End If
                '            Next
                '        Next
                '        For Each group In _signalMgr.AllDataConfigOutputGroupedByPMU
                '            If group.SignalSignature.SamplingRate <> freq Then
                '                group.SignalSignature.IsEnabled = False
                '            Else
                '                group.SignalSignature.IsEnabled = True
                '            End If
                '        Next
                '        For Each group In _signalMgr.AllDataConfigOutputGroupedByType
                '            If group.SignalSignature.SamplingRate <> freq Then
                '                group.SignalSignature.IsEnabled = False
                '            Else
                '                group.SignalSignature.IsEnabled = True
                '            End If
                '        Next
                '        For Each group In _signalMgr.AllProcessConfigOutputGroupedByPMU
                '            If group.SignalSignature.SamplingRate <> freq Then
                '                group.SignalSignature.IsEnabled = False
                '            Else
                '                group.SignalSignature.IsEnabled = True
                '            End If
                '        Next
                '        For Each group In _signalMgr.AllProcessConfigOutputGroupedByType
                '            If group.SignalSignature.SamplingRate <> freq Then
                '                group.SignalSignature.IsEnabled = False
                '            Else
                '                group.SignalSignature.IsEnabled = True
                '            End If
                '        Next
                '    ElseIf _currentTabIndex = 4 Then
                '        For Each group In _signalMgr.GroupedSignalByDetectorInput
                '            For Each subgroup In group.SignalList
                '                If subgroup.SignalSignature.SamplingRate <> freq Then
                '                    subgroup.SignalSignature.IsEnabled = False
                '                Else
                '                    subgroup.SignalSignature.IsEnabled = True
                '                End If
                '            Next
                '        Next
                '        For Each group In _signalMgr.AllDataConfigOutputGroupedByPMU
                '            If group.SignalSignature.SamplingRate <> freq Then
                '                group.SignalSignature.IsEnabled = False
                '            Else
                '                group.SignalSignature.IsEnabled = True
                '            End If
                '        Next
                '        For Each group In _signalMgr.AllDataConfigOutputGroupedByType
                '            If group.SignalSignature.SamplingRate <> freq Then
                '                group.SignalSignature.IsEnabled = False
                '            Else
                '                group.SignalSignature.IsEnabled = True
                '            End If
                '        Next
                '        For Each group In _signalMgr.AllProcessConfigOutputGroupedByPMU
                '            If group.SignalSignature.SamplingRate <> freq Then
                '                group.SignalSignature.IsEnabled = False
                '            Else
                '                group.SignalSignature.IsEnabled = True
                '            End If
                '        Next
                '        For Each group In _signalMgr.AllProcessConfigOutputGroupedByType
                '            If group.SignalSignature.SamplingRate <> freq Then
                '                group.SignalSignature.IsEnabled = False
                '            Else
                '                group.SignalSignature.IsEnabled = True
                '            End If
                '        Next
                '        For Each group In _signalMgr.AllPostProcessOutputGroupedByPMU
                '            If group.SignalSignature.SamplingRate <> freq Then
                '                group.SignalSignature.IsEnabled = False
                '            Else
                '                group.SignalSignature.IsEnabled = True
                '            End If
                '        Next
                '        For Each group In _signalMgr.AllPostProcessOutputGroupedByType
                '            If group.SignalSignature.SamplingRate <> freq Then
                '                group.SignalSignature.IsEnabled = False
                '            Else
                '                group.SignalSignature.IsEnabled = True
                '            End If
                '        Next
                '    End If
                'Else        'enable all to be checkable regardless sampling rate
                '    If _currentTabIndex = 1 Then
                '        For Each group In _signalMgr.GroupedSignalByDataConfigStepsInput
                '            For Each subgroup In group.SignalList
                '                subgroup.SignalSignature.IsEnabled = True
                '            Next
                '        Next
                '        For Each group In _signalMgr.GroupedSignalByDataConfigStepsOutput
                '            For Each subgroup In group.SignalList
                '                subgroup.SignalSignature.IsEnabled = True
                '            Next
                '        Next
                '    ElseIf _currentTabIndex = 2 Then
                '        For Each group In _signalMgr.GroupedSignalByProcessConfigStepsInput
                '            For Each subgroup In group.SignalList
                '                subgroup.SignalSignature.IsEnabled = True
                '            Next
                '        Next
                '        For Each group In _signalMgr.GroupedSignalByProcessConfigStepsOutput
                '            For Each subgroup In group.SignalList
                '                subgroup.SignalSignature.IsEnabled = True
                '            Next
                '        Next
                '        For Each group In _signalMgr.AllDataConfigOutputGroupedByPMU
                '            group.SignalSignature.IsEnabled = True
                '        Next
                '        For Each group In _signalMgr.AllDataConfigOutputGroupedByType
                '            group.SignalSignature.IsEnabled = True
                '        Next
                '    ElseIf _currentTabIndex = 3 Then
                '        For Each group In _signalMgr.GroupedSignalByPostProcessConfigStepsInput
                '            For Each subgroup In group.SignalList
                '                subgroup.SignalSignature.IsEnabled = True
                '            Next
                '        Next
                '        For Each group In _signalMgr.GroupedSignalByPostProcessConfigStepsOutput
                '            For Each subgroup In group.SignalList
                '                subgroup.SignalSignature.IsEnabled = True
                '            Next
                '        Next
                '        For Each group In _signalMgr.AllDataConfigOutputGroupedByPMU
                '            group.SignalSignature.IsEnabled = True
                '        Next
                '        For Each group In _signalMgr.AllDataConfigOutputGroupedByType
                '            group.SignalSignature.IsEnabled = True
                '        Next
                '        For Each group In _signalMgr.AllProcessConfigOutputGroupedByPMU
                '            group.SignalSignature.IsEnabled = True
                '        Next
                '        For Each group In _signalMgr.AllProcessConfigOutputGroupedByType
                '            group.SignalSignature.IsEnabled = True
                '        Next
                '    ElseIf _currentTabIndex = 4 Then
                '        For Each group In _signalMgr.GroupedSignalByDetectorInput
                '            For Each subgroup In group.SignalList
                '                subgroup.SignalSignature.IsEnabled = True
                '            Next
                '        Next
                '        For Each group In _signalMgr.AllDataConfigOutputGroupedByPMU
                '            group.SignalSignature.IsEnabled = True
                '        Next
                '        For Each group In _signalMgr.AllDataConfigOutputGroupedByType
                '            group.SignalSignature.IsEnabled = True
                '        Next
                '        For Each group In _signalMgr.AllProcessConfigOutputGroupedByPMU
                '            group.SignalSignature.IsEnabled = True
                '        Next
                '        For Each group In _signalMgr.AllProcessConfigOutputGroupedByType
                '            group.SignalSignature.IsEnabled = True
                '        Next
                '        For Each group In _signalMgr.AllPostProcessOutputGroupedByPMU
                '            group.SignalSignature.IsEnabled = True
                '        Next
                '        For Each group In _signalMgr.AllPostProcessOutputGroupedByType
                '            group.SignalSignature.IsEnabled = True
                '        Next
                '    End If
            End If
            _signalMgr.DetermineSamplingRateCheckableStatus(_currentSelectedStep, _currentTabIndex, freq)
        End Sub
#Region "Step manipulation: Add a step"
        Private _dqfilterSelected As ICommand
        Public Property DQFilterSelected As ICommand
            Get
                Return _dqfilterSelected
            End Get
            Set(ByVal value As ICommand)
                _dqfilterSelected = value
            End Set
        End Property
        Private Sub _dqfilterSelection(obj As Object)
            Dim thisFilterName = obj.ToString
            Dim newFilter As New Object
            Select Case thisFilterName
                Case "Status Flags"
                    newFilter = New StatusFlagsDQFilter
                Case "Zeros"
                    newFilter = New ZerosDQFilter
                Case "Missing"
                    newFilter = New MissingDQFilter
                Case "Nominal Voltage"
                    newFilter = New VoltPhasorDQFilter
                Case "Nominal Frequency"
                    newFilter = New FreqDQFilter
                Case "Outliers"
                    newFilter = New OutlierDQFilter
                Case "Stale Data"
                    newFilter = New StaleDQFilter
                Case "Data Frame"
                    newFilter = New DataFrameDQFilter
                Case "Channel"
                    newFilter = New PMUchanDQFilter
                Case "Entire PMU"
                    newFilter = New PMUallDQFilter
                Case "Angle Wrapping"
                    newFilter = New WrappingFailureDQFilter
            End Select
            newFilter.IsExpanded = True
            newFilter.StepCounter = DataConfigure.CollectionOfSteps.Count + 1
            newFilter.ThisStepInputsAsSignalHerachyByType.SignalSignature.SignalName = "Step " & newFilter.StepCounter.ToString & "-" & newFilter.Name
            newFilter.ThisStepOutputsAsSignalHierachyByPMU.SignalSignature.SignalName = "Step " & newFilter.StepCounter.ToString & "-" & newFilter.Name
            'GroupedSignalByStepsInput.Add(newFilter.ThisStepInputsAsSignalHerachyByType)
            'GroupedSignalByDataConfigStepsOutput.Add(newFilter.ThisStepOutputsAsSignalHierachyByPMU)

            'For Each parameter In DataConfigure.DQFilterNameParametersDictionary(newFilter.Name)
            '    If parameter = "FlagAllByFreq" Then
            '        newFilter.FilterParameters.Add(New ParameterValuePair(parameter, False))
            '    ElseIf parameter = "SetToNaN" OrElse parameter = "FlagBit" Then
            '        'newFilter.FilterParameters.Add(New ParameterValuePair(parameter, True))
            '        'ElseIf newFilter.Name = "Nominal Frequency" And parameter = "FlagBit" Then
            '        '    newFilter.FilterParameters.Add(New ParameterValuePair(parameter, "", False))
            '    Else
            '        newFilter.FilterParameters.Add(New ParameterValuePair(parameter, ""))
            '    End If
            'Next
            'newFilter.Parameters
            DataConfigure.CollectionOfSteps.Add(newFilter)
            _stepSelectedToEdit(newFilter)
        End Sub
        Private _customizationSelected As ICommand
        Public Property CustomizationSelected As ICommand
            Get
                Return _customizationSelected
            End Get
            Set(ByVal value As ICommand)
                _customizationSelected = value
            End Set
        End Property
        Private Sub _customizationStepSelection(obj As Object)
            Dim thisCustmizationName = obj(0).ToString
            Dim newCustomization As Object
            Try
                Select Case thisCustmizationName
                    Case "Scalar Repetition"
                        newCustomization = New ScalarRepCust
                        Dim newSignal = New SignalSignatureViewModel("", newCustomization.CustPMUname, "SC")
                        newSignal.IsCustomSignal = True
                        newCustomization.OutputChannels.Add(newSignal)
                    Case "Addition"
                        newCustomization = New AdditionCust
                        Dim newSignal = New SignalSignatureViewModel("", newCustomization.CustPMUname)
                        newSignal.IsCustomSignal = True
                        newCustomization.OutputChannels.Add(newSignal)
                    Case "Subtraction"
                        newCustomization = New SubtractionCust
                        Dim newSignal = New SignalSignatureViewModel("", newCustomization.CustPMUname)
                        newSignal.IsCustomSignal = True
                        newCustomization.OutputChannels.Add(newSignal)
                        Dim dummy = New SignalSignatureViewModel("", "")
                        dummy.IsValid = False
                        newCustomization.Minuend = dummy
                        newCustomization.Subtrahend = dummy
                    Case "Multiplication"
                        newCustomization = New MultiplicationCust
                        Dim newSignal = New SignalSignatureViewModel("", newCustomization.CustPMUname)
                        newSignal.IsCustomSignal = True
                        newCustomization.OutputChannels.Add(newSignal)
                    Case "Division"
                        newCustomization = New DivisionCust
                        Dim newSignal = New SignalSignatureViewModel("", newCustomization.CustPMUname)
                        newSignal.IsCustomSignal = True
                        newCustomization.OutputChannels.Add(newSignal)
                        Dim dummy = New SignalSignatureViewModel("", "")
                        dummy.IsValid = False
                        newCustomization.Dividend = dummy
                        newCustomization.Divisor = dummy
                    Case "Exponential"
                        newCustomization = New ExponentialCust
                        newCustomization.Exponent = "1"
                    Case "Sign Reversal"
                        newCustomization = New SignReversalCust
                    Case "Absolute Value"
                        newCustomization = New AbsValCust
                    Case "Real Component"
                        newCustomization = New RealComponentCust
                    Case "Imaginary Component"
                        newCustomization = New ImagComponentCust
                    Case "Angle Calculation"
                        newCustomization = New AngleCust
                    Case "Complex Conjugate"
                        newCustomization = New ComplexConjCust
                    Case "Phasor Creation"
                        newCustomization = New CreatePhasorCust
                    Case "Power Calculation"
                        newCustomization = New PowerCalcCust
                        Dim newSignal = New SignalSignatureViewModel("", newCustomization.CustPMUname, newCustomization.PowType.ToString)
                        Select Case newSignal.TypeAbbreviation
                            Case "CP", "S"
                                newSignal.Unit = "MVA"
                            Case "Q"
                                newSignal.Unit = "MVAR"
                            Case "P"
                                newSignal.Unit = "MW"
                        End Select
                        newSignal.IsCustomSignal = True
                        newCustomization.OutputChannels.Add(newSignal)
                        Dim newPair = New KeyValuePair(Of SignalSignatureViewModel, ObservableCollection(Of SignalSignatureViewModel))(newSignal, New ObservableCollection(Of SignalSignatureViewModel))
                        newCustomization.OutputInputMappingPair.Add(newPair)
                    Case "Signal Type/Unit"
                        newCustomization = New SpecifySignalTypeUnitCust
                        Dim newSignal = New SignalSignatureViewModel("", newCustomization.CustPMUname, "")
                        newSignal.IsCustomSignal = True
                        newCustomization.OutputChannels.Add(newSignal)
                        Dim dummy = New SignalSignatureViewModel("", "", "OTHER")
                        dummy.IsValid = False
                        newCustomization.InputChannels.Add(dummy)
                    Case "Metric Prefix"
                        newCustomization = New MetricPrefixCust
                    Case "Angle Conversion"
                        newCustomization = New AngleConversionCust
                    Case "Duplicate Signals"
                        newCustomization = New SignalReplicationCust
                    Case "Graph Eigenvalue"
                        newCustomization = New GraphEigenvalueCust
                        Dim newSignal = New SignalSignatureViewModel("", newCustomization.CustPMUname, "OTHER")
                        newSignal.IsCustomSignal = True
                        newCustomization.OutputChannels.Add(newSignal)
                    Case "PCA"
                        newCustomization = New PCACust
                    Case Else
                        Throw New Exception("Customization step not supported!")
                End Select
                newCustomization.IsExpanded = True
                If _lastCustPMUname Is Nothing Then
                    _lastCustPMUname = ""
                End If
                newCustomization.CustPMUname = _lastCustPMUname
            Catch ex As Exception
                Forms.MessageBox.Show("Error selecting signal(s) for customization step!" & ex.Message, "Error!", MessageBoxButtons.OK)
            End Try
            newCustomization.ThisStepOutputsAsSignalHierachyByPMU.SignalList = _signalMgr.SortSignalByPMU(newCustomization.OutputChannels)
            'GroupedSignalByDataConfigStepsInput.Add(newCustomization.ThisStepInputsAsSignalHerachyByType)
            'GroupedSignalByDataConfigStepsOutput.Add(newCustomization.ThisStepOutputsAsSignalHierachyByPMU)
            'newCustomization.IsStepSelected = True
            If obj(1) = "DataConfig" Then
                newCustomization.StepCounter = DataConfigure.CollectionOfSteps.Count + 1
                DataConfigure.CollectionOfSteps.Add(newCustomization)
                newCustomization.ThisStepInputsAsSignalHerachyByType.SignalSignature.SignalName = "Step " & newCustomization.StepCounter.ToString & " - " & newCustomization.Name
                newCustomization.ThisStepOutputsAsSignalHierachyByPMU.SignalSignature.SignalName = "Step " & newCustomization.StepCounter.ToString & " - " & newCustomization.Name
                _stepSelectedToEdit(newCustomization)
            Else
                newCustomization.StepCounter = PostProcessConfigure.CollectionOfSteps.Count + 1
                PostProcessConfigure.CollectionOfSteps.Add(newCustomization)
                newCustomization.ThisStepInputsAsSignalHerachyByType.SignalSignature.SignalName = "Step " & newCustomization.StepCounter.ToString & " - " & newCustomization.Name
                newCustomization.ThisStepOutputsAsSignalHierachyByPMU.SignalSignature.SignalName = "Step " & newCustomization.StepCounter.ToString & " - " & newCustomization.Name
                _postProcessConfigureStepSelected(newCustomization)
            End If
        End Sub
        Private _choosePhasorForPowerCalculation As ICommand
        Public Property ChoosePhasorForPowerCalculation As ICommand
            Get
                Return _choosePhasorForPowerCalculation
            End Get
            Set(ByVal value As ICommand)
                _choosePhasorForPowerCalculation = value
                OnPropertyChanged()
            End Set
        End Property
        Private Sub _powerCalculationPhasorOption(obj As Object)
            Dim vPhasor = New SignalSignatureViewModel("", _currentSelectedStep.CustPMUname)
            vPhasor.IsValid = False
            _currentSelectedStep.OutputInputMappingPair(0).Value.Add(vPhasor)
            Dim iPhasor = New SignalSignatureViewModel("", _currentSelectedStep.CustPMUname)
            iPhasor.IsValid = False
            _currentSelectedStep.OutputInputMappingPair(0).Value.Add(iPhasor)
            _disableEnableAllButPhasorSignalsInDataConfig(False)
        End Sub
        Private _chooseMagAngForPowerCalculation As ICommand
        Public Property ChooseMagAngForPowerCalculation As ICommand
            Get
                Return _chooseMagAngForPowerCalculation
            End Get
            Set(ByVal value As ICommand)
                _chooseMagAngForPowerCalculation = value
                OnPropertyChanged()
            End Set
        End Property
        Private Sub _powerCalculationMagAngOption(obj As Object)
            Dim vMag = New SignalSignatureViewModel("", _currentSelectedStep.CustPMUname)
            vMag.IsValid = False
            _currentSelectedStep.OutputInputMappingPair(0).Value.Add(vMag)
            Dim vAng = New SignalSignatureViewModel("", _currentSelectedStep.CustPMUname)
            vAng.IsValid = False
            _currentSelectedStep.OutputInputMappingPair(0).Value.Add(vAng)
            Dim iMag = New SignalSignatureViewModel("", _currentSelectedStep.CustPMUname)
            iMag.IsValid = False
            _currentSelectedStep.OutputInputMappingPair(0).Value.Add(iMag)
            Dim iAng = New SignalSignatureViewModel("", _currentSelectedStep.CustPMUname)
            iAng.IsValid = False
            _currentSelectedStep.OutputInputMappingPair(0).Value.Add(iAng)
            _disableEnableAllButMagnitudeAngleSignalsInDataConfig(False)
        End Sub

#End Region

#Region "Step Manipulations: Delete, Select or DeSelect"
        Private _currentSelectedStep As Object
        Public Property CurrentSelectedStep As Object
            Get
                Return _currentSelectedStep
            End Get
            Set(ByVal value As Object)
                _currentSelectedStep = value
                OnPropertyChanged()
            End Set
        End Property

        Private _dataConfigStepSelected As ICommand
        Public Property DataConfigStepSelected As ICommand
            Get
                Return _dataConfigStepSelected
            End Get
            Set(ByVal value As ICommand)
                _dataConfigStepSelected = value
            End Set
        End Property
        Private Sub _stepSelectedToEdit(processStep As Object)


            If processStep IsNot CurrentSelectedStep AndAlso CurrentSelectedStep IsNot Nothing Then
                If Not CurrentSelectedStep.CheckStepIsComplete() Then
                    'here need to check if the currentSelectedStep is complete, if not, cannot switch
                    Forms.MessageBox.Show("Missing field(s) in this step, please double check!", "Error!", MessageBoxButtons.OK)
                    Exit Sub
                End If
                'here do all the stuff that is needed such as sort signals to make sure the step is set up.

                CurrentSelectedStep.ThisStepOutputsAsSignalHierachyByPMU.SignalList = _signalMgr.SortSignalByPMU(_currentSelectedStep.OutputChannels)
                If TypeOf CurrentSelectedStep Is Customization Then
                    CurrentSelectedStep.ThisStepInputsAsSignalHerachyByType.SignalList = _signalMgr.SortSignalByType(_currentSelectedStep.InputChannels)
                End If
            End If




            ' if processStep is already selected, then the selection is not changed, nothing needs to be done.
            ' however, if processStep is not selected, which means a new selection, we need to find the old selection, unselect it and all it's input signal
            If Not processStep.IsStepSelected Then
                Try
                    Dim lastNumberOfSteps = processStep.StepCounter
                    Dim stepsInputAsSignalHierachy As New ObservableCollection(Of SignalTypeHierachy)
                    Dim stepsOutputAsSignalHierachy As New ObservableCollection(Of SignalTypeHierachy)
                    Dim selectedFound = False
                    For Each stp In DataConfigure.CollectionOfSteps
                        If stp.IsStepSelected Then
                            stp.IsStepSelected = False
                            For Each signal In stp.InputChannels
                                signal.IsChecked = False
                            Next
                            'If TypeOf (stp) Is Customization Then
                            '    For Each signal In stp.OutputChannels
                            '        signal.IsChecked = False
                            '    Next
                            'End If
                            'Exit For
                            selectedFound = True
                        End If
                        If stp.StepCounter < lastNumberOfSteps Then
                            If TypeOf (stp) Is Customization Then
                                'stp.ThisStepInputsAsSignalHerachyByType.SignalList = _signalMgr.SortSignalByType(stp.InputChannels)
                                stepsInputAsSignalHierachy.Add(stp.ThisStepInputsAsSignalHerachyByType)
                            End If
                            'stp.ThisStepOutputsAsSignalHierachyByPMU.SignalList = _signalMgr.SortSignalByPMU(stp.OutputChannels)
                            stepsOutputAsSignalHierachy.Add(stp.ThisStepOutputsAsSignalHierachyByPMU)
                        End If
                        If stp.StepCounter >= lastNumberOfSteps AndAlso selectedFound Then
                            Exit For
                        End If
                    Next
                    '_signalMgr.DetermineFileDirCheckableStatus()
                    '_determineSamplingRateCheckableStatus()
                    _signalMgr.DetermineSamplingRateCheckableStatus(Nothing, _currentTabIndex, -1)
                    processStep.IsStepSelected = True

                    For Each signal In processStep.InputChannels
                        signal.IsChecked = True
                    Next

                    If processStep.Name = "Scalar Repetition" Then
                        SignalSelectionTreeViewVisibility = "Collapsed"
                    Else
                        SignalSelectionTreeViewVisibility = "Visible"
                    End If

                    'If CurrentSelectedStep.Name = "Power Calculation" AndAlso CurrentSelectedStep.OutputInputMappingPair(0).Value.Count = 2 Then
                    '    _disableEnableAllButPhasorSignals(True)
                    'End If
                    If CurrentSelectedStep IsNot Nothing Then
                        If CurrentSelectedStep.Name = "Phasor Creation" Then
                            _disableEnableAllButMagnitudeAngleSignalsInDataConfig(True)
                        ElseIf CurrentSelectedStep.Name = "Power Calculation" Then
                            If CurrentSelectedStep.OutputInputMappingPair.Count > 0 Then
                                Dim situation = CurrentSelectedStep.OutputInputMappingPair(0).Value.Count
                                If situation = 4 Then
                                    _disableEnableAllButMagnitudeAngleSignalsInDataConfig(True)
                                ElseIf situation = 2 Then
                                    _disableEnableAllButPhasorSignalsInDataConfig(True)
                                End If
                            End If
                        ElseIf CurrentSelectedStep.Name = "Metric Prefix" Then
                            _enableDisableAllButAngleDigitalScalarOtherSignalsInDataConfig(True)
                            'ElseIf CurrentSelectedStep.Name = "Angle Conversion" Or CurrentSelectedStep.Name = "Graph Eigenvalue" Then
                        ElseIf CurrentSelectedStep.Name = "Angle Conversion" Then
                            _disableEnableAllButAngleSignalsInDataConfig(True)
                        End If
                    End If

                    _signalMgr.GroupedSignalByDataConfigStepsInput = stepsInputAsSignalHierachy
                    _signalMgr.GroupedSignalByDataConfigStepsOutput = stepsOutputAsSignalHierachy
                    _signalMgr.DataConfigDetermineAllParentNodeStatus()
                    '_signalMgr.DetermineFileDirCheckableStatus()
                    '_determineSamplingRateCheckableStatus()

                    If processStep.Name = "Phasor Creation" Then
                        '_disableEnableGroupForPhasorCreationCustomization(GroupedSignalsByType, False)
                        '_disableEnableGroupForPhasorCreationCustomization(GroupedSignalByStepsInput, False)
                        '_disableEnableGroupForPhasorCreationCustomization(GroupedSignalByStepsOutput, False)
                        _disableEnableAllButMagnitudeAngleSignalsInDataConfig(False)
                    ElseIf processStep.Name = "Power Calculation" Then
                        If processStep.OutputInputMappingPair.Count > 0 Then
                            Dim situation = processStep.OutputInputMappingPair(0).Value.Count
                            If situation = 4 Then
                                _disableEnableAllButMagnitudeAngleSignalsInDataConfig(False)
                            ElseIf situation = 2 Then
                                _disableEnableAllButPhasorSignalsInDataConfig(False)
                            End If
                        End If
                    ElseIf processStep.Name = "Metric Prefix" Then
                        _enableDisableAllButAngleDigitalScalarOtherSignalsInDataConfig(False)
                        'ElseIf processStep.Name = "Angle Conversion" Or processStep.Name = "Graph Eigenvalue" Then
                    ElseIf processStep.Name = "Angle Conversion" Then
                        _disableEnableAllButAngleSignalsInDataConfig(False)
                    End If
                    CurrentSelectedStep = processStep
                    _determineSamplingRateCheckableStatus()
                Catch ex As Exception
                    Forms.MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK)
                End Try
            End If
        End Sub

        ''' <summary>
        ''' disable all but magnitude of current or magnitude of voltage signals
        ''' </summary>
        ''' <param name="isEnabled"></param>
        Private Sub _disableEnableAllButMagnitudeSignalsInDataConfig(isEnabled As Boolean)
            For Each group In _signalMgr.GroupedRawSignalsByType
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        If subgroup.SignalSignature.TypeAbbreviation <> "I" AndAlso subgroup.SignalSignature.TypeAbbreviation <> "V" Then
                            subgroup.SignalSignature.IsEnabled = isEnabled
                        Else
                            For Each subsubgroup In subgroup.SignalList
                                If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse subsubgroup.SignalSignature.TypeAbbreviation.Substring(1) <> "M" Then
                                    subsubgroup.SignalSignature.IsEnabled = isEnabled
                                End If
                            Next
                        End If
                    Next
                Next
            Next
            For Each group In _signalMgr.GroupedRawSignalsByPMU
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        For Each subsubgroup In subgroup.SignalList
                            If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse subsubgroup.SignalSignature.TypeAbbreviation.Length <> 3 OrElse subsubgroup.SignalSignature.TypeAbbreviation.Substring(1, 1) <> "M" Then
                                subsubgroup.SignalSignature.IsEnabled = isEnabled
                            End If
                        Next
                    Next
                Next
            Next

            'If CurrentTabIndex = 1 Then
            For Each group In _signalMgr.GroupedSignalByDataConfigStepsInput
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        If subgroup.SignalSignature.TypeAbbreviation <> "I" AndAlso subgroup.SignalSignature.TypeAbbreviation <> "V" Then
                            subgroup.SignalSignature.IsEnabled = isEnabled
                        Else
                            For Each subsubgroup In subgroup.SignalList
                                If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse subsubgroup.SignalSignature.TypeAbbreviation.Substring(1) <> "M" Then
                                    subsubgroup.SignalSignature.IsEnabled = isEnabled
                                End If
                            Next
                        End If
                    Next
                Next
            Next
            For Each group In _signalMgr.GroupedSignalByDataConfigStepsOutput
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        For Each subsubgroup In subgroup.SignalList
                            If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse subsubgroup.SignalSignature.TypeAbbreviation.Length <> 3 OrElse subsubgroup.SignalSignature.TypeAbbreviation.Substring(1, 1) <> "M" Then
                                subsubgroup.SignalSignature.IsEnabled = isEnabled
                            End If
                        Next
                    Next
                Next
            Next
            'ElseIf CurrentTabIndex = 3 Then
            '    For Each group In GroupedSignalByPostProcessConfigStepsInput
            '        For Each subgroup In group.SignalList
            '            If subgroup.SignalSignature.TypeAbbreviation <> "I" AndAlso subgroup.SignalSignature.TypeAbbreviation <> "V" Then
            '                subgroup.SignalSignature.IsEnabled = isEnabled
            '            Else
            '                For Each subsubgroup In subgroup.SignalList
            '                    If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse subsubgroup.SignalSignature.TypeAbbreviation.Substring(1) <> "M" Then
            '                        subsubgroup.SignalSignature.IsEnabled = isEnabled
            '                    End If
            '                Next
            '            End If
            '        Next
            '    Next
            '    For Each group In GroupedSignalByPostProcessConfigStepsOutput
            '        For Each subgroup In group.SignalList
            '            For Each subsubgroup In subgroup.SignalList
            '                If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse subsubgroup.SignalSignature.TypeAbbreviation.Length <> 3 OrElse subsubgroup.SignalSignature.TypeAbbreviation.Substring(1, 1) <> "M" Then
            '                    subsubgroup.SignalSignature.IsEnabled = isEnabled
            '                End If
            '            Next
            '        Next
            '    Next


            'End If
        End Sub
        Private Sub _disableEnableAllButMagnitudeAngleSignalsInDataConfig(isEnabled As Boolean)
            For Each group In _signalMgr.GroupedRawSignalsByType
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        If subgroup.SignalSignature.TypeAbbreviation <> "I" AndAlso subgroup.SignalSignature.TypeAbbreviation <> "V" Then
                            subgroup.SignalSignature.IsEnabled = isEnabled
                        Else
                            For Each subsubgroup In subgroup.SignalList
                                If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse (subsubgroup.SignalSignature.TypeAbbreviation.Substring(1) <> "M" AndAlso subsubgroup.SignalSignature.TypeAbbreviation.Substring(1) <> "A") Then
                                    subsubgroup.SignalSignature.IsEnabled = isEnabled
                                End If
                            Next
                        End If
                    Next
                Next
            Next
            For Each group In _signalMgr.GroupedRawSignalsByPMU
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        For Each subsubgroup In subgroup.SignalList
                            If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse subsubgroup.SignalSignature.TypeAbbreviation.Length <> 3 OrElse (subsubgroup.SignalSignature.TypeAbbreviation.Substring(1, 1) <> "M" AndAlso subsubgroup.SignalSignature.TypeAbbreviation.Substring(1, 1) <> "A") Then
                                subsubgroup.SignalSignature.IsEnabled = isEnabled
                            End If
                        Next
                    Next
                Next
            Next
            For Each group In _signalMgr.GroupedSignalByDataConfigStepsInput
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        If subgroup.SignalSignature.TypeAbbreviation <> "I" AndAlso subgroup.SignalSignature.TypeAbbreviation <> "V" Then
                            subgroup.SignalSignature.IsEnabled = isEnabled
                        Else
                            For Each subsubgroup In subgroup.SignalList
                                If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse (subsubgroup.SignalSignature.TypeAbbreviation.Substring(1) <> "M" AndAlso subsubgroup.SignalSignature.TypeAbbreviation.Substring(1) <> "A") Then
                                    subsubgroup.SignalSignature.IsEnabled = isEnabled
                                End If
                            Next
                        End If
                    Next
                Next
            Next
            For Each group In _signalMgr.GroupedSignalByDataConfigStepsOutput
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        For Each subsubgroup In subgroup.SignalList
                            If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse subsubgroup.SignalSignature.TypeAbbreviation.Length <> 3 OrElse (subsubgroup.SignalSignature.TypeAbbreviation.Substring(1, 1) <> "M" AndAlso subsubgroup.SignalSignature.TypeAbbreviation.Substring(1, 1) <> "A") Then
                                subsubgroup.SignalSignature.IsEnabled = isEnabled
                            End If
                        Next
                    Next
                Next
            Next
        End Sub
        Private Sub _disableEnableAllButPhasorSignalsInDataConfig(isEnabled As Boolean)
            For Each group In _signalMgr.GroupedRawSignalsByType
                group.SignalSignature.IsEnabled = isEnabled
            Next
            For Each group In _signalMgr.GroupedRawSignalsByPMU
                group.SignalSignature.IsEnabled = isEnabled
            Next
            For Each group In _signalMgr.GroupedSignalByDataConfigStepsInput
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        If subgroup.SignalSignature.TypeAbbreviation <> "I" AndAlso subgroup.SignalSignature.TypeAbbreviation <> "V" Then
                            subgroup.SignalSignature.IsEnabled = isEnabled
                        Else
                            For Each subsubgroup In subgroup.SignalList
                                If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse subsubgroup.SignalSignature.TypeAbbreviation.Substring(1) <> "P" Then
                                    subsubgroup.SignalSignature.IsEnabled = isEnabled
                                End If
                            Next
                        End If
                    Next
                Next
            Next
            For Each group In _signalMgr.GroupedSignalByDataConfigStepsOutput
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        For Each subsubgroup In subgroup.SignalList
                            If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse subsubgroup.SignalSignature.TypeAbbreviation.Length <> 3 OrElse subsubgroup.SignalSignature.TypeAbbreviation.Substring(1, 1) <> "P" Then
                                subsubgroup.SignalSignature.IsEnabled = isEnabled
                            End If
                        Next
                    Next
                Next
            Next
        End Sub
        Private Sub _disableEnableAllButAngleSignalsInDataConfig(isEnabled As Boolean)
            For Each group In _signalMgr.GroupedRawSignalsByType
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        If subgroup.SignalSignature.TypeAbbreviation <> "I" AndAlso subgroup.SignalSignature.TypeAbbreviation <> "V" Then
                            subgroup.SignalSignature.IsEnabled = isEnabled
                        Else
                            For Each subsubgroup In subgroup.SignalList
                                If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse subsubgroup.SignalSignature.TypeAbbreviation.Substring(1) <> "A" Then
                                    subsubgroup.SignalSignature.IsEnabled = isEnabled
                                End If
                            Next
                        End If
                    Next
                Next
            Next
            For Each group In _signalMgr.GroupedRawSignalsByPMU
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        For Each subsubgroup In subgroup.SignalList
                            If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse subsubgroup.SignalSignature.TypeAbbreviation.Length <> 3 OrElse subsubgroup.SignalSignature.TypeAbbreviation.Substring(1, 1) <> "A" Then
                                subsubgroup.SignalSignature.IsEnabled = isEnabled
                            End If
                        Next
                    Next
                Next
            Next
            For Each group In _signalMgr.GroupedSignalByDataConfigStepsInput
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        If subgroup.SignalSignature.TypeAbbreviation <> "I" AndAlso subgroup.SignalSignature.TypeAbbreviation <> "V" Then
                            subgroup.SignalSignature.IsEnabled = isEnabled
                        Else
                            For Each subsubgroup In subgroup.SignalList
                                If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse subsubgroup.SignalSignature.TypeAbbreviation.Substring(1) <> "A" Then
                                    subsubgroup.SignalSignature.IsEnabled = isEnabled
                                End If
                            Next
                        End If
                    Next
                Next
            Next
            For Each group In _signalMgr.GroupedSignalByDataConfigStepsOutput
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        For Each subsubgroup In subgroup.SignalList
                            If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse subsubgroup.SignalSignature.TypeAbbreviation.Length <> 3 OrElse subsubgroup.SignalSignature.TypeAbbreviation.Substring(1, 1) <> "A" Then
                                subsubgroup.SignalSignature.IsEnabled = isEnabled
                            End If
                        Next
                    Next
                Next
            Next
        End Sub
        Private Sub _enableDisableAllButAngleDigitalScalarOtherSignalsInDataConfig(isEnabled As Boolean)
            For Each group In _signalMgr.GroupedRawSignalsByType
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        If subgroup.SignalSignature.TypeAbbreviation = "OTHER" OrElse subgroup.SignalSignature.TypeAbbreviation = "SC" OrElse subgroup.SignalSignature.TypeAbbreviation = "D" Then
                            subgroup.SignalSignature.IsEnabled = isEnabled
                        Else
                            For Each subsubgroup In subgroup.SignalList
                                If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse (subsubgroup.SignalSignature.TypeAbbreviation.Length = 2 AndAlso subsubgroup.SignalSignature.TypeAbbreviation.Substring(1) = "A") Then
                                    subsubgroup.SignalSignature.IsEnabled = isEnabled
                                End If
                            Next
                        End If
                    Next
                Next
            Next
            For Each group In _signalMgr.GroupedRawSignalsByPMU
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        For Each subsubgroup In subgroup.SignalList
                            If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse subsubgroup.SignalSignature.TypeAbbreviation = "OTHER" OrElse subsubgroup.SignalSignature.TypeAbbreviation = "SC" OrElse subsubgroup.SignalSignature.TypeAbbreviation = "D" OrElse (subsubgroup.SignalSignature.TypeAbbreviation.Length = 3 AndAlso subsubgroup.SignalSignature.TypeAbbreviation.Substring(1, 1) = "A") Then
                                subsubgroup.SignalSignature.IsEnabled = isEnabled
                            End If
                        Next
                    Next
                Next
            Next
            For Each group In _signalMgr.GroupedSignalByDataConfigStepsInput
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        If subgroup.SignalSignature.TypeAbbreviation = "OTHER" OrElse subgroup.SignalSignature.TypeAbbreviation = "SC" OrElse subgroup.SignalSignature.TypeAbbreviation = "D" Then
                            subgroup.SignalSignature.IsEnabled = isEnabled
                        Else
                            For Each subsubgroup In subgroup.SignalList
                                If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse (subsubgroup.SignalSignature.TypeAbbreviation.Length = 2 AndAlso subsubgroup.SignalSignature.TypeAbbreviation.Substring(1) = "A") Then
                                    subsubgroup.SignalSignature.IsEnabled = isEnabled
                                End If
                            Next
                        End If
                    Next
                Next
            Next
            For Each group In _signalMgr.GroupedSignalByDataConfigStepsOutput
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        For Each subsubgroup In subgroup.SignalList
                            If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse subsubgroup.SignalSignature.TypeAbbreviation = "OTHER" OrElse subsubgroup.SignalSignature.TypeAbbreviation = "SC" OrElse subsubgroup.SignalSignature.TypeAbbreviation = "D" OrElse (subsubgroup.SignalSignature.TypeAbbreviation.Length = 3 AndAlso subsubgroup.SignalSignature.TypeAbbreviation.Substring(1, 1) = "A") Then
                                subsubgroup.SignalSignature.IsEnabled = isEnabled
                            End If
                        Next
                    Next
                Next
            Next
        End Sub
        'this function disables/enables or current and voltage signals, including phasor signals
        Private Sub _disableEnableAllButCurrentVoltageSignalsInDataConfig(isEnabled As Boolean)
            For Each group In _signalMgr.GroupedRawSignalsByType
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        If subgroup.SignalSignature.TypeAbbreviation <> "I" AndAlso subgroup.SignalSignature.TypeAbbreviation <> "V" Then
                            subgroup.SignalSignature.IsEnabled = isEnabled
                        End If
                    Next
                Next
            Next
            For Each group In _signalMgr.GroupedRawSignalsByPMU
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        For Each subsubgroup In subgroup.SignalList
                            If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse (subsubgroup.SignalSignature.TypeAbbreviation.Substring(0, 1) <> "V" AndAlso subsubgroup.SignalSignature.TypeAbbreviation.Substring(0, 1) <> "I") Then
                                subsubgroup.SignalSignature.IsEnabled = isEnabled
                            End If
                        Next
                    Next
                Next
            Next
            For Each group In _signalMgr.GroupedSignalByDataConfigStepsInput
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        If subgroup.SignalSignature.TypeAbbreviation <> "I" AndAlso subgroup.SignalSignature.TypeAbbreviation <> "V" Then
                            subgroup.SignalSignature.IsEnabled = isEnabled
                        End If
                    Next
                Next
            Next
            For Each group In _signalMgr.GroupedSignalByDataConfigStepsOutput
                For Each subgroupBySamplingRate In group.SignalList
                    For Each subgroup In subgroupBySamplingRate.SignalList
                        For Each subsubgroup In subgroup.SignalList
                            If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse (subsubgroup.SignalSignature.TypeAbbreviation.Substring(0, 1) <> "V" AndAlso subsubgroup.SignalSignature.TypeAbbreviation.Substring(0, 1) <> "I") Then
                                subsubgroup.SignalSignature.IsEnabled = isEnabled
                            End If
                        Next
                    Next
                Next
            Next
            'For Each group In GroupedSignalByProcessConfigStepsInput
            '    For Each subgroup In group.SignalList
            '        If subgroup.SignalSignature.TypeAbbreviation <> "I" AndAlso subgroup.SignalSignature.TypeAbbreviation <> "V" AndAlso subgroup.SignalSignature.TypeAbbreviation <> "F" AndAlso subgroup.SignalSignature.TypeAbbreviation <> "R" Then
            '            subgroup.SignalSignature.IsEnabled = isEnabled
            '        Else
            '            For Each subsubgroup In subgroup.SignalList
            '                If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse (subsubgroup.SignalSignature.TypeAbbreviation.Length = 2 AndAlso subsubgroup.SignalSignature.TypeAbbreviation.Substring(1) <> "M") Then
            '                    subsubgroup.SignalSignature.IsEnabled = isEnabled
            '                End If
            '            Next
            '        End If
            '    Next
            'Next
            'For Each group In GroupedSignalByProcessConfigStepsOutput
            '    For Each subgroup In group.SignalList
            '        For Each subsubgroup In subgroup.SignalList
            '            If String.IsNullOrEmpty(subsubgroup.SignalSignature.TypeAbbreviation) OrElse (subsubgroup.SignalSignature.TypeAbbreviation.Length <> 3 AndAlso subsubgroup.SignalSignature.TypeAbbreviation <> "F" AndAlso subsubgroup.SignalSignature.TypeAbbreviation <> "R") OrElse (subsubgroup.SignalSignature.TypeAbbreviation.Length = 3 AndAlso subsubgroup.SignalSignature.TypeAbbreviation.Substring(1, 1) <> "M") Then
            '                subsubgroup.SignalSignature.IsEnabled = isEnabled
            '            End If
            '        Next
            '    Next
            'Next
        End Sub

        Private _signalSelectionTreeViewVisibility As String
        Public Property SignalSelectionTreeViewVisibility As String
            Get
                Return _signalSelectionTreeViewVisibility
            End Get
            Set(ByVal value As String)
                _signalSelectionTreeViewVisibility = value
                OnPropertyChanged()
            End Set
        End Property

        Private _dataConfigStepDeSelected As ICommand
        Public Property DataConfigStepDeSelected As ICommand
            Get
                Return _dataConfigStepDeSelected
            End Get
            Set(ByVal value As ICommand)
                _dataConfigStepDeSelected = value
            End Set
        End Property
        ''' <summary>
        ''' When user click outside the step list and none of the steps should be selected, then we need to uncheck all checkboxes
        ''' </summary>
        Public Sub DeSelectAllDataConfigSteps()
            If _currentSelectedStep IsNot Nothing Then

                If Not _currentSelectedStep.CheckStepIsComplete() Then
                    'here need to check if the currentSelectedStep is complete, if not, cannot switch
                    Forms.MessageBox.Show("Missing field(s) in step : " + CurrentSelectedStep.Name + " , please double check!", "Error!", MessageBoxButtons.OK)
                    Exit Sub
                End If
                CurrentSelectedStep.ThisStepOutputsAsSignalHierachyByPMU.SignalList = _signalMgr.SortSignalByPMU(_currentSelectedStep.OutputChannels)
                If TypeOf CurrentSelectedStep Is Customization Then
                    CurrentSelectedStep.ThisStepInputsAsSignalHerachyByType.SignalList = _signalMgr.SortSignalByType(_currentSelectedStep.InputChannels)
                End If

                For Each signal In _currentSelectedStep.InputChannels
                    signal.IsChecked = False
                Next
                'If _currentSelectedStep.OutputChannels IsNot Nothing Then
                '    For Each signal In _currentSelectedStep.OutputChannels
                '        signal.IsChecked = False
                '    Next
                'End If
                Dim stepsInputAsSignalHierachy As New ObservableCollection(Of SignalTypeHierachy)
                Dim stepsOutputAsSignalHierachy As New ObservableCollection(Of SignalTypeHierachy)

                'here do all the stuff that is needed such as sort signals to make sure the step is set up.
                For Each stp In DataConfigure.CollectionOfSteps
                    If TypeOf (stp) Is Customization Then
                        'stp.ThisStepInputsAsSignalHerachyByType.SignalList = _signalMgr.SortSignalByType(stp.InputChannels)
                        stepsInputAsSignalHierachy.Add(stp.ThisStepInputsAsSignalHerachyByType)
                    End If
                    'stp.ThisStepOutputsAsSignalHierachyByPMU.SignalList = _signalMgr.SortSignalByPMU(stp.OutputChannels)
                    stepsOutputAsSignalHierachy.Add(stp.ThisStepOutputsAsSignalHierachyByPMU)
                    'If TypeOf (stp) Is Customization Then
                    '    stepsInputAsSignalHierachy.Add(stp.ThisStepInputsAsSignalHerachyByType)
                    'End If
                    'If TypeOf (stp) Is MetricPrefixCust Then
                    '    stp.ThisStepOutputsAsSignalHierachyByPMU.SignalList = SortSignalByPMU(stp.OutputChannels)
                    'End If
                    'stepsOutputAsSignalHierachy.Add(stp.ThisStepOutputsAsSignalHierachyByPMU)
                Next
                _signalMgr.GroupedSignalByDataConfigStepsInput = stepsInputAsSignalHierachy
                _signalMgr.GroupedSignalByDataConfigStepsOutput = stepsOutputAsSignalHierachy
                If CurrentSelectedStep.Name = "Phasor Creation" Then
                    _disableEnableAllButMagnitudeAngleSignalsInDataConfig(True)
                ElseIf CurrentSelectedStep.Name = "Power Calculation" Then
                    If CurrentSelectedStep.OutputInputMappingPair.Count > 0 Then
                        Dim situation = CurrentSelectedStep.OutputInputMappingPair(0).Value.Count
                        If situation = 4 Then
                            _disableEnableAllButMagnitudeAngleSignalsInDataConfig(True)
                        Else
                            _disableEnableAllButPhasorSignalsInDataConfig(True)
                        End If
                    End If
                ElseIf CurrentSelectedStep.Name = "Metric Prefix" Then
                    _enableDisableAllButAngleDigitalScalarOtherSignalsInDataConfig(True)
                    'ElseIf CurrentSelectedStep.Name = "Angle Conversion" Or CurrentSelectedStep.Name = "Graph Eigenvalue" Then
                ElseIf CurrentSelectedStep.Name = "Angle Conversion" Then
                    _disableEnableAllButAngleSignalsInDataConfig(True)
                End If
                _changeCheckStatusAllParentsOfGroupedSignal(_signalMgr.GroupedSignalByDataConfigStepsInput, False)
                _changeCheckStatusAllParentsOfGroupedSignal(_signalMgr.GroupedSignalByDataConfigStepsOutput, False)
                _changeCheckStatusAllParentsOfGroupedSignal(_signalMgr.GroupedRawSignalsByPMU, False)
                _changeCheckStatusAllParentsOfGroupedSignal(_signalMgr.GroupedRawSignalsByType, False)
                _currentSelectedStep.IsStepSelected = False
                CurrentSelectedStep = Nothing

                '_signalMgr.DataConfigDetermineAllParentNodeStatus()
                _signalMgr.DetermineAllParentNodeStatus()

                '_signalMgr.DetermineFileDirCheckableStatus()
                _determineSamplingRateCheckableStatus()
                SignalSelectionTreeViewVisibility = "Visible"
            End If
        End Sub
        Private Sub _changeCheckStatusAllParentsOfGroupedSignal(groups As ObservableCollection(Of SignalTypeHierachy), checkStatus As Boolean)
            For Each node In groups
                node.SignalSignature.IsChecked = checkStatus
                For Each child In node.SignalList
                    child.SignalSignature.IsChecked = checkStatus
                    For Each grandChild In child.SignalList
                        grandChild.SignalSignature.IsChecked = checkStatus
                        For Each greatGrandChild In grandChild.SignalList
                            greatGrandChild.SignalSignature.IsChecked = checkStatus
                            For Each greatgreatGrandChild In greatGrandChild.SignalList
                                greatgreatGrandChild.SignalSignature.IsChecked = checkStatus
                            Next
                        Next
                    Next
                Next
            Next
        End Sub

        Private _deleteDataConfigStep As ICommand
        Public Property DeleteDataConfigStep As ICommand
            Get
                Return _deleteDataConfigStep
            End Get
            Set(ByVal value As ICommand)
                _deleteDataConfigStep = value
            End Set
        End Property
        Private Sub _deleteADataConfigStep(obj As Object)
            Dim result = Forms.MessageBox.Show("Delete step " & obj.StepCounter.ToString & " in Data Configuration: " & obj.Name & " ?", "Warning!", MessageBoxButtons.OKCancel)
            If result = DialogResult.OK Then
                Try
                    DataConfigure.CollectionOfSteps.Remove(obj)
                    Dim steps = New ObservableCollection(Of Object)(DataConfigure.CollectionOfSteps)
                    For Each stp In steps
                        If stp.StepCounter > obj.StepCounter Then
                            stp.StepCounter -= 1
                            If TypeOf (stp) Is Customization Then
                                stp.ThisStepInputsAsSignalHerachyByType.SignalSignature.SignalName = "Step " & stp.StepCounter.ToString & "-" & stp.Name
                            End If
                            stp.ThisStepOutputsAsSignalHierachyByPMU.SignalSignature.SignalName = "Step " & stp.StepCounter.ToString & "-" & stp.Name
                        End If
                    Next
                    If TypeOf obj Is DQFilter Then
                        For Each signal In obj.OutputChannels
                            signal.PassedThroughDQFilter = signal.PassedThroughDQFilter - 1
                        Next
                    End If
                    'If obj Is CurrentSelectedStep Then
                    '    CurrentSelectedStep = Nothing
                    'Else
                    DeSelectAllDataConfigSteps()
                    'End If
                    'CurrentSelectedStep = Nothing
                    _addLog("Step " & obj.StepCounter & ", " & obj.Name & " is deleted!")
                    DataConfigure.CollectionOfSteps = steps
                    'SignalSelectionTreeViewVisibility = "Visible"
                Catch ex As Exception
                    Forms.MessageBox.Show("Error deleting step " & obj.StepCounter.ToString & " in Data Configuration, " & obj.Name & ex.Message, "Error!", MessageBoxButtons.OK)
                End Try
            Else
                Exit Sub
            End If
        End Sub

#End Region

        Private _selectSignalMethods As List(Of String)
        Public Property SelectSignalMethods As List(Of String)
            Get
                Return _selectSignalMethods
            End Get
            Set(ByVal value As List(Of String))
                _selectSignalMethods = value
                OnPropertyChanged()
            End Set
        End Property

        Private _selectedSelectionMethod As String
        Public Property SelectedSelectionMethod As String
            Get
                Return _selectedSelectionMethod
            End Get
            Set(ByVal value As String)
                _selectedSelectionMethod = value
                OnPropertyChanged()
            End Set
        End Property

        'Private _allPMUs As ObservableCollection(Of String)
        'Public ReadOnly Property AllPMUs As ObservableCollection(Of PMUWithSamplingRate)
        'Get
        '        'Dim allPMU = GroupedRawSignalsByPMU.SelectMany(Function(x) x.SignalList).Distinct.Select(Function(y) y.SignalSignature.PMUName & "-" & y.SignalSignature.SamplingRate.ToString).ToList()
        '        'allPMU.AddRange(AllDataConfigOutputGroupedByPMU.SelectMany(Function(x) x.SignalList).Distinct.Select(Function(y) y.SignalSignature.PMUName & "-" & y.SignalSignature.SamplingRate.ToString).ToList())
        '        'allPMU.AddRange(AllProcessConfigOutputGroupedByPMU.SelectMany(Function(x) x.SignalList).Distinct.Select(Function(y) y.SignalSignature.PMUName & "-" & y.SignalSignature.SamplingRate.ToString).ToList())
        '        'allPMU.AddRange(AllPostProcessOutputGroupedByPMU.SelectMany(Function(x) x.SignalList).Distinct.Select(Function(y) y.SignalSignature.PMUName & "-" & y.SignalSignature.SamplingRate.ToString).ToList())

        '        Dim allPMU = _signalMgr.GroupedRawSignalsByPMU.SelectMany(Function(x) x.SignalList).Distinct.SelectMany(Function(r) r.SignalList).Distinct.Select(Function(y) New PMUWithSamplingRate(y.SignalSignature.PMUName, y.SignalSignature.SamplingRate)).ToList()
        '        allPMU.AddRange(_signalMgr.AllDataConfigOutputGroupedByPMU.SelectMany(Function(x) x.SignalList).Distinct.SelectMany(Function(r) r.SignalList).Distinct.Select(Function(y) New PMUWithSamplingRate(y.SignalSignature.PMUName, y.SignalSignature.SamplingRate)).ToList())
        '        allPMU.AddRange(_signalMgr.AllProcessConfigOutputGroupedByPMU.SelectMany(Function(x) x.SignalList).Distinct.SelectMany(Function(r) r.SignalList).Distinct.Select(Function(y) New PMUWithSamplingRate(y.SignalSignature.PMUName, y.SignalSignature.SamplingRate)).ToList())
        '        allPMU.AddRange(_signalMgr.AllPostProcessOutputGroupedByPMU.SelectMany(Function(x) x.SignalList).Distinct.SelectMany(Function(r) r.SignalList).Distinct.Select(Function(y) New PMUWithSamplingRate(y.SignalSignature.PMUName, y.SignalSignature.SamplingRate)).ToList())



        '        Return New ObservableCollection(Of PMUWithSamplingRate)(allPMU.Distinct)

        '        'Return _allPMUs
        '    End Get
        '    'Set(ByVal value As ObservableCollection(Of String))
        '    '    _allPMUs = value
        '    '    OnPropertyChanged()
        '    'End Set
        'End Property

        Private _timezoneList As ReadOnlyCollection(Of TimeZoneInfo)
        Public ReadOnly Property TimeZoneList As ReadOnlyCollection(Of TimeZoneInfo)
            Get
                Return _timezoneList
            End Get
        End Property

        Private _logs As ObservableCollection(Of String)
        Public Property Logs As ObservableCollection(Of String)
            Get
                Return _logs
            End Get
            Set(ByVal value As ObservableCollection(Of String))
                _logs = value
                OnPropertyChanged()
            End Set
        End Property
        Private Sub _addLog(log As String)
            Dim timeDate = DateTime.Now
            Logs.Add(timeDate.ToString & ": " & log)
        End Sub

        Private _addFileSource As ICommand
        Public Property AddFileSource As ICommand
            Get
                Return _addFileSource
            End Get
            Set(ByVal value As ICommand)
                _addFileSource = value
            End Set
        End Property
        Private Sub _addAFileSource(obj As Object)
            Dim newFileSource = New InputFileInfoViewModel()
            newFileSource.IsExpanded = True
            'SignalMgr.FileInfo.Add(newFileSource)
            DataConfigure.ReaderProperty.InputFileInfos.Add(newFileSource)
            SignalMgr.FileInfo = DataConfigure.ReaderProperty.InputFileInfos
        End Sub
        Private _deleteThisFileSource As ICommand
        Public Property DeleteThisFileSource As ICommand
            Get
                Return _deleteThisFileSource
            End Get
            Set(ByVal value As ICommand)
                _deleteThisFileSource = value
            End Set
        End Property

        Private Sub _deleteAFileSource(obj As InputFileInfoViewModel)
            Dim result = Forms.MessageBox.Show("Delete this file source: " & obj.Mnemonic & " ?", "Warning!", MessageBoxButtons.OKCancel)
            If result = DialogResult.OK Then
                'if the file info to be deleted exist in the signal manager, it is a good file info
                'if it does not exist, it is a bad file info that only exist in the reader property, then look through reader property to deleted it.
                'Dim fileDeleted = False
                For Each source In DataConfigure.ReaderProperty.InputFileInfos
                    If obj Is source Then
                        'fileDeleted = True
                        For Each group In _signalMgr.GroupedRawSignalsByType
                            Dim frags = group.SignalSignature.SignalName.Split(",")
                            Dim dir = frags(0).Trim
                            Dim mnic = frags(1).Trim
                            If dir = obj.FileDirectory AndAlso mnic = obj.Mnemonic Then
                                _signalMgr.GroupedRawSignalsByType.Remove(group)
                                Exit For
                            End If
                        Next

                        For Each group In _signalMgr.ReGroupedRawSignalsByType
                            Dim frags = group.SignalSignature.SignalName.Split(",")
                            Dim dir = frags(0).Trim
                            Dim mnic = frags(1).Trim
                            If dir = obj.FileDirectory AndAlso mnic = obj.Mnemonic Then
                                _signalMgr.ReGroupedRawSignalsByType.Remove(group)
                                Exit For
                            End If
                        Next
                        For Each group In _signalMgr.GroupedRawSignalsByPMU
                            Dim frags = group.SignalSignature.SignalName.Split(",")
                            Dim dir = frags(0).Trim
                            Dim mnic = frags(1).Trim
                            If dir = obj.FileDirectory AndAlso mnic = obj.Mnemonic Then
                                _signalMgr.GroupedRawSignalsByPMU.Remove(group)
                                Exit For
                            End If
                        Next
                        DataConfigure.ReaderProperty.InputFileInfos.Remove(obj)
                        'SignalMgr.FileInfo = DataConfigure.ReaderProperty.InputFileInfos
                        Exit For
                    End If
                Next
                'If Not fileDeleted Then
                '    For Each source In DataConfigure.ReaderProperty.InputFileInfos
                '        If obj Is source Then
                '            DataConfigure.ReaderProperty.InputFileInfos.Remove(obj)
                '            Exit For
                '        End If
                '    Next
                'End If
                DataConfigure.ReaderProperty.CheckHasDBDataSource()
                'If obj.FileType = DataFileType.PI Then
                '    DataConfigure.ReaderProperty.CanChooseMode = True
                '    For Each info In DataConfigure.ReaderProperty.InputFileInfos
                '        If info.FileType = DataFileType.PI Then
                '            DataConfigure.ReaderProperty.CanChooseMode = False
                '            Exit For
                '        End If
                '    Next
                'End If
                'If _configData IsNot Nothing Then
                '    _readDataConfigStages(_configData)
                '    _readProcessConfig(_configData)
                '    _readPostProcessConfig(_configData)
                '    _readDetectorConfig(_configData)
                'End If
            Else
                Exit Sub
            End If
        End Sub

        'Private _walkerStageChanged As ICommand
        'Public Property WalkerStageChanged As ICommand
        '    Get
        '        Return _walkerStageChanged
        '    End Get
        '    Set(value As ICommand)
        '        _walkerStageChanged = value
        '    End Set
        'End Property


        'Private Sub _changeSignalSelectionDropDownChoice(index As Integer)
        '    Select Case index
        '        Case 0
        '            CurrentTabIndex = 0
        '        Case 1
        '            SelectSignalMethods = {"All Initial Input Channels by Signal Type",
        '                                   "All Initial Input Channels by PMU",
        '                                   "Input Channels by Step",
        '                                   "Output Channels by Step"}.ToList
        '            'SelectedSelectionMethod = "All Initial Input Channels by Signal Type"
        '            CurrentTabIndex = 1
        '        Case 2
        '            SelectSignalMethods = {"All Initial Input Channels by Signal Type",
        '                                   "All Initial Input Channels by PMU",
        '                                   "Output from SignalSelectionAndManipulation by Signal Type",
        '                                   "Output from SignalSelectionAndManipulation by PMU",
        '                                   "Input to MultiRate steps",
        '                                   "Output Channels by Step"}.ToList
        '            'SelectedSelectionMethod = "All Initial Input Channels by Signal Type"
        '            CurrentTabIndex = 2
        '        Case 3
        '            SelectSignalMethods = {"NOT Implemented Yet!",
        '                                    "All Initial Input Channels by Signal Type",
        '                                    "All Initial Input Channels by PMU",
        '                                    "Output from SignalSelectionAndManipulation by Signal Type",
        '                                    "Output from SignalSelectionAndManipulation by PMU",
        '                                    "Input to MultiRate steps",
        '                                    "Output Channels by Step"}.ToList
        '            'SelectedSelectionMethod = "NOT Implemented Yet!"
        '            CurrentTabIndex = 3
        '    End Select
        'End Sub
        Private _oldTabIndex As Integer
        Private _currentTabIndex As Integer
        Public Property CurrentTabIndex As Integer
            Get
                Return _currentTabIndex
            End Get
            Set(value As Integer)
                _currentTabIndex = value
                Try
                    If _oldTabIndex = 1 And _currentTabIndex <> 1 Then
                        '_groupAllDataConfigOutputSignal()
                        DeSelectAllDataConfigSteps()
                    End If
                    If _oldTabIndex = 2 And _currentTabIndex <> 2 Then
                        '_groupAllProcessConfigOutputSignal()
                        DeSelectAllProcessConfigSteps()
                    End If
                    If _oldTabIndex = 3 And _currentTabIndex <> 3 Then
                        '_groupAllPostProcessConfigOutputSignal()
                        DeSelectAllPostProcessConfigSteps()
                    End If
                    If _oldTabIndex = 4 And _currentTabIndex <> 4 Then
                        '_groupAllPostProcessConfigOutputSignal()
                        DeSelectAllDetectors()
                        _updateDEFAreas()
                    End If
                    If _oldTabIndex = 5 And _currentTabIndex <> 5 Then
                        '_groupAllPostProcessConfigOutputSignal()
                        DeSelectAllDetectors()
                    End If

                    If (_currentTabIndex < 2 AndAlso _oldTabIndex >= 2) OrElse (_currentTabIndex >= 2 AndAlso _oldTabIndex < 2) Then
                        ReverseSignalPassedThroughNameTypeUnit()
                        _reGroupRawSignalByType()
                    End If

                    If _currentTabIndex = 2 Then
                        Dim allDataConfigOutputSignals = _getAllDataConfigOutput()
                        _signalMgr.AllDataConfigOutputGroupedByType = _signalMgr.SortSignalByType(allDataConfigOutputSignals)
                        _signalMgr.AllDataConfigOutputGroupedByPMU = _signalMgr.SortSignalByPMU(allDataConfigOutputSignals)
                    ElseIf _currentTabIndex = 3 Then
                        Dim allDataConfigOutputSignals = _getAllDataConfigOutput()
                        _signalMgr.AllDataConfigOutputGroupedByType = _signalMgr.SortSignalByType(allDataConfigOutputSignals)
                        _signalMgr.AllDataConfigOutputGroupedByPMU = _signalMgr.SortSignalByPMU(allDataConfigOutputSignals)
                        Dim allProcessOutputSignals = _getAllprocessOutputSignals()
                        _signalMgr.AllProcessConfigOutputGroupedByPMU = _signalMgr.SortSignalByPMU(allProcessOutputSignals)
                        _signalMgr.AllProcessConfigOutputGroupedByType = _signalMgr.SortSignalByType(allProcessOutputSignals)
                        If _oldTabIndex = 2 Then
                            _reGroupRawSignalByType()
                        End If
                    ElseIf _currentTabIndex = 4 OrElse _currentTabIndex = 5 Then
                        Dim allDataConfigOutputSignals = _getAllDataConfigOutput()
                        _signalMgr.AllDataConfigOutputGroupedByType = _signalMgr.SortSignalByType(allDataConfigOutputSignals)
                        _signalMgr.AllDataConfigOutputGroupedByPMU = _signalMgr.SortSignalByPMU(allDataConfigOutputSignals)
                        Dim allProcessOutputSignals = _getAllprocessOutputSignals()
                        _signalMgr.AllProcessConfigOutputGroupedByPMU = _signalMgr.SortSignalByPMU(allProcessOutputSignals)
                        _signalMgr.AllProcessConfigOutputGroupedByType = _signalMgr.SortSignalByType(allProcessOutputSignals)
                        Dim allPostProcessOutputSignals = _getAllPostProcessOutput()
                        _signalMgr.AllPostProcessOutputGroupedByPMU = _signalMgr.SortSignalByPMU(allPostProcessOutputSignals)
                        _signalMgr.AllPostProcessOutputGroupedByType = _signalMgr.SortSignalByType(allPostProcessOutputSignals)
                        If _oldTabIndex = 2 Then
                            _reGroupRawSignalByType()
                        End If
                    End If

                    'tab index 6 is the coordinate setup setting. no signal selection needed, but need to get unique detector signals and DEF areas.
                    If _currentTabIndex = 6 Then
                        _signalMgr.DistinctMappingSignal()
                        'find all distinct areas used in DEF detecotr
                        '_updateDEFAreas()
                    End If
                Catch ex As Exception
                    _addLog(ex.Message)
                    Forms.MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK)
                End Try
                _oldTabIndex = _currentTabIndex
                If CurrentSelectedStep IsNot Nothing Then
                    CurrentSelectedStep.IsStepSelected = False
                    CurrentSelectedStep = Nothing
                End If
                OnPropertyChanged()
            End Set
        End Property

        Private Sub _updateDEFAreas()
            For Each dtr In DetectorConfigure.DetectorList
                If TypeOf (dtr) Is DEFDetectorViewModel Then
                    Dim detector As DEFDetectorViewModel = dtr
                    Dim addedNewArea = False
                    Dim newAreaList = New List(Of String)
                    For Each pth In detector.Paths
                        If String.IsNullOrEmpty(pth.FromArea) Then
                            Forms.MessageBox.Show("From Area in the path of Dissipation Energy Flow detector is required. If left empty, will result in MATLAB calculation error.", "Error!", MessageBoxButtons.OK)
                        Else
                            If Not detector.UniqueAreas.Contains(pth.FromArea) AndAlso Not addedNewArea Then
                                addedNewArea = True
                            End If
                            If Not newAreaList.Contains(pth.FromArea) Then
                                newAreaList.Add(pth.FromArea)
                            End If
                        End If
                        'If Not String.IsNullOrEmpty(pth.FromArea) AndAlso Not newAreaList.Contains(pth.FromArea) Then
                        '    newAreaList.Add(pth.FromArea)
                        'Else

                        'End If
                        If Not String.IsNullOrEmpty(pth.ToArea) AndAlso Not detector.UniqueAreas.Contains(pth.ToArea) And Not addedNewArea Then
                            addedNewArea = True
                        End If
                        If Not String.IsNullOrEmpty(pth.ToArea) AndAlso Not newAreaList.Contains(pth.ToArea) Then
                            newAreaList.Add(pth.ToArea)
                        End If
                    Next
                    If addedNewArea OrElse newAreaList.Count <> detector.UniqueAreas.Count Then
                        detector.UniqueAreas = newAreaList
                        RaiseEvent DEFAreasChanged()
                    End If
                    Exit For
                End If
            Next
        End Sub

        Public Event DEFAreasChanged()

        ''' <summary>
        ''' if signal type has been changed in the prosessing tab, need to re-group them by type
        ''' </summary>
        Private Sub _reGroupRawSignalByType()
            SignalMgr.ReGroupedRawSignalsByType = New ObservableCollection(Of SignalTypeHierachy)
            For Each info In SignalMgr.FileInfo
                If info.TaggedSignals.Count > 0 Then
                    'Dim b = New SignalTypeHierachy(New SignalSignatureViewModel(info.FileDirectory))
                    Dim b = New SignalTypeHierachy(New SignalSignatureViewModel(info.FileDirectory + ", " + info.Mnemonic + ", Sampling Rate: " + info.SamplingRate.ToString + "/Second"))
                    b.SignalList = SignalMgr.SortSignalByType(info.TaggedSignals)
                    SignalMgr.ReGroupedRawSignalsByType.Add(b)
                End If
            Next
            'For Each info In DataConfigure.ReaderProperty.InputFileInfos
            '    Dim b = New SignalTypeHierachy(New SignalSignatureViewModel(info.FileDirectory))
            '    b.SignalList = _signalMgr.SortSignalByType(info.TaggedSignals)
            '    _signalMgr.ReGroupedRawSignalsByType.Add(b)
            'Next
        End Sub

        'Private Sub _groupAllDataConfigOutputSignal()
        '    Dim allOutputSignals = New ObservableCollection(Of SignalSignatures)
        '    For Each stp In DataConfigure.CollectionOfSteps
        '        For Each signal In stp.OutputChannels
        '            If Not allOutputSignals.Contains(signal) AndAlso signal.IsSignalInformationComplete Then
        '                allOutputSignals.Add(signal)
        '            End If
        '        Next
        '    Next
        '    AllDataConfigOutputGroupedByType = SortSignalByType(allOutputSignals)
        '    AllDataConfigOutputGroupedByPMU = SortSignalByPMU(allOutputSignals)
        'End Sub

        'Private _postProcessingSelected As ICommand
        'Public Property PostProcessingSelected As ICommand
        '    Get
        '        Return _postProcessingSelected
        '    End Get
        '    Set(value As ICommand)
        '        _postProcessingSelected = value
        '    End Set
        'End Property
        'Private Sub _selectPostProcessing(obj As Object)

        'End Sub
    End Class

End Namespace
