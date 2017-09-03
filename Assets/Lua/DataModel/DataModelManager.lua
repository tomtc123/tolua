local M = {}

local modelName = require("DataModel/DataModelName")

function M.RegisterAll()
    M.allDataModel = {}
    for i, name in ipairs(modelName) do
        local model = require("DataModel/"..name).New()
        model:Clear()
        model:Init()
        M.allDataModel[name] = model
    end
end

function M.Clear()
    for k, model in pairs(M.allDataModel) do
        model:Clear()
    end
end

M.RegisterAll()

return M