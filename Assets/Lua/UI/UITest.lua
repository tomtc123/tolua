require "Class"

local base = require("UI/UIBase")

local M = class("UITest", base)

local dataModelManager = require("DataModel/DataModelManager")

function M:Awake()
    self.super:Awake()
    print("UITest.Awake")
    self.button = self:FindButton(self.transform, "Button")
    self.label = self:FindText(self.transform, "Button/Text")
    self:AddClick(self.button, M.OnClick)
end

function M:Start()
    self.super:Start()
    print("UITest.Start")

    --self:TestCortinue()
end

function M:Update()
end

function M:OnClick(go)
    print("click button")
    self.label.text = "HelloWorld"
end

function M:CoFunc(a,b)
    print(a, b)
	local c = 1
    local t = 0

	while t < 1 do
        t = t + Time.deltaTime
		coroutine.wait(1) 
		print("Count: "..c, t)
		c = c + 1
	end
end

function M:TestCortinue()	
    coroutine.start(function()
        self:CoFunc(1, 2)
    end)
end

return M