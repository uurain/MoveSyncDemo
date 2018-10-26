function Msg_ReqLogin(account)
 	local req = Protol.MsgDefine_pb.ReqLogin()
 	req.accountId = account
 	local reqBuff = req:SerializeToString()
    Network.SendMessage(Msg.Id.ReqLogin, reqBuff)
end

function Msg_ReqMove(ident, targetTable)
 	local req = Protol.MsgDefine_pb.ReqAckPlayerMove()
 	req.mover = ident
 	for i,v in ipairs(targetTable) do
 		local posTable = Protol.MsgDefine_pb.Vector3()
 		posTable.x = v.x
 		posTable.y = v.y
 		posTable.z = v.z
 		table.insert(req.target_pos, posTable)
 	end
 	local reqBuff = req:SerializeToString()
    Network.SendMessage(Msg.Id.ReqAckPlayerMove, reqBuff)
end

function Msg_ReqStop(ident, tPos)
 	local req = Protol.MsgDefine_pb.ReqAckStop()
 	req.mover = ident
 	req.source_pos.x = tPos.x
 	req.source_pos.y = tPos.y
 	req.source_pos.z = tPos.z
 	local reqBuff = req:SerializeToString()
    Network.SendMessage(Msg.Id.ReqAckStop, reqBuff)
end

function Msg_ReqUseSkill(skillId)
 	local req = Protol.MsgDefine_pb.ReqUseSkill()
 	req.skill_id = skillId
 	local reqBuff = req:SerializeToString()
    Network.SendMessage(Msg.Id.ReqUseSkill, reqBuff)
end

