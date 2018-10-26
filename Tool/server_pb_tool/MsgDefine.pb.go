// Code generated by protoc-gen-go. DO NOT EDIT.
// source: MsgDefine.proto

package msg

import proto "github.com/golang/protobuf/proto"
import fmt "fmt"
import math "math"

// Reference imports to suppress errors if they are not otherwise used.
var _ = proto.Marshal
var _ = fmt.Errorf
var _ = math.Inf

// This is a compile-time assertion to ensure that this generated file
// is compatible with the proto package it is being compiled against.
// A compilation error at this line likely means your copy of the
// proto package needs to be updated.
const _ = proto.ProtoPackageIsVersion2 // please upgrade the proto package

type UseSkillResult int32

const (
	UseSkillResult_CAST_SKILL_SUCCESS      UseSkillResult = 0
	UseSkillResult_CAST_SKILL_NOT_EXIST    UseSkillResult = 1
	UseSkillResult_CAST_SKILL_IN_CD        UseSkillResult = 2
	UseSkillResult_CAST_SKILL_NOTENOUGH_HP UseSkillResult = 3
	UseSkillResult_CAST_SKILL_NOTENOUGH_MP UseSkillResult = 4
	UseSkillResult_CAST_SKILL_FAR_DISTANCE UseSkillResult = 5
	UseSkillResult_CAST_SKILL_NO_TARGET    UseSkillResult = 6
	UseSkillResult_CAST_SKILL_DISABLED     UseSkillResult = 7
	UseSkillResult_CAST_SKILL_BAD_TARGET   UseSkillResult = 8
	UseSkillResult_CAST_SKILL_CASTING      UseSkillResult = 9
	UseSkillResult_CAST_SKILL_UNKNOW_ERROR UseSkillResult = 10
)

var UseSkillResult_name = map[int32]string{
	0:  "CAST_SKILL_SUCCESS",
	1:  "CAST_SKILL_NOT_EXIST",
	2:  "CAST_SKILL_IN_CD",
	3:  "CAST_SKILL_NOTENOUGH_HP",
	4:  "CAST_SKILL_NOTENOUGH_MP",
	5:  "CAST_SKILL_FAR_DISTANCE",
	6:  "CAST_SKILL_NO_TARGET",
	7:  "CAST_SKILL_DISABLED",
	8:  "CAST_SKILL_BAD_TARGET",
	9:  "CAST_SKILL_CASTING",
	10: "CAST_SKILL_UNKNOW_ERROR",
}
var UseSkillResult_value = map[string]int32{
	"CAST_SKILL_SUCCESS":      0,
	"CAST_SKILL_NOT_EXIST":    1,
	"CAST_SKILL_IN_CD":        2,
	"CAST_SKILL_NOTENOUGH_HP": 3,
	"CAST_SKILL_NOTENOUGH_MP": 4,
	"CAST_SKILL_FAR_DISTANCE": 5,
	"CAST_SKILL_NO_TARGET":    6,
	"CAST_SKILL_DISABLED":     7,
	"CAST_SKILL_BAD_TARGET":   8,
	"CAST_SKILL_CASTING":      9,
	"CAST_SKILL_UNKNOW_ERROR": 10,
}

func (x UseSkillResult) Enum() *UseSkillResult {
	p := new(UseSkillResult)
	*p = x
	return p
}
func (x UseSkillResult) String() string {
	return proto.EnumName(UseSkillResult_name, int32(x))
}
func (x *UseSkillResult) UnmarshalJSON(data []byte) error {
	value, err := proto.UnmarshalJSONEnum(UseSkillResult_value, data, "UseSkillResult")
	if err != nil {
		return err
	}
	*x = UseSkillResult(value)
	return nil
}
func (UseSkillResult) EnumDescriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{0}
}

type DamageFlag int32

const (
	DamageFlag_DAMAGE_FLAG_DODGE     DamageFlag = 1
	DamageFlag_DAMAGE_FLAG_PARRY     DamageFlag = 2
	DamageFlag_DAMAGE_FLAG_CRIT      DamageFlag = 4
	DamageFlag_DAMAGE_FLAG_WITHSTAND DamageFlag = 8
)

var DamageFlag_name = map[int32]string{
	1: "DAMAGE_FLAG_DODGE",
	2: "DAMAGE_FLAG_PARRY",
	4: "DAMAGE_FLAG_CRIT",
	8: "DAMAGE_FLAG_WITHSTAND",
}
var DamageFlag_value = map[string]int32{
	"DAMAGE_FLAG_DODGE":     1,
	"DAMAGE_FLAG_PARRY":     2,
	"DAMAGE_FLAG_CRIT":      4,
	"DAMAGE_FLAG_WITHSTAND": 8,
}

func (x DamageFlag) Enum() *DamageFlag {
	p := new(DamageFlag)
	*p = x
	return p
}
func (x DamageFlag) String() string {
	return proto.EnumName(DamageFlag_name, int32(x))
}
func (x *DamageFlag) UnmarshalJSON(data []byte) error {
	value, err := proto.UnmarshalJSONEnum(DamageFlag_value, data, "DamageFlag")
	if err != nil {
		return err
	}
	*x = DamageFlag(value)
	return nil
}
func (DamageFlag) EnumDescriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{1}
}

type Vector2 struct {
	X                    *float32 `protobuf:"fixed32,1,req,name=x" json:"x,omitempty"`
	Y                    *float32 `protobuf:"fixed32,2,req,name=y" json:"y,omitempty"`
	XXX_NoUnkeyedLiteral struct{} `json:"-"`
	XXX_unrecognized     []byte   `json:"-"`
	XXX_sizecache        int32    `json:"-"`
}

func (m *Vector2) Reset()         { *m = Vector2{} }
func (m *Vector2) String() string { return proto.CompactTextString(m) }
func (*Vector2) ProtoMessage()    {}
func (*Vector2) Descriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{0}
}
func (m *Vector2) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_Vector2.Unmarshal(m, b)
}
func (m *Vector2) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_Vector2.Marshal(b, m, deterministic)
}
func (dst *Vector2) XXX_Merge(src proto.Message) {
	xxx_messageInfo_Vector2.Merge(dst, src)
}
func (m *Vector2) XXX_Size() int {
	return xxx_messageInfo_Vector2.Size(m)
}
func (m *Vector2) XXX_DiscardUnknown() {
	xxx_messageInfo_Vector2.DiscardUnknown(m)
}

var xxx_messageInfo_Vector2 proto.InternalMessageInfo

func (m *Vector2) GetX() float32 {
	if m != nil && m.X != nil {
		return *m.X
	}
	return 0
}

func (m *Vector2) GetY() float32 {
	if m != nil && m.Y != nil {
		return *m.Y
	}
	return 0
}

type Vector3 struct {
	X                    *float32 `protobuf:"fixed32,1,req,name=x" json:"x,omitempty"`
	Y                    *float32 `protobuf:"fixed32,2,req,name=y" json:"y,omitempty"`
	Z                    *float32 `protobuf:"fixed32,3,req,name=z" json:"z,omitempty"`
	XXX_NoUnkeyedLiteral struct{} `json:"-"`
	XXX_unrecognized     []byte   `json:"-"`
	XXX_sizecache        int32    `json:"-"`
}

func (m *Vector3) Reset()         { *m = Vector3{} }
func (m *Vector3) String() string { return proto.CompactTextString(m) }
func (*Vector3) ProtoMessage()    {}
func (*Vector3) Descriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{1}
}
func (m *Vector3) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_Vector3.Unmarshal(m, b)
}
func (m *Vector3) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_Vector3.Marshal(b, m, deterministic)
}
func (dst *Vector3) XXX_Merge(src proto.Message) {
	xxx_messageInfo_Vector3.Merge(dst, src)
}
func (m *Vector3) XXX_Size() int {
	return xxx_messageInfo_Vector3.Size(m)
}
func (m *Vector3) XXX_DiscardUnknown() {
	xxx_messageInfo_Vector3.DiscardUnknown(m)
}

var xxx_messageInfo_Vector3 proto.InternalMessageInfo

func (m *Vector3) GetX() float32 {
	if m != nil && m.X != nil {
		return *m.X
	}
	return 0
}

func (m *Vector3) GetY() float32 {
	if m != nil && m.Y != nil {
		return *m.Y
	}
	return 0
}

func (m *Vector3) GetZ() float32 {
	if m != nil && m.Z != nil {
		return *m.Z
	}
	return 0
}

type ReqLogin struct {
	AccountId            *string  `protobuf:"bytes,1,req,name=accountId" json:"accountId,omitempty"`
	XXX_NoUnkeyedLiteral struct{} `json:"-"`
	XXX_unrecognized     []byte   `json:"-"`
	XXX_sizecache        int32    `json:"-"`
}

func (m *ReqLogin) Reset()         { *m = ReqLogin{} }
func (m *ReqLogin) String() string { return proto.CompactTextString(m) }
func (*ReqLogin) ProtoMessage()    {}
func (*ReqLogin) Descriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{2}
}
func (m *ReqLogin) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_ReqLogin.Unmarshal(m, b)
}
func (m *ReqLogin) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_ReqLogin.Marshal(b, m, deterministic)
}
func (dst *ReqLogin) XXX_Merge(src proto.Message) {
	xxx_messageInfo_ReqLogin.Merge(dst, src)
}
func (m *ReqLogin) XXX_Size() int {
	return xxx_messageInfo_ReqLogin.Size(m)
}
func (m *ReqLogin) XXX_DiscardUnknown() {
	xxx_messageInfo_ReqLogin.DiscardUnknown(m)
}

var xxx_messageInfo_ReqLogin proto.InternalMessageInfo

func (m *ReqLogin) GetAccountId() string {
	if m != nil && m.AccountId != nil {
		return *m.AccountId
	}
	return ""
}

type AckLogin struct {
	ErrorCode            *int32   `protobuf:"varint,1,req,name=errorCode" json:"errorCode,omitempty"`
	Uid                  *int32   `protobuf:"varint,2,opt,name=uid" json:"uid,omitempty"`
	XXX_NoUnkeyedLiteral struct{} `json:"-"`
	XXX_unrecognized     []byte   `json:"-"`
	XXX_sizecache        int32    `json:"-"`
}

func (m *AckLogin) Reset()         { *m = AckLogin{} }
func (m *AckLogin) String() string { return proto.CompactTextString(m) }
func (*AckLogin) ProtoMessage()    {}
func (*AckLogin) Descriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{3}
}
func (m *AckLogin) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_AckLogin.Unmarshal(m, b)
}
func (m *AckLogin) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_AckLogin.Marshal(b, m, deterministic)
}
func (dst *AckLogin) XXX_Merge(src proto.Message) {
	xxx_messageInfo_AckLogin.Merge(dst, src)
}
func (m *AckLogin) XXX_Size() int {
	return xxx_messageInfo_AckLogin.Size(m)
}
func (m *AckLogin) XXX_DiscardUnknown() {
	xxx_messageInfo_AckLogin.DiscardUnknown(m)
}

var xxx_messageInfo_AckLogin proto.InternalMessageInfo

func (m *AckLogin) GetErrorCode() int32 {
	if m != nil && m.ErrorCode != nil {
		return *m.ErrorCode
	}
	return 0
}

func (m *AckLogin) GetUid() int32 {
	if m != nil && m.Uid != nil {
		return *m.Uid
	}
	return 0
}

type PlayerInfo struct {
	Atk                  *int32   `protobuf:"varint,6,opt,name=atk" json:"atk,omitempty"`
	Def                  *int32   `protobuf:"varint,7,opt,name=def" json:"def,omitempty"`
	Hp                   *int32   `protobuf:"varint,8,opt,name=hp" json:"hp,omitempty"`
	HpMax                *int32   `protobuf:"varint,9,opt,name=hpMax" json:"hpMax,omitempty"`
	MoveSpeed            *int32   `protobuf:"varint,10,opt,name=moveSpeed" json:"moveSpeed,omitempty"`
	Level                *int32   `protobuf:"varint,11,opt,name=level" json:"level,omitempty"`
	ModelClothes         *int32   `protobuf:"varint,12,opt,name=modelClothes" json:"modelClothes,omitempty"`
	Name                 *string  `protobuf:"bytes,13,opt,name=name" json:"name,omitempty"`
	XXX_NoUnkeyedLiteral struct{} `json:"-"`
	XXX_unrecognized     []byte   `json:"-"`
	XXX_sizecache        int32    `json:"-"`
}

func (m *PlayerInfo) Reset()         { *m = PlayerInfo{} }
func (m *PlayerInfo) String() string { return proto.CompactTextString(m) }
func (*PlayerInfo) ProtoMessage()    {}
func (*PlayerInfo) Descriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{4}
}
func (m *PlayerInfo) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_PlayerInfo.Unmarshal(m, b)
}
func (m *PlayerInfo) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_PlayerInfo.Marshal(b, m, deterministic)
}
func (dst *PlayerInfo) XXX_Merge(src proto.Message) {
	xxx_messageInfo_PlayerInfo.Merge(dst, src)
}
func (m *PlayerInfo) XXX_Size() int {
	return xxx_messageInfo_PlayerInfo.Size(m)
}
func (m *PlayerInfo) XXX_DiscardUnknown() {
	xxx_messageInfo_PlayerInfo.DiscardUnknown(m)
}

var xxx_messageInfo_PlayerInfo proto.InternalMessageInfo

func (m *PlayerInfo) GetAtk() int32 {
	if m != nil && m.Atk != nil {
		return *m.Atk
	}
	return 0
}

func (m *PlayerInfo) GetDef() int32 {
	if m != nil && m.Def != nil {
		return *m.Def
	}
	return 0
}

func (m *PlayerInfo) GetHp() int32 {
	if m != nil && m.Hp != nil {
		return *m.Hp
	}
	return 0
}

func (m *PlayerInfo) GetHpMax() int32 {
	if m != nil && m.HpMax != nil {
		return *m.HpMax
	}
	return 0
}

func (m *PlayerInfo) GetMoveSpeed() int32 {
	if m != nil && m.MoveSpeed != nil {
		return *m.MoveSpeed
	}
	return 0
}

func (m *PlayerInfo) GetLevel() int32 {
	if m != nil && m.Level != nil {
		return *m.Level
	}
	return 0
}

func (m *PlayerInfo) GetModelClothes() int32 {
	if m != nil && m.ModelClothes != nil {
		return *m.ModelClothes
	}
	return 0
}

func (m *PlayerInfo) GetName() string {
	if m != nil && m.Name != nil {
		return *m.Name
	}
	return ""
}

type PlayerEntryInfo struct {
	ObjectGuid           *int32      `protobuf:"varint,1,req,name=object_guid" json:"object_guid,omitempty"`
	X                    *float32    `protobuf:"fixed32,2,req,name=x" json:"x,omitempty"`
	Y                    *float32    `protobuf:"fixed32,3,req,name=y" json:"y,omitempty"`
	Z                    *float32    `protobuf:"fixed32,4,req,name=z" json:"z,omitempty"`
	R                    *int32      `protobuf:"varint,5,opt,name=r" json:"r,omitempty"`
	PlayerInfo           *PlayerInfo `protobuf:"bytes,6,req,name=player_info" json:"player_info,omitempty"`
	XXX_NoUnkeyedLiteral struct{}    `json:"-"`
	XXX_unrecognized     []byte      `json:"-"`
	XXX_sizecache        int32       `json:"-"`
}

func (m *PlayerEntryInfo) Reset()         { *m = PlayerEntryInfo{} }
func (m *PlayerEntryInfo) String() string { return proto.CompactTextString(m) }
func (*PlayerEntryInfo) ProtoMessage()    {}
func (*PlayerEntryInfo) Descriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{5}
}
func (m *PlayerEntryInfo) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_PlayerEntryInfo.Unmarshal(m, b)
}
func (m *PlayerEntryInfo) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_PlayerEntryInfo.Marshal(b, m, deterministic)
}
func (dst *PlayerEntryInfo) XXX_Merge(src proto.Message) {
	xxx_messageInfo_PlayerEntryInfo.Merge(dst, src)
}
func (m *PlayerEntryInfo) XXX_Size() int {
	return xxx_messageInfo_PlayerEntryInfo.Size(m)
}
func (m *PlayerEntryInfo) XXX_DiscardUnknown() {
	xxx_messageInfo_PlayerEntryInfo.DiscardUnknown(m)
}

var xxx_messageInfo_PlayerEntryInfo proto.InternalMessageInfo

func (m *PlayerEntryInfo) GetObjectGuid() int32 {
	if m != nil && m.ObjectGuid != nil {
		return *m.ObjectGuid
	}
	return 0
}

func (m *PlayerEntryInfo) GetX() float32 {
	if m != nil && m.X != nil {
		return *m.X
	}
	return 0
}

func (m *PlayerEntryInfo) GetY() float32 {
	if m != nil && m.Y != nil {
		return *m.Y
	}
	return 0
}

func (m *PlayerEntryInfo) GetZ() float32 {
	if m != nil && m.Z != nil {
		return *m.Z
	}
	return 0
}

func (m *PlayerEntryInfo) GetR() int32 {
	if m != nil && m.R != nil {
		return *m.R
	}
	return 0
}

func (m *PlayerEntryInfo) GetPlayerInfo() *PlayerInfo {
	if m != nil {
		return m.PlayerInfo
	}
	return nil
}

type AckPlayerEntryList struct {
	ObjectList           []*PlayerEntryInfo `protobuf:"bytes,1,rep,name=object_list" json:"object_list,omitempty"`
	XXX_NoUnkeyedLiteral struct{}           `json:"-"`
	XXX_unrecognized     []byte             `json:"-"`
	XXX_sizecache        int32              `json:"-"`
}

func (m *AckPlayerEntryList) Reset()         { *m = AckPlayerEntryList{} }
func (m *AckPlayerEntryList) String() string { return proto.CompactTextString(m) }
func (*AckPlayerEntryList) ProtoMessage()    {}
func (*AckPlayerEntryList) Descriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{6}
}
func (m *AckPlayerEntryList) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_AckPlayerEntryList.Unmarshal(m, b)
}
func (m *AckPlayerEntryList) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_AckPlayerEntryList.Marshal(b, m, deterministic)
}
func (dst *AckPlayerEntryList) XXX_Merge(src proto.Message) {
	xxx_messageInfo_AckPlayerEntryList.Merge(dst, src)
}
func (m *AckPlayerEntryList) XXX_Size() int {
	return xxx_messageInfo_AckPlayerEntryList.Size(m)
}
func (m *AckPlayerEntryList) XXX_DiscardUnknown() {
	xxx_messageInfo_AckPlayerEntryList.DiscardUnknown(m)
}

var xxx_messageInfo_AckPlayerEntryList proto.InternalMessageInfo

func (m *AckPlayerEntryList) GetObjectList() []*PlayerEntryInfo {
	if m != nil {
		return m.ObjectList
	}
	return nil
}

type AckPlayerLeaveList struct {
	ObjectList           []int32  `protobuf:"varint,1,rep,name=object_list" json:"object_list,omitempty"`
	XXX_NoUnkeyedLiteral struct{} `json:"-"`
	XXX_unrecognized     []byte   `json:"-"`
	XXX_sizecache        int32    `json:"-"`
}

func (m *AckPlayerLeaveList) Reset()         { *m = AckPlayerLeaveList{} }
func (m *AckPlayerLeaveList) String() string { return proto.CompactTextString(m) }
func (*AckPlayerLeaveList) ProtoMessage()    {}
func (*AckPlayerLeaveList) Descriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{7}
}
func (m *AckPlayerLeaveList) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_AckPlayerLeaveList.Unmarshal(m, b)
}
func (m *AckPlayerLeaveList) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_AckPlayerLeaveList.Marshal(b, m, deterministic)
}
func (dst *AckPlayerLeaveList) XXX_Merge(src proto.Message) {
	xxx_messageInfo_AckPlayerLeaveList.Merge(dst, src)
}
func (m *AckPlayerLeaveList) XXX_Size() int {
	return xxx_messageInfo_AckPlayerLeaveList.Size(m)
}
func (m *AckPlayerLeaveList) XXX_DiscardUnknown() {
	xxx_messageInfo_AckPlayerLeaveList.DiscardUnknown(m)
}

var xxx_messageInfo_AckPlayerLeaveList proto.InternalMessageInfo

func (m *AckPlayerLeaveList) GetObjectList() []int32 {
	if m != nil {
		return m.ObjectList
	}
	return nil
}

// 属性同步
type AckPublicPropertyList struct {
	Id                   *int32      `protobuf:"varint,1,req,name=id" json:"id,omitempty"`
	PlayerInfo           *PlayerInfo `protobuf:"bytes,2,req,name=player_info" json:"player_info,omitempty"`
	XXX_NoUnkeyedLiteral struct{}    `json:"-"`
	XXX_unrecognized     []byte      `json:"-"`
	XXX_sizecache        int32       `json:"-"`
}

func (m *AckPublicPropertyList) Reset()         { *m = AckPublicPropertyList{} }
func (m *AckPublicPropertyList) String() string { return proto.CompactTextString(m) }
func (*AckPublicPropertyList) ProtoMessage()    {}
func (*AckPublicPropertyList) Descriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{8}
}
func (m *AckPublicPropertyList) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_AckPublicPropertyList.Unmarshal(m, b)
}
func (m *AckPublicPropertyList) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_AckPublicPropertyList.Marshal(b, m, deterministic)
}
func (dst *AckPublicPropertyList) XXX_Merge(src proto.Message) {
	xxx_messageInfo_AckPublicPropertyList.Merge(dst, src)
}
func (m *AckPublicPropertyList) XXX_Size() int {
	return xxx_messageInfo_AckPublicPropertyList.Size(m)
}
func (m *AckPublicPropertyList) XXX_DiscardUnknown() {
	xxx_messageInfo_AckPublicPropertyList.DiscardUnknown(m)
}

var xxx_messageInfo_AckPublicPropertyList proto.InternalMessageInfo

func (m *AckPublicPropertyList) GetId() int32 {
	if m != nil && m.Id != nil {
		return *m.Id
	}
	return 0
}

func (m *AckPublicPropertyList) GetPlayerInfo() *PlayerInfo {
	if m != nil {
		return m.PlayerInfo
	}
	return nil
}

// 场景信息
type AckSwapScene struct {
	SceneId              *int32   `protobuf:"varint,1,req,name=scene_id" json:"scene_id,omitempty"`
	X                    *float32 `protobuf:"fixed32,2,opt,name=x" json:"x,omitempty"`
	Y                    *float32 `protobuf:"fixed32,3,opt,name=y" json:"y,omitempty"`
	Z                    *float32 `protobuf:"fixed32,4,opt,name=z" json:"z,omitempty"`
	R                    *int32   `protobuf:"varint,5,opt,name=r" json:"r,omitempty"`
	XXX_NoUnkeyedLiteral struct{} `json:"-"`
	XXX_unrecognized     []byte   `json:"-"`
	XXX_sizecache        int32    `json:"-"`
}

func (m *AckSwapScene) Reset()         { *m = AckSwapScene{} }
func (m *AckSwapScene) String() string { return proto.CompactTextString(m) }
func (*AckSwapScene) ProtoMessage()    {}
func (*AckSwapScene) Descriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{9}
}
func (m *AckSwapScene) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_AckSwapScene.Unmarshal(m, b)
}
func (m *AckSwapScene) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_AckSwapScene.Marshal(b, m, deterministic)
}
func (dst *AckSwapScene) XXX_Merge(src proto.Message) {
	xxx_messageInfo_AckSwapScene.Merge(dst, src)
}
func (m *AckSwapScene) XXX_Size() int {
	return xxx_messageInfo_AckSwapScene.Size(m)
}
func (m *AckSwapScene) XXX_DiscardUnknown() {
	xxx_messageInfo_AckSwapScene.DiscardUnknown(m)
}

var xxx_messageInfo_AckSwapScene proto.InternalMessageInfo

func (m *AckSwapScene) GetSceneId() int32 {
	if m != nil && m.SceneId != nil {
		return *m.SceneId
	}
	return 0
}

func (m *AckSwapScene) GetX() float32 {
	if m != nil && m.X != nil {
		return *m.X
	}
	return 0
}

func (m *AckSwapScene) GetY() float32 {
	if m != nil && m.Y != nil {
		return *m.Y
	}
	return 0
}

func (m *AckSwapScene) GetZ() float32 {
	if m != nil && m.Z != nil {
		return *m.Z
	}
	return 0
}

func (m *AckSwapScene) GetR() int32 {
	if m != nil && m.R != nil {
		return *m.R
	}
	return 0
}

type ReqAckStop struct {
	Mover                *int32   `protobuf:"varint,1,req,name=mover" json:"mover,omitempty"`
	SourcePos            *Vector3 `protobuf:"bytes,2,req,name=source_pos" json:"source_pos,omitempty"`
	XXX_NoUnkeyedLiteral struct{} `json:"-"`
	XXX_unrecognized     []byte   `json:"-"`
	XXX_sizecache        int32    `json:"-"`
}

func (m *ReqAckStop) Reset()         { *m = ReqAckStop{} }
func (m *ReqAckStop) String() string { return proto.CompactTextString(m) }
func (*ReqAckStop) ProtoMessage()    {}
func (*ReqAckStop) Descriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{10}
}
func (m *ReqAckStop) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_ReqAckStop.Unmarshal(m, b)
}
func (m *ReqAckStop) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_ReqAckStop.Marshal(b, m, deterministic)
}
func (dst *ReqAckStop) XXX_Merge(src proto.Message) {
	xxx_messageInfo_ReqAckStop.Merge(dst, src)
}
func (m *ReqAckStop) XXX_Size() int {
	return xxx_messageInfo_ReqAckStop.Size(m)
}
func (m *ReqAckStop) XXX_DiscardUnknown() {
	xxx_messageInfo_ReqAckStop.DiscardUnknown(m)
}

var xxx_messageInfo_ReqAckStop proto.InternalMessageInfo

func (m *ReqAckStop) GetMover() int32 {
	if m != nil && m.Mover != nil {
		return *m.Mover
	}
	return 0
}

func (m *ReqAckStop) GetSourcePos() *Vector3 {
	if m != nil {
		return m.SourcePos
	}
	return nil
}

type ReqAckPlayerMove struct {
	Mover                *int32     `protobuf:"varint,1,req,name=mover" json:"mover,omitempty"`
	TargetPos            []*Vector3 `protobuf:"bytes,2,rep,name=target_pos" json:"target_pos,omitempty"`
	XXX_NoUnkeyedLiteral struct{}   `json:"-"`
	XXX_unrecognized     []byte     `json:"-"`
	XXX_sizecache        int32      `json:"-"`
}

func (m *ReqAckPlayerMove) Reset()         { *m = ReqAckPlayerMove{} }
func (m *ReqAckPlayerMove) String() string { return proto.CompactTextString(m) }
func (*ReqAckPlayerMove) ProtoMessage()    {}
func (*ReqAckPlayerMove) Descriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{11}
}
func (m *ReqAckPlayerMove) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_ReqAckPlayerMove.Unmarshal(m, b)
}
func (m *ReqAckPlayerMove) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_ReqAckPlayerMove.Marshal(b, m, deterministic)
}
func (dst *ReqAckPlayerMove) XXX_Merge(src proto.Message) {
	xxx_messageInfo_ReqAckPlayerMove.Merge(dst, src)
}
func (m *ReqAckPlayerMove) XXX_Size() int {
	return xxx_messageInfo_ReqAckPlayerMove.Size(m)
}
func (m *ReqAckPlayerMove) XXX_DiscardUnknown() {
	xxx_messageInfo_ReqAckPlayerMove.DiscardUnknown(m)
}

var xxx_messageInfo_ReqAckPlayerMove proto.InternalMessageInfo

func (m *ReqAckPlayerMove) GetMover() int32 {
	if m != nil && m.Mover != nil {
		return *m.Mover
	}
	return 0
}

func (m *ReqAckPlayerMove) GetTargetPos() []*Vector3 {
	if m != nil {
		return m.TargetPos
	}
	return nil
}

type ReqUseSkill struct {
	SkillId              *int32   `protobuf:"varint,1,req,name=skill_id" json:"skill_id,omitempty"`
	XXX_NoUnkeyedLiteral struct{} `json:"-"`
	XXX_unrecognized     []byte   `json:"-"`
	XXX_sizecache        int32    `json:"-"`
}

func (m *ReqUseSkill) Reset()         { *m = ReqUseSkill{} }
func (m *ReqUseSkill) String() string { return proto.CompactTextString(m) }
func (*ReqUseSkill) ProtoMessage()    {}
func (*ReqUseSkill) Descriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{12}
}
func (m *ReqUseSkill) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_ReqUseSkill.Unmarshal(m, b)
}
func (m *ReqUseSkill) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_ReqUseSkill.Marshal(b, m, deterministic)
}
func (dst *ReqUseSkill) XXX_Merge(src proto.Message) {
	xxx_messageInfo_ReqUseSkill.Merge(dst, src)
}
func (m *ReqUseSkill) XXX_Size() int {
	return xxx_messageInfo_ReqUseSkill.Size(m)
}
func (m *ReqUseSkill) XXX_DiscardUnknown() {
	xxx_messageInfo_ReqUseSkill.DiscardUnknown(m)
}

var xxx_messageInfo_ReqUseSkill proto.InternalMessageInfo

func (m *ReqUseSkill) GetSkillId() int32 {
	if m != nil && m.SkillId != nil {
		return *m.SkillId
	}
	return 0
}

// 释放技能失败
type AckSkillFail struct {
	SkillId              *int32          `protobuf:"varint,1,req,name=skill_id" json:"skill_id,omitempty"`
	Result               *UseSkillResult `protobuf:"varint,2,req,name=result,enum=msg.UseSkillResult" json:"result,omitempty"`
	XXX_NoUnkeyedLiteral struct{}        `json:"-"`
	XXX_unrecognized     []byte          `json:"-"`
	XXX_sizecache        int32           `json:"-"`
}

func (m *AckSkillFail) Reset()         { *m = AckSkillFail{} }
func (m *AckSkillFail) String() string { return proto.CompactTextString(m) }
func (*AckSkillFail) ProtoMessage()    {}
func (*AckSkillFail) Descriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{13}
}
func (m *AckSkillFail) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_AckSkillFail.Unmarshal(m, b)
}
func (m *AckSkillFail) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_AckSkillFail.Marshal(b, m, deterministic)
}
func (dst *AckSkillFail) XXX_Merge(src proto.Message) {
	xxx_messageInfo_AckSkillFail.Merge(dst, src)
}
func (m *AckSkillFail) XXX_Size() int {
	return xxx_messageInfo_AckSkillFail.Size(m)
}
func (m *AckSkillFail) XXX_DiscardUnknown() {
	xxx_messageInfo_AckSkillFail.DiscardUnknown(m)
}

var xxx_messageInfo_AckSkillFail proto.InternalMessageInfo

func (m *AckSkillFail) GetSkillId() int32 {
	if m != nil && m.SkillId != nil {
		return *m.SkillId
	}
	return 0
}

func (m *AckSkillFail) GetResult() UseSkillResult {
	if m != nil && m.Result != nil {
		return *m.Result
	}
	return UseSkillResult_CAST_SKILL_SUCCESS
}

// 技能成功开始释放
type AckSkillStart struct {
	Caster               *int32   `protobuf:"varint,1,req,name=caster" json:"caster,omitempty"`
	SkillId              *int32   `protobuf:"varint,2,req,name=skill_id" json:"skill_id,omitempty"`
	Dir                  *int32   `protobuf:"varint,3,opt,name=dir" json:"dir,omitempty"`
	XXX_NoUnkeyedLiteral struct{} `json:"-"`
	XXX_unrecognized     []byte   `json:"-"`
	XXX_sizecache        int32    `json:"-"`
}

func (m *AckSkillStart) Reset()         { *m = AckSkillStart{} }
func (m *AckSkillStart) String() string { return proto.CompactTextString(m) }
func (*AckSkillStart) ProtoMessage()    {}
func (*AckSkillStart) Descriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{14}
}
func (m *AckSkillStart) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_AckSkillStart.Unmarshal(m, b)
}
func (m *AckSkillStart) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_AckSkillStart.Marshal(b, m, deterministic)
}
func (dst *AckSkillStart) XXX_Merge(src proto.Message) {
	xxx_messageInfo_AckSkillStart.Merge(dst, src)
}
func (m *AckSkillStart) XXX_Size() int {
	return xxx_messageInfo_AckSkillStart.Size(m)
}
func (m *AckSkillStart) XXX_DiscardUnknown() {
	xxx_messageInfo_AckSkillStart.DiscardUnknown(m)
}

var xxx_messageInfo_AckSkillStart proto.InternalMessageInfo

func (m *AckSkillStart) GetCaster() int32 {
	if m != nil && m.Caster != nil {
		return *m.Caster
	}
	return 0
}

func (m *AckSkillStart) GetSkillId() int32 {
	if m != nil && m.SkillId != nil {
		return *m.SkillId
	}
	return 0
}

func (m *AckSkillStart) GetDir() int32 {
	if m != nil && m.Dir != nil {
		return *m.Dir
	}
	return 0
}

type DamageInfo struct {
	Target               *int32   `protobuf:"varint,1,req,name=target" json:"target,omitempty"`
	Damage               *int32   `protobuf:"varint,2,req,name=damage" json:"damage,omitempty"`
	Flags                *int32   `protobuf:"varint,3,req,name=flags" json:"flags,omitempty"`
	Leech                *int32   `protobuf:"varint,4,opt,name=leech" json:"leech,omitempty"`
	XXX_NoUnkeyedLiteral struct{} `json:"-"`
	XXX_unrecognized     []byte   `json:"-"`
	XXX_sizecache        int32    `json:"-"`
}

func (m *DamageInfo) Reset()         { *m = DamageInfo{} }
func (m *DamageInfo) String() string { return proto.CompactTextString(m) }
func (*DamageInfo) ProtoMessage()    {}
func (*DamageInfo) Descriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{15}
}
func (m *DamageInfo) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_DamageInfo.Unmarshal(m, b)
}
func (m *DamageInfo) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_DamageInfo.Marshal(b, m, deterministic)
}
func (dst *DamageInfo) XXX_Merge(src proto.Message) {
	xxx_messageInfo_DamageInfo.Merge(dst, src)
}
func (m *DamageInfo) XXX_Size() int {
	return xxx_messageInfo_DamageInfo.Size(m)
}
func (m *DamageInfo) XXX_DiscardUnknown() {
	xxx_messageInfo_DamageInfo.DiscardUnknown(m)
}

var xxx_messageInfo_DamageInfo proto.InternalMessageInfo

func (m *DamageInfo) GetTarget() int32 {
	if m != nil && m.Target != nil {
		return *m.Target
	}
	return 0
}

func (m *DamageInfo) GetDamage() int32 {
	if m != nil && m.Damage != nil {
		return *m.Damage
	}
	return 0
}

func (m *DamageInfo) GetFlags() int32 {
	if m != nil && m.Flags != nil {
		return *m.Flags
	}
	return 0
}

func (m *DamageInfo) GetLeech() int32 {
	if m != nil && m.Leech != nil {
		return *m.Leech
	}
	return 0
}

// 技能结果用于伤害显示
type AckSkillResult struct {
	SkillId              *int32        `protobuf:"varint,1,req,name=skill_id" json:"skill_id,omitempty"`
	Caster               *int32        `protobuf:"varint,2,req,name=caster" json:"caster,omitempty"`
	Damages              []*DamageInfo `protobuf:"bytes,3,rep,name=damages" json:"damages,omitempty"`
	XXX_NoUnkeyedLiteral struct{}      `json:"-"`
	XXX_unrecognized     []byte        `json:"-"`
	XXX_sizecache        int32         `json:"-"`
}

func (m *AckSkillResult) Reset()         { *m = AckSkillResult{} }
func (m *AckSkillResult) String() string { return proto.CompactTextString(m) }
func (*AckSkillResult) ProtoMessage()    {}
func (*AckSkillResult) Descriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{16}
}
func (m *AckSkillResult) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_AckSkillResult.Unmarshal(m, b)
}
func (m *AckSkillResult) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_AckSkillResult.Marshal(b, m, deterministic)
}
func (dst *AckSkillResult) XXX_Merge(src proto.Message) {
	xxx_messageInfo_AckSkillResult.Merge(dst, src)
}
func (m *AckSkillResult) XXX_Size() int {
	return xxx_messageInfo_AckSkillResult.Size(m)
}
func (m *AckSkillResult) XXX_DiscardUnknown() {
	xxx_messageInfo_AckSkillResult.DiscardUnknown(m)
}

var xxx_messageInfo_AckSkillResult proto.InternalMessageInfo

func (m *AckSkillResult) GetSkillId() int32 {
	if m != nil && m.SkillId != nil {
		return *m.SkillId
	}
	return 0
}

func (m *AckSkillResult) GetCaster() int32 {
	if m != nil && m.Caster != nil {
		return *m.Caster
	}
	return 0
}

func (m *AckSkillResult) GetDamages() []*DamageInfo {
	if m != nil {
		return m.Damages
	}
	return nil
}

// 技能打断
type AckSkillStop struct {
	SkillId              *int32   `protobuf:"varint,1,req,name=skill_id" json:"skill_id,omitempty"`
	Caster               *int32   `protobuf:"varint,2,req,name=caster" json:"caster,omitempty"`
	XXX_NoUnkeyedLiteral struct{} `json:"-"`
	XXX_unrecognized     []byte   `json:"-"`
	XXX_sizecache        int32    `json:"-"`
}

func (m *AckSkillStop) Reset()         { *m = AckSkillStop{} }
func (m *AckSkillStop) String() string { return proto.CompactTextString(m) }
func (*AckSkillStop) ProtoMessage()    {}
func (*AckSkillStop) Descriptor() ([]byte, []int) {
	return fileDescriptor_MsgDefine_7a076fb3ea1a06b1, []int{17}
}
func (m *AckSkillStop) XXX_Unmarshal(b []byte) error {
	return xxx_messageInfo_AckSkillStop.Unmarshal(m, b)
}
func (m *AckSkillStop) XXX_Marshal(b []byte, deterministic bool) ([]byte, error) {
	return xxx_messageInfo_AckSkillStop.Marshal(b, m, deterministic)
}
func (dst *AckSkillStop) XXX_Merge(src proto.Message) {
	xxx_messageInfo_AckSkillStop.Merge(dst, src)
}
func (m *AckSkillStop) XXX_Size() int {
	return xxx_messageInfo_AckSkillStop.Size(m)
}
func (m *AckSkillStop) XXX_DiscardUnknown() {
	xxx_messageInfo_AckSkillStop.DiscardUnknown(m)
}

var xxx_messageInfo_AckSkillStop proto.InternalMessageInfo

func (m *AckSkillStop) GetSkillId() int32 {
	if m != nil && m.SkillId != nil {
		return *m.SkillId
	}
	return 0
}

func (m *AckSkillStop) GetCaster() int32 {
	if m != nil && m.Caster != nil {
		return *m.Caster
	}
	return 0
}

func init() {
	proto.RegisterType((*Vector2)(nil), "msg.Vector2")
	proto.RegisterType((*Vector3)(nil), "msg.Vector3")
	proto.RegisterType((*ReqLogin)(nil), "msg.ReqLogin")
	proto.RegisterType((*AckLogin)(nil), "msg.AckLogin")
	proto.RegisterType((*PlayerInfo)(nil), "msg.PlayerInfo")
	proto.RegisterType((*PlayerEntryInfo)(nil), "msg.PlayerEntryInfo")
	proto.RegisterType((*AckPlayerEntryList)(nil), "msg.AckPlayerEntryList")
	proto.RegisterType((*AckPlayerLeaveList)(nil), "msg.AckPlayerLeaveList")
	proto.RegisterType((*AckPublicPropertyList)(nil), "msg.AckPublicPropertyList")
	proto.RegisterType((*AckSwapScene)(nil), "msg.AckSwapScene")
	proto.RegisterType((*ReqAckStop)(nil), "msg.ReqAckStop")
	proto.RegisterType((*ReqAckPlayerMove)(nil), "msg.ReqAckPlayerMove")
	proto.RegisterType((*ReqUseSkill)(nil), "msg.ReqUseSkill")
	proto.RegisterType((*AckSkillFail)(nil), "msg.AckSkillFail")
	proto.RegisterType((*AckSkillStart)(nil), "msg.AckSkillStart")
	proto.RegisterType((*DamageInfo)(nil), "msg.DamageInfo")
	proto.RegisterType((*AckSkillResult)(nil), "msg.AckSkillResult")
	proto.RegisterType((*AckSkillStop)(nil), "msg.AckSkillStop")
	proto.RegisterEnum("msg.UseSkillResult", UseSkillResult_name, UseSkillResult_value)
	proto.RegisterEnum("msg.DamageFlag", DamageFlag_name, DamageFlag_value)
}

func init() { proto.RegisterFile("MsgDefine.proto", fileDescriptor_MsgDefine_7a076fb3ea1a06b1) }

var fileDescriptor_MsgDefine_7a076fb3ea1a06b1 = []byte{
	// 815 bytes of a gzipped FileDescriptorProto
	0x1f, 0x8b, 0x08, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0xff, 0x84, 0x53, 0x5d, 0x73, 0xab, 0x36,
	0x10, 0x2d, 0x38, 0x38, 0xf6, 0xda, 0x71, 0x14, 0x25, 0xe9, 0xa5, 0xd3, 0xe9, 0x5c, 0x0f, 0xed,
	0x83, 0x6f, 0x1e, 0x32, 0x9d, 0xf4, 0xb9, 0xed, 0x70, 0x81, 0x38, 0xf4, 0xfa, 0x6b, 0x80, 0xf4,
	0xb6, 0x4f, 0x0c, 0xc1, 0x32, 0xa1, 0xc6, 0x16, 0x01, 0xd9, 0x4d, 0xfa, 0x07, 0xfa, 0x7f, 0xfa,
	0x0b, 0x3b, 0x12, 0xc6, 0x26, 0x8e, 0x3b, 0x7d, 0x5b, 0xed, 0xd9, 0x3d, 0x7b, 0xf6, 0x48, 0x82,
	0xd3, 0x61, 0x1e, 0x99, 0x64, 0x16, 0x2f, 0xc9, 0x75, 0x9a, 0x51, 0x46, 0x71, 0x6d, 0x91, 0x47,
	0xda, 0x7b, 0x38, 0xfe, 0x95, 0x84, 0x8c, 0x66, 0x37, 0xb8, 0x09, 0xd2, 0xb3, 0x2a, 0x75, 0xe5,
	0x9e, 0xcc, 0xc3, 0x17, 0x55, 0xe6, 0xa1, 0x76, 0x55, 0x16, 0xfc, 0x70, 0xb8, 0x80, 0x87, 0x7f,
	0xa9, 0x35, 0x51, 0xfb, 0x0d, 0x34, 0x1c, 0xf2, 0x34, 0xa0, 0x51, 0xbc, 0xc4, 0x67, 0xd0, 0x0c,
	0xc2, 0x90, 0xae, 0x96, 0xcc, 0x9e, 0x8a, 0xa6, 0xa6, 0x76, 0x05, 0x0d, 0x3d, 0x9c, 0x6f, 0x61,
	0x92, 0x65, 0x34, 0x33, 0xe8, 0x94, 0x08, 0x58, 0xc1, 0x2d, 0xa8, 0xad, 0xe2, 0xa9, 0x2a, 0x77,
	0xa5, 0x9e, 0xa2, 0xfd, 0x2d, 0x01, 0x4c, 0x92, 0xe0, 0x85, 0x64, 0xf6, 0x72, 0x46, 0x39, 0x16,
	0xb0, 0xb9, 0x5a, 0xe7, 0x18, 0x3f, 0x4c, 0xc9, 0x4c, 0x3d, 0x16, 0x07, 0x00, 0xf9, 0x31, 0x55,
	0x1b, 0x22, 0x3e, 0x01, 0xe5, 0x31, 0x1d, 0x06, 0xcf, 0x6a, 0x53, 0x1c, 0xcf, 0xa0, 0xb9, 0xa0,
	0x6b, 0xe2, 0xa6, 0x84, 0x4c, 0x55, 0x28, 0x2b, 0x12, 0xb2, 0x26, 0x89, 0xda, 0x12, 0xc7, 0x0b,
	0x68, 0x2f, 0xe8, 0x94, 0x24, 0x46, 0x42, 0xd9, 0x23, 0xc9, 0xd5, 0xb6, 0xc8, 0xb6, 0xe1, 0x68,
	0x19, 0x2c, 0x88, 0x7a, 0xd2, 0x95, 0x7a, 0x4d, 0xed, 0x19, 0x4e, 0x0b, 0x21, 0xd6, 0x92, 0x65,
	0x2f, 0x42, 0xcd, 0x39, 0xb4, 0xe8, 0xc3, 0x1f, 0x24, 0x64, 0x7e, 0xc4, 0x15, 0x17, 0xf2, 0x85,
	0x3b, 0xf2, 0xce, 0x9d, 0xda, 0xce, 0x9d, 0xa3, 0x32, 0xcc, 0x54, 0x45, 0x4c, 0xf8, 0x0e, 0x5a,
	0xa9, 0xe0, 0xf4, 0xe3, 0xe5, 0x8c, 0xaa, 0xf5, 0xae, 0xdc, 0x6b, 0xdd, 0x9c, 0x5e, 0x2f, 0xf2,
	0xe8, 0x7a, 0xb7, 0xb4, 0xf6, 0x33, 0x60, 0x3d, 0x9c, 0x57, 0x86, 0x0f, 0xe2, 0x9c, 0xe1, 0x0f,
	0xdb, 0xe1, 0x49, 0x9c, 0x33, 0x55, 0xea, 0xd6, 0x7a, 0xad, 0x9b, 0x8b, 0x4a, 0xef, 0x56, 0xa7,
	0xf6, 0xa1, 0x42, 0x30, 0x20, 0xc1, 0x9a, 0x08, 0x82, 0xf3, 0xb7, 0x04, 0x8a, 0x66, 0xc3, 0x25,
	0x2f, 0x5d, 0x3d, 0x24, 0x71, 0x38, 0xc9, 0x68, 0x4a, 0x32, 0x56, 0x8c, 0x03, 0x90, 0xb7, 0x2b,
	0xee, 0xc9, 0x96, 0x0f, 0xcb, 0x1e, 0x40, 0x5b, 0x0f, 0xe7, 0xee, 0x9f, 0x41, 0xea, 0x86, 0x64,
	0x49, 0x30, 0x82, 0x46, 0xce, 0x03, 0x7f, 0xdf, 0x2a, 0x69, 0x67, 0x95, 0xb4, 0xb3, 0x4a, 0x7a,
	0x65, 0x95, 0xf6, 0x23, 0x80, 0x43, 0x9e, 0x38, 0x21, 0xa3, 0x29, 0xbf, 0x3f, 0x7e, 0xa5, 0xd9,
	0x86, 0xa8, 0x0b, 0x90, 0xd3, 0x55, 0x16, 0x12, 0x3f, 0xa5, 0xf9, 0x46, 0x4f, 0x5b, 0xe8, 0xd9,
	0xbc, 0x59, 0xcd, 0x00, 0x54, 0xb4, 0x17, 0x02, 0x87, 0x74, 0x4d, 0x0e, 0x90, 0xb0, 0x20, 0x8b,
	0x08, 0xdb, 0x90, 0xd4, 0xde, 0x90, 0xbc, 0x87, 0x96, 0x43, 0x9e, 0xee, 0x73, 0xe2, 0xce, 0xe3,
	0x24, 0x11, 0x0b, 0xf1, 0x60, 0xbb, 0x90, 0x66, 0x15, 0x2b, 0xf3, 0xe4, 0x6d, 0x10, 0x1f, 0xa8,
	0xc0, 0xdf, 0x42, 0x3d, 0x23, 0xf9, 0x2a, 0x61, 0x42, 0x65, 0xe7, 0xe6, 0x5c, 0x0c, 0x28, 0x29,
	0x1d, 0x01, 0x69, 0x3f, 0xc1, 0x49, 0x49, 0xe3, 0xb2, 0x20, 0x63, 0xb8, 0x03, 0xf5, 0x30, 0xc8,
	0xd9, 0x56, 0x6a, 0x95, 0x57, 0x2e, 0x3f, 0xcd, 0x34, 0xce, 0x84, 0x83, 0x8a, 0xf6, 0x0b, 0x80,
	0x19, 0x2c, 0x82, 0x88, 0x88, 0x57, 0xda, 0x81, 0x7a, 0xb1, 0xd7, 0xa6, 0xb9, 0x03, 0xf5, 0xa9,
	0x40, 0x37, 0xad, 0x27, 0xa0, 0xcc, 0x92, 0x20, 0xca, 0xc5, 0x4b, 0xdd, 0x7c, 0x0d, 0x12, 0x3e,
	0x8a, 0x2b, 0x50, 0x34, 0x0f, 0x3a, 0xa5, 0x96, 0x42, 0xdd, 0x81, 0xa5, 0x76, 0xf2, 0xe4, 0x8d,
	0x93, 0xc7, 0xc5, 0x04, 0xce, 0x59, 0xdb, 0xbe, 0x8d, 0x9d, 0x26, 0xed, 0xfb, 0x9d, 0x51, 0xe2,
	0x3e, 0xff, 0x97, 0xf3, 0xea, 0x1f, 0x19, 0x3a, 0xaf, 0x6d, 0xc2, 0x5f, 0x02, 0x36, 0x74, 0xd7,
	0xf3, 0xdd, 0x4f, 0xf6, 0x60, 0xe0, 0xbb, 0xf7, 0x86, 0x61, 0xb9, 0x2e, 0xfa, 0x02, 0xab, 0x70,
	0x51, 0xc9, 0x8f, 0xc6, 0x9e, 0x6f, 0xfd, 0x66, 0xbb, 0x1e, 0x92, 0xf0, 0x05, 0xa0, 0x0a, 0x62,
	0x8f, 0x7c, 0xc3, 0x44, 0x32, 0xfe, 0x1a, 0xde, 0xbd, 0xae, 0xb7, 0x46, 0xe3, 0xfb, 0xfe, 0x9d,
	0x7f, 0x37, 0x41, 0xb5, 0xff, 0x04, 0x87, 0x13, 0x74, 0xb4, 0x07, 0xde, 0xea, 0x8e, 0x6f, 0xda,
	0xae, 0xa7, 0x8f, 0x0c, 0x0b, 0x29, 0x6f, 0x64, 0xf8, 0x9e, 0xee, 0xf4, 0x2d, 0x0f, 0xd5, 0xf1,
	0x3b, 0x38, 0xaf, 0x20, 0xa6, 0xed, 0xea, 0x1f, 0x07, 0x96, 0x89, 0x8e, 0xf1, 0x57, 0x70, 0x59,
	0x01, 0x3e, 0xea, 0x66, 0xd9, 0xd3, 0xd8, 0x5b, 0x96, 0x87, 0xf6, 0xa8, 0x8f, 0x9a, 0x7b, 0x12,
	0xee, 0x47, 0x9f, 0x46, 0xe3, 0xcf, 0xbe, 0xe5, 0x38, 0x63, 0x07, 0xc1, 0xd5, 0xbc, 0x7c, 0x08,
	0xb7, 0x49, 0x10, 0xe1, 0x4b, 0x38, 0x33, 0xf5, 0xa1, 0xde, 0xb7, 0xfc, 0xdb, 0x81, 0xde, 0xf7,
	0xcd, 0xb1, 0xd9, 0xb7, 0x90, 0xb4, 0x9f, 0x9e, 0xe8, 0x8e, 0xf3, 0x3b, 0x92, 0xb9, 0x57, 0xd5,
	0xb4, 0xe1, 0xd8, 0x1e, 0x3a, 0xe2, 0x0a, 0xab, 0xd9, 0xcf, 0xb6, 0x77, 0xc7, 0x17, 0x36, 0x51,
	0xe3, 0xdf, 0x00, 0x00, 0x00, 0xff, 0xff, 0x5c, 0x8c, 0x7c, 0x60, 0x59, 0x06, 0x00, 0x00,
}