using UnityEngine;
using System;
using LuaInterface;

public class LuaManager : MonoBehaviour
{
	LuaState m_lua;

	LuaLooper m_luaLooper;

	private static LuaManager s_Instance;
	public static LuaManager Instance {
		get{ 
			return s_Instance;
		}
	}

	void Awake()
	{
		s_Instance = this;
		#if UNITY_EDITOR
		Debug.Log("ToLua# Version: " + LuaDLL.version);
		#endif

		m_lua = new LuaState();

		OpenLibs();

		m_lua.LuaSetTop(0);

		LuaBinder.Bind(m_lua);
		//LuaCoroutine.Register(m_lua, this);

		m_lua.Start();

		m_luaLooper = gameObject.AddComponent<LuaLooper>();
		m_luaLooper.luaState = m_lua;
	}

	void OnDestroy()
	{
		if (m_lua != null)
		{
			m_lua.Dispose();
			m_lua = null;

			if (m_luaLooper)
			{
				m_luaLooper.Destroy();
				m_luaLooper = null;
			}
		}
	}

	void OpenLibs()
	{
		m_lua.OpenLibs(LuaDLL.luaopen_pb);
		m_lua.OpenLibs(LuaDLL.luaopen_struct);
		m_lua.OpenLibs(LuaDLL.luaopen_lpeg);
		//m_lua.OpenLibs(LuaDLL.luaopen_bit);
		//m_lua.OpenLibs(LuaDLL.luaopen_socket_core);
	}

	void OpenCJson()
	{
		m_lua.LuaGetField(LuaIndexes.LUA_REGISTRYINDEX, "_LOADED");
		m_lua.OpenLibs(LuaDLL.luaopen_cjson);
		m_lua.LuaSetField(-2, "cjson");

		m_lua.OpenLibs(LuaDLL.luaopen_cjson_safe);
		m_lua.LuaSetField(-2, "cjson.safe");
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LuaOpen_Socket_Core(IntPtr L)
	{
		return LuaDLL.luaopen_socket_core(L);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LuaOpen_Mime_Core(IntPtr L)
	{
		return LuaDLL.luaopen_mime_core(L);
	}

	protected void OpenLuaSocket()
	{
		m_lua.BeginPreLoad();
		m_lua.RegFunction("socket.core", LuaOpen_Socket_Core);
		m_lua.RegFunction("mime.core", LuaOpen_Mime_Core);
		m_lua.EndPreLoad();
	}

	public object[] DoFile(string name)
	{
		return m_lua.DoFile(name);
	}

	public object[] DoString(string chunk, string chunkName)
	{
		return m_lua.DoString(chunk, chunkName);
	}

	public void Call(string name)
	{
		LuaFunction func = m_lua.GetFunction(name);
		if (func != null)
		{
			func.Call();
			func.Dispose();
		}
	}

	/// <summary>
	/// 慎用
	/// </summary>
//	public object[] CallArgs(string name, params object[] args)
//	{
//		LuaFunction func = m_lua.GetFunction(name);
//		if (func != null)
//		{
//			object[] rets = func.Call(args);
//			func.Dispose();
//			return rets;
//		}
//		return null;
//	}

	public void GC(bool systemGC)
	{
		//m_lua.LuaGC(LuaGCOptions.LUA_GCCOLLECT);
		m_lua.RefreshDelegateMap();

		if (systemGC)
			System.GC.Collect();
	}

	[NoToLua]
	public static bool IsValid()
	{
		return s_Instance && s_Instance.m_luaLooper;
	}

	[NoToLua]
	public LuaTable GetTable(string name)
	{
		return m_lua.GetTable(name);
	}

	[NoToLua]
	public LuaFunction GetFunction(string name)
	{
		return m_lua.GetFunction(name);
	}
}