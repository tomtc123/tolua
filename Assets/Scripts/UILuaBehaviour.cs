using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using LuaInterface;

public class UILuaBehaviour : MonoBehaviour {

	[SerializeField]
	protected string m_scriptPath = "UI";
	public string scriptFullPath
	{
		get
		{
			return this.m_scriptPath + "/" + this.name;
		}
	}

	protected LuaTable m_luaTable;

	void Awake()
	{
		string callStr = string.Format("return require(\"{0}\").New()", this.scriptFullPath);
		object[] ret = LuaManager.Instance.DoString(callStr, this.scriptFullPath);
		m_luaTable = ret[0] as LuaTable;

		m_luaTable["behaviour"] = this;
		m_luaTable["gameObject"] = this.gameObject;
		m_luaTable["transform"] = this.transform;

		CallLuaFunction("Awake");
	}

	void OnDestroy()
	{
		if (LuaManager.IsValid())
			CallLuaFunction("OnDestroy");

		m_luaTable.Dispose();
		m_luaTable = null;
	}

	void Start()
	{
		CallLuaFunction("Start");
	}

	void CallLuaFunction(string name)
	{
		if (m_luaTable != null)
		{
			LuaFunction func = m_luaTable[name] as LuaFunction;
			if (func != null)
			{
				func.BeginPCall();
				func.Push(m_luaTable);
				func.PCall();
				func.EndPCall();
				func.Dispose();
			}
			else
			{
				Debug.LogError(this + " CallLuaFunction falied: " + name);
			}
		}
	}

	public void AddClick(GameObject go, LuaFunction func, LuaTable luatable) {
		if (go == null || func == null) {
			Debug.LogError ("gameObject and function can't null");
			return;
		}
		Button button = go.GetComponent<Button>();
		if (button == null)
			button = go.AddComponent<Button>();
		button.onClick.AddListener(delegate()
		{
			func.BeginPCall();
			if (luatable != null)
				func.Push(luatable);
			func.Push(go);
			func.PCall();
			func.EndPCall();
		});
	}
}
