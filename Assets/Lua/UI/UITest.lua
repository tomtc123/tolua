require "Class"

local base = require("UI/UIBase")

local M = class("UITest", base)

local dataModelManager = require("DataModel/DataModelManager")

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

    M.TestCortinue()
end

function M:Update()
end

function M:OnClick(go)
    print("click button")
    self.label.text = "HelloWorld"
end

function M.CoFunc()
    print('Coroutine started')    
    for i = 0, 10, 1 do
        --print(fib(i))                    
        coroutine.wait(0.1)						
    end	
	print("current frameCount: "..Time.frameCount)
	coroutine.step()
	print("yield frameCount: "..Time.frameCount)

	local www = UnityEngine.WWW("http://www.baidu.com")
	coroutine.www(www)
	local s = tolua.tolstring(www.bytes)
	print(s:sub(1, 128))
    print('Coroutine ended')
end

function M.TestCortinue()	
    coroutine.start(M.CoFunc)
end

return M