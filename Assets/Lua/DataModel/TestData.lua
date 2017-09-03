require "Class"

local base = require("DataModel/DataModelBase")

local M = class("TestData", base)

function M:Init()
    M.super.Init(self)
    print("TestData.Init")
end

return M