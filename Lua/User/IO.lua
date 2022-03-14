IO = class("IO")


function IO:AddDirectory(fullPath)
    Kernel.AddDirectory({fullPath})
end

function IO:DeleteDirectory(fullPath)
    Kernel.DeleteDirectory({fullPath})
end

function IO:WriteFile(fullPath, file)
    Kernel.WriteFile({fullPath, file})
end

function IO:DeleteFile(fullPath)
    Kernel.DeleteFile({fullPath})
end

function IO:ReadFile(fullPath)
    Kernel.ReadFile({fullPath})
end

function IO:IsFile(fullPath)
    Kernel.IsFile({fullPath})
end

function IO:IsDirectory(fullPath)
    Kernel.IsDirectory({fullPath})
end

function IO:Exists(fullPath)
    Kernel.Exists({fullPath})
end

function IO:ParentExists(fullPath)
    Kernel.ParentExists({fullPath})
end


return IO