Console();
Console();
Console();
Console();
Console("Voltage: " .. GetVoltage());
Console("Amperage: " .. GetAmperage());
Console("Watts: " .. GetVoltage() * GetAmperage());
AddProcessorOperation(ProcessorOperationMode.Kernel, ProcessorOp(GetFile("Multimeter/voltamps.lua"), 0))

