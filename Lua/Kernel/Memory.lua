Console("Setting Up Memory...")
--Memory
function MemRead(address)
    return MemOpRead(0, address)
end
SetUserGlobalFunction("MemRead", MemRead)

function MemWrite(address, value)
    MemOpWrite(0, address, value)
end
SetUserGlobalFunction("MemWrite", MemWrite)