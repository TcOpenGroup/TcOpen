MODULE BackGround

    !WorkObjects
    PERS wobjdata wobjActual;
    !Tools
    PERS tooldata toolActual;
    !RobTargets
    PERS robtarget pHome;
    !Actual Position
    VAR robtarget pTempPos_toolActual;
    !Variables
    VAR jointtarget joints;
    VAR robtarget pTempPos;
    !declaration for Zone position
    CONST num nHomeZone:=1;
    CONST num nPickZone:=2;
    CONST num nPlaceZone:=3;
    CONST num nOutDefineZone:=255;


    PROC main()

        VAR bool TempBool;

        pTempPos_toolActual:=CRobT();
        joints:=CJointT();
        !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        IF Distance(pTempPos_toolActual.trans,pHome.trans)<50 THEN
            !safeZone
            SetGO go_Zones,nHomeZone;
            !        ELSEIF pTempPos_toolActual.trans.x<360 AND pTempPos_toolActual.trans.y<-470 THEN
            !            !
            !            SetGO go_Zones,nPickZone;
            !        ELSEIF pTempPos_toolActual.trans.x>505 AND pTempPos_toolActual.trans.y<340 AND pTempPos_toolActual.trans.z<650 THEN
            !            !
            !            SetGO go_Zones,nPlaceZone;
            !            !
            !        ELSEIF pTempPos_toolActual.trans.x>280 AND pTempPos_toolActual.trans.x<650 AND pTempPos_toolActual.trans.y>400
            !        AND pTempPos_toolActual.trans.y<520 AND pTempPos_toolActual.trans.z<600 THEN

        ELSE
            SetGO go_Zones,nOutDefineZone;

        ENDIF

        !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        WaitTime 0.05;

    ENDPROC

ENDMODULE
