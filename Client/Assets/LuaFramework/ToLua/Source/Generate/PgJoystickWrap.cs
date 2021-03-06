﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class PgJoystickWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(PgJoystick), typeof(SingetonMono<PgJoystick>));
		L.RegFunction("SetLockView", SetLockView);
		L.RegFunction("SetSceneLockView", SetSceneLockView);
		L.RegFunction("BeginAutoCamZoomIn", BeginAutoCamZoomIn);
		L.RegFunction("StopAutoCamZoomIn", StopAutoCamZoomIn);
		L.RegFunction("SetCamLookAt", SetCamLookAt);
		L.RegFunction("SetCam", SetCam);
		L.RegFunction("SetSceneRotPoint", SetSceneRotPoint);
		L.RegFunction("ResetSceneRotPoint", ResetSceneRotPoint);
		L.RegFunction("UpdateFollowOffset", UpdateFollowOffset);
		L.RegFunction("Init", Init);
		L.RegFunction("Update", Update);
		L.RegFunction("GetCamLookPos", GetCamLookPos);
		L.RegFunction("LookAtPos", LookAtPos);
		L.RegFunction("AutoCamZoomIn", AutoCamZoomIn);
		L.RegFunction("CameraZoomTarget", CameraZoomTarget);
		L.RegFunction("CameraZoomIn", CameraZoomIn);
		L.RegFunction("GetCamRotateOffset", GetCamRotateOffset);
		L.RegFunction("DelayAutoCamRotate", DelayAutoCamRotate);
		L.RegFunction("AutoCamRotate", AutoCamRotate);
		L.RegFunction("CameraRotate", CameraRotate);
		L.RegFunction("AutoMove", AutoMove);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("DebugMode", get_DebugMode, set_DebugMode);
		L.RegVar("offsetLook", get_offsetLook, set_offsetLook);
		L.RegVar("onMove", get_onMove, set_onMove);
		L.RegVar("onMoveEnd", get_onMoveEnd, set_onMoveEnd);
		L.RegVar("isLockView", get_isLockView, set_isLockView);
		L.RegVar("enableCamera", get_enableCamera, set_enableCamera);
		L.RegVar("enableMoveDir", get_enableMoveDir, set_enableMoveDir);
		L.RegVar("_enable", get__enable, set__enable);
		L.RegVar("sceneRotPoint", get_sceneRotPoint, set_sceneRotPoint);
		L.RegVar("RangeRotateY", get_RangeRotateY, set_RangeRotateY);
		L.RegVar("directTransform", get_directTransform, set_directTransform);
		L.RegVar("cameraTransform", get_cameraTransform, set_cameraTransform);
		L.RegVar("cameraLookAt", get_cameraLookAt, set_cameraLookAt);
		L.RegVar("followOffset", get_followOffset, set_followOffset);
		L.RegVar("minCamDistance", get_minCamDistance, set_minCamDistance);
		L.RegVar("maxCamDistance", get_maxCamDistance, set_maxCamDistance);
		L.RegVar("cameraTargetDist", get_cameraTargetDist, set_cameraTargetDist);
		L.RegVar("autoRotateSpeed", get_autoRotateSpeed, set_autoRotateSpeed);
		L.RegVar("defaultFollow", get_defaultFollow, set_defaultFollow);
		L.RegVar("defaultRotation", get_defaultRotation, set_defaultRotation);
		L.RegVar("MaxCamDistance", get_MaxCamDistance, null);
		L.RegVar("Enable", get_Enable, set_Enable);
		L.RegVar("EnableAutoCamRotate", get_EnableAutoCamRotate, set_EnableAutoCamRotate);
		L.RegFunction("OnMoveEndHandler2", PgJoystick_OnMoveEndHandler2);
		L.RegFunction("OnMoveHandler2", PgJoystick_OnMoveHandler2);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetLockView(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.SetLockView(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetSceneLockView(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			obj.SetSceneLockView(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BeginAutoCamZoomIn(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.BeginAutoCamZoomIn(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopAutoCamZoomIn(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			obj.StopAutoCamZoomIn();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetCamLookAt(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			UnityEngine.Transform arg0 = (UnityEngine.Transform)ToLua.CheckObject<UnityEngine.Transform>(L, 2);
			obj.SetCamLookAt(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetCam(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			UnityEngine.Transform arg0 = (UnityEngine.Transform)ToLua.CheckObject<UnityEngine.Transform>(L, 2);
			obj.SetCam(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetSceneRotPoint(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.SetSceneRotPoint(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResetSceneRotPoint(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			obj.ResetSceneRotPoint();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UpdateFollowOffset(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 2)
			{
				PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
				System.Collections.Generic.List<UnityEngine.Vector3> arg0 = (System.Collections.Generic.List<UnityEngine.Vector3>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Vector3>));
				obj.UpdateFollowOffset(arg0);
				return 0;
			}
			else if (count == 3)
			{
				PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
				System.Collections.Generic.List<UnityEngine.Vector3> arg0 = (System.Collections.Generic.List<UnityEngine.Vector3>)ToLua.CheckObject(L, 2, typeof(System.Collections.Generic.List<UnityEngine.Vector3>));
				bool arg1 = LuaDLL.luaL_checkboolean(L, 3);
				obj.UpdateFollowOffset(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: PgJoystick.UpdateFollowOffset");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Init(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			FairyGUI.GComponent arg0 = (FairyGUI.GComponent)ToLua.CheckObject<FairyGUI.GComponent>(L, 2);
			obj.Init(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Update(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			obj.Update();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCamLookPos(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			UnityEngine.Vector3 o = obj.GetCamLookPos();
			ToLua.Push(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LookAtPos(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.LookAtPos(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AutoCamZoomIn(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			obj.AutoCamZoomIn();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CameraZoomTarget(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.CameraZoomTarget(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CameraZoomIn(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.CameraZoomIn(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetCamRotateOffset(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			float o = obj.GetCamRotateOffset();
			LuaDLL.lua_pushnumber(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DelayAutoCamRotate(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			obj.DelayAutoCamRotate();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AutoCamRotate(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			obj.AutoCamRotate();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CameraRotate(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			float arg1 = (float)LuaDLL.luaL_checknumber(L, 3);
			obj.CameraRotate(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AutoMove(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			PgJoystick obj = (PgJoystick)ToLua.CheckObject<PgJoystick>(L, 1);
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.AutoMove(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_DebugMode(IntPtr L)
	{
		try
		{
			LuaDLL.lua_pushboolean(L, PgJoystick.DebugMode);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_offsetLook(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			float ret = obj.offsetLook;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index offsetLook on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onMove(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			PgJoystick.OnMoveHandler2 ret = obj.onMove;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index onMove on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_onMoveEnd(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			PgJoystick.OnMoveEndHandler2 ret = obj.onMoveEnd;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index onMoveEnd on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_isLockView(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			bool ret = obj.isLockView;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index isLockView on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_enableCamera(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			bool ret = obj.enableCamera;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index enableCamera on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_enableMoveDir(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			bool ret = obj.enableMoveDir;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index enableMoveDir on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get__enable(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			bool ret = obj._enable;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index _enable on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_sceneRotPoint(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			int ret = obj.sceneRotPoint;
			LuaDLL.lua_pushinteger(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index sceneRotPoint on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_RangeRotateY(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			UnityEngine.Vector2 ret = obj.RangeRotateY;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index RangeRotateY on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_directTransform(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			UnityEngine.Transform ret = obj.directTransform;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index directTransform on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cameraTransform(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			UnityEngine.Transform ret = obj.cameraTransform;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index cameraTransform on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cameraLookAt(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			UnityEngine.Transform ret = obj.cameraLookAt;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index cameraLookAt on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_followOffset(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			UnityEngine.Vector3 ret = obj.followOffset;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index followOffset on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_minCamDistance(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			float ret = obj.minCamDistance;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index minCamDistance on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_maxCamDistance(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			float ret = obj.maxCamDistance;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index maxCamDistance on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_cameraTargetDist(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			float ret = obj.cameraTargetDist;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index cameraTargetDist on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_autoRotateSpeed(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			float ret = obj.autoRotateSpeed;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index autoRotateSpeed on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_defaultFollow(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			UnityEngine.Vector3 ret = obj.defaultFollow;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index defaultFollow on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_defaultRotation(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			UnityEngine.Quaternion ret = obj.defaultRotation;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index defaultRotation on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MaxCamDistance(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			float ret = obj.MaxCamDistance;
			LuaDLL.lua_pushnumber(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index MaxCamDistance on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Enable(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			bool ret = obj.Enable;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index Enable on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_EnableAutoCamRotate(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			bool ret = obj.EnableAutoCamRotate;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index EnableAutoCamRotate on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_DebugMode(IntPtr L)
	{
		try
		{
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			PgJoystick.DebugMode = arg0;
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_offsetLook(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.offsetLook = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index offsetLook on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onMove(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			PgJoystick.OnMoveHandler2 arg0 = (PgJoystick.OnMoveHandler2)ToLua.CheckDelegate<PgJoystick.OnMoveHandler2>(L, 2);
			obj.onMove = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index onMove on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_onMoveEnd(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			PgJoystick.OnMoveEndHandler2 arg0 = (PgJoystick.OnMoveEndHandler2)ToLua.CheckDelegate<PgJoystick.OnMoveEndHandler2>(L, 2);
			obj.onMoveEnd = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index onMoveEnd on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_isLockView(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.isLockView = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index isLockView on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_enableCamera(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.enableCamera = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index enableCamera on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_enableMoveDir(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.enableMoveDir = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index enableMoveDir on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set__enable(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj._enable = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index _enable on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_sceneRotPoint(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.sceneRotPoint = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index sceneRotPoint on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_RangeRotateY(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			UnityEngine.Vector2 arg0 = ToLua.ToVector2(L, 2);
			obj.RangeRotateY = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index RangeRotateY on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_directTransform(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			UnityEngine.Transform arg0 = (UnityEngine.Transform)ToLua.CheckObject<UnityEngine.Transform>(L, 2);
			obj.directTransform = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index directTransform on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cameraTransform(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			UnityEngine.Transform arg0 = (UnityEngine.Transform)ToLua.CheckObject<UnityEngine.Transform>(L, 2);
			obj.cameraTransform = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index cameraTransform on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cameraLookAt(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			UnityEngine.Transform arg0 = (UnityEngine.Transform)ToLua.CheckObject<UnityEngine.Transform>(L, 2);
			obj.cameraLookAt = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index cameraLookAt on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_followOffset(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.followOffset = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index followOffset on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_minCamDistance(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.minCamDistance = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index minCamDistance on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_maxCamDistance(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.maxCamDistance = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index maxCamDistance on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_cameraTargetDist(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.cameraTargetDist = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index cameraTargetDist on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_autoRotateSpeed(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			float arg0 = (float)LuaDLL.luaL_checknumber(L, 2);
			obj.autoRotateSpeed = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index autoRotateSpeed on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_defaultFollow(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			UnityEngine.Vector3 arg0 = ToLua.ToVector3(L, 2);
			obj.defaultFollow = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index defaultFollow on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_defaultRotation(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			UnityEngine.Quaternion arg0 = ToLua.ToQuaternion(L, 2);
			obj.defaultRotation = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index defaultRotation on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Enable(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.Enable = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index Enable on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_EnableAutoCamRotate(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			PgJoystick obj = (PgJoystick)o;
			bool arg0 = LuaDLL.luaL_checkboolean(L, 2);
			obj.EnableAutoCamRotate = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index EnableAutoCamRotate on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PgJoystick_OnMoveEndHandler2(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);

			if (count == 1)
			{
				Delegate arg1 = DelegateTraits<PgJoystick.OnMoveEndHandler2>.Create(func);
				ToLua.Push(L, arg1);
			}
			else
			{
				LuaTable self = ToLua.CheckLuaTable(L, 2);
				Delegate arg1 = DelegateTraits<PgJoystick.OnMoveEndHandler2>.Create(func, self);
				ToLua.Push(L, arg1);
			}
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int PgJoystick_OnMoveHandler2(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);
			LuaFunction func = ToLua.CheckLuaFunction(L, 1);

			if (count == 1)
			{
				Delegate arg1 = DelegateTraits<PgJoystick.OnMoveHandler2>.Create(func);
				ToLua.Push(L, arg1);
			}
			else
			{
				LuaTable self = ToLua.CheckLuaTable(L, 2);
				Delegate arg1 = DelegateTraits<PgJoystick.OnMoveHandler2>.Create(func, self);
				ToLua.Push(L, arg1);
			}
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}
}

