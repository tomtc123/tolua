local Button = UnityEngine.UI.Button
local Text = UnityEngine.UI.Text
local Image = UnityEngine.UI.Image

require "Class"

local M = class("UIBase")

function M:Awake()
    print("UIBase.Awake")
end

function M:Start()
    print("UIBase.Start")
end

function M:Update()
end

function M:FindComponent(trans, path, comp)
    local child = trans:FindChild(path)
    if child then
        return child:GetComponent(typeof(comp))
    else
        print("Can't find child"..path)
    end
    return nil
end

function M:FindButton(trans, path)
    return self:FindComponent(trans, path, Button)
end

function M:FindText(trans, path)
    return self:FindComponent(trans, path, Text)
end

function M:FindImage(trans, path)
    return self:FindComponent(trans, path, Image)
end

function M:AddClick(go, func)
    self.behaviour:AddClick(go.gameObject, func, self)
end

return M