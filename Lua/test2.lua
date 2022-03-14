sys = require 'User.System'()

mem = sys:Memory()
mem:Prefix("NET-firstsend")

net = sys:Networking()


function PrintMsgType(msg)
    Console("Got Messag with msgtype: " .. msg.msgtype)
end
net:DequeueAll(mem, PrintMsgType)

sys:Run("test2.lua")