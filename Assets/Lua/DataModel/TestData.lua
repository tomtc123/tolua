require "Class"

local base = require("DataModel/DataModelBase")

local M = class("TestData", base)

function M:Init()
    self.super:Init()
    print("TestData.Init")
end

return M