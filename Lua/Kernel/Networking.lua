Console("Setting Up Networking...")
--Networking
function SendData(card, memaddress, datatable, target)
    local pkg = { address=memaddress, data=json.serialize(datatable) }
    SendPacket(card, Packet(json.serialize(pkg), GetGuid(), target))
end
SetUserGlobalFunction("SendData", SendData)

function GetGuidFromName(name)
    return GetGuidFromHostname(name)
end
SetUserGlobalFunction("GetGuidFromHostname", GetGuidFromName)