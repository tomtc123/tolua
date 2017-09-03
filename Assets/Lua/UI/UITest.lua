require "Class"

local base = require("UI/UIBase")

local M = class("UITest", base)

function M:Awake()
    M.super.Awake(self)
    print("UITest.Awake")
    self.button = self:FindButton(self.transform, "Button")
    self.label = self:FindText(self.transform, "Button/Text")
    self:AddClick(self.button, M.OnClick)
end

function M:Start()
    M.super.Start(self)
    print("UITest.Start")
end

function M:Update()
end

function M:OnClick(go)
    print("click button")
    self.label.text = "HelloWorld"
end


return M