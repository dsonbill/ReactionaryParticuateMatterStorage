Console("Setting Up IO...")
--IO
drives = {}
for i = 0, GetHarddriveCount() - 1, 1 do
    drives[i + 1] = GetHarddrive(i)
end

function GetDriveNumber(fullPath)
    local drivestr = split(fullPath, "/")[1]
    return tonumber( string.sub(drivestr, 1, string.len(drivestr) - 1) )
end

function AddDirectory(fullPath)
    local drivenumber = GetDriveNumber(fullPath)
    drives[drivenumber + 1].AddDirectory(fullPath)
end
SetUserGlobalFunction("AddDirectory", AddDirectory)

function DeleteDirectory(fullPath)
    local drivenumber = GetDriveNumber(fullPath)
    drives[drivenumber + 1].DeleteDirectory(fullPath)
end
SetUserGlobalFunction("DeleteDirectory", DeleteDirectory)

function WriteFile(fullPath, file)
    local drivenumber = GetDriveNumber(fullPath)
    drives[drivenumber + 1].WriteFile(fullPath, file)
end
SetUserGlobalFunction("WriteFile", WriteFile)

function DeleteFile(fullPath)
    drivenumber = GetDriveNumber(fullPath)
    drives[drivenumber + 1].DeleteFile(fullPath)
end
SetUserGlobalFunction("DeleteFile", DeleteFile)

function ReadFile(fullPath)
    drivenumber = GetDriveNumber(fullPath)
    drives[drivenumber + 1].ReadFile(fullPath)
end
SetUserGlobalFunction("ReadFile", ReadFile)

function IsFile(fullPath)
    drivenumber = GetDriveNumber(fullPath)
    drives[drivenumber + 1].IsFile(fullPath)
end
SetUserGlobalFunction("IsFile", IsFile)

function IsDirectory(fullPath)
    drivenumber = GetDriveNumber(fullPath)
    drives[drivenumber + 1].IsDirectory(fullPath)
end
SetUserGlobalFunction("IsDirectory", IsDirectory)

function Exists(fullPath)
    drivenumber = GetDriveNumber(fullPath)
    drives[drivenumber + 1].Exists(fullPath)
end
SetUserGlobalFunction("Exists", Exists)

function ParentExists(fullPath)
    drivenumber = GetDriveNumber(fullPath)
    drives[drivenumber + 1].ParentExists(fullPath)
end
SetUserGlobalFunction("ParentExists", ParentExists)