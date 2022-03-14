Console("Setting Up Networking Service...")
--Networking
function ProcessPacket(pkt)
    local pkg = json.parse(pkt.data)
    local data = json.parse(pkg["data"])
    if MemOpRead(0, "NET-" .. pkg["address"]) ~= nil then
        local memtable = MemOpRead(0, "NET-" .. pkg["address"])
        memtable[#memtable + 1] = data
        MemOpWrite(0, "NET-" .. pkg["address"], memtable)
    else
        local memtable = {}
        memtable[#memtable + 1] = data
        MemOpWrite(0, "NET-" .. pkg["address"], memtable)
    end
end

function HandlePackets()
    -- Each network card 
    for i = 0, GetNetcardCount() - 1, 1 do
        local pkt = ReadPacket(i)
        if pkt ~= nil then
            ProcessPacket(pkt)
        end
    end
end


--Start Service
ServiceStart(HandlePackets);