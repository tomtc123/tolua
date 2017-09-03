require "Class"

local M = class("DataModelBase")

function M:Init()
    print("DataModelBase.Init")
    M.content = {}
end

function M:Clear()
    print("DataModelBase.Clear")
    M.content = {}
end

return M
