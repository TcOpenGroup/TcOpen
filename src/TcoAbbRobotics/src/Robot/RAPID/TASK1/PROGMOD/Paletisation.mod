MODULE Paletisation
    !Module for control blister

    !Wobj definition 
    PERS robtarget pX1:=[[738.58,-52.85,395],[8.59678E-06,0.00750632,0.999972,4.16653E-05],[-1,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    PERS robtarget pX2:=[[738.94,-172.69,395],[0.000145704,-0.00742294,-0.999972,-2.75452E-05],[-1,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    PERS robtarget pY:=[[598.82,-53.2,395],[0.00017905,-0.00741872,-0.999972,-3.18388E-05],[-1,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    PERS robtarget pPickBlist:=[[0.07,0.00,0.00],[0.700709,-2.29863E-05,-3.53151E-05,0.713447],[-1,0,-1,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    PERS wobjdata wobj_blist:=[FALSE,TRUE,"",[[0,0,0],[1,0,0,0]],[[738.58,-52.7802,395],[0,0.708168,-0.706044,0]]];
    VAR pose poseWobjBlist;
    VAR robtarget pTemp;


    !Insert number of rows(x direction)
    PERS num nRowsX:=5;
    !Insert number of columns(y direction)
    PERS num nColsY:=8;

    !rows distance -> x direction
    PERS num nDistX:=29.9601;
    !columns distance -> y direction
    PERS num nDistY:=19.9658;

    !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    !!!!!!!!!!   UCENIE WOBJ        !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    !    PROC wobj_Calc_Blister()
    !        !Wobj paletization

    !        MoveJ Offs(pX1,0,0,50),v100,z5,t_Grp\WObj:=wobj0;
    !        !position pX1
    !        MoveL pX1,v50,fine,t_Grp\WObj:=wobj0;
    !        !
    !        MoveL Offs(pX1,0,0,100),v100,z5,t_Grp\WObj:=wobj0;
    !        MoveJ Offs(pX2,0,0,100),v100,z5,t_Grp\WObj:=wobj0;
    !        !posiiton pX2
    !        MoveL pX2,v50,fine,t_Grp\WObj:=wobj0;
    !        !
    !        MoveL Offs(pX2,0,0,100),v100,z5,t_Grp\WObj:=wobj0;
    !        MoveL Offs(pY,0,0,100),v100,z5,t_Grp\WObj:=wobj0;
    !        !pozicia pY
    !        MoveL pY,v50,fine,t_Grp\WObj:=wobj0;
    !        !
    !        MoveL Offs(pY,0,0,100),v100,fine,t_Grp\WObj:=wobj0;

    !        !creation Wobj
    !        poseWobjBlist:=DefFrame(pX1_NOK,pX2_NOK,pY_NOK,\Origin:=3);
    !        wobjBlist.uframe:=wobj0.uframe;
    !        wobjBlist.oframe:=poseWobjBlist;

    !        !teach pick position in workobject
    !        MoveJ Offs(pX1,0,0,100),v100,z5,t_Grp\WObj:=wobj0;
    !        MoveL pX1,v50,fine,t_Grp\WObj:=wobj0;

    !        pTemp:=CRobT(\Tool:=t_Grp\WObj:=wobjBlist);
    !        pPickBlist:=pTemp;
    !        !naucenie pociatocnej pozicie v novom wobj
    !        MoveL pPickBlist,v50,fine,t_Grp\WObj:=wobjBlist;
    !        !

    !        !Rows and Cols distance in blister
    !        nDistX:=Distance(pX1.trans,pX2.trans)/(nRowsX-1);
    !        nDistY:=Distance(pX1.trans,pY.trans)/(nColsY-1);
    !    ENDPROC

ENDMODULE