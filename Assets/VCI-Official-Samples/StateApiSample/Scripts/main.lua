local button1Name = "Button1"
local button2Name = "Button2"
local button1MaterialName = button1Name
local button2MaterialName = button2Name
local currentStateTextName = "CurrentState"
local currentStateMaterialName = currentStateTextName

local button1Color = vci.assets.material.GetColor(button1MaterialName)
local button2Color = vci.assets.material.GetColor(button2MaterialName)
local defaultColor = vci.assets.material.GetColor(currentStateMaterialName)

local myStateName = "MyState"
local localMyState

local transforms = {}
transforms.board = vci.assets.GetTransform("Board")
transforms.buttons = {
    vci.assets.GetTransform(button1Name),
    vci.assets.GetTransform(button2Name)
}

-- ボタンのTransformを板に追従させる
local function syncTransform()
    local position = transforms.board.GetPosition()
    local rotation = transforms.board.GetRotation()
    for i = 1, #(transforms.buttons) do
        local transform = transforms.buttons[i]
        transform.SetPosition(position)
        transform.SetRotation(rotation)
    end
end

-- state値を可視化
local function showState(state)
    local color
    if state == 1 then
        color = button1Color
    elseif state == 2 then
        color = button2Color
    else
        color = defaultColor
    end
    vci.assets.material.SetColor(currentStateMaterialName, color)
    vci.assets.SetText(currentStateTextName, tostring(state))
end

-- ボタンを触るとstateを変更する
function onUse(name)
    if name == button1Name then
        vci.state.Set(myStateName, 1)
    elseif name == button2Name then
        vci.state.Set(myStateName, 2)
    end
end

function update()
    syncTransform()
end

function updateAll()
    -- state値を監視して更新があれば表示を更新
    local upstreamMyState = vci.state.Get(myStateName)
    if upstreamMyState ~= localMyState then
        showState(upstreamMyState)
        localMyState = upstreamMyState
    end
end

-- サーバー上のstate値で表示を初期化
-- * 全員退出した際にこの値が揮発するかはサーバー側の実装により変化する
-- * ルームの場合: 揮発しない
-- * スタジオの場合: 揮発する
localMyState = vci.state.Get(myStateName)
showState(localMyState)