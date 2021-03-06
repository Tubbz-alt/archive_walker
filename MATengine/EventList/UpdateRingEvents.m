% function EventList = EventList = UpdateRingEvents(DetectionResults, ~, EventList, ~, ~)
%
% This function updates a list of ringdown events based on
% new detection results. For example, if a ringdown is detected in two
% adjacent sets of  data, this function judges whether they are the same
% event and lists them as such. 
%
% Inputs:
%   DetectionResults = Detection results from the out-of-range and ringdown 
%                      detectors. See RingdownDetector.m for specifications. 
%   AdditionalOutput - Unused -> allows multiple functions to be called from UpdateEvents.m
%   EventList = A structure array containing the current list of events.
%               Each entry in the array contains a separate event and has
%               the fields:
%                   Start = datenum value for the start of the event
%                   End = datenum value for the end of the event
%                   Channel = cell array of strings specifying channels
%                             involved in the event
%                   PMU = cell array of strings specifying the PMUs
%                         associated with the channels listed in Channel
%   Params - Unused -> allows multiple functions to be called from UpdateEvents.m
%   AlarmParams - Unused -> allows multiple functions to be called from UpdateEvents.m
%   
% Outputs:
%   EventList = updated event list. See EventList in the list of Inputs
%               for specifications.
%
% Created by Jim Follum (james.follum@pnnl.gov) in November, 2016.


function EventList = UpdateRingEvents(DetectionResults, ~, EventList, ~, AlarmParams)

% Separate the detections from all the different channels into groups that
% overlap. The start and end of each of these groups will be compared to
% the start and end points of previously detected events. 
%
% Get the start and end of detections from each channel. Convert from time
% strings to date number. ChannelIdx is the same size
% and indicates which channel the boundary points came from.
NewStarts = [];
NewEnds = [];
ChannelIdx = [];
for ChanIdx = 1:length(DetectionResults)
    % RingStart is set to NaN (not a cell) when the channel is 
    % discarded. Ignore these channels.
    if iscell(DetectionResults(ChanIdx).RingStart)
        NewStarts = [NewStarts cellfun(@datenum,DetectionResults(ChanIdx).RingStart)];
        NewEnds = [NewEnds cellfun(@datenum,DetectionResults(ChanIdx).RingEnd)];
        ChannelIdx = [ChannelIdx ChanIdx*ones(1,length(DetectionResults(ChanIdx).RingStart))];
    end
end
% 
% NewEdges contains all the starts and ends. 
NewEdges = [NewStarts NewEnds];
ChannelIdx = [ChannelIdx ChannelIdx];
%
% Starts are coded as 1, Ends are coded as -1. This coding is useful for
% identifying groups after sorting.
BoundTypeCode = [ones(size(NewStarts)) -1*ones(size(NewEnds))];
%
% Sort all of the boundary points. Use the sorting index to sort the coded
% boundary type and the index for the channel.
[NewEdges,idx] = sort(NewEdges);
BoundTypeCode = BoundTypeCode(idx);
ChannelIdx = ChannelIdx(idx);
%
% BoundaryTypeCode contains 1 for starts and -1 for ends. A cumulative sum
% of 0 indicates the number of starts equals the number of ends and thus
% a group.
endIndex = find(cumsum(BoundTypeCode)==0);
startIndex = [1 (endIndex+1)];
startIndex = startIndex(1:end-1);
%
% For each group, store the index of the channel, the overall start time, and
% the overall end time.
Group = struct('ChannelIdx',[],'Start',[],'End',[]);
for GrpIdx = 1:length(startIndex)
    Group(GrpIdx).ChannelIdx = unique(ChannelIdx(startIndex(GrpIdx):endIndex(GrpIdx)));
    Group(GrpIdx).Start = NewEdges(startIndex(GrpIdx));
    Group(GrpIdx).End = NewEdges(endIndex(GrpIdx));
end

% If no events were detected return the current EventList without any
% changes
if isempty(Group(1).Start)
    return
end
        
% For each group of detections
for GrpIdx = 1:length(Group)
    if isfield(EventList,'Start')
        % Events have already been detected. Update them based on the
        % contents of Group
        
        % Cell arrays containing datenum values for the start and stop 
        % times of all the currently stored events
        EventStarts = [EventList.Start];
        EventEnds = [EventList.End];
        
        % Indices of events in EventList where the the currently
        % considered event overlaps with a previously listed event.
        % In practice, this will almost always contain a maximum of 1
        % event. The code is written to handle multiple entries just in
        % case.
        EventIdx = find(((Group(GrpIdx).Start >= EventStarts) & (Group(GrpIdx).Start < EventEnds)) | ...
            ((Group(GrpIdx).End > EventStarts) & (Group(GrpIdx).End <= EventEnds)) | ...
            ((Group(GrpIdx).Start <= EventStarts) & (Group(GrpIdx).End >= EventEnds)));
        
        if isempty(EventIdx)
            % This is a newly detected event, add it to the list
            EventList(end+1).ID = AssignEventID();
            EventList(end).Start = Group(GrpIdx).Start;
            EventList(end).End = Group(GrpIdx).End;
            EventList(end).Channel = [EventList(end).Channel DetectionResults(Group(GrpIdx).ChannelIdx).Channel];
            EventList(end).PMU = [EventList(end).PMU DetectionResults(Group(GrpIdx).ChannelIdx).PMU];

            % Remove duplicate channel/PMU listings
            ChannelPMU = strcat(EventList(end).Channel,EventList(end).PMU);
            [~,Uidx] = unique(ChannelPMU);
            EventList(end).Channel = EventList(end).Channel(Uidx);
            EventList(end).PMU = EventList(end).PMU(Uidx);
        else
            % The currently considered event overlaps with a previously
            % listed event.
            
            % EventIdx will normally only contain one value, but it is
            % placed in a for loop for unusual circumstances where it
            % contains multiple values
            for ThisEvent = EventIdx
                EventList(ThisEvent).Start = min([EventList(ThisEvent).Start Group(GrpIdx).Start]);
                EventList(ThisEvent).End = max([EventList(ThisEvent).End Group(GrpIdx).End]);
                EventList(ThisEvent).Channel = [EventList(ThisEvent).Channel DetectionResults(Group(GrpIdx).ChannelIdx).Channel];
                EventList(ThisEvent).PMU = [EventList(ThisEvent).PMU DetectionResults(Group(GrpIdx).ChannelIdx).PMU];

                % Remove duplicate channel/PMU listings
                ChannelPMU = strcat(EventList(ThisEvent).Channel,EventList(ThisEvent).PMU);
                [~,Uidx] = unique(ChannelPMU);
                EventList(ThisEvent).Channel = EventList(ThisEvent).Channel(Uidx);
                EventList(ThisEvent).PMU = EventList(ThisEvent).PMU(Uidx);
            end
        end
        
        % If this event overlapped with more than one previously listed
        % event those events need to be combined.
        if length(EventIdx) > 1
            EventList(EventIdx(1)).Start = min([EventList(EventIdx).Start]);
            EventList(EventIdx(1)).End = max([EventList(EventIdx).End]);
            EventList(EventIdx(1)).Channel = [EventList(EventIdx).Channel];
            EventList(EventIdx(1)).PMU = [EventList(EventIdx).PMU];
            
            % Remove duplicate channel/PMU listings
            ChannelPMU = strcat(EventList(EventIdx(1)).Channel,EventList(EventIdx(1)).PMU);
            [~,Uidx] = unique(ChannelPMU);
            EventList(EventIdx(1)).Channel = EventList(EventIdx(1)).Channel(Uidx);
            EventList(EventIdx(1)).PMU = EventList(EventIdx(1)).PMU(Uidx);
            
            % Remove all events in EventIdx except EventIdx(1)
            EventList(EventIdx(2:end)) = [];
        end
    else
        % Events have not yet been detected, so those in Group definitely
        % need to be added
        EventList(1).ID = AssignEventID();
        EventList(1).Start = Group(GrpIdx).Start;
        EventList(1).End = Group(GrpIdx).End;
        EventList(1).Channel = [DetectionResults(Group(GrpIdx).ChannelIdx).Channel];
        EventList(1).PMU = [DetectionResults(Group(GrpIdx).ChannelIdx).PMU];

        % Remove duplicate channel/PMU listings
        ChannelPMU = strcat(EventList(1).Channel,EventList(1).PMU);
        [~,Uidx] = unique(ChannelPMU);
        EventList(1).Channel = EventList(1).Channel(Uidx);
        EventList(1).PMU = EventList(1).PMU(Uidx);
    end
end


% For ringdown alarms, the user can set a maximum duration. Ringdowns
% lasting longer than this are not considered ringdowns. The following
% lines remove ringdown events with durations that exceed the limit.
if isfield(AlarmParams,'MaxDuration')
    % Cell arrays containing datenum values for the start and stop 
    % times of all the currently stored events
    EventStarts = [EventList.Start];
    EventEnds = [EventList.End];
    EventDuration = EventEnds - EventStarts;
    
    MaxDuration = AlarmParams.MaxDuration/(60*60*24);
    
    EventList = EventList(EventDuration < MaxDuration);
end
