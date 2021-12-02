-- 写真撮影用カメラのサンプル
-- 
-- ※注意※
-- * 本サンプルでは、カメラで撮影した写真と写真のディスプレイを表示するマテリアルが割り当てられているメッシュは、
--   16:9 で UV 割り当てされていることが期待されています
-- 
-- * ズームアウトボタンをUseすると、カメラがズームアウトします
-- * ズームインボタンをUseすると、カメラがズームインします
-- * カメラ本体をUseすると、写真を撮影します
-- * 撮影される写真のプレビューは、カメラ本体の背面のディスプレイに表示されます
-- * 撮影した写真は、カメラ本体下のディスプレイに表示されます
-- * 上記すべての操作は、ネットワーク上で同期されます

local instanceId = vci.assets.GetInstanceId()

local cameraName = "Camera"
local cameraLensAnchorName = "LensAnchor"

local wideButtonName = "ButtonWide"
local wideButtonTransform = vci.assets.GetTransform(wideButtonName)
local wideButtonAnchorTransform = vci.assets.GetTransform("ButtonWideAnchor")
local isWideButtonUsed = false

local teleButtonName = "ButtonTele"
local teleButtonTransform = vci.assets.GetTransform(teleButtonName)
local teleButtonAnchorTransform = vci.assets.GetTransform("ButtonTeleAnchor")
local isTeleButtonUsed = false

local fovStateName = "FOV"
local initialFov = 60
local minFov = 10
local maxFov = 170
local fovStep = 2

local takePhotoMessageName = "message_take_photo"

local previewMaterialName = "Display"
local photoMaterialName = "Photo"

local isInitialized = false
local photographyCamera = nil

-- 写真撮影時のコールバック
-- * ExportPhotographyCamera.SetOnTakePhotoCallbackにこの関数を渡すことで、 
--   ExportPhotographyCamera.TakePhotograph実行時にこの関数が呼ばれます。
-- * 引数のphotoMetadataの中に、撮影したテクスチャを示すIDが入っています。
-- * そのテクスチャIDをvci.assets.material.SetTextureに渡すことで写真を表示します
-- * また、撮影した写真は5秒間の間のみ表示され、その後真っ黒になります。
function onTakePhotoCallback(photoMetadata)
    local photoTextureId = photoMetadata.textureId
    vci.assets.material.SetTexture(photoMaterialName, photoTextureId)
end

-- 写真撮影の message 受け取り時に実行される関数
-- * リモート側でこのVCIがUseされたときに実行されます
function onTakePhotoMessageReceived(sender, name, messnilge)
    photographyCamera.TakePhotograph()
end

-- 写真撮影用カメラ生成
-- * 写真撮影用カメラが生成され、"LensAnchor"のTransformに追従するようになります。
local lensTransform = vci.assets.GetTransform(cameraLensAnchorName)
photographyCamera = vci.cameraSystem.CreatePhotographyCamera(lensTransform)

-- プレビュー描画
-- * GetCameraPreviewTextureIdでカメラのプレビューのテクスチャを示すIDを取得します。
-- * このテクスチャIDをvci.assets.material.SetTextureに渡すことでプレビューを表示します。
local previewTextureId = photographyCamera.GetCameraPreviewTextureId()
vci.assets.material.SetTexture(previewMaterialName, previewTextureId)

-- 写真撮影時のコールバックをセット
-- * ExportPhotographyCamera.TakePhotograph実行時に、ここで渡した関数が実行されます。
photographyCamera.SetOnTakePhotoCallback(onTakePhotoCallback)

-- アイテム内同期変数を初期化
-- * 自身がこのVCIの所有権を持っているときのみ実行します。
if vci.assets.IsMine then
    local isFovInitialized = vci.state.Get(fovStateName) ~= nil
-- カメラの fov が初期化されていない場合、初期化処理を実行する
    if not isFovInitialized then
        print("initialize fov...")
        vci.state.Set(fovStateName, initialFov)
    end
end


-- カメラのNear Clip Planeと垂直FOVの初期値をセット
initialFov = vci.state.Get(fovStateName)
photographyCamera.SetVerticalFieldOfView(initialFov)
-- 必要ならば、near clip plane を任意の値にセットする
-- photographyCamera.SetNearClipPlane(initialNearClip)

-- 写真撮影のメッセージを受け取る
-- * リモート側から送信された、"message_take_photo"という名前のメッセージを受信したときに、
--   ここで渡した関数が実行されます。
vci.message.On(takePhotoMessageName, onTakePhotoMessageReceived)

-- カメラの初期化完了
isInitialized = true

function updateAll()
    -- ズームアウトのボタンをカメラ本体に追従させる
    -- * 追従処理は、ズームアウトボタンの所有権を持っているユーザーの環境上でのみ行われます。
    if wideButtonTransform.IsMine then
        wideButtonAnchorPos = wideButtonAnchorTransform.GetPosition()
        wideButtonTransform.SetPosition(wideButtonAnchorPos)
        wideButtonAnchorRot = wideButtonAnchorTransform.GetRotation()
        wideButtonTransform.SetRotation(wideButtonAnchorRot)
    end    

    -- ズームインのボタンをカメラ本体に追従させる
    -- * 追従処理は、ズームインボタンの所有権を持っているユーザーの環境上でのみ行われます。
    if teleButtonTransform.IsMine then
        teleButtonAnchorPos = teleButtonAnchorTransform.GetPosition()
        teleButtonTransform.SetPosition(teleButtonAnchorPos)
        teleButtonAnchorRot = teleButtonAnchorTransform.GetRotation()
        teleButtonTransform.SetRotation(teleButtonAnchorRot)
    end

    -- カメラの初期化が終わってない場合、これ以降の処理をスキップする
    if not isInitialized then
        return
    end

    -- vci.state から垂直FOVを取得してローカルに反映する
    local currentFov = vci.state.Get(fovStateName)
    photographyCamera.SetVerticalFieldOfView(currentFov)

    -- ズームアウトボタンが押されている場合、vci.stateのFOVを更新する
    if isWideButtonUsed then
        local updatedFov = math.min(maxFov, currentFov + fovStep)
        vci.state.Set(fovStateName, updatedFov)
    end

    -- ズームインボタンが押されている場合、vci.stateのFOVを更新する
    if isTeleButtonUsed then
        local updatedFov = math.max(minFov, currentFov - fovStep)
        vci.state.Set(fovStateName, updatedFov)
    end
end

function onUse(item)
    -- カメラの初期化が終わってない場合、これ以降の処理をスキップする
    if not isInitialized then
        return
    end

    -- カメラ本体がUseされた
    if item == cameraName then
        -- "message_take_photo"メッセージをリモート側に送信する
        -- * instanceIdを指定することで、自身にのみメッセージを送信します。
        vci.message.EmitWithId(takePhotoMessageName, nil, instanceId)
    end

    -- ズームアウトボタンがUseされた
    if item == wideButtonName then
        isWideButtonUsed = true
    end

    -- ズームインボタンがUseされた
    if item == teleButtonName then
        isTeleButtonUsed = true
    end
end

function onUnuse(item)
    -- カメラの初期化が終わってない場合、これ以降の処理をスキップする
    if not isInitialized then
        return
    end

    -- ズームアウトボタンがUnuseされた
    if item == wideButtonName then
        isWideButtonUsed = false
    end

    -- ズームインボタンがUnuseされた
    if item == teleButtonName then
        isTeleButtonUsed = false
    end
end