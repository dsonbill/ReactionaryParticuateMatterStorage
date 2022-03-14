sys = require 'User.System'()
net = sys:Networking()

local targetGuid = Kernel.GetGuidFromHostname({"0.1"})
local msg =
{
    msgtype="openacct",
    data=
    {
        name="Foo",
        password="123abc"
    }
}


net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)

net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)

net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)

net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)

net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)

net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)

net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)
net:Send(0, "firstsend", msg, targetGuid)

