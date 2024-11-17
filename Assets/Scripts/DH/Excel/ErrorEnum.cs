using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorEnum : MonoSingleton<ErrorEnum>
{
    public string GetErrorCode(ErrorCodeEnum errorCode) => errorCode switch
    {
        ErrorCodeEnum.QuickJoinLobby => "\"QuickJoinLobbyFail\"",
        ErrorCodeEnum.CodeJoinLobby => "\"CodeJoinLobbyFail\"",
        ErrorCodeEnum.CreateLobbyFail_Name => "\"CreateLobbyFail_Name\"",
        ErrorCodeEnum.CreateLobbyFail_Case => "\"CreateLobbyFail_Case\"",
        _ => throw new Exception("ErrorCode not Defined ")
    };
}

public enum ErrorCodeEnum
{
    QuickJoinLobby,
    CodeJoinLobby,
    CreateLobbyFail_Name,
    CreateLobbyFail_Case,
}
