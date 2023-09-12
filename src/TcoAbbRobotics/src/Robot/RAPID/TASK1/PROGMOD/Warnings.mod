MODULE Warnings

    !!!!!! FOR ERRORS!!!!!!
    PERS tooldata CurtoolValue;
    PERS string CurToolName;
    PERS pos pActualPos;
    PERS string sErr;
    VAR btnres answer;
    PERS string sHeader:="NIE JE MOZNE VYKONAT DRAHU ROBOTA.";

    !aktualna chybova hlaska
    PERS string sMsgLine2:="NESPRAVNE VOLANE CISLO AKCIE (gi_ActionNo).";
    PERS string sMsgLine3:="PRIJATE CISLO: ActionNo = 0";
    PERS string sMsgLine4:="POTREBNY RESET ROBOTA";
    PERS string sMsgLine5:="";

    !jednotlive typy chybovych hlasok
    PERS string sLine2Action:="NESPRAVNE VOLANE CISLO AKCIE (gi_ActionNo).";
    PERS string sLine3Action:="PRIJATE CISLO: ActionNo = ";
    PERS string sLine4Action:="POTREBNY RESET ROBOTA";
    
    PERS string sLine2Zone:="ROBOT SA NACHADZA MIMO DEFINOVANEJ ZONY";
    PERS string sLine3Zone:="PRESUNTE ROBOTA V MANUALNOM REZIME DO pHomePose";
    PERS string sLine4Zone:="PRIJATE CISLO: ActionNo = ";
    PERS string sLine5Zone:="AKTUALNA POZICIA NASTROJA = ";

    PERS string sLine2Home:="ROBOT SA NENACHADZA V HOME POZICII,";
    PERS string sLine3Home:="ODKIAL ZACINA POHYB TEJTO RUTINY.";

    PERS string sLine2Reset:="ROBOT SA NENACHADZA V ZONACH Z KTORYCH JE MOZNY RESET.";
    PERS string sLine3Reset:="PRESUNTE ROBOTA V MANUALNOM REZIME DO pHome.";

    PERS string sLine2Point:="NESPRAVNE VOLANE CISLO POZICIE (gi_PointNo).";
    PERS string sLine3Point:="PRIJATE CISLO: gi_PointNo = ";

    PERS string sLine2Wobj:="NESPRAVNE VOLANE CISLO WORKOBJECTU (gi_WorkobjectNo).";
    PERS string sLine3Wobj:="PRIJATE CISLO: gi_WorkobjectNo = ";

    PERS string sLine2Tool:="NESPRAVNE VOLANE CISLO NASTROJA (gi_ToolNo).";
    PERS string sLine3Tool:="PRIJATE CISLO: gi_ToolNo = ";

    PERS string sLine2PartIn:="DIEL JE UCHOPENY V GRIPPRI.";
    PERS string sLine3PartIn:="NIE JE MOZNE IST VZIAT DALSI.";

    PERS string sLine2PartOut:="DIEL SA NENACHADZA V GRIPPRI.";
    PERS string sLine3PartOut:="NIE JE MOZNE IST HO VYLOZIT.";

    PERS string sLine2PartMiss:="DIEL SA NENACHADZA V MIESTE ODOBERANIA.";
    PERS string sLine3PartMiss:="ZABEZPECIT PRITOMNOST DIELU PRE ODOBRATIE.";

    PERS string sLine2Vacuum:="VAKUUM NEDOSIAHNUTE,";
    PERS string sLine3Vacuum:="DIEL NECHYTENY V GRIPPRI.";

    PERS string sLine2Gripper:="GRIPPER NIE JE SPRAVNE PRIPOJENY K PRIRUBE.";
    PERS string sLine3Gripper:="SKONTROLUJTE UCHYTENIE VYMENITELNEHO GRIPPRA.";

    PERS string sLine2Mechanism:="NEZNAME ZAVOLANIE MECHANIZMU";
    PERS string sLine3Mechanism:="SKONTROLUJTE NAZOV VOLANIA MECHANIZMU";

    PERS string sLine2Unknown:="NESPECIFIKOVANA CHYBA.";
    PERS string sLine3Unknown:="PRESUNTE ROBOTA V MANUALNOM REZIME DO pHome.";

    !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    PROC Errgener(string sErr)
        TPErase;
        pActualPos:=Cpos(\Tool:=tActual\WObj:=wActual);
        GetSysData CurToolvalue\ObjectName:=CurToolName;
        Set do_Error;
        TEST sErr

        CASE "ErrAction":
            sMsgLine2:=sLine2Action;
            sMsgLine3:=sLine3Action+ValToStr(gi_ActionNo);
            sMsgLine4:=sLine4Action;
           

        CASE "ErrZone":
            sMsgLine2:=sLine2Zone;
            sMsgLine3:=sLine3Zone;
            sMsgLine4:=sLine4Zone+ValToStr(gi_ActionNo);
            sMsgLine5:=sLine5Zone+CurToolName+ValToStr(pActualPos);

        CASE "ErrHome":
            sMsgLine2:=sLine2Home;
            sMsgLine3:=sLine3Home;
            sMsgLine4:=" ";

        CASE "ErrReset":
            sMsgLine2:=sLine2Reset;
            sMsgLine3:=sLine3Reset;
            sMsgLine4:=" ";

        CASE "ErrPoint":
            sMsgLine2:=sLine2Point;
            sMsgLine3:=sLine3Point+ValToStr(gi_PointNo);
            sMsgLine4:=" ";

        CASE "ErrWobj":
            sMsgLine2:=sLine2Wobj;
            sMsgLine3:=sLine3Wobj+ValToStr(gi_WorkobjectNo);
            sMsgLine4:=" ";

        CASE "ErrTool":
            sMsgLine2:=sLine2Tool;
            sMsgLine3:=sLine3Tool+ValToStr(gi_ToolNo);
            sMsgLine4:=" ";

        CASE "ErrPartIn":
            sMsgLine2:=sLine2PartIn;
            sMsgLine3:=sLine3PartIn;
            sMsgLine4:=" ";

        CASE "ErrPartOut":
            sMsgLine2:=sLine2PartOut;
            sMsgLine3:=sLine3PartOut;
            sMsgLine4:=" ";

        CASE "ErrPartMiss":
            sMsgLine2:=sLine2PartMiss;
            sMsgLine3:=sLine3PartMiss;
            sMsgLine4:=" ";

        CASE "ErrVacuum":
            sMsgLine2:=sLine2Vacuum;
            sMsgLine3:=sLine3Vacuum;
            sMsgLine4:=" ";

        CASE "ErrGripper":
            sMsgLine2:=sLine2Gripper;
            sMsgLine3:=sLine3Gripper;
            sMsgLine4:=" ";
        CASE "ErrMechanism":
            sMsgLine2:=sLine2Mechanism;
            sMsgLine3:=sLine3Mechanism;
            sMsgLine4:=" ";

        DEFAULT:
            UIMsgBox
            \Header:=sHeader,
            " "
            \MsgLine2:=sLine2Unknown,\MsgLine3:=sLine3Unknown,\MsgLine4:=sMsgLine4,\MsgLine5:=sMsgLine5
            \Buttons:=btnOK
            \Icon:=iconWarning
            \Result:=answer;
            IF answer=resOK
            sMsgLine5:="";
            EXIT;
        ENDTEST

        UIMsgBox
        \Header:=sHeader,
        " "
        \MsgLine2:=sMsgLine2,\MsgLine3:=sMsgLine3,\MsgLine4:=sMsgLine4,\MsgLine5:=sMsgLine5
        \Buttons:=btnOK
        \Icon:=iconWarning
        \Result:=answer;
        IF answer=resOK
        sMsgLine5:="";
        EXIT;
    ENDPROC

ENDMODULE