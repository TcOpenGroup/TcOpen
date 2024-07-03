MODULE mts_function

    PERS wobjdata wobjActual:=[FALSE,FALSE,"",[[0,0,0],[0,0,0,0]],[[0,0,0],[0,0,0,0]]];
    !Tools - orientation
    PERS tooldata toolActual:=[FALSE,[[0,0,0],[0,0,0,0]],[0,[0,0,0],[0,0,0,0],0,0,0]];

    !In position constants
    PERS num GlobalSpeed:=0;
    PERS num VersionNumber:=0;
    PERS num ActualToolNumber:=0;
    PERS num ActualWobjNumber:=0;
    PERS num ActualPointNumber:=0;
    PERS speeddata UserSpecSpeed1:=[0,0,0,0];
    PERS dnum ActualUserSpecSpeed1:=0;
    PERS speeddata UserSpecSpeed2:=[0,0,0,0];
    PERS dnum ActualUserSpecSpeed2:=0;
    PERS dnum CoordinateX:=0;
    PERS dnum CoordinateY:=0;
    PERS dnum CoordinateZ:=0;
    PERS dnum CoordinateRx:=0;
    PERS dnum CoordinateRy:=0;
    PERS dnum CoordinateRz:=0;

    PERS num iActionNumber;
    PERS num qActionNumber;

    !for write to logfile
    PERS num nNokPick;
    VAR string time;
    VAR string date;
    PERS string temp_date:="2023-05-11";
    VAR iodev logsfile;
    PERS num nCamOffsX;
    PERS num nCamOffsY;
    PERS num nCamAngleRz;



    PROC Initialization()
        !set off all signal for procesing
        Reset do_Error;
        Reset do_Tool1_Extend;
        Reset do_Tool1_Retract;
        Reset do_Tool2_Extend;
        Reset do_Tool2_Retract;
        Reset do_Tool3_Extend;
        Reset do_Tool3_Retract;
        Reset do_Tool4_Extend;
        Reset do_Tool4_Retract;
        Reset do_InArea_1;
        Reset do_InArea_2;
        Reset do_InArea_3;
        Reset do_InArea_4;
        Reset do_InPosition_1;
        Reset do_InPosition_2;
        Reset do_InPosition_3;
        Reset do_InPosition_4;
        SetGO go_InPosition,0;

        SetGlobalSpeed(100);
        SetUserSpecSpeed1(30);
        SetUserSpecSpeed2(30);
        VersionNumber:=0;
        toolActual:=tool0;
        wobjActual:=wobj0;
        ActualToolNumber:=0;
        ActualWobjNumber:=0;
        ActualPointNumber:=0;
        CoordinateX:=0;
        CoordinateY:=0;
        CoordinateZ:=0;
        CoordinateRx:=0;
        CoordinateRy:=0;
        CoordinateRz:=0;
        qActionNumber:=255;
        CornerPathWarning FALSE;
        TPErase;
        TPShow TP_LATEST;

    ENDPROC

    FUNC robtarget GetCoordinatesOnPallette(num PointNumber,robtarget ZeroPosition)
        VAR num Xindex:=0;
        VAR num Yindex:=0;
        VAR num tmpIndex:=0;
        VAR num Xoffset:=0;
        VAR num Yoffset:=0;
        VAR num Xdiff:=3.0;
        VAR num Ydiff:=2.0;
        VAR robtarget TempLocalPoint;
        IF PointNumber>=0 AND PointNumber<=120 THEN
            Xindex:=(PointNumber) MOD nRowsX;
            Yindex:=(PointNumber) DIV 6;
        ELSE
            Xindex:=0;
            Yindex:=0;
        ENDIF
        TempLocalPoint:=ZeroPosition;
        TempLocalPoint.trans.x:=TempLocalPoint.trans.x+Xindex*Xdiff;
        TempLocalPoint.trans.y:=TempLocalPoint.trans.y+Yindex*Ydiff;
        RETURN TempLocalPoint;
    ENDFUNC

    PROC SetMovementsExtParams(num InGlobalSpeed,num InToolNo,num InWorkobjectNo,num InActualPointNumber,dnum InUserSpecSpeed1,dnum InUserSpecSpeed2,dnum InCoordX,dnum InCoordY,dnum InCoordZ,dnum InCoordRx,dnum InCoordRy,dnum InCoordRz)
        SetGlobalSpeed(InGlobalSpeed);
        toolActual:=SetActualTool(InToolNo);
        wobjActual:=SetActualWorkobject(InWorkobjectNo);
        ActualPointNumber:=InActualPointNumber;
        SetUserSpecSpeed1(InUserSpecSpeed1);
        SetUserSpecSpeed2(InUserSpecSpeed2);
        CoordinateX:=Unscale(InCoordX,10000,10000.0);
        CoordinateY:=Unscale(InCoordY,10000,10000.0);
        CoordinateZ:=Unscale(InCoordZ,10000,10000.0);
        CoordinateRx:=Unscale(InCoordRx,10000,360.0);
        CoordinateRy:=Unscale(InCoordRy,10000,360.0);
        CoordinateRz:=Unscale(InCoordRz,10000,360.0);
    ENDPROC


    PROC WriteMovementsExtParams()
        SetGO go_ActionNo,qActionNumber;
        SetGo go_GlobalSpeed,GlobalSpeed;
        SetGo go_ToolNo,ActualToolNumber;
        SetGo go_WorkobjectNo,ActualWobjNumber;
        SetGo go_PointNo,ActualPointNumber;
        SetGo go_UserSpecSpeed1,ActualUserSpecSpeed1;
        SetGo go_UserSpecSpeed2,ActualUserSpecSpeed2;
        SetGo go_CoordinateX,Scale(CoordinateX,10000,10000.0);
        SetGo go_CoordinateY,Scale(CoordinateY,10000,10000.0);
        SetGo go_CoordinateZ,Scale(CoordinateZ,10000,10000.0);
        SetGo go_CoordinateRx,Scale(CoordinateRx,10000,360.0);
        SetGo go_CoordinateRy,Scale(CoordinateRy,10000,360.0);
        SetGo go_CoordinateRz,Scale(CoordinateRz,10000,360.0);
    ENDPROC

    PROC SetGlobalSpeed(num InGlobalSpeed)
        IF InGlobalSpeed=0 THEN
            SpeedRefresh 100;
        ELSE
           SpeedRefresh InGlobalSpeed;
        ENDIF

        GlobalSpeed:=InGlobalSpeed;
    ENDPROC

    PROC SetVersionNumber(num InVersionNumber)

        VersionNumber:=InVersionNumber;
    ENDPROC

    FUNC tooldata SetActualTool(num RequiredToolNo)
        VAR tooldata LocalTool;

        ActualToolNumber:=RequiredToolNo;
        RETURN LocalTool;
    ENDFUNC

    FUNC wobjdata SetActualWorkobject(num RequiredWorkobjectNo)
        VAR wobjdata LocalWorkobject;

        ActualWobjNumber:=RequiredWorkobjectNo;
        RETURN LocalWorkobject;
    ENDFUNC

    PROC SetUserSpecSpeed1(dnum InUserSpecSpeed1)
        UserSpecSpeed1:=v100;
        UserSpecSpeed1.v_tcp:=dnumtonum(InUserSpecSpeed1);
        UserSpecSpeed1.v_ori:=dnumtonum(InUserSpecSpeed1);
        UserSpecSpeed1.v_leax:=dnumtonum(InUserSpecSpeed1);
        UserSpecSpeed1.v_reax:=dnumtonum(InUserSpecSpeed1);
        ActualUserSpecSpeed1:=InUserSpecSpeed1;
    ENDPROC

    PROC SetUserSpecSpeed2(dnum InUserSpecSpeed2)
        UserSpecSpeed2:=v100;
        UserSpecSpeed2.v_tcp:=dnumtonum(InUserSpecSpeed2);
        UserSpecSpeed2.v_ori:=dnumtonum(InUserSpecSpeed2);
        UserSpecSpeed2.v_leax:=dnumtonum(InUserSpecSpeed2);
        UserSpecSpeed2.v_reax:=dnumtonum(InUserSpecSpeed2);
        ActualUserSpecSpeed2:=InUserSpecSpeed2;
    ENDPROC

    FUNC dnum Unscale(dnum InCoordinate,dnum denom,dnum offset)
        VAR dnum Retval;
        Retval:=Rounddnum(((InCoordinate)/denom-offset)\Dec:=3);
        RETURN Retval;
    ENDFUNC

    FUNC dnum Scale(dnum InCoordinate,dnum nom,dnum offset)
        VAR dnum Retval;
        Retval:=Rounddnum(((InCoordinate+offset)*nom)\Dec:=3);
        RETURN Retval;
    ENDFUNC

    FUNC dnum SetCoordinateX(dnum InCoordinate)
        VAR dnum Retval;
        Retval:=Rounddnum(((InCoordinate)/1000.0-10000.0)\Dec:=3);
        RETURN Retval;
    ENDFUNC

    FUNC dnum SetCoordinateY(dnum InCoordinate)
        VAR dnum Retval;
        Retval:=Rounddnum(((InCoordinate)/1000.0-10000.0)\Dec:=3);
        RETURN Retval;
    ENDFUNC

    FUNC dnum SetCoordinateZ(dnum InCoordinate)
        VAR dnum Retval;
        Retval:=Rounddnum(((InCoordinate)/1000.0-10000.0)\Dec:=3);
        RETURN Retval;
    ENDFUNC

    FUNC dnum SetCoordinateRx(dnum InCoordinate)
        VAR dnum Retval;
        Retval:=Rounddnum(((InCoordinate)/1000.0-360.0)\Dec:=3);
        RETURN Retval;
    ENDFUNC

    FUNC dnum SetCoordinateRy(dnum InCoordinate)
        VAR dnum Retval;
        Retval:=Rounddnum(((InCoordinate)/1000.0-360.0)\Dec:=3);
        RETURN Retval;
    ENDFUNC

    FUNC dnum SetCoordinateRz(dnum InCoordinate)
        VAR dnum Retval;
        Retval:=Rounddnum(((InCoordinate)/1000.0-360.0)\Dec:=3);
        RETURN Retval;
    ENDFUNC

    FUNC orient MultiplyQuaternion(orient a,orient b)
        VAR orient retval;

        retval.q1:=a.q1*b.q1-a.q2*b.q2-a.q3*b.q3-a.q4*b.q4;
        retval.q2:=a.q1*b.q2+a.q2*b.q1+a.q3*b.q4-a.q4*b.q3;
        retval.q3:=a.q1*b.q3-a.q2*b.q4+a.q3*b.q1+a.q4*b.q2;
        retval.q4:=a.q1*b.q4+a.q2*b.q3-a.q3*b.q2+a.q4*b.q1;

        RETURN retval;
    ENDFUNC

    FUNC orient DivideQuaternion(orient a,orient b)
        VAR orient retval;
        VAR orient invb;

        invb:=CalculateInverse(b);

        retval:=MultiplyQuaternion(a,invb);

        RETURN retval;
    ENDFUNC

    FUNC orient CalculateInverse(orient a)
        VAR orient retval;
        VAR num divider;

        divider:=a.q1*a.q1+a.q2*a.q2+a.q3*a.q3+a.q4*a.q4;
        IF divider=0 THEN
            divider:=1;
        endif
        retval.q1:=a.q1/divider;
        retval.q2:=-a.q2/divider;
        retval.q3:=-a.q3/divider;
        retval.q4:=-a.q4/divider;

        RETURN retval;
    ENDFUNC

    FUNC orient AddQuaternion(orient a,orient b)
        VAR orient retval;

        retval.q1:=a.q1+b.q1;
        retval.q2:=a.q2+b.q2;
        retval.q3:=a.q3+b.q3;
        retval.q4:=a.q4+b.q4;

        RETURN retval;
    ENDFUNC

    FUNC orient SubQuaternion(orient a,orient b)
        VAR orient retval;

        retval.q1:=a.q1-b.q1;
        retval.q2:=a.q2-b.q2;
        retval.q3:=a.q3-b.q3;
        retval.q4:=a.q4-b.q4;

        RETURN retval;
    ENDFUNC

    FUNC robtarget apply_tool(robtarget p1,tooldata tool)
        VAR orient p1_t;
        VAR orient p1_r;
        VAR orient p1_r_inv;
        VAR orient tool_t;
        VAR orient tool_r;
        VAR orient p2_t;
        VAR orient p2_r;
        VAR orient tmp;
        Var robtarget retval;

        p1_t.q1:=0;
        p1_t.q2:=p1.trans.x;
        p1_t.q3:=p1.trans.y;
        p1_t.q4:=p1.trans.z;

        p1_r:=p1.rot;

        p1_r_inv:=CalculateInverse(p1.rot);

        tool_t.q1:=0;
        tool_t.q2:=tool.tframe.trans.x;
        tool_t.q3:=tool.tframe.trans.y;
        tool_t.q4:=tool.tframe.trans.z;

        tool_r:=tool.tframe.rot;

        tmp:=MultiplyQuaternion(p1_r,tool_t);
        tmp:=MultiplyQuaternion(tmp,p1_r_inv);
        tmp:=AddQuaternion(p1_t,tmp);

        retval.trans.x:=tmp.q2;
        retval.trans.y:=tmp.q3;
        retval.trans.z:=tmp.q4;

        tmp:=MultiplyQuaternion(p1_r,tool_r);

        retval.rot:=tmp;

        RETURN retval;
    ENDFUNC

    FUNC robtarget remove_tool(robtarget p1,tooldata tool)
        VAR orient p1_t;
        VAR orient p1_r;
        VAR orient tool_t;
        VAR orient tool_r;
        VAR orient tool_r_inv;
        VAR orient p2_t;
        VAR orient p2_r;
        VAR orient p2_r_inv;
        VAR orient tmp;
        Var robtarget retval;

        p1_t.q1:=0;
        p1_t.q2:=p1.trans.x;
        p1_t.q3:=p1.trans.y;
        p1_t.q4:=p1.trans.z;

        p1_r:=p1.rot;


        tool_t.q1:=0;
        tool_t.q2:=tool.tframe.trans.x;
        tool_t.q3:=tool.tframe.trans.y;
        tool_t.q4:=tool.tframe.trans.z;

        tool_r:=tool.tframe.rot;
        tool_r_inv:=CalculateInverse(tool_r);

        p2_r:=MultiplyQuaternion(p1_r,tool_r_inv);
        p2_r_inv:=CalculateInverse(p2_r);

        tmp:=MultiplyQuaternion(p2_r,tool_t);
        tmp:=MultiplyQuaternion(tmp,p2_r_inv);
        tmp:=SubQuaternion(p1_t,tmp);

        retval.trans.x:=tmp.q2;
        retval.trans.y:=tmp.q3;
        retval.trans.z:=tmp.q4;

        retval.rot:=p2_r;
        RETURN retval;
    ENDFUNC

    FUNC robtarget apply_wobj(robtarget p1,wobjdata wobj)
        VAR orient p1_t;
        VAR orient p1_r;
        VAR orient wobj_t;
        VAR orient wobj_r;
        VAR orient wobj_r_inv;
        VAR orient p2_t;
        VAR orient p2_r;
        VAR orient tmp;
        Var robtarget retval;

        p1_t.q1:=0;
        p1_t.q2:=p1.trans.x;
        p1_t.q3:=p1.trans.y;
        p1_t.q4:=p1.trans.z;

        p1_r:=p1.rot;


        wobj_t.q1:=0;
        wobj_t.q2:=wobj.uframe.trans.x;
        wobj_t.q3:=wobj.uframe.trans.y;
        wobj_t.q4:=wobj.uframe.trans.z;

        wobj_r:=wobj.uframe.rot;
        wobj_r_inv:=CalculateInverse(wobj_r);


        tmp:=SubQuaternion(p1_t,wobj_t);
        tmp:=MultiplyQuaternion(wobj_r_inv,tmp);
        tmp:=MultiplyQuaternion(tmp,wobj_r);

        retval.trans.x:=tmp.q2;
        retval.trans.y:=tmp.q3;
        retval.trans.z:=tmp.q4;

        tmp:=MultiplyQuaternion(wobj_r_inv,p1_r);

        retval.rot:=tmp;

        RETURN retval;
    ENDFUNC


    FUNC robtarget remove_wobj(robtarget p1,wobjdata wobj)
        VAR orient p1_t;
        VAR orient p1_r;
        VAR orient wobj_t;
        VAR orient wobj_r;
        VAR orient wobj_r_inv;
        VAR orient p2_t;
        VAR orient p2_r;
        VAR orient tmp;
        Var robtarget retval;

        p1_t.q1:=0;
        p1_t.q2:=p1.trans.x;
        p1_t.q3:=p1.trans.y;
        p1_t.q4:=p1.trans.z;

        p1_r:=p1.rot;


        wobj_t.q1:=0;
        wobj_t.q2:=wobj.uframe.trans.x;
        wobj_t.q3:=wobj.uframe.trans.y;
        wobj_t.q4:=wobj.uframe.trans.z;

        wobj_r:=wobj.uframe.rot;
        wobj_r_inv:=CalculateInverse(wobj_r);


        tmp:=MultiplyQuaternion(p1_t,wobj_r_inv);
        tmp:=MultiplyQuaternion(wobj_r,tmp);
        tmp:=AddQuaternion(tmp,wobj_t);

        retval.trans.x:=tmp.q2;
        retval.trans.y:=tmp.q3;
        retval.trans.z:=tmp.q4;

        tmp:=MultiplyQuaternion(wobj_r,p1_r);

        retval.rot:=tmp;

        RETURN retval;
    ENDFUNC

    PROC LogstoFile()

        time:=CTime();
        date:=CDate();

        IF date<>temp_date THEN
            temp_date:=date;
            Open "HOME:"\File:="LOGSFILE.txt",logsfile\Write;
            nNokPick:=1;
        ELSEIF date=temp_date THEN
            Open "HOME:"\File:="LOGSFILE.txt",logsfile\Append;
            Incr nNokPick;
        ENDIF

        !Write logsfile,sVersion;
        !Write logsfile,sRoutine;
        Write logsfile,"Datum ="+date;
        Write logsfile,"CAS: "+time;
        Write logsfile,"OffsetX ="\Num:=nCamOffsX;
        Write logsfile,"OffsetY ="\Num:=nCamOffsY;
        !Write logsfile,"Rotácia ="\Num:=nCamAngleRz;
        !Write logsfile,"Pocet zle odobranych kusov = "\Num:=nNokPick;

        Write logsfile,"____________________________________";
        Close logsfile;
    ENDPROC
ENDMODULE
