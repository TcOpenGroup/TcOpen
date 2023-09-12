MODULE SendEventID

    VAR intnum err_interrupt_common;
    VAR intnum err_interrupt_OPState;
    VAR intnum err_interrupt_System;
    VAR intnum err_interrupt_Hardware;
    VAR intnum err_interrupt_Program;
    VAR intnum err_interrupt_Motion;
    VAR intnum err_interrupt_Operator;
    VAR intnum err_interrupt_IO;
    VAR intnum err_interrupt_User;
    VAR intnum err_interrupt_Safety;
    VAR intnum err_interrupt_process;
    VAR intnum err_interrupt_configuration;
    VAR trapdata err_data;
    VAR errdomain err_domain;
    VAR num err_number;
    VAR errtype err_type;

    VAR string titlestr;
    VAR string string1;
    VAR string string2;
    VAR string string3;
    VAR string string4;
    VAR string string5;

    VAR num ErrID;
    CONST num nZeroErrID:=0;


    PROC main()

        IF di_ResetError=1 THEN
            SetGO go_EventID,0;
        endif
        IDelete err_interrupt_common;
        !        IDelete err_interrupt_opstate;
        !        IDelete err_interrupt_system;
        !        IDelete err_interrupt_hardware;
        !        IDelete err_interrupt_program;
        !        IDelete err_interrupt_motion;
        !        IDelete err_interrupt_operator;
        !        IDelete err_interrupt_IO;
        !        IDelete err_interrupt_User;
        !        IDelete err_interrupt_safety;
        !        IDelete err_interrupt_process;
        !        IDelete err_interrupt_configuration;

        CONNECT err_interrupt_common WITH trap_err_common;
        !connect trap for individual Event message
        !        CONNECT err_interrupt_opstate WITH trap_err_opstate;
        !        CONNECT err_interrupt_system WITH trap_err_system;
        !        CONNECT err_interrupt_hardware WITH trap_err_hardware;
        !        CONNECT err_interrupt_program WITH trap_err_program;
        !        CONNECT err_interrupt_motion WITH trap_err_motion;
        !        CONNECT err_interrupt_operator WITH trap_err_operator;
        !        CONNECT err_interrupt_IO WITH trap_err_IO;
        !        CONNECT err_interrupt_User WITH trap_err_User;
        !        CONNECT err_interrupt_safety WITH trap_err_safety;
        !        CONNECT err_interrupt_process WITH trap_err_process;
        !        CONNECT err_interrupt_configuration WITH trap_err_configuration;
        IError COMMON_ERR,err_type,err_interrupt_common;
        !        IError OP_STATE,err_type,err_interrupt_opstate;
        !        IError SYSTEM_ERR,err_type,err_interrupt_system;
        !        IError HARDWARE_ERR,err_type,err_interrupt_hardware;
        !        IError PROGRAM_ERR,err_type,err_interrupt_program;
        !        IError MOTION_ERR,err_type,err_interrupt_motion;
        !        IError OPERATOR_ERR,err_type,err_interrupt_operator;
        !        IError IO_COM_ERR,err_type,err_interrupt_IO;
        !        IError USER_DEF_ERR,err_type,err_interrupt_User;
        !        IError SAFETY_ERR,err_type,err_interrupt_safety;
        !        IError PROCESS_ERR,err_type,err_interrupt_process;
        !        IError CFG_ERR,err_type,err_interrupt_configuration;
        WaitTime 0.01;


    ENDPROC


    TRAP trap_err_common

        GetTrapData err_data;
        ReadErrData err_data,err_domain,err_number,err_type\Title:=titlestr\Str1:=string1\Str2:=string2\Str3:=string3\Str4:=string4\Str5:=string5;
        ErrID:=err_domain*10000+err_number;
        SetGO go_EventID,ErrID;
    ENDTRAP

!    TRAP trap_err_opstate

!        GetTrapData err_data;
!        ReadErrData err_data,err_domain,err_number,err_type\Title:=titlestr\Str1:=string1\Str2:=string2\Str3:=string3\Str4:=string4\Str5:=string5;
!        ErrID:=err_domain*10000+err_number;
!        IF err_domain=1 THEN
!            SetGO go_EventID,ErrID;
!        ENDIF
!    ENDTRAP

!    TRAP trap_err_system

!        GetTrapData err_data;
!        ReadErrData err_data,err_domain,err_number,err_type\Title:=titlestr\Str1:=string1\Str2:=string2\Str3:=string3\Str4:=string4\Str5:=string5;
!        ErrID:=err_domain*10000+err_number;
!        IF err_domain=2 THEN
!            SetGO go_EventID,ErrID;
!        ENDIF
!    ENDTRAP

!    TRAP trap_err_hardware

!        GetTrapData err_data;
!        ReadErrData err_data,err_domain,err_number,err_type\Title:=titlestr\Str1:=string1\Str2:=string2\Str3:=string3\Str4:=string4\Str5:=string5;
!        ErrID:=err_domain*10000+err_number;
!        IF err_domain=3 THEN
!            SetGO go_EventID,ErrID;
!        ENDIF
!    ENDTRAP

!    TRAP trap_err_program

!        GetTrapData err_data;
!        ReadErrData err_data,err_domain,err_number,err_type\Title:=titlestr\Str1:=string1\Str2:=string2\Str3:=string3\Str4:=string4\Str5:=string5;
!        ErrID:=err_domain*10000+err_number;
!        IF err_domain=4 THEN
!            SetGO go_EventID,ErrID;
!        ENDIF
!    ENDTRAP

!    TRAP trap_err_motion

!        GetTrapData err_data;
!        ReadErrData err_data,err_domain,err_number,err_type\Title:=titlestr\Str1:=string1\Str2:=string2\Str3:=string3\Str4:=string4\Str5:=string5;
!        ErrID:=err_domain*10000+err_number;
!        IF err_domain=5 THEN
!            SetGO go_EventID,ErrID;
!        ENDIF
!    ENDTRAP

!    TRAP trap_err_operator

!        GetTrapData err_data;
!        ReadErrData err_data,err_domain,err_number,err_type\Title:=titlestr\Str1:=string1\Str2:=string2\Str3:=string3\Str4:=string4\Str5:=string5;
!        ErrID:=err_domain*10000+err_number;
!        IF err_domain=6 THEN
!            SetGO go_EventID,ErrID;
!        ENDIF
!    ENDTRAP

!    TRAP trap_err_IO

!        GetTrapData err_data;
!        ReadErrData err_data,err_domain,err_number,err_type\Title:=titlestr\Str1:=string1\Str2:=string2\Str3:=string3\Str4:=string4\Str5:=string5;
!        ErrID:=err_domain*10000+err_number;
!        IF err_domain=7 THEN
!            SetGO go_EventID,ErrID;
!        ENDIF
!    ENDTRAP

!    TRAP trap_err_user

!        GetTrapData err_data;
!        ReadErrData err_data,err_domain,err_number,err_type\Title:=titlestr\Str1:=string1\Str2:=string2\Str3:=string3\Str4:=string4\Str5:=string5;
!        ErrID:=err_domain*10000+err_number;
!        IF err_domain=8 THEN
!            SetGO go_EventID,ErrID;
!        ENDIF
!    ENDTRAP

!    TRAP trap_err_safety

!        GetTrapData err_data;
!        ReadErrData err_data,err_domain,err_number,err_type\Title:=titlestr\Str1:=string1\Str2:=string2\Str3:=string3\Str4:=string4\Str5:=string5;
!        ErrID:=err_domain*10000+err_number;
!        IF err_domain=9 THEN
!            SetGO go_EventID,ErrID;
!        ENDIF
!    ENDTRAP

!    TRAP trap_err_process

!        GetTrapData err_data;
!        ReadErrData err_data,err_domain,err_number,err_type\Title:=titlestr\Str1:=string1\Str2:=string2\Str3:=string3\Str4:=string4\Str5:=string5;
!        ErrID:=err_domain*10000+err_number;
!        IF err_domain=11 THEN
!            SetGO go_EventID,ErrID;
!        ENDIF
!    ENDTRAP

!    TRAP trap_err_configuration

!        GetTrapData err_data;
!        ReadErrData err_data,err_domain,err_number,err_type\Title:=titlestr\Str1:=string1\Str2:=string2\Str3:=string3\Str4:=string4\Str5:=string5;
!        ErrID:=err_domain*10000+err_number;
!        IF err_domain=12 THEN
!            SetGO go_EventID,ErrID;
!        ENDIF
!    ENDTRAP
ENDMODULE