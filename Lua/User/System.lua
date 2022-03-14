class = require '30log';


System = class("System")
Memory = require 'User.Memory'
Networking = require 'User.Networking'
IO = require 'User.IO'


function System:Memory()
    return Memory()
end

function System:Networking()
    return Networking()
end

function System:IO()
    return IO()
end


function System:Run(name)
    Kernel.Run({Kernel.GetFile({name})})
end

function System:Eval(script)
    Kernel.Run({script})
end

function System:GetFile(name)
    return Kernel.GetFile({name})
end


return System