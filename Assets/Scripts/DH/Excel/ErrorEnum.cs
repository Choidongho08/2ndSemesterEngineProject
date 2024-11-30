using System;

public class ErrorEnum : MonoSingleton<ErrorEnum>
{
    public string GetErrorCode(ErrorCodeEnum errorCode) => errorCode switch
    {
        ErrorCodeEnum.QuickJoinLobbyFail => "QuickJoinLobbyFail",
        ErrorCodeEnum.CodeJoinLobbyFail_Code => "CodeJoinLobbyFail_Code",
        ErrorCodeEnum.CreateLobbyFail_Name => "CreateLobbyFail_Name",
        ErrorCodeEnum.CreateLobbyFail_Case => "CreateLobbyFail_Case",
        ErrorCodeEnum.ChangePlayerNameFail => "ChangePlayerNameFail",
        ErrorCodeEnum.ChangePlayerNameFail_Length => "ChangePlayerNameFail_Length",
        ErrorCodeEnum.CodeJoinLobbyFail_Empty => "CodeJoinLobbyFail_Empty",
        ErrorCodeEnum.ManyLobbyRequests => "ManyLobbyRequests",
        ErrorCodeEnum.YouAreNotHost => "YouAreNotHost",
        ErrorCodeEnum.LobbyPlayerReady => "LobbyPlayerReady",
        _ => throw new Exception("ErrorCode not Defined ")
    };
}

public enum ErrorCodeEnum
{
    QuickJoinLobbyFail,
    CodeJoinLobbyFail_Empty,
    CodeJoinLobbyFail_Code,
    CreateLobbyFail_Name,
    CreateLobbyFail_Case,
    ChangePlayerNameFail,
    ChangePlayerNameFail_Length,
    ManyLobbyRequests,
    YouAreNotHost,
    LobbyPlayerReady,

}
