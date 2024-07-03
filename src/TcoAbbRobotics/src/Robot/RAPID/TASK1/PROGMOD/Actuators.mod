MODULE Actuators
    !Module for control actuators
    !chybová premenná po ubehnutí max casu na zatvorenie gripra pri odoberaní kusu
    VAR bool bGrpErr;

    !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    !!!!!!!!!!!!!!gripper control!!!!!!!!!!!!!!!!!
    !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    !example
    !    FUNC BOOL A50Grp_OUT()
    !        !opening the gripper
    !        !SET do_A50_Grp;
    !        !WaitDI di_32B2_A50_OUT,1\Visualize\Header:="WAITING FOR SIGNAL: di_32B2_A50_OUT"\MsgArray:=["CHECK SENSOR -32B2"]\Icon:=iconError;
    !        !WaitTime\InPos,0.3;
    !       GripLoad load0;
    !        RETURN TRUE;
    !    ENDFUNC

    !FUNC BOOL A50Grp_IN()
    !        closing the gripper
    !        RESET do_A50_Grp;
    !        WaitDI di_32B1_A50_IN,1\Visualize\Header:="WAITING FOR SIGNAL: di_32B1_A50_IN"\MsgArray:=["CHECK SENSOR -32B1"]\Icon:=iconError;
    !    GripLoad ldata1;
    !        RETURN TRUE;
    !ENDFUNC


    !    FUNC BOOL VacumON()
    !!        Set do_Vacuum;
    !!        WaitDI di_IsVacuum,1\Visualize\Header:="WAITING FOR SIGNAL: di_IsVacuum"\MsgArray:=["CHECK VACUUM SENSOR"]\Icon:=iconError;
    !!        WaitTime\InPos,0.3;
    !        RETURN TRUE;
    !    ENDFUNC

    !    FUNC BOOL Exhaust()
    !!        Reset do_Vacuum;
    !         WaitTime\InPos,0.1;
    !        RETURN TRUE;
    !    ENDFUNC


ENDMODULE
