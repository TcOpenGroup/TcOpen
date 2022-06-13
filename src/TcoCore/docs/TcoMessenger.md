# Messaging

Messaging system provides a simple mechanism for posting various information (state, diagnostics, etc.) to the higher-level application (e.g. HMI/SCADA).

Each [TcoObject](~/api/TcoCore/PlcDocu.TcoCore.TcoObject.yml) contains a messenger that allows reporting information about that object to a higher-level application.

## Usage

In order to post a message, you access `Messenger` of the object (TcoObject).

~~~iecst
IF(applo13.LunarModule.OxigenTankLowLimit) THEN
    apolo13.LunarModule.Messenger.Warning('Huston, we have a problem');
END_IF;    


IF(appolo13.LunarModule.OxigenTankEmpty) THEN
    apolo13.LunarModule.Messenger.Catastrophic('Huston we are not going to make it to the Moon.');
END_IF;   

IF(appolo13.OceanSplash) THEN
    apolo13.Messenger.Info('Huston this is Eagle.');
END_IF; 
~~~

## Prioritisation

Each object has a single instance of a message holder. This instance is located in `TcoObject.Messenger._mime`. When there are more messages posted in the same cycle, the messenger will prioritize the one with the highest severity.

More messages in the same cycle will reflect in the counter `PerCycleCount`, which will be greater than `1`.

## Message persistency - Pinned messages

We can persist static messages using the `Pin` method. The persisting message will be active in the diagnostics (alarm) view until acknowledged by the user. **Pinned message is not able to accept other incoming messages** until acknowledged. Incoming messages are be discarded for the alarm view; such messages will be, however, logged by the context logger when implemented.

You can query the *pin* status of a messenger via `Pinned` property. `Pinned` property returns `true` when the message is *pinned* (active and/or unacknowledged).

#### Usage
~~~iec
IF(NOT _message.OnCondition(ar[0]).Error('This is an error').Pinned) THEN
    _startActuator := TRUE;
END_IF;
~~~

### Creating unique pinned messages

If you want to pin a message unequivocally, you should create a separate variable of `TcoMessenger` and activate persistency on that instance of the messenger.

#### Usage
~~~
// These vars must be instance members of a block (not temp members of a method or function).
VAR
    someMessenger : TcoCore.TcoMessenger(THIS^);
END_VAR
someMessenger.Error('This is an error that we want presist').Pin();
~~~

## Conditional message

Message can be conditionally activated using the `OnCondition` method.

#### Usage
~~~
someMessenger.OnContition(hasErrorCondition).Error('This is an error that we want presist');

someOtherMessenger.OnContition(hasErrorCondition).Error('This is an error that we want presist').Pin();
~~~

## Fluent message content composition

Messenger allows for fluent composition of the messages. The fluent interface is accessible via `Build()` method. You can append text or a value to the message.
The message will be posted via `As()` method and consequent qualification of the message (e.g. `AsDebug()`). 

> **IMPORTANT**: When you compose a message text with a changing value(s), the context logger (when activated) will log each change as a new log entry. This behavior might be desirable in specific scenarios; however, be aware that message buffer overruns may occur, and the log storage space may come under stress.

### Usage

~~~iec
Messenger.OnCondition(temperature > 100.0).Build().Append('Water temperature is boiling: ').AppendAny(temperature).As().AsInfo();
~~~

## Message structure

| Item          | Description                                              |
|---------------|----------------------------------------------------------|
| TimeStamp     | Time-stamp of the last occurrence of the message.        |
| Text          | Arbitrary message text.                                  |
| Identity      | Identity of the object that posted this message          |
|               | (for future expansion and buffering).                    |
| Category      | Message category indicating the severity of the message. |
| Cycle         | Context cycle in which the message was posted.           |
| PerCycleCount | Counter of messages posted in the same context cycle     |
| Pinned        | Indicates that the message is persistent                 |


## Message categories

[Message categories](~/api/TcoCore/TcoCore.eMessageCategory.yml)


## How to access the message

When you try to read the messages from a non-inxton application, you will need to evaluate whether the message is valid. The validity of the message can be determined by comparing the equality of `._mime.Cycle` with `Context._startCycleCount`. When these two values equal, the message is valid. The validity of persisted messages is given by the value `true` of `_mime._persists` variable. The equality evaluation should occur in the higher-level application, not in the PLC.

[TcoLogger integration](TcoLogger.md#tcomessenger-and-tcologger)