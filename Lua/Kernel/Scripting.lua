Console("Setting Up Scripting...")
--Scripting
function Run(script)
    AddProcessorOperation(ProcessorOperationMode.User, ProcessorOp(script, 0))
end
SetUserGlobalFunction("Run", Run)

function UserGetFile(name)
    return GetFile(name)
end
SetUserGlobalFunction("GetFile", UserGetFile)