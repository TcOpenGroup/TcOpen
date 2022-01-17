# Logging

Logger collects event logs from the controller.
In contrast to [TcoMessenger](TcoMessenger.md) that statically holds current most important message the logger can capture multiple and short lived events (single PLC cycle). Actual size of circular buffer is `1000` log entries.

## TcoLogger

- [PLC API](~/api/TcoCore/PlcDocu.TcoCore.TcoLogger.yml)
- [TWIN API](~/api/TcoCore/TcoCore.TcoLogger.yml)

*Logger usage in PLC code:*

~~~iecst
FUNCTION_BLOCK LoggerUsage EXTENDS TcoCore.TcoObject
VAR
	_logger : TcoCore.TcoLogger(THIS^);
	_counter : INT;
END_VAR
//---------------------------------------------
_counter := _counter + 1;
IF((_counter MOD 100) = 0) THEN
	_logger.Push('hey friend! we got a problem', eMessageCategory.Critical);
END_IF;	
~~~

*Logger usage in IVF application Log retrieval*

~~~CSharp
TcOpen.Inxton.TcoAppDomain.Current.Builder
                .SetUpLogger(new TcOpen.Inxton.Logging.SerilogAdapter(new LoggerConfiguration()
                                                        // Writes logs to console
                                                        .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Verbose)
                                                        // Writes loggs to notepad
                                                        .WriteTo.Notepad()
                                                        .MinimumLevel.Verbose()));


// Starts retrieval loop from given logger.
PlcTcoCoreExamples.EXAMPLES_PRG._loggerContext._loggerUsage._logger.StartLoggingMessages(eMessageCategory.All);
~~~

**NOTE: you can use several endpoints (sinks) to store logs. For possible sinks see the packages [here](https://www.nuget.org/packages?q=serilog).**

## TcoMessenger and TcoLogger

Messenger integrates logger to capture the messages into log output. The messengers uses the logger instance of the [context](~/api/TcoCore/PlcDocu.TcoCore.TcoContext.yml#PlcDocu_TcoCore_TcoContext_Logger). 

In default setting the message is emitted into logger only on the rising edge of the message.
Detection of rising edge is deremined by the *message text, category, and context cycle*.

For possible configurations see: 

- [Message digest method](~/api/TcoCore/PlcDocu.TcoCore.TcoMessengerEnv.yml#PlcDocu_TcoCore_TcoMessengerEnv_MessageDigestMethod) / [eMessageDigestMethod](~/api/TcoCore/TcoCore.eMessageDigestMethod.yml)
- [Message logging method](~/api/TcoCore/PlcDocu.TcoCore.TcoMessengerEnv.yml#PlcDocu_TcoCore_TcoMessengerEnv_MessengerLoggingMethod) / [eMessengerLogMethod](~/api/TcoCore/TcoCore.eMessengerLogMethod.yml)

Each messenger can have up to 20 concurrent messages in two consecutive cycles.
