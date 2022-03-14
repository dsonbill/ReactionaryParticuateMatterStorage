Networking = class("Networking")


function Networking:Send(card, memaddress, datatable, target)
    Kernel.SendData({card, memaddress, datatable, target})
end

function Networking:DequeueAll(mem, func)
    if mem:Read() ~= nil then
        if #mem:Read() > 0 then
            msgtable = mem:Read()
            if #msgtable > 0 then
                for i = #msgtable, 1, -1 do
                    msg = table.remove(msgtable)
                    if msg ~= nil then
                        func(msg)
                    else
                        break
                    end
                end
            end
        end
    end
end

function Networking:Dequeue(mem, func)
    if mem:Read() ~= nil then
        if #mem:Read() > 0 then
            msgtable = mem:Read()
            if #msgtable > 0 then
                msg = table.remove(msgtable)
                func(msg)
            end
        end
    end
end


return Networking