--function packetProc(pkt)
--    Console("We got a packet!");
--end
--
--Kernel.RegisterPacketHandler({packetProc})
--Kernel.PacketHandlers[#Kernel.PacketHandlers + 1] = Del(packetProc)
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

--Console("Getting Harddrive")
--drv = GetHarddrive(0)
--Console("Adding Dir '0:/foo'")
--drv.AddDirectory("0:/foo")
--Console("Adding Dir '0:/foo/bar'")
--drv.AddDirectory("0:/foo/bar")
--Console("Changing To Dir '0:/foo/bar'")
--drv.ChangeDirectory("0:/foo/bar")
--
--Console("Moving Up One Dir: " .. drv.GetFullPath(".."))
--Console("Moving Up Two Dirs: " .. drv.GetFullPath("../.."))
--Console("Moving Up One and Down One Dir: " .. drv.GetFullPath("../perfect"))
--drv.ChangeDirectory(drv.GetFullPath(".."))
--Console("Current Directory: " .. drv.currentWorkingDirectory)
