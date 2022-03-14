Memory = class("Memory")


function Memory:Prefix(prefix)
    self.prefix = prefix
end

function Memory:Read(address)
    if address ~= nil then
        return Kernel.MemRead({self.prefix .. address})
    else
        return Kernel.MemRead({self.prefix})
    end
end

function Memory:Write(address, value)
    if address ~= nil then
        Kernel.MemWrite({self.prefix .. address, value})
    else
        Kernel.MemWrite({self.prefix, value})
    end
end


return Memory