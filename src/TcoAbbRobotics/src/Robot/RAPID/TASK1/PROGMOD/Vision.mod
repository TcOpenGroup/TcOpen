MODULE Vision
    TASK PERS wobjdata Wobj_Camera:=[FALSE,TRUE,"",[[588.87,143.38,436.19],[0.000144387,0.00372492,-0.999993,0.00065001]],[[0,0,0],[1,0,0,0]]];
    PERS robtarget pCalibHousing:=[[588.87,143.38,436.19],[0.000144387,0.00372492,-0.999993,0.00065001],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];
    PERS robtarget pPickCam:=[[-0.0122448,-0.0152346,-4.26659],[0.995628,0.0934037,1.19577E-06,1.01026E-05],[0,0,0,0],[9E+09,9E+09,9E+09,9E+09,9E+09,9E+09]];

    PROC wobj_Calib_Cam()
        VAR robtarget pCalibTemp1;
        !position for creating wobj using a calibration grid ( robot and camera)

        MoveJ Offs(pCalibHousing,0,0,30),v1500,z10,tool0\WObj:=wobj0;
        MoveL pCalibHousing,v300,fine,tool0\WObj:=wobj0;

        Wobj_Camera.oframe.trans:=wobj0.oframe.trans;
        Wobj_Camera.oframe.rot:=wobj0.oframe.rot;
        Wobj_Camera.uframe.trans:=pCalibHousing.trans;
        Wobj_Camera.uframe.rot:=pCalibHousing.rot;
        Stop;
        !Save pick position to wobj
        pCalibTemp1:=CRobT(\Tool:=tool0\WObj:=Wobj_Camera);
        pPickCam:=pCalibTemp1;
        MoveL pPickCam,v300,fine,tool0\WObj:=Wobj_Camera;
    ENDPROC

    FUNC robtarget calcTarget(robtarget p1,wobjdata wobj,num noffsX,num noffsY,num nAngle)
        VAR pose pose1;
        VAR pose pose2;
        VAR pose pose3;
        VAR robtarget retval;

        VAR num anglex;
        VAR num angley;
        VAR num anglez;

        anglex:=EulerZYX(\X,p1.rot);
        angley:=EulerZYX(\Y,p1.rot);
        anglez:=EulerZYX(\Z,p1.rot);
        anglez:=nAngle;

        p1.rot:=OrientZYX(anglez,angley,anglex);
        p1.trans.x:=p1.trans.x+noffsX;
        p1.trans.y:=p1.trans.y+noffsY;
        pose1:=wobj.uframe;
        pose2.trans:=p1.trans;
        pose2.rot:=p1.rot;

        pose3:=PoseMult(pose1,pose2);
        p1.rot:=pose3.rot;
        p1.trans:=pose3.trans;

        retval:=p1;

        RETURN retval;
    ENDFUNC

    PROC pick_part()
        ConfJ\Off;
        ConfL\Off;
        MoveJ RelTool(apply_wobj(calcTarget(pPickCam,Wobj_Camera,nCamOffsX,nCamOffsY,nCamAngleRz),Wobj_Camera),0,0,-140),v2000,z10,tool0\WObj:=Wobj_Camera;
        MoveL RelTool(apply_wobj(calcTarget(pPickCam,Wobj_Camera,nCamOffsX,nCamOffsY,nCamAngleRz),Wobj_Camera),0,0,0),v500,fine,tool0\WObj:=Wobj_Camera;
        !griper
        MoveL RelTool(apply_wobj(calcTarget(pPickCam,Wobj_Camera,nCamOffsX,nCamOffsY,nCamAngleRz),Wobj_Camera),0,0,-5),v500,z1,tool0\WObj:=Wobj_Camera;
        MoveJ RelTool(apply_wobj(calcTarget(pPickCam,Wobj_Camera,nCamOffsX,nCamOffsY,nCamAngleRz),Wobj_Camera),0,0,-140),v2000,z20,tool0\WObj:=Wobj_Camera;
        ConfJ\on;
        ConfL\on;
    ENDPROC
ENDMODULE
