MODULE MainModule

    !define tool
    PERS loaddata ldata1:=[5,[50,0,50],[1,0,0,0],0,0,0];
    !define workobject

    !define variables

    !define position
    PERS robtarget pHome:=[[-20.03,-80.1,874.36],[0.879435,3.62139E-05,0.47602,0.000354728],[0,-2,0,4],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];





    PROC main()

        StartMove;
        Initialization;


        WHILE TRUE DO

            iActionNumber:=gi_ActionNo;
            TEST iActionNumber

            CASE 1:
                IF (GOutput(go_ActionNo)=255) THEN

                    qActionNumber:=iActionNumber;
                ENDIF

            CASE 2:
                IF (GOutput(go_ActionNo)=255) THEN

                    qActionNumber:=iActionNumber;
                ENDIF

            CASE 3:
                IF (GOutput(go_ActionNo)=255) THEN

                    qActionNumber:=iActionNumber;
                ENDIF

            CASE 4:
                IF (GOutput(go_ActionNo)=255) THEN

                    qActionNumber:=iActionNumber;
                ENDIF

            CASE 5:
                IF (GOutput(go_ActionNo)=255) THEN

                    qActionNumber:=iActionNumber;
                ENDIF


            CASE 100:
                IF (GOutput(go_ActionNo)=255) THEN
                    P100_ResetSequence;
                    qActionNumber:=iActionNumber;
                ENDIF

            CASE 254:
                IF (GOutput(go_ActionNo)=255) THEN
                    SetMovementsExtParams gi_GlobalSpeed,gi_ToolNo,gi_WorkobjectNo,gi_PointNo,GInputdnum(gi_UserSpecSpeed1),GInputDnum(gi_UserSpecSpeed2),
                                                                    GInputDnum(gi_CoordinateX),GInputDnum(gi_CoordinateY),GInputDnum(gi_CoordinateZ),
                                                                    GInputDnum(gi_CoordinateRx),GInputDnum(gi_CoordinateRy),GInputDnum(gi_CoordinateRz);
                    WriteMovementsExtParams;
                    qActionNumber:=iActionNumber;
                ENDIF
            CASE 255,0:
                qActionNumber:=iActionNumber;
            DEFAULT:
                Errgener("ErrAction");
                EXIT;
            ENDTEST
            WriteMovementsExtParams;
        ENDWHILE
    ENDPROC



    PROC P100_ResetSequence()


    ENDPROC


    PROC zero()

        MoveAbsJ [[0,0,0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]]\NoEOffs,v100,z0,tool0;
    ENDPROC


ENDMODULE
