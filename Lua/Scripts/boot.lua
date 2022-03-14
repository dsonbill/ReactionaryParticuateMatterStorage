-- class = require '30log'
-- split = require 'split'
-- 
-- Console("Loading Kernel V0.0.0.3")
-- Console()
-- 
-- -- System
-- Console("Loading Core System")
-- require 'Kernel.Memory'
-- require 'Kernel.Scripting'
-- require 'Kernel.Networking'
-- require 'Kernel.IO'
-- Console()
-- 
-- -- Services
-- Console("Loading Services")
-- require 'Kernel.Services.Networking'
-- Console()

-- Op.Add(Op.Kernel(Internal.GetFile("Scripts/boot.lua")))
-- Op.Add(Op.Kernel(Internal.GetFile("Scripts/boot.lua")))


-- x = 1 + 2

-- Internal.SysLog("Value is: " .. x)

Internal.SysLog("A double-edged test");
GPU.Console("Running Boot Script");

-- CPU.Add(CPU.Kernel(Internal.GetFile("Scripts/boot.lua")))