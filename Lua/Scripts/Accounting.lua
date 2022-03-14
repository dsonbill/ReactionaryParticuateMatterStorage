--Accounting.lua
sys = require 'User.System'()

mem = sys:Memory()
mem:Prefix("ACCT-")

Console("Checking Existing Accounts...")
if mem:Read("Accounts") ~= nil then
    accountTable = mem:Read("Accounts")
    for i = 1, #accountTable, 1 do
        Console("Have Account: " .. accountTable[i])
    end
end

Console("Adding New Accounts...")
if mem:Read("Accounts") ~= nil then
    accountTable = mem:Read("Accounts")
    for i = #accountTable + 1, #accountTable + 5, 1 do
        accountTable[i] = "Account-" .. i
        Console("Adding Account " .. i .. ": " .. accountTable[i])
    end
    mem:Write("Accounts", accountTable)
else
    accountTable = {}
    for i = #accountTable + 1, #accountTable + 5, 1 do
        accountTable[i] = "Account-" .. i
        Console("Adding Account " .. i .. ": " .. accountTable[i])
    end
    mem:Write("Accounts", accountTable)
end


Console("Sending Transfers...")
Console("Ending...")

sys:Run("Scripts/Accounting.lua")