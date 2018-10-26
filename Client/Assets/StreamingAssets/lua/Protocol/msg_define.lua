Msg = {
	Id = {
		ReqLogin = 0,
		AckLogin = 1,
		AckPlayerEntryList = 2,
		AckPlayerLeaveList = 3,
		AckPublicPropertyList = 4,
		AckSwapScene = 5,
		ReqAckStop = 6,
		ReqAckPlayerMove = 7,
		AckSkillFail = 8,
		AckSkillStart = 9,
		AckSkillResult = 10,
		AckSkillStop = 11,
		ReqUseSkill = 12,
	},
	Func = {
		[0] = function (p)
		    local msg = Protol.MsgDefine_pb.ReqLogin()
		    msg:ParseFromString(p)
		    return msg
		end,
		[1] = function (p)
		    local msg = Protol.MsgDefine_pb.AckLogin()
		    msg:ParseFromString(p)
		    return msg
		end,
		[2] = function (p)
		    local msg = Protol.MsgDefine_pb.AckPlayerEntryList()
		    msg:ParseFromString(p)
		    return msg
		end,
		[3] = function (p)
		    local msg = Protol.MsgDefine_pb.AckPlayerLeaveList()
		    msg:ParseFromString(p)
		    return msg
		end,
		[4] = function (p)
		    local msg = Protol.MsgDefine_pb.AckPublicPropertyList()
		    msg:ParseFromString(p)
		    return msg
		end,
		[5] = function (p)
		    local msg = Protol.MsgDefine_pb.AckSwapScene()
		    msg:ParseFromString(p)
		    return msg
		end,
		[6] = function (p)
		    local msg = Protol.MsgDefine_pb.ReqAckStop()
		    msg:ParseFromString(p)
		    return msg
		end,
		[7] = function (p)
		    local msg = Protol.MsgDefine_pb.ReqAckPlayerMove()
		    msg:ParseFromString(p)
		    return msg
		end,
		[8] = function (p)
		    local msg = Protol.MsgDefine_pb.AckSkillFail()
		    msg:ParseFromString(p)
		    return msg
		end,
		[9] = function (p)
		    local msg = Protol.MsgDefine_pb.AckSkillStart()
		    msg:ParseFromString(p)
		    return msg
		end,
		[10] = function (p)
		    local msg = Protol.MsgDefine_pb.AckSkillResult()
		    msg:ParseFromString(p)
		    return msg
		end,
		[11] = function (p)
		    local msg = Protol.MsgDefine_pb.AckSkillStop()
		    msg:ParseFromString(p)
		    return msg
		end,
		[12] = function (p)
		    local msg = Protol.MsgDefine_pb.ReqUseSkill()
		    msg:ParseFromString(p)
		    return msg
		end,
	},
}