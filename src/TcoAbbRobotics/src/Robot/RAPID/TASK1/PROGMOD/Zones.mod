MODULE Zones

    !!!!!!!!!!!!!!!!!!FOR ZONES!!!!!!!!!!!!!!!!
    Var string sZones;
    Var robtarget pTempPosZone;
    PERS tooldata tActual;
    PERS wobjdata wActual;

    PROC CheckZones()

        tActual:=CTool();
        wActual:=CWObj();

        pTempPosZone:=CRobT(\Tool:=tActual\WObj:=wActual);

        !tu si povytvaraj podmienky pre ZONY
        !Prve si prirad mensie zony az potom vacsie zony napr blistra
        IF Distance(pTempPosZone.trans,pHome.trans)<100 THEN
            sZones:="HomeZone";

        ELSEIF pTempPosZone.trans.x>pHome.trans.x-150 AND pTempPosZone.trans.x<pHome.trans.x+150 AND
                   pTempPosZone.trans.y>pHome.trans.y-100 AND pTempPosZone.trans.y<pHome.trans.y+100 THEN
            !sZones:="HomeZone";

        ELSE
            Errgener("ErrZone");
        ENDIF
    ENDPROC


ENDMODULE
