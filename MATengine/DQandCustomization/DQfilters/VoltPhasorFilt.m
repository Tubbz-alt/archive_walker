% function [PMUstruct,setNaNMatrix] = VoltPhasorFilt(PMUstruct,SigsToFilt,Parameters,setNaNMatrix)
% This function flags any data point of voltage magnitude signal that
% falls outside a user selected range, and sets flagged data to NaN
% depending on user provided parameters.
% 
% Inputs:
	% PMUstruct: structure in the common format for a single PMU
        % PMUstruct.Signal_Type: a cell array of strings specifying
        % signal(s) type in the PMU (size:1 by number of data channel)
        % PMUstruct.Signal_Name: a cell array of strings specifying name of
        % signal(s) in the PMU (size:1 by number of data channel)
        % PMUstruct.Signal_Unit: a cell array of strings specifying unit of
        % signal(s) in the PMU (size:1 by number of data channel)
        % PMUstruct.Data: Matrix containing PMU measurements (size:
        % number of data points by number of channels in the PMU)
        % PMUstruct.Flag: 3-dimensional matrix indicating PMU
        % measurements flagged by different filter operation (size: number 
        % of data points by number of channels by number of flag bits)                     
    % SigsToFilt: a cell array of strings specifying name of signals to be
    % filtered
    % Parameters: a struct array containing user provided information to
    % check data quality
        % Parameters.VoltMin: Minimum voltage magnitude for which voltage
        % data is not flagged
        % Parameters.VoltMax: Maximum voltage magnitude for which voltage
        % data is not flagged
        % Parameters.SetToNaN: If TRUE, flagged data are set to NaN
        % Parameters.FlagBit: Flag bit for this filter operation
        % Parameters.NomVoltage: Nominal voltage of the voltage signal
    % setNaNMatrix: Matrix of size: number of data points by number of
    % channels in a PMU. '0' indicates data is not to be set to NaN after
    % filter operation, any other value indicates data should be set to NaN
    % FileType: Data file type (csv or PDAT)
% 
% Outputs:
    % PMUstruct
    % setNaNMatrix
%     
%Created by: Jim Follum(james.follum@pnnl.gov)
%Modified on June 7, 2016 by Urmila Agrawal(urmila.agrawal@pnnl.gov):
    %1. Changed the flag matrix from a 2 dimensional double matrix to a 3 dimensional logical matrix
    %2. data are set to NaN after carrying out all filter operation instead of setting data to NaN after each filter operation
% Modified June 28, 2016 by Jim Follum:
    % Added the ability to filter voltage phasors    
% Modified June 28, 2016 by Urmila Agrawal:
    % fixed a minor bug for finding the indices of voltage magnitude and
    % voltage phasor signals
% Modified July 28, 2016 by Urmila Agrawal:
    % added file type input parameter as voltage quantity given in pdat
    % file is per phase and that in csv file is line-to-line.% 
% Modified March 31, 2017 by Jim Follum:
    % The nominal voltage must now be specified in the XML. Previously, if
    % it was not specified in the XML it was extracted from the signal
    % name. This approach fails for custom signal names and also required
    % the FileType, which may no longer be consistent because multiple
    % files can be read simultaneously from different directories. The
    % FileType input was removed.

function [PMUstruct,setNaNMatrix] = VoltPhasorFilt(PMUstruct,SigsToFilt,Parameters,setNaNMatrix)

VoltMin = str2num(Parameters.VoltMin);
VoltMax = str2num(Parameters.VoltMax);
SetToNaN = strcmp(Parameters.SetToNaN,'TRUE');
FlagBit = str2double(Parameters.FlagBit);
setNaNmatrixIni = zeros(size(setNaNMatrix));
% If specific signals were not listed, apply to all voltages
if isempty(SigsToFilt)
    VmagIdx = find(strcmp(cellfun(@GetFirstTwo,PMUstruct.Signal_Type,'UniformOutput',false),'VM')); 
    VphasorIdx = find(strcmp(cellfun(@GetFirstTwo,PMUstruct.Signal_Type,'UniformOutput',false),'VP'));
    SigsToFilt = PMUstruct.Signal_Name([VmagIdx VphasorIdx]);
end

for SigIdx = 1:length(SigsToFilt)
    ThisSig = find(strcmp(PMUstruct.Signal_Name,SigsToFilt{SigIdx}));
    
    % If the specified signal is not in PMUstruct, skip the rest of the
    % code and go to the next SigIdx.
    if isempty(ThisSig)
        warning(['Signal ' SigsToFilt{SigIdx} ' could not be found.']);
        continue
    end    
    
    % Make sure signal is a voltage magnitude or phasor. If not, throw an error.
    if strcmp(PMUstruct.Signal_Type{ThisSig}(1:2), 'VM') || strcmp(PMUstruct.Signal_Type{ThisSig}(1:2), 'VP')
        % Get nominal value
        if isfield(Parameters,'NomVoltage')
            % The nominal voltage was specified in the XML
            NomVoltage = str2num(Parameters.NomVoltage);
        else
            error('The nominal voltage must be specified when implementing the data quality filter for voltage phasors');
%             % The nominal voltage must be extracted from the signal name
%             NomVoltage = strsplit(PMUstruct.Signal_Name{ThisSig},'.');
%             if strcmpi(FileType,'csv')
%                 NomVoltage = str2double(NomVoltage{1}(end-2:end));
%             elseif strcmpi(FileType,'pdat')
%                 NomVoltage = str2double(NomVoltage{1}(6:8));
%                 % Transform rating from line-to-line to line-to-neutral
%                 NomVoltage = NomVoltage/sqrt(3);
%             end
        end
        
        % Nominal value is in kV, adjust if signal is in V
        if strcmp(PMUstruct.Signal_Unit{ThisSig}, 'V')
            % Convert to V from kV
            NomVoltage = NomVoltage*1000;
        end
        
        ThisData = PMUstruct.Data(:,ThisSig);
        % If it's a phasor, just need to evaluate the magnitude
        if strcmp(PMUstruct.Signal_Type{ThisSig}(1:2), 'VP')
            ThisData = abs(ThisData);
        end
        OutIdx = find((ThisData > VoltMax*NomVoltage) | (ThisData < VoltMin*NomVoltage));
        PMUstruct.Flag(OutIdx,ThisSig,FlagBit) = true;
        if SetToNaN
            setNaNmatrixIni(OutIdx,ThisSig) = 1;
        end
        
        % If the input signal is a voltage magnitude, flag corresponding angles
        if strcmp(PMUstruct.Signal_Type{ThisSig}(1:2), 'VM')
            ThisSigAng = find(strcmp(PMUstruct.Signal_Name,strrep(SigsToFilt{SigIdx},'.MAG','.ANG')));
            if ~isempty(ThisSigAng)
                PMUstruct.Flag(OutIdx,ThisSigAng,FlagBit) = true;
                if SetToNaN
                    setNaNmatrixIni(OutIdx,ThisSigAng) = 1;
                end
            end
        end
    else
        error('Only voltage magnitudes and phasors can be filtered');
    end
end
%setNaNmatrixIni has element '1' for the current PMU which
%is to be set to NaN for the current filter operation
%setNaNMatrix has non-zero positive elements for the current PMU which
%is to be set to NaN after all filter operation that has been carried out
setNaNMatrix = setNaNMatrix + setNaNmatrixIni;
end

function out = GetFirstTwo(In)
out = In(1:min([length(In),2]));
end